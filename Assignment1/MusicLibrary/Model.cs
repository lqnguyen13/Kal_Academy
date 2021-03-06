﻿using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary
{
    [DataContract]
    public class Model
    {
        public Model()
        {
            Initialize();
        }

        private void Initialize()
        {
            this._songs = new ObservableCollection<Song>();
        }
        [DataMember]
        public ObservableCollection<Song> SongCollection
        {
            get
            {
                return _songs;
            }
        }

        public User UserFromName(string name) => Family.FirstOrDefault(user => user.UserName.Equals(name, StringComparison.OrdinalIgnoreCase));


        [DataMember]
        public ObservableCollection<User> Family
        {
            get
            {
                return _family;
            }
        }

        //       public void AddSong(Song song) => SongCollection.Add(new Song(song));

        public void DeleteSong(Song songToDelete) => SongCollection.Remove(songToDelete);

        private ObservableCollection<Song> _songs;
        private ObservableCollection<User> _family;
    }
}
