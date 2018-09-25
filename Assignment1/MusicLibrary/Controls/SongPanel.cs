using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MusicLibrary.Controls
{
    public sealed class SongPanel : ItemsControl
    {
        public SongPanel()
        {
            DefaultStyleKey = typeof(SongPanel);
        }

        public int CountSongs(User user)
        {
            return CanvasControl.CountSongs(user);
        }

        private SongCanvas CanvasControl
        {
            get
            {
                return (SongCanvas)ItemsPanelRoot;
            }
        }
    }
}
