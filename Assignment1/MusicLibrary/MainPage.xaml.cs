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
using MusicLibrary.Controls;
using MusicLibrary.Speech;
using Windows.UI.Core;
using System.Diagnostics;
using System.Threading.Tasks;

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
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // Perform initialization for speech recognition.
            _speechManager = new SpeechManager(FamilyModel);
            _speechManager.PhraseRecognized += speechManager_PhraseRecognized;
            _speechManager.StateChanged += speechManager_StateChanged;
            await _speechManager.StartContinuousRecognition();

            await Song.CopyAllFromAssetToLocal();
            base.OnNavigatedTo(e);
            var users = await User.GetUsers();           
            UpdateGreetingAsync(users.First().UserName);
            DataContext = await Song.GetSongsAsync();

            _pageParameters = e.Parameter as VoiceCommandObjects.VoiceCommand;
            if (_pageParameters != null)
            {
                switch (_pageParameters.VoiceCommandName)
                {
                    case "playSong":
                        _activeSong = await PlaySong(_pageParameters.SongSpoken);
                        break;
                    case "stopSong":
                        _activeSong = await StopSong(_pageParameters.SongSpoken);
                        break;
                    case "pauseSong":
                        _activeSong = await PauseSong(_pageParameters.SongSpoken);
                        break;
                    default:
                        break;
                }
            }
        }

        public async void UpdateGreetingAsync(string name)
        {
            var now = DateTime.Now;
            var greeting = 
                now.Hour < 12 ? "Good morning" : now.Hour < 18 ? "Good afternoon" : "Good night";
            var user = string.IsNullOrEmpty(name) ? "!" : $", {name}!";
            welcomeUser.Text = $"{greeting}{user}";
            if (!string.IsNullOrEmpty(name))
            {
                //await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                //{
                    var SpeakGreeting = $"{greeting} {name}";

                    //var songs = taskPanel.CountSongs(FamilyModel.UserFromName(name));

                    //if (songs > 0)
                    //{
                    //    if (songs == 1)
                    //        SpeakGreeting += ",there is a song for you.";
                    //    else
                    //        SpeakGreeting += $",there are {songs} song for you.";
                    //}

                    await this._speechManager.SpeakAsync(
                                SpeakGreeting,
                                 this._media);
               // });
            }
        }

        private Model FamilyModel { get; set; }

        private void SongsGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            BitmapImage image = new BitmapImage(new Uri("ms-appdata:///local/" + song.SongImage, UriKind.RelativeOrAbsolute));
            this.SelectedSongTitle.Text = song.SongTitle;
            this.SelectedSongArtist.Text = song.SongArtist;
            this.SelectedSongImage.Source = image;
            this.myMediaElement.Source = new Uri("ms-appdata:///local/" + song.AudioFileName, UriKind.RelativeOrAbsolute);
        }

        private void AddRemove_Click(object sender, RoutedEventArgs e)
        {
            // ENTER CODE/FUNCTION CALL HERE TO NAVIGATE TO A SEPARATE PAGE TO ADD A SONG
            this.Frame.Navigate(typeof(AddRemoveSong));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // ENTER CODE/FUNCTION CALL HERE TO SEARCH FOR SONG INDICATED IN SEARCH BOX
            this.Frame.Navigate(typeof(SearchResults));
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

        public async Task<Song> PlaySong(string name)
        {
            var songName = SpeechRecognitionMode.Play;
            var song = await Song.GetSongsAsync();
            if (songName.ToString() == name)
            {
                return song.FirstOrDefault();
            }
            myMediaElement.Play();
            return null;
        }

        public async Task<Song> StopSong(string name)
        {
            var songName = SpeechRecognitionMode.Stop;
            var song = await Song.GetSongsAsync();
            if (songName.ToString() == name)
            {
                return song.FirstOrDefault();
            }
            myMediaElement.Stop();
            return null;
        }

        public async Task<Song> PauseSong(string name)
        {
            var songName = SpeechRecognitionMode.Pause;
            var song = await Song.GetSongsAsync();
            if (songName.ToString() == name)
            {
                return song.FirstOrDefault();
            }
            myMediaElement.Pause();
            return null;
        }

        #region Speech Handling

        private async void speechManager_StateChanged(object sender, StateChangedEventArgs e)
        {

            if (e.IsSessionState && !e.SessionCompletedSuccessfully && e.SessionTimedOut)
            {
                Debug.WriteLine("Timeout exceeded, resetting RecognitionMode to CommandPhrases");
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    await _speechManager.SetRecognitionMode(SpeechRecognitionMode.CommandPhrases);
                });
            }
        }

        /// <summary>
        /// Handles the <see cref="SpeechManager.PhraseRecognized"/> event.
        /// </summary>
        /// <param name="sender">the <see cref="SpeechManager"/> that raised the event.</param>
        /// <param name="e">The event data.</param>
        private async void speechManager_PhraseRecognized(object sender, PhraseRecognizedEventArgs e)
        {
            User user = e.PhraseTargetUser;
            string phrase = e.PhraseText;
            CommandVerb verb = e.Verb;

            string msg = String.Format("Heard phrase: {0}", phrase);
            Debug.WriteLine(msg);

            switch (verb)
            {
                case CommandVerb.Play:
                    {
                        // The phrase came from dictation, so transition speech recognition
                        // to listen for command phrases.
                        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            _activeSong = await PlaySong(CommandVerb.Play.ToString());
                            await _speechManager.SetRecognitionMode(SpeechRecognitionMode.CommandPhrases);
                        });

                        break;
                    }
                case CommandVerb.Stop:
                    {
                        // A command for creating a note was recognized.
                        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            _activeSong = await StopSong(CommandVerb.Stop.ToString());
                            await _speechManager.SpeakAsync("Stop your song", _media);
                            await _speechManager.SetRecognitionMode(SpeechRecognitionMode.Stop);
                        });

                        break;
                    }
                case CommandVerb.Pause:
                    {
                        // The command for reading a note was recognized.
                        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            _activeSong = await PauseSong(CommandVerb.Pause.ToString());
                            await _speechManager.SpeakAsync("Pause your song", _media);
                            await _speechManager.SetRecognitionMode(SpeechRecognitionMode.Stop);
                        });

                        break;
                    }
                case CommandVerb.Show:
                    {
                        Debug.WriteLine("SpeechManager.PhraseRecognized handler: Show TBD");
                        break;
                    }
                case CommandVerb.Help:
                    {
                        // A command for spoken help was recognized.
                        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                        {
                            await _speechManager.SpeakAsync(_helpString, _media);
                        });

                        break;
                    }
                default:
                    {
                        Debug.WriteLine("SpeechManager.PhraseRecognized handler: Couldn't determine phrase intent");
                        break;
                    }
            }
        }

       
        #endregion
        #region Private fields
        private Song _activeSong;
        private CoreDispatcher _dispatcher;
        private SpeechManager _speechManager;
        private VoiceCommandObjects.VoiceCommand _pageParameters;
        private const string _helpString = "You can say: add song for user. For the active song, you can say, play song, pause song, and stop song.";
        #endregion
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
