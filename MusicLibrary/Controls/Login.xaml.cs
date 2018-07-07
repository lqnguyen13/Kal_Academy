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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MusicLibrary.Controls
{
    public sealed partial class Login : Page
    {
        string user;
        public Login()
        {
            this.InitializeComponent();
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            user = User.GetUser(this.nameText.Text);
            this.DataContext = user;
            this.Frame.Navigate(typeof(MainPage), user);
        }

        //private void cancelbtn_Click(object sender, RoutedEventArgs e)
        //{
        //    // var user = User.GetGuestUser();
        //    //  this.nameText.Text = user.UserName;
        //    //, user);

        //    //MainPage. UpdateGreeting(user.UserName);
        //}
    }
}
