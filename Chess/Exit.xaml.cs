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
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Логика взаимодействия для Exit.xaml
    /// </summary>
    public partial class Exit : Window
    {
        public Exit(Win win, bool visMov)
        {
            InitializeComponent();
            window = win;
            isMoveVisible = visMov;
        }
        private Win window;
        private bool isMoveVisible;
        private void saveAndExit_Click(object sender, RoutedEventArgs e)
        {
            File.SaveHistoryInFile();
            Label a = window.whiteTimer;
            Label b = window.blackTimer;
            if (window.whiteTimer.Content != null && window.blackTimer.Content != null)
            {
                File.SaveSetting(window.whiteTimer.Content.ToString(), window.blackTimer.Content.ToString(), isMoveVisible);
            }
            else { File.SaveSetting("", "", isMoveVisible); }
            Environment.Exit(0);//выход из всей программы
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
