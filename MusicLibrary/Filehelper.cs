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
    class FileHelper
    {
        public static async Task<string> WriteOneLine(string filename, string line)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(filename);

            var datastream = await sampleFile.OpenAsync(FileAccessMode.ReadWrite);

            using (var outputStream = datastream.GetOutputStreamAt(datastream.Size))
            {
                using (DataWriter datawriter = new DataWriter(outputStream))
                {
                    datawriter.WriteString(line);
                    await datawriter.StoreAsync();
                    await outputStream.FlushAsync();
                }
            }
            datastream.Dispose();
            return sampleFile.Path;
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
    }
}
