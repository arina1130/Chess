using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Chess
{
    /// <summary>
    /// Логика взаимодействия для ContinuationOfGame.xaml
    /// </summary>
   
    public partial class ContinuationOfGame : Page
    {
        ObservableCollection<string> files = new ObservableCollection<string>();
        Win win;
        public ContinuationOfGame()
        {
            InitializeComponent();
            listOfName.ItemsSource = files;
            foreach (string f in Directory.GetFiles("Games"))
            {
                if (!f.Contains("_set")) files.Add(Path.GetFileNameWithoutExtension(f));
            }
        }

        private void continue_Click(object sender, RoutedEventArgs e)
        {
            win = File.OpeningFile(listOfName.SelectedItem.ToString());
            if (win != null)
            {
                ChessBoard chess = new ChessBoard(win);
                NavigationService.Navigate(win);
            }
            else
            {
                NavigationService.Navigate(new Menu());
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }
}
