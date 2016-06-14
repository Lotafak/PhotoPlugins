using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommandInterface
{
    public interface ICommand
    {
        string Name { get; }
        string IconPath { get; }
        BitmapSource Execute(ImageSource imageSource);
        BitmapSource UnExecute(ImageSource imageSource);
    }
}
