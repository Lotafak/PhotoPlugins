using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICommand = CommandInterface.ICommand;

namespace Plugins
{
    public class RotateClockwise : ICommand
    {
        private ImageSource _imageSource;

        public RotateClockwise()
        {
        }

        public RotateClockwise(ImageSource imageSource)
        {
            _imageSource = imageSource;
        }



        public BitmapSource Execute()
        {
            TransformedBitmap transformBmp = new TransformedBitmap();
            transformBmp.BeginInit();
            transformBmp.Source = (BitmapSource)_imageSource;
            RotateTransform transform = new RotateTransform(90);
            transformBmp.Transform = transform;
            transformBmp.EndInit();

            BitmapSource bitmapSource = transformBmp;

            return bitmapSource;
        }

        public BitmapSource UnExecute()
        {
            TransformedBitmap transformBmp = new TransformedBitmap();
            transformBmp.BeginInit();
            transformBmp.Source = (BitmapSource)_imageSource;
            RotateTransform transform = new RotateTransform(-90);
            transformBmp.Transform = transform;
            transformBmp.EndInit();

            BitmapSource bitmapSource = transformBmp;

            return bitmapSource;
        }
    }
}

