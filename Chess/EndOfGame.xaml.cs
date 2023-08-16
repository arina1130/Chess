using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для EndOfGame.xaml
    /// </summary>
    public partial class EndOfGame : Page
    {
        public EndOfGame(string winner)
        {
            InitializeComponent();
            winnerText.Text = winnerText.Text + winner;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void backToMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            NavigationService.Navigate(menu);
        }
    }
}
