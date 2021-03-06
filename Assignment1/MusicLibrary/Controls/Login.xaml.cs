﻿using MusicLibrary.Models;
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
        private List<User> users;
        public Login()
        {
            this.InitializeComponent();
        }

        private async void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var user = User.GetGuestUser();
            await User.WriteUsersToFile(user.UserName);
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                UserName = this.nameText.Text
            };
            //users.Add(user);
            await User.WriteUsersToFile(user.UserName);
          //  this.DataContext = user;
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
