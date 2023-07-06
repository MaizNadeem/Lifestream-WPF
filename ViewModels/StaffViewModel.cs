using System.Collections.ObjectModel;
using WPFApp.Models;
using WPFApp.Repositories;

namespace WPFApp.ViewModels
{
    public class StaffViewModel : ViewModelBase
    {
        public ObservableCollection<UserModel> Users { get; }

        public StaffViewModel()
        {
            // Create an instance of the UserRepository
            var userRepository = new UserRepository();

            // Retrieve all users from the repository
            var allUsers = userRepository.GetByAll();

            // Set the default profile picture for users with null photo
            foreach (var user in allUsers)
            {
                if (user.Photo == null)
                {
                    user.Photo = LoadDefaultProfilePicture();
                }
            }

            // Populate the Users collection with the retrieved users
            Users = new ObservableCollection<UserModel>(allUsers);
        }

        private byte[] LoadDefaultProfilePicture()
        {
            // Load the default profile picture from the resource
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourcePath = "WPFApp.Images.myprofile.jpg"; // Replace "WPFApp" with your project namespace
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                var defaultImageBytes = new byte[stream.Length];
                stream.Read(defaultImageBytes, 0, defaultImageBytes.Length);
                return defaultImageBytes;
            }
        }
    }
}
