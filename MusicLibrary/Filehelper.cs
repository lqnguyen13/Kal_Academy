using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MusicLibrary
{
    public static class FileHelper
    {
        public static async Task WriteTextFile(string filename, List<string> lines)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder; ;
            StorageFile file =  await folder.CreateFileAsync(filename,CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteLinesAsync(file, lines);
        }

        public static async Task<IList<string>> ReadTextFile(string filename)
        {
            // Get the folder object that corresponds to this absolute path in the file system.
            var folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(filename);
            if ( !File.Exists(file.Path))
            {
                return null;
            }
            var contents = await FileIO.ReadLinesAsync(file);
            return contents;
        }

        public static async Task<string> WriteTextFile(string filename, string contents)
        {

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(filename,
               CreationCollisionOption.OpenIfExists); // ReplaceExisting change it to append instead of deleting the content

            using (IRandomAccessStream textStream = await
               textFile.OpenAsync(FileAccessMode.ReadWrite))
            {

                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(contents);
                    await textWriter.StoreAsync();
                }
            }
            return textFile.Path;
        }

        public static async Task<string> ReadTextFileSongs(string filename)
        {
            string contents;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.GetFileAsync(filename);

            using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
            {

                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    await textReader.LoadAsync(textLength);
                    contents = textReader.ReadString(textLength);
                }

            }
            return contents;
        }
        /// <summary>
        /// Copy all the assets to the local folder. Later, could just copy the assets listed in the song file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static async Task CopyAllFromAssetToLocal()
        {
            // Get the path to the app's Assets folder.
            string installedLocationPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string assetPath = installedLocationPath + @"\Assets";

            StorageFolder assetFolder = await StorageFolder.GetFolderFromPathAsync(assetPath);

            IReadOnlyList<StorageFile>assetFilesList = await assetFolder.GetFilesAsync();

            StorageFolder localStoragFolder = ApplicationData.Current.LocalFolder;
            
            foreach (var fileFromAssetFolder in assetFilesList)
            {
                var fileName = fileFromAssetFolder.Name;
                // if the file already exists, don't copy it again
                if (await localStoragFolder.TryGetItemAsync(fileName) == null)
                {
                    await fileFromAssetFolder.CopyAsync(localStoragFolder);
                }
            }
        }

        public static async Task CopyUploadFileToLocal(StorageFile file)
        {
            // Get the path to the local file storage location
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // If file we want to add already exists in local, delete file
            try
            {
                StorageFile localFile = await localFolder.GetFileAsync(file.Name);

                if (localFile.IsAvailable)
                {
                    await localFile.DeleteAsync();
                }
            }
            catch
            {
                // if the file doesn't exist, do nothing
            }

            // Copy new file to local
            await file.CopyAsync(localFolder);
        }

        public static async Task RemoveFileFromLocal(string fileName)
        {
            // Get the path to the local file storage location
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            // If file we want to delete already exists in local, delete file
            try
            {
                StorageFile localFile = await localFolder.GetFileAsync(fileName);

                if (localFile.IsAvailable)
                {
                    await localFile.DeleteAsync();
                }
            }
            catch
            {
                // if the file doesn't exist, do nothing
            }
        }
    }
}
