using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace MusicLibrary.Models
{
    class User
    {
        const string USERS_TEXT_FILE_NAME = "UsersTextFile.txt";
        public string UserName { get; set; }
        public string Password { get; set; }
        public BitmapImage UserImage { get; set; }
        
        public static User GetGuestUser()
        {
            var user = new User
            {
                UserName = "Guest",
                UserImage = null
            };
            return user;
        }

        public static string GetUser(string user)
        {
            return user;
        }

        // type of interface using Icollection
        public async static Task<ICollection<User>> GetUsers()
        {
           var users = new List<User>();

           var folder = ApplicationData.Current.LocalFolder;
           var userFile = await folder.GetFileAsync(USERS_TEXT_FILE_NAME);
           var lines = await FileIO.ReadLinesAsync(userFile);

            foreach (var line in lines)
            {
               var userData = line.Split(',');
                var user = new User
                {
                    UserName = userData[0],
                    Password = userData[1]
                };
                users.Add(user);
            }
            return users;
        }
    }
}
