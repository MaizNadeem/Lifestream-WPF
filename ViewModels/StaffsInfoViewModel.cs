using System;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Reflection;
using WPFApp.Repositories;

namespace WPFApp.ViewModels
{
    public class StaffsInfoViewModel : ViewModelBase
    {
        private BitmapImage selectedImage;

        public BitmapImage SelectedImage
        {
            get { return selectedImage; }
            set
            {
                if (selectedImage != value)
                {
                    selectedImage = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }

        public ViewModelCommand SelectImageCommand { get; }

        public StaffsInfoViewModel()
        {
            SelectImageCommand = new ViewModelCommand(SelectImage);
            SetDefaultImage();
        }

        private void SetDefaultImage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = "WPFApp.Images.myprofile.jpg"; // Replace "WPFApp" with your project namespace
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream != null)
                {
                    var defaultImage = new BitmapImage();
                    defaultImage.BeginInit();
                    defaultImage.StreamSource = stream;
                    defaultImage.CacheOption = BitmapCacheOption.OnLoad;
                    defaultImage.EndInit();
                    defaultImage.Freeze(); // Freeze the image for better performance
                    SelectedImage = defaultImage;
                }
            }
        }

        private void SelectImage(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                // Create a new BitmapImage and set it as the source
                SelectedImage = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
