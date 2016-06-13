using System;
using System.IO;
using System.Reflection;
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
            RotateCw();
        }

        private void RotateCw()
        {
            TransformedBitmap transformBmp = new TransformedBitmap();
            transformBmp.BeginInit();
            transformBmp.Source = (BitmapSource)Image.Source;
            RotateTransform transform = new RotateTransform(90);
            transformBmp.Transform = transform;
            transformBmp.EndInit();

            BitmapSource bitmapSource = transformBmp;

            Image.Source = bitmapSource;
        }

        private void UndoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Undo");
        }

        private void RedoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Redo");
        }

        private void AddPlugins_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            var o = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            if (o != null)
            {
                var startupPath = o.FullName;
                path = startupPath + @"\Plugins\bin\Debug\Plugins.dll";
            }
            //Ladowanie wszystkich pluginow z jakiegos predefiniowanego folderu
            //Albo Plugin manager pozwalajacy wybierac jakie pluginy uzyc
            var assembly = Assembly.LoadFrom(path);

            //var type = assembly.GetType("Plugin.ExamplePlugin");    <<< Musimy znac konkretna klase --- Zamiast tego to co ponizej
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass && type.IsPublic && typeof(CommandInterface.ICommand).IsAssignableFrom(type))
                {
                    var pluginPlainObject = Activator.CreateInstance(type, Image.Source);

                    var plugin = (CommandInterface.ICommand)pluginPlainObject;

                    Image.Source = plugin.Execute();
                }
            }

            /*
 * Aplikacja z GUI
 * Pozwala dogrywac wtyczki
 * Może dotyczyć na przykład przetwarzania obrazu
 * Ładujemy obraz z pliku
 * Ładujemy wtyczki z jakiegoś folderu
 * Wtyczki to np Sepia, skala szarości itp
 * Po załadowaniu wtyczek pojawiają się elementy GUI pozwalające używać pluginów
 * Obsługuje funkcje undo i redo
 * Undo redo można zrealizować na przykład przez zapamiętywanie wszystkich stanów jako osobne obrazy (gorzej)
 * Undo redo lepiej zrealizować wykonując na niewiczocznym obrazie wszystkie operacje oprócz ostatniej i zwracanie takiego obrazu
 */
        }
    }
}
