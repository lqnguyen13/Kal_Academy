using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{
    public class Song
    {
        public int SongID { get; set; }
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }
        public string SongImage { get; set; }
        public string AudioFile { get; set; }

    }



    public class SongManager
    {
        public static List<Song> GetSong()
        {
            var songs = new List<Song>();

            songs.Add(new Song { SongID = 1, SongTitle = "Roar", SongArtist = "Kathy Perry", SongImage = "Assets/lata.jpg" });
            songs.Add(new Song { SongID = 2, SongTitle = "DarkHorse", SongArtist = "Britney Spherse", SongImage = "Assets/lata.jpg" });
            songs.Add(new Song { SongID = 3, SongTitle = "Bollywood", SongArtist = "Latha Mangeshwar", SongImage = "Assets/katyperry.jpg" });
            return songs;
        }
    }









}
