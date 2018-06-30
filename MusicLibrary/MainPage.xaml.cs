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
using MusicLibrary.Models;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //private List<Song> Songs;

        //public int Assets { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            //Songs = SongManager.GetSong();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Songs = await SongManager.ReadSongsFromFile();
            this.DataContext = await Song.GetSongs();
        }

        private void SongsGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            BitmapImage image = new BitmapImage(new Uri("ms-appx:///" + song.SongImage, UriKind.RelativeOrAbsolute));
            this.SelectedSongTitle.Text = song.SongTitle;
            this.SelectedSongArtist.Text = song.SongArtist;
            this.SelectedSongImage.Source = image;
            this.myMediaElement.Source = new Uri("ms-appx:///" + song.AudioFile, UriKind.RelativeOrAbsolute);
        }

        private void AddRemove_Click(object sender, RoutedEventArgs e)
        {
            // ENTER CODE/FUNCTION CALL HERE TO NAVIGATE TO A SEPARATE PAGE TO ADD A SONG
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // ENTER CODE/FUNCTION CALL HERE TO SEARCH FOR SONG INDICATED IN SEARCH BOX
        }

        public void PlaySoundButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Play();
        }

        private void StopSoundButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
        }

        private void PauseSoundButton_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Pause();
        }

        //public void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        //{


        //    var song = (Song)e.ClickedItem;
        //    ResultTextBlock.Text = "You Selected" + song.SongTitle;
        //    // song.Play_Button_Click();

        //}




        //public void Play_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //  if (song.SongTitle == "Roar")



        //}


        //private void Stop_Button_Click(object sender, RoutedEventArgs e)
        //{

        //    myMediaElement.Stop();
        //}
    }


}
