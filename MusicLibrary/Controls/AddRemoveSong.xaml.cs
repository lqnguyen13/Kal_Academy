﻿using System;
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
        const string TEXT_FILE_NAME = "SampleTextFile.txt";

        public AddRemoveSong()
        {
            this.InitializeComponent();
        }

        private async void readFile_Click(object sender, RoutedEventArgs e)
        {
          //  string str = await FileHelper.ReadTextFile(TEXT_FILE_NAME);
           // textBlock.Text = str;
        }

        private async void writeFile_Click(object sender, RoutedEventArgs e)
        {
          // string textFilePath = await FileHelper.WriteTextFile(TEXT_FILE_NAME, textBox.Text);
        }

        private void backToPlayList_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
