using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommandInterface;

namespace Plugins
{
    public class ToGrayscale : ICommand
    {
        public string Name { get; } = "Convert to grayscale";
        public string IconPath { get; } = "grayscale.png";
        public BitmapSource BitmapSource;

        public ToGrayscale()
        {
        }

        public BitmapSource Execute(ImageSource imageSource)
        {
            BitmapSource = (BitmapSource) imageSource;
            FormatConvertedBitmap grayBitmapSource = new FormatConvertedBitmap();
            grayBitmapSource.BeginInit();
            grayBitmapSource.Source = (BitmapSource)imageSource;
            grayBitmapSource.DestinationFormat = PixelFormats.Gray32Float;
            grayBitmapSource.EndInit();

            BitmapSource bitmapSource = grayBitmapSource;

            return bitmapSource;
        }

        public BitmapSource UnExecute(ImageSource imageSource)
        {
            return BitmapSource;
        }
    }
}
