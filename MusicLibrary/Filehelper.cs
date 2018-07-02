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
        public static async Task<string> WriteTextFile(string filename, List<string> lines)
        {
            // Get the path to the app's Assets folder.
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets";

            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);
            StorageFile file =  await folder.CreateFileAsync(filename,CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteLinesAsync(file, lines);

            return file.Path;
        }

        public static async Task<IList<string>> ReadTextFile(string filename)
        {           
            // Get the path to the app's Assets folder.
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets";

            // Get the folder object that corresponds to this absolute path in the file system.
            StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(path);

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
    }
}
