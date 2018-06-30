﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{

    public class Song
    {

        const string TEXT_FILE_NAME = "SongsTextfile.txt";

        public int SongID { get; set; }
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }
        public string SongImage { get; set; }
        public string AudioFile { get; set; }

        public Song()
        {

        }

        public async static Task<ICollection<Song>> GetSongs()
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
                    AudioFile = songParts[4]
                };
                songs.Add(newSong);
            }
            return songs;
        }

        public async static void WriteSong(Song song)
        {
            var songData = $"{song.SongID}|{song.SongTitle}|{song.SongArtist}|{song.SongImage}|{song.AudioFile}";
            await FileHelper.WriteOneLine(TEXT_FILE_NAME, songData);
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
    //                AudioFile = songParts[4]
    //            };
    //            songs.Add(newSong);
    //        }
    //        return songs;
    //    }

    //}









}
