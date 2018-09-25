using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        private StorageFile songImageFile;
        private StorageFile songAudioFile;

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
            var song = new Song
            {
                SongTitle = songTitle.Text,
                SongArtist = songArtist.Text,
                SongID = 0,
                SongImage = songImage.Text,
                AudioFileName = songAudio.Text
            };
            DataContext = await Song.AddSongAsync(song, songImageFile, songAudioFile);
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

        private async void AddSongImage_Click(object sender, RoutedEventArgs e)
        {
            // Based on https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-using-file-and-folder-pickers
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                //Application now has read/write access to the picked file
                this.songImage.Text = file.Name;
                songImageFile = file;

            }
        }

        private async void AddSongAudio_Click(object sender, RoutedEventArgs e)
        {
            // Based on https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-using-file-and-folder-pickers
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            picker.FileTypeFilter.Add(".mp3");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                //Application now has read/write access to the picked file
                this.songAudio.Text = file.Name;
                songAudioFile = file;

            }
        }
    }
}
