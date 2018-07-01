﻿using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MusicLibrary.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResults : Page
    {
        public SearchResults()
        {
            this.InitializeComponent();
        }
        private ICollection<Song> songs;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            songs = await Song.GetSongs();
        }

        private void SearchForSongs_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = Song.FilterSongs(this.SearchSongTitle.Text, songs);
        }


        // SongCanvas sg = new SongCanvas();
        // Song song = sg.DataContext as Song;

        //   
        // this.SelectedSongTitle.Text = this.SearchSongTitle.Text;


    }
}
