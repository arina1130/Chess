using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chess
{
    class HistoryOfGame
    {
        public static ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();
        public HistoryOfGame(Button cancel, PlayerNames pl)
        {
            ChessBoard.GameEvent += AddToHistory;
            cancel.Click += RemoveFromHistory;
            players = new string[2] { pl.white.Text, pl.black.Text };
        }
        public HistoryOfGame(Win win, ObservableCollection<Figure> figures)
        {
            ChessBoard.GameEvent += AddToHistory;
            win.cancel.Click += RemoveFromHistory;
            AddToListOfFigure(figures, win);
        }
        private static string[] players;
        public delegate void MoveCancelDelegate(string cell, string name, string lastMove);
        public static event MoveCancelDelegate MoveCancelEvent;
        private static void OnMoveCancelEvant(string cell, string name, string lastMove)
        {
            if (MoveCancelEvent != null)
                MoveCancelEvent(cell, name, lastMove);
        }
        private static void AddToHistory(Figure figure)
        {
            string p = "";
            if (figure.IsWhite) p = players[0];
            else p = players[1];
            History.Insert(0, p + " : " + figure.Name +  "." + figure.Id + " " + figure.Cell);
        }
        private static void RemoveFromHistory(object sender, RoutedEventArgs e)
        {
            string str = History[0];
            string[] s = History[0].Split(' ');
            History.RemoveAt(0);
            OnMoveCancelEvant(s[3], s[2], SearchForLastMove(str));
        }
        private static string SearchForLastMove(string move)
        {
            foreach (string m in History)
            {
                string s = m.Remove(m.Length - 2);//ход без ячейки
                if (move.Remove(move.Length - 2) == s) return m.Substring(m.Length - 2);//возвращаем ячейку
            }
            return "";
        }
        private static void AddToListOfFigure(ObservableCollection<Figure> figures, Win win)
        {
            players = new string[2] { File.NameGame.Split('-')[0].Trim(), File.NameGame.Split('-')[1].Trim() };
            win.whiteText.Text = players[0];
            win.blackText.Text = players[1];
            //добавление фигур в список фигур
            bool isWhite = true;
            List<string> cells = new List<string>();
            foreach (string move in History)
            {
                string[] s = move.Split(' ');
                if (s[0] == players[0]) isWhite = true;
                else if (s[0] == players[1]) isWhite = false;
                if (!cells.Contains(s[s.Length - 1]))
                {
                    if (s[2].Contains("ладья")) { figures.Add(new Rook(s[3], isWhite) { Id = Convert.ToInt32(s[2].Substring(6)) }); }
                    else if (s[2].Contains("конь")) { figures.Add(new Knight(s[3], isWhite) { Id = Convert.ToInt32(s[2].Substring(5)) }); }
                    else if (s[2].Contains("ферзь")) 
                    { figures.Add(new Queen(s[3], isWhite) { Id = Convert.ToInt32(s[2].Substring(6)) }); }
                    else if (s[2].Contains("пешка")) { figures.Add(new Pawn(s[3], isWhite) { Id = Convert.ToInt32(s[2].Substring(6)) }); }
                    else if (s[2].Contains("слон")) { figures.Add(new Bishop(s[3], isWhite) { Id = Convert.ToInt32(s[2].Substring(5)) }); }
                    else if (s[2].Contains("король")) { figures.Add(new King(s[3], isWhite) { Id = Convert.ToInt32(s[2].Substring(7)) }); }
                    cells.Add(s[3]);
                }
                if (figures.Count > 1)
                {
                    Figure f = figures[figures.Count - 1];
                    for (int i = 0; i < figures.Count - 2; i++)
                    {
                        if (figures[i].Name + figures[i].Id == f.Name + f.Id) figures.Remove(figures[i]);
                    }
                }
            }
        }
    }
}
