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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        static Settings set = new Settings(new Menu());
        static Win wind;
        static ContinuationOfGame files;
        RulesOfGame rul;
        public Menu()
        {
            InitializeComponent();
            rul = new RulesOfGame(this);
        }

        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            wind = new Win((bool)set.isTimerOn.IsChecked, set.Minute.Value.ToString()+":00", set.Minute.Value.ToString()+":00", (bool)set.isVisibleMoves.IsChecked);
            PlayerNames players = new PlayerNames(wind, (bool)set.isVisibleMoves.IsChecked);
            NavigationService.Navigate(players);
        }

        private void rules_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(rul);
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(set);
        }

        private void continue_Click(object sender, RoutedEventArgs e)
        {
            files = new ContinuationOfGame();
            NavigationService.Navigate(files);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
