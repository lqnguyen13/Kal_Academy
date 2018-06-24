using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicLibrary.Models
{
    class Song
    {
        public string SongTitle { get; set; }
        public string SongFileName { get; set; }
        public BitmapImage SongImage { get; set; }
    }
}
