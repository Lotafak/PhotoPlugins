using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CommandInterface
{
    public interface ICommand
    {
        BitmapSource Execute();
        BitmapSource UnExecute();
    }
}
