using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicLibrary.Models
{
    [DataContract]
    public class Song : BindableBase
    {
        const string TEXT_FILE_NAME = "SongsTextfile.txt";
        // private static int _globalCount = 3;
        public int SongID { get; set; }
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }
        public string SongImage { get; set; }
        public string AudioFileName { get; set; }
        public BitmapImage SongSetImage => new BitmapImage(IsProfileImage ? new Uri(ImageFileName) : new Uri(new Uri("ms-appx://"), ImageFileName)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };

        public bool IsProfileImage { get; set; }

        [DataMember]
        public string ImageFileName
        {
            get
            {
                return _imageFileName;
            }
            set
            {
                SetProperty(ref _imageFileName, value);
                OnPropertyChanged(nameof(SongSetImage));
            }
        }

        private string _imageFileName;

        public Song()
        {

        }

        //public Song(Song song)
        //{
        //    this.SongTitle = song.SongTitle;
        //    this.SongArtist = song.SongArtist;
        //    this.ImageFileName = song.ImageFileName;
        //    IsProfileImage = false;
        //    this.SongID = ++_globalCount;
        //}

        public static async Task<List<Song>> GetSongsAsync()
        {
            var songs = new List<Song>();
            var songList = await FileHelper.ReadTextFile(TEXT_FILE_NAME);
            foreach (var aSong in songList)
            {
                var songParts = aSong.Split('|');
                var newSong = new Song
                {
                    SongID = int.Parse(songParts[0]),
                    SongTitle = songParts[1],
                    SongArtist = songParts[2],
                    SongImage = songParts[3],
                    AudioFileName = songParts[4]
                };
                songs.Add(newSong);
            }
            return songs;
        }

        public static List<Song> FilterSongs(string searchTerm, ICollection<Song> songList)
        {
            // songList = await Song.GetSongsAsync();
            var results = new List<Song>();
            foreach (var song in songList)
            {
                var st = song.SongTitle.ToLower();
                var sa = song.SongArtist.ToLower();

                if (st.Contains(searchTerm.ToLower()) || sa.Contains(searchTerm.ToLower()))
                {
                    results.Add(song);
                }
            }
            return results;
        }

        public static async Task WriteSongsToFile(List<Song> songs)
        {
            var songLines = new List<string>();
            string songLine = "";
            foreach (var song in songs)
            {
                songLine = $"{song.SongID.ToString()}|{song.SongTitle}|{song.SongArtist}|{song.SongImage}|{song.AudioFileName}";
                songLines.Add(songLine);
            }
            await FileHelper.WriteTextFile(TEXT_FILE_NAME, songLines);
        }

        public static async Task<List<Song>> GetSongsAsyncKeep()
        {
            var songs = new List<Song>();

            var folder = ApplicationData.Current.LocalFolder;
            var songFile = await folder.GetFileAsync(TEXT_FILE_NAME);
            var lines = await FileIO.ReadLinesAsync(songFile);

            foreach (var line in lines)
            {
                var songData = line.Split('|');
                var song = new Song
                {
                    SongTitle = songData[0],
                    SongArtist = songData[1]
                };
                songs.Add(song);
            }
            return songs;
        }

        public static async Task<List<Song>> AddSongAsync(Song songToAdd)
        {
            var songs = await GetSongsAsync();
            var lastID = songs[songs.Count - 1].SongID;
            songToAdd.SongID = lastID + 1;
            songs.Add(songToAdd);
            await WriteSongsToFile(songs);
            var newSongs = await GetSongsAsync();
            return newSongs;
        }

        public static async void WriteSong(Song song)
        {
            var songData = $"{song.SongTitle}, {song.SongArtist}";
            await FileHelper.WriteTextFile(TEXT_FILE_NAME, songData);
        }

        public static async Task<List<Song>> RemoveSongAsync(Song songToRemove)
        {
            var songs = await GetSongsAsync();

            foreach (var song in songs.ToList())
            {
                if (song.SongID == songToRemove.SongID)
                {
                    songs.Remove(song);
                }
            }
            await WriteSongsToFile(songs);
            var newSongs = await GetSongsAsync();
            return newSongs;
        }

        public static async Task CopyAllFromAssetToLocal()
        {
            await FileHelper.CopyAllFromAssetToLocal();
            
        }
    }
    //public class SongManager
    //{
    //    const string TEXT_FILE_NAME = "SongsTextfile.txt";

    //    public static List<Song> GetSong()
    //    {
    //        var songs = new List<Song>();

    //        songs.Add(new Song { SongID = 1, SongTitle = "Roar", SongArtist = "Kathy Perry", SongImage = "Assets/lata.jpg" });
    //        songs.Add(new Song { SongID = 2, SongTitle = "DarkHorse", SongArtist = "Britney Spherse", SongImage = "Assets/lata.jpg" });
    //        songs.Add(new Song { SongID = 3, SongTitle = "Bollywood", SongArtist = "Latha Mangeshwar", SongImage = "Assets/katyperry.jpg" });
    //        return songs;
    //    }

    //    // read songs from file
    //    // return list of Song objects

    //    public static async  Task<List<Song>> ReadSongsFromFile()
    //    {
    //        var songs = new List<Song>();
    //        var songList = await FileHelper.ReadTextFile(TEXT_FILE_NAME);
    //        foreach (var aSong in songList)
    //        {
    //            var songParts = aSong.Split('|');
    //            var newSong = new Song
    //            {
    //                SongID = int.Parse(songParts[0]),
    //                SongTitle = songParts[1],
    //                SongArtist = songParts[2],
    //                SongImage = songParts[3],
    //                AudioFileName = songParts[4]
    //            };
    //            songs.Add(newSong);
    //        }
    //        return songs;
    //    }

    //}
    
}
