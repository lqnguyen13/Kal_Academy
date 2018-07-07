using MusicLibrary.Models;
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
    public sealed partial class AddRemoveSong : Page
    {
        const string TEXT_FILE_NAME = "SongsTextfile.txt";
        private Song selectedSong;
        private ICollection<Song> songList;

        public AddRemoveSong()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = await Song.GetSongsAsync();
        }

        private async void addSong_Click(object sender, RoutedEventArgs e)
        {
            //string textFilePath = await FileHelper.WriteTextFile(TEXT_FILE_NAME, textBox.Text);
            var song = new Song
            {
                SongTitle = songTitle.Text,
                SongArtist = songArtist.Text,
                SongID = 0,
                SongImage = "N/A",
                AudioFileName = "N/A"
            };
            DataContext = await Song.AddSongAsync(song);
            AddMessage.Text = "New song " + song.SongTitle + " was successfully added!";
        }

        private async void removeSong_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = await Song.RemoveSongAsync(selectedSong);
                RemoveMessage.Text = selectedSong.SongTitle + " has been successfully removed!";
            }
            catch 
            {
                RemoveMessage.Text = "No song selected!";
            }
            
        }

        private void CurrentSongsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            selectedSong = (Song)e.ClickedItem;
        }

        private void backToPlayList_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}
