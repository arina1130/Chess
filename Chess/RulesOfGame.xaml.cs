using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для RulesOfGame.xaml
    /// </summary>
    public partial class RulesOfGame : Page
    {
        public RulesOfGame(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
            FileStream stream = new FileStream(@"Resources\rules.txt", FileMode.Open);
            TextRange text = new TextRange(rules.Document.ContentStart, rules.Document.ContentEnd);
            text.Load(stream, DataFormats.Text);
            stream.Close();
        }
        private Menu menu;
        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(menu);
        }
    }
}
