using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PhotoPlugin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the TransformedBitmap
            //TransformedBitmap transformBmp = new TransformedBitmap();

            // Create a BitmapImage
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri("../../Images/pidgeon.jpg", UriKind.RelativeOrAbsolute);
            bmpImage.EndInit();

            // Properties must be set between BeginInit and EndInit
            //transformBmp.BeginInit();
            //transformBmp.Source = bmpImage;
            //RotateTransform transform = new RotateTransform(90);
            //transformBmp.Transform = transform;
            //transformBmp.EndInit();

            var image = sender as Image;
            if (image != null) image.Source = bmpImage;
        }
    
    }
}
