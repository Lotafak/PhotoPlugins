using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            // Create a BitmapImage
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = new Uri("../../Images/pidgeon.jpg", UriKind.RelativeOrAbsolute);
            bmpImage.EndInit();

            var image = sender as Image;
            if (image != null) image.Source = bmpImage;
        }

        private void RotateClockwise_Click(object sender, RoutedEventArgs e)
        {
            Image.Source = RotateCw();
        }

        private BitmapSource RotateCw()
        {
            TransformedBitmap transformBmp = new TransformedBitmap();
            transformBmp.BeginInit();
            transformBmp.Source = (BitmapSource)Image.Source;
            RotateTransform transform = new RotateTransform(90);
            transformBmp.Transform = transform;
            transformBmp.EndInit();

            BitmapSource bitmapSource = transformBmp;

            return bitmapSource;
        }

        private void UndoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Undo");
        }

        private void RedoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Redo");
        }
    }
}
