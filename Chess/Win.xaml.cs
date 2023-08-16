using DotLiquid.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Chess
{
    /// <summary>
    /// Логика взаимодействия для Win.xaml
    /// </summary>
    public partial class Win : Page
    {
        public static Exit exit;
        public bool IsMoveVisible { get; private set; }
        private Pawn pawn;
        public Win(bool isTimerOn, string minuteW, string minuteB, bool visMov)
        {
            InitializeComponent();
            IsMoveVisible = visMov;
            history.ItemsSource = HistoryOfGame.History;
            ChessBoard.WinnerEvent += Winner_Click;
            ChessBoard.ChangeOfCourseEvent += ChangeOfCourse;
            if (isTimerOn)
            {
                cancel.IsEnabled = false;
                whiteTimer.Visibility = Visibility.Visible;
                blackTimer.Visibility = Visibility.Visible;
                if (minuteB != "" || minuteB != ":00") blackTimer.Content = minuteB;
                if (minuteW != "" || minuteW != ":00") whiteTimer.Content = minuteW;

                disptcherTimerWhite.Interval = TimeSpan.FromSeconds(1);
                disptcherTimerWhite.Tick += new EventHandler((e, v) => Countdown(whiteTimer));
                disptcherTimerBlack.Interval = TimeSpan.FromSeconds(1);
                disptcherTimerBlack.Tick += new EventHandler((e, v) => Countdown(blackTimer));
            }
            else
            {
                whiteTimer.Visibility = Visibility.Collapsed;
                blackTimer.Visibility = Visibility.Collapsed;
            }
        }
        private DispatcherTimer disptcherTimerWhite = new DispatcherTimer();
        private DispatcherTimer disptcherTimerBlack = new DispatcherTimer();
        private void ChangeOfCourse(bool isWhite)
        {
            if (isWhite)
            {
                disptcherTimerBlack.Stop();
                disptcherTimerWhite.Start();
            }
            else
            {
                disptcherTimerWhite.Stop();
                disptcherTimerBlack.Start();
            }
        }
        private void Countdown(object ti)
        {
            if (ti is Label)
            {
                string time = null;
                Dispatcher.Invoke(() => time = (ti as Label).Content.ToString());
                string[] t = time.Split(':');
                int min = Convert.ToInt32(t[0]), sec = Convert.ToInt32(t[1]);
                if (min == 0 && sec == 0)
                {
                    if ((ti as Label).Name == "whiteTimer")
                    {
                        Winner_Click(false);
                    }
                    else if ((ti as Label).Name == "blackTimer")
                    {
                        Winner_Click(true);
                    }
                }
                if (Convert.ToInt32(t[1]) != 0)
                {
                    sec = sec - 1;
                }
                else
                {
                    min = min - 1;
                    sec = 59;
                }
                time = min + ":" + sec;
                Dispatcher.Invoke(() => (ti as Label).Content = time);
            }
        }
        private void Winner_Click(bool isWhite)
        {
            if (isWhite)
            {
                NavigationService.Navigate(new EndOfGame(this.whiteText.Text));
            }
            else
            {
                string d = this.blackText.Text;
                NavigationService.Navigate(new EndOfGame(this.blackText.Text));
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            exit = new Exit(this, IsMoveVisible);
            exit.Show();
        }
        public void PawnChangeChallenge(Pawn pawn)//вызов изменения пешки
        {
            this.pawn = pawn;
            NavigationService.Navigate(new PawnChange(pawn,this));
        }
    }
}
