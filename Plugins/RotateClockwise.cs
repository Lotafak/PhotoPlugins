﻿using System.Windows.Media;
using System.Windows.Media.Imaging;
using ICommand = CommandInterface.ICommand;

namespace Plugins
{
    public class RotateClockwise : ICommand
    {
        public string Name { get; } = "Rotate Clockwise";
        public string IconPath { get; } = "rotate-cw.png";

        public BitmapSource Execute(ImageSource imageSource)
        {
            return doWork(imageSource, 90);
        }

        public BitmapSource UnExecute(ImageSource imageSource)
        {
            return doWork(imageSource, -90);
        }

        private BitmapSource doWork(ImageSource imageSource, int degree)
        {
            TransformedBitmap transformBmp = new TransformedBitmap();
            transformBmp.BeginInit();
            transformBmp.Source = (BitmapSource)imageSource;
            RotateTransform transform = new RotateTransform(degree);
            transformBmp.Transform = transform;
            transformBmp.EndInit();

            BitmapSource bitmapSource = transformBmp; 
            return bitmapSource;
        }
    }
}

