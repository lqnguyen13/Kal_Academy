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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MusicLibrary.Controls
{
    public sealed partial class SongControl : UserControl
    {
        public SongControl()
        {
            this.InitializeComponent();
        }

        public SongControl SongBusinessObject { get; set; }


        //Create a property, which we expect to be a NotesCanvas
        private SongCanvas CanvasControl
        {
            get
            {
                //get the parent of this object (using VisualTreeHelper)
                //this = current control
                //parent = container
                var parent = VisualTreeHelper.GetParent(this);
                //get the parent of the container (which is the ContentPresenter), which should be a canvas
                //canvas = SongsCanvas
                var canvas = VisualTreeHelper.GetParent(parent);
                //Return typecast because we know that it must be a canvas
                return (SongCanvas)canvas;
            }
        }
    }
}
