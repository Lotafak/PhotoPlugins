using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ICommand = CommandInterface.ICommand;

namespace PhotoPlugin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<ICommand> pluginList = new List<ICommand>();
        private Stack<ICommand> undoStack = new Stack<ICommand>(); 
        private Stack<ICommand> redoStack = new Stack<ICommand>(); 

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

        //private void RotateClockwise_Click(object sender, RoutedEventArgs e)
        //{
        //    RotateCw();
        //}

        //private void RotateCw()
        //{
        //    TransformedBitmap transformBmp = new TransformedBitmap();
        //    transformBmp.BeginInit();
        //    transformBmp.Source = (BitmapSource)Image.Source;
        //    RotateTransform transform = new RotateTransform(90);
        //    transformBmp.Transform = transform;
        //    transformBmp.EndInit();

        //    BitmapSource bitmapSource = transformBmp;

        //    Image.Source = bitmapSource;
        //}

        private void UndoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            if (undoStack.Count > 0)
            {
                Image tempImg = Image;
                ICommand plugin = undoStack.Pop();
                tempImg.Source = plugin.UnExecute(tempImg.Source);
                redoStack.Push(plugin);
                Image.Source = tempImg.Source;
                //CheckProperties(e.OriginalSource as MenuItem, undoStack);
            }
        }

        private void RedoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            if (redoStack.Count > 0)
            {
                Image tempImg = Image;
                ICommand plugin = redoStack.Pop();
                tempImg.Source = plugin.Execute(tempImg.Source);
                undoStack.Push(plugin);
                Image.Source = tempImg.Source;
            }
            //CheckProperties(sender as MenuItem, redoStack);
        }

        private void AddPlugins_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var o = directoryInfo?.Parent;
            if (o != null)
            {
                var startupPath = o.FullName;
                path = startupPath + @"\Plugins\bin\Debug\Plugins.dll";
            }
            var assembly = Assembly.LoadFrom(path);

            //var type = assembly.GetType("Plugin.ExamplePlugin");    <<< Musimy znac konkretna klase --- Zamiast tego to co ponizej
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass && type.IsPublic && typeof(ICommand).IsAssignableFrom(type))
                {
                    var pluginPlainObject = Activator.CreateInstance(type);

                    var plugin = (ICommand)pluginPlainObject;
                    pluginList.Add(plugin);

                    MenuItem menuItem = new MenuItem();
                    menuItem.ToolTip = plugin.Name;
                    menuItem.Width = 24;
                    menuItem.Click += MenuItem_Click;
                    menuItem.DataContext = plugin;
                    Image img = new Image();
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    string iconPath = "Plugins.Resources." + plugin.IconPath;
                    bi.StreamSource = assembly.GetManifestResourceStream(iconPath);
                    bi.EndInit();
                    img.Source = bi;
                    menuItem.Icon = img; 
                    Menu.Items.Add(menuItem);
                }
            }
            var button = sender as MenuItem;
            if (button != null) button.IsEnabled = false;

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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                var plugin = (ICommand) menuItem.DataContext;
                Image.Source = plugin.Execute(Image.Source);
                undoStack.Push(plugin);
            }
        }

        private void CheckProperties(MenuItem menuItem, Stack<ICommand> stack )
        {
            menuItem.IsEnabled = stack.Count != 0;
        }
    }
}