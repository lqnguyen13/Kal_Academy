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

        public static async Task<string> WriteTextFileSongs(string filename, string contents)
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

            IReadOnlyList< StorageFile>fileList = await assetFolder.GetFilesAsync();

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            foreach (var file in fileList)
            {
                StorageFile assetFile = await assetFolder.GetFileAsync(file.Name);
                StorageFile localFile = await localFolder.CreateFileAsync(file.Name, CreationCollisionOption.OpenIfExists);

                if (localFile.IsAvailable)
                {
                    // do nothing
                }
                else
                {
                    await assetFile.CopyAsync(localFolder);
                }
            }
        }

        public static async Task CopyFromAssetToLocal(string filename)
        {
            // Get the path to the app's Assets folder.
            string installedLocationPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string assetPath = installedLocationPath + @"\Assets";

            StorageFolder assetFolder = await StorageFolder.GetFolderFromPathAsync(assetPath);
            StorageFile assetFile = await assetFolder.GetFileAsync(filename);
            
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile localFile = await localFolder.GetFileAsync(filename);

            if (localFile.IsAvailable)
            {
                await localFile.DeleteAsync();
            }

            await assetFile.CopyAsync(localFolder);
        }
    }
}
