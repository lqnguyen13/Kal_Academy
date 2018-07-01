using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLibrary.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MusicLibrary.Controls
{
    public class SongCanvas : Canvas
    {
        /// <summary>
        /// Gets the <see cref="SongControl"/> control that has focus currently (been clicked).
        /// </summary>
        public SongControl SelectedSong { get; private set; }

        /// <summary>
        /// Gets a collection of <see cref="SongControl"/> controls that
        /// are contained in the current <see cref="SongsCanvas"/> control.
        /// </summary>
        public List<SongControl> SongControls
        {
            get
            {
                if (_songsList == null)
                {
                    _songsList = GetSongsFromVisualTree();
                }

                return _songsList;
            }
        }

        // Adapted from http://stackoverflow.com/questions/4586106/wpf-how-to-access-control-from-datatemplate.
        private T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }

            return child;
        }

        private List<SongControl> GetSongsFromVisualTree()
        {
            var songs = Children.Select(cp => GetVisualChild<SongControl>(cp));
            return songs.ToList();
        }
        public Song songs { get; set; }

       

        /*public Song AddSong(Song song)
        {
            Song newSong = new Song(song);

            return newSong;
        }

        public Song RemoveSong()
        {

        }*/

        private List<SongControl> _songsList;
    }
}
