using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicLibrary.Models
{
    class Album
    {
        public string AlbumTitle { get; set; }
        public string AlbumArtist { get; set; }
        public BitmapImage AlbumImage { get; set; }
    }
}
