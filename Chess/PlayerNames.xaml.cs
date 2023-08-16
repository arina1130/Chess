using System;
using System.Collections.Generic;
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

namespace Chess
{
    /// <summary>
    /// Логика взаимодействия для PlayerNames.xaml
    /// </summary>
    public partial class PlayerNames : Page
    {
        public PlayerNames(Win win, bool visMove)
        {
            InitializeComponent();
            window = win;
            board = new ChessBoard(window, this, visMove);
        }
        ChessBoard board;
        Win window;
        private void start_Click(object sender, RoutedEventArgs e)
        {
            File file = new File(white.Text, black.Text);
            if (File.FileGame != null)
            {
                window.whiteText.Text = white.Text;
                window.blackText.Text = black.Text;
                board.History = new HistoryOfGame(window.cancel, this);
                NavigationService.Navigate(window);
            }
            else
            {
                white.Text = "";
                black.Text = "";
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }
}
