using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Navigation;
using System.Threading;

namespace Chess
{
    class ChessBoard
    {
        private static ObservableCollection<Rectangle> ListCells { get; set; } = new ObservableCollection<Rectangle>();
        public static ObservableCollection<Figure> ListOfFigures { get; set; } = new ObservableCollection<Figure>();
        private static ObservableCollection<Figure> ListOfDeleteFigures { get; set; } = new ObservableCollection<Figure>();
        private Grid boardGrid;
        private Win win;
        private static int visMoves;
        private static bool isMoveWhite = true;
        public static bool IsMoveWhite { get { OnChangeOfCourseEvent(isMoveWhite); return isMoveWhite; } private set { isMoveWhite = value; OnChangeOfCourseEvent(isMoveWhite); } }
        public HistoryOfGame History;
        public ChessBoard(Win board, PlayerNames newGame, bool visMov)
        {
            win = board;
            boardGrid = board.board;
            Cells();
            newGame.start.Click += newGame_Click;
            if (visMov) visMoves = 2;
            else visMoves = 0;
        }
        public ChessBoard(Win wind)
        {
            if (wind.IsMoveVisible) visMoves = 2;
            else visMoves = 0;
            boardGrid = wind.board;
            Cells();
            History = new HistoryOfGame(wind, ListOfFigures);
            ContinuationGame();
        }
        public delegate void ChangeOfCourseDelegate(bool change);
        public static event ChangeOfCourseDelegate ChangeOfCourseEvent;
        private static void OnChangeOfCourseEvent(bool change)
        {
            if (ChangeOfCourseEvent != null)
                ChangeOfCourseEvent(change);
        }
        public delegate void WinnerDelegate(bool isWhite);
        public static event WinnerDelegate WinnerEvent;
        private static void OnWinnerEvent(bool isWhite)
        {
            if (WinnerEvent != null)
                WinnerEvent(isWhite);
        }
        public delegate void GameDelegate(Figure figure);
        public static event GameDelegate GameEvent;
        private static void OnGameEvent(Figure figure)
        {
            if (GameEvent != null)
                GameEvent(figure);
        }
        private static void CleanAvMoves()
        {
            foreach (Rectangle cell in ListCells)
            {
                cell.Stroke = null;
            }
        }
        private void board_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)//работа с фигурками на доске
        {
            if (sender != null)
            {
                Figure q = null;
                foreach (Figure figure in ListOfFigures)
                {
                    if (figure.IsSelected) q = figure;
                }
                for (int j = 0; j < ListOfFigures.Count; j++)
                {
                    Figure figure = ListOfFigures[j];
                    Rectangle f = e.Source as Rectangle;
                    int col = Grid.GetColumn(f);
                    int row = Grid.GetRow(f);
                    if (Grid.GetColumn(figure.ObjFig) == col && Grid.GetRow(figure.ObjFig) == row && figure.IsSelected)//Уберает выделение с фигуры, если на нее нажали
                    {
                        figure.ObjFig.Stroke = null;
                        CleanAvMoves();
                        return;
                    }
                    if (q != null && (Grid.GetColumn(q.ObjFig) != col || Grid.GetRow(q.ObjFig) != row) && Grid.GetColumn(figure.ObjFig) == col
                        && Grid.GetRow(figure.ObjFig) == row && q.IsWhite == IsMoveWhite && figure.IsWhite != q.IsWhite)//случай, когда фигура бьет другую фигуру
                    {
                        CheckMoves(q);
                        if (q.ListAvailableMoves.Contains(figure.Cell))
                        {
                            if (IsMoveWhite) IsMoveWhite = false;
                            else IsMoveWhite = true;
                            boardGrid.Children.Remove(figure.ObjFig);
                            ListOfFigures.Remove(figure);
                            ListOfDeleteFigures.Add(figure);
                            boardGrid.Children.Remove(q.ObjFig);
                            q.Cell = NameCell(col, row);
                            Point p = CellSearch(q.Cell);
                            Grid.SetColumn(q.ObjFig, (int)p.X);
                            Grid.SetRow(q.ObjFig, (int)p.Y);
                            boardGrid.Children.Add(q.ObjFig);
                            q.ObjFig.Stroke = null;
                            OnGameEvent(q);
                            CleanAvMoves();
                            if (figure is King)
                            {
                                if (figure.IsWhite)
                                {
                                    OnWinnerEvent(false);
                                }
                                else
                                {
                                    OnWinnerEvent(true);
                                }
                            }
                            return;
                        }
                    }
                    if (Grid.GetColumn(figure.ObjFig) == col && Grid.GetRow(figure.ObjFig) == row && figure.IsWhite == IsMoveWhite && !figure.IsSelected)//выделяем нужную фигурку
                    {
                        for (int i = 0; i < ListOfFigures.Count; i++)
                        {
                            if (figure != ListOfFigures[i] && figure.Cell == ListOfFigures[i].Cell)
                            {
                                boardGrid.Children.Remove(figure.ObjFig);
                                ListOfFigures.Remove(figure);
                                ListOfDeleteFigures.Add(figure);
                                if (figure is King)
                                {
                                    if (figure.IsWhite)
                                    {
                                        OnWinnerEvent(false);
                                    }
                                    else
                                    {
                                        OnWinnerEvent(true);
                                    }
                                }
                                CleanAvMoves();
                                foreach (Figure fig in ListOfFigures)
                                {
                                    fig.ObjFig.Stroke = null;
                                }
                                return;
                            }
                        }
                        CleanAvMoves();
                        foreach (Figure fig in ListOfFigures)
                        {
                            fig.ObjFig.Stroke = null;
                        }
                        figure.ObjFig.Stroke = Brushes.Black;
                        figure.ObjFig.StrokeThickness = 2;
                        bool g = figure.IsSelected;//с этим над что то придумать, по другому вызывать метод   нужно, чтобы менялось своойство фигуры
                        CheckMoves(figure);
                        return;
                    }
                    else if (figure.IsSelected && ListCells[CheckMove(col, row)].Stroke != null)//передвигаем выделенную фигуру
                    {
                        if (IsMoveWhite) IsMoveWhite = false;
                        else IsMoveWhite = true;
                        boardGrid.Children.Remove(figure.ObjFig);
                        figure.Cell = NameCell(col, row);
                        Point p = CellSearch(figure.Cell);
                        Grid.SetColumn(figure.ObjFig, (int)p.X);
                        Grid.SetRow(figure.ObjFig, (int)p.Y);
                        boardGrid.Children.Add(figure.ObjFig);
                        figure.ObjFig.Stroke = null;
                        OnGameEvent(figure);
                        CleanAvMoves();
                    }
                }
            }
        }
        private static int CheckMove(int col, int row)//индекс выбранной ячейки
        {
            for (int i = 0; i < ListCells.Count; i++)
            {
                if (ListCells[i].Name == NameCell(col, row)) return i;
            }
            return -1;
        }
        private static void CheckMoves(Figure figure)//убираем из доступных ходов запрещенные ходы
        {
            if (figure is Rook || figure is Queen || figure is Pawn)
            {
                string[] fig = new string[4];
                bool[] color = new bool[4];
                foreach (Figure f in ListOfFigures)
                {
                    if (figure.ListAvailableMoves.Contains(f.Cell))
                    {
                        if (f.Cell[0] == figure.Cell[0] && f.Cell[1] != figure.Cell[1])
                        {
                            if (Convert.ToInt32(figure.Cell[1].ToString()) > Convert.ToInt32(f.Cell[1].ToString()))
                            {
                                string e = f.Cell;
                                if (fig[0] == null)
                                {
                                    fig[0] = e;
                                    if (f.IsWhite == figure.IsWhite) color[0] = true;
                                    else color[0] = false;
                                }
                                else if (Convert.ToInt32(e[1].ToString()) > Convert.ToInt32(fig[0][1].ToString()))
                                {
                                    fig[0] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[0] = true;
                                    else color[0] = false;
                                }
                            }
                            else if (Convert.ToInt32(figure.Cell[1].ToString()) < Convert.ToInt32(f.Cell[1].ToString()))
                            {
                                string e = f.Cell;
                                if (fig[1] == null)
                                {
                                    fig[1] = e;
                                    if (f.IsWhite == figure.IsWhite) color[1] = true;
                                    else color[1] = false;
                                }
                                else if (Convert.ToInt32(e[1].ToString()) < Convert.ToInt32(fig[1][1].ToString()))
                                {
                                    fig[1] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[1] = true;
                                    else color[1] = false;
                                }
                            }
                        }
                        else if (f.Cell[1] == figure.Cell[1] && f.Cell[0] != figure.Cell[0])
                        {
                            if (f.Cell[0].CompareTo(figure.Cell[0]) < 0)
                            {
                                string e = f.Cell;
                                if (fig[2] == null)
                                {
                                    fig[2] = e;
                                    if (f.IsWhite == figure.IsWhite) color[2] = true;
                                    else color[2] = false;
                                }
                                else if (e[0].CompareTo(fig[2][0]) > 0)
                                {
                                    fig[2] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[2] = true;
                                    else color[2] = false;
                                }
                            }
                            else if (f.Cell[0].CompareTo(figure.Cell[0]) > 0)
                            {
                                string e = f.Cell;
                                if (fig[3] == null)
                                {
                                    fig[3] = e;
                                    if (f.IsWhite == figure.IsWhite) color[3] = true;
                                    else color[3] = false;
                                }
                                else if (e[0].CompareTo(fig[3][0]) < 0)
                                {
                                    fig[3] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[3] = true;
                                    else color[3] = false;
                                }
                            }
                        }
                    }
                }
                if (fig[0] != null)
                {
                    int k = 0;
                    if (color[0]) k = Convert.ToInt32(fig[0][1].ToString());
                    else k = Convert.ToInt32(fig[0][1].ToString()) - 1;
                    while (k > 0)
                    {
                        figure.ListAvailableMoves.Remove(fig[0][0].ToString() + k.ToString());
                        k--;
                    }
                }
                if (fig[1] != null)
                {
                    int k = 0;
                    if (color[1]) k = Convert.ToInt32(fig[1][1].ToString());
                    else k = Convert.ToInt32(fig[1][1].ToString()) + 1;
                    while (k < 9)
                    {
                        figure.ListAvailableMoves.Remove(fig[1][0].ToString() + k.ToString());
                        k++;
                    }
                }
                string str = "ABCDEFGH";
                if (fig[2] != null)
                {
                    int ind = 0;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == fig[2][0]) { ind = i; break; }
                    }
                    int k = 0, m;
                    if (color[2]) m = ind + 1;
                    else m = ind;
                    while (k < m)
                    {
                        figure.ListAvailableMoves.Remove(str[k] + fig[2][1].ToString());
                        k++;
                    }
                }
                if (fig[3] != null)
                {
                    int ind = 0;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == fig[3][0]) { ind = i; break; }
                    }
                    int k = 0;
                    if (color[3]) k = ind;
                    else k = ind + 1;
                    while (k < str.Length)
                    {
                        figure.ListAvailableMoves.Remove(str[k] + fig[3][1].ToString());
                        k++;
                    }
                }
            }
            if (figure is Bishop || figure is Queen)
            {
                string[] fig = new string[4];
                bool[] color = new bool[4];
                foreach (Figure f in ListOfFigures)
                {
                    if (figure.ListAvailableMoves.Contains(f.Cell))
                    {
                        if (f.Cell != figure.Cell && Convert.ToInt32(figure.Cell[1].ToString()) > Convert.ToInt32(f.Cell[1].ToString()))
                        {
                            if (figure.Cell[0].CompareTo(f.Cell[0]) < 0)
                            {
                                string e = f.Cell;
                                if (fig[0] == null)
                                {
                                    fig[0] = e;
                                    if (f.IsWhite == figure.IsWhite) color[0] = true;
                                    else color[0] = false;
                                }
                                else if (Convert.ToInt32(e[1].ToString()) > Convert.ToInt32(fig[0][1].ToString()) && e[0].CompareTo(fig[0][0]) < 0)
                                {
                                    fig[0] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[0] = true;
                                    else color[0] = false;
                                }
                            }
                            else if (figure.Cell[0].CompareTo(f.Cell[0]) > 0)
                            {
                                string e = f.Cell;
                                if (fig[1] == null)
                                {
                                    fig[1] = e;
                                    if (f.IsWhite == figure.IsWhite) color[1] = true;
                                    else color[1] = false;
                                }
                                else if (Convert.ToInt32(e[1].ToString()) > Convert.ToInt32(fig[1][1].ToString()) && e[0].CompareTo(fig[1][0]) > 0)
                                {
                                    fig[1] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[1] = true;
                                    else color[1] = false;
                                }
                            }
                        }
                        else if (f.Cell[0] != figure.Cell[0] && Convert.ToInt32(figure.Cell[1].ToString()) < Convert.ToInt32(f.Cell[1].ToString()))
                        {
                            if (f.Cell[0].CompareTo(figure.Cell[0]) > 0)
                            {
                                string e = f.Cell;
                                if (fig[2] == null)
                                {
                                    fig[2] = e;
                                    if (f.IsWhite == figure.IsWhite) color[2] = true;
                                    else color[2] = false;
                                }
                                else if (Convert.ToInt32(e[1].ToString()) < Convert.ToInt32(fig[2][1].ToString()) && e[0].CompareTo(fig[2][0]) < 0)
                                {
                                    fig[2] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[2] = true;
                                    else color[2] = false;
                                }
                            }
                            else if (f.Cell[0].CompareTo(figure.Cell[0]) < 0)
                            {
                                string e = f.Cell;
                                if (fig[3] == null)
                                {
                                    fig[3] = e;
                                    if (f.IsWhite == figure.IsWhite) color[3] = true;
                                    else color[3] = false;
                                }
                                else if (Convert.ToInt32(e[1].ToString()) < Convert.ToInt32(fig[3][1].ToString()) && e[0].CompareTo(fig[3][0]) > 0)
                                {
                                    fig[3] = f.Cell;
                                    if (f.IsWhite == figure.IsWhite) color[3] = true;
                                    else color[3] = false;
                                }
                            }
                        }
                    }
                }
                if (fig[0] != null)
                {
                    bool d = true;
                    for (int j = 0; j < figure.ListAvailableMoves.Count; j++)
                    {
                        if (figure.ListAvailableMoves[j][0].CompareTo(fig[0][0]) >= 0 && Convert.ToInt32(figure.ListAvailableMoves[j][1].ToString()) < Convert.ToInt32(figure.Cell[1].ToString()))
                        {
                            if (!color[0] && d && fig[0] == figure.ListAvailableMoves[j]) { d = false; continue; }
                            figure.ListAvailableMoves.Remove(figure.ListAvailableMoves[j]);
                            j = j - 1;
                        }
                    }
                }
                if (fig[1] != null)
                {
                    bool d = true;
                    for (int j = 0; j < figure.ListAvailableMoves.Count; j++)
                    {
                        if (figure.ListAvailableMoves[j][0].CompareTo(fig[1][0]) <= 0 && Convert.ToInt32(figure.ListAvailableMoves[j][1].ToString()) < Convert.ToInt32(figure.Cell[1].ToString()))
                        {
                            if (!color[1] && d && fig[1] == figure.ListAvailableMoves[j]) { d = false; continue; }
                            figure.ListAvailableMoves.Remove(figure.ListAvailableMoves[j]);
                            j = j - 1;
                        }
                    }
                }
                if (fig[2] != null)
                {
                    bool d = true;
                    for (int j = 0; j < figure.ListAvailableMoves.Count; j++)
                    {
                        if (figure.ListAvailableMoves[j][0].CompareTo(fig[2][0]) >= 0 && Convert.ToInt32(figure.ListAvailableMoves[j][1].ToString()) > Convert.ToInt32(figure.Cell[1].ToString()))
                        {
                            if (!color[2] && d && fig[2] == figure.ListAvailableMoves[j]) { d = false; continue; }
                            figure.ListAvailableMoves.Remove(figure.ListAvailableMoves[j]);
                            j = j - 1;
                        }
                    }
                }
                if (fig[3] != null)
                {
                    bool d = true;
                    for (int j = 0; j < figure.ListAvailableMoves.Count; j++)
                    {
                        if (figure.ListAvailableMoves[j][0].CompareTo(fig[3][0]) <= 0 && Convert.ToInt32(figure.ListAvailableMoves[j][1].ToString()) > Convert.ToInt32(figure.Cell[1].ToString()))
                        {
                            if (!color[3] && d && fig[3] == figure.ListAvailableMoves[j]) { d = false; continue; }
                            figure.ListAvailableMoves.Remove(figure.ListAvailableMoves[j]);
                            j = j - 1;
                        }
                    }
                }
            }
            if (figure is Pawn || figure is King || figure is Queen || figure is Knight)
            {
                foreach (Figure f in ListOfFigures)
                {
                    if (figure.ListAvailableMoves.Contains(f.Cell))
                    {
                        for (int i = 0; i < figure.ListAvailableMoves.Count; i++)
                        {
                            if (figure.IsWhite != f.IsWhite && !(figure is Pawn)) continue;
                            else if (figure.ListAvailableMoves[i] == f.Cell) figure.ListAvailableMoves.Remove(f.Cell);
                        }
                    }
                    else if (figure is Pawn && f.IsWhite != figure.IsWhite)
                    {
                        if (figure.IsWhite)
                        {
                            if (Math.Abs(Grid.GetColumn(figure.ObjFig) - Grid.GetColumn(f.ObjFig)) == 1 && Grid.GetRow(f.ObjFig) - Grid.GetRow(figure.ObjFig) == 1)
                            {
                                figure.ListAvailableMoves.Add(f.Cell);
                            }
                        }
                        else if (Math.Abs(Grid.GetColumn(figure.ObjFig) - Grid.GetColumn(f.ObjFig)) == 1 && Grid.GetRow(figure.ObjFig) - Grid.GetRow(f.ObjFig) == 1)
                        {
                            figure.ListAvailableMoves.Add(f.Cell);
                        }
                    }
                }
            }
            foreach (string move in figure.ListAvailableMoves)
            {
                foreach (Rectangle cell in ListCells)
                {
                    if (move == cell.Name)
                    {
                        cell.Stroke = Brushes.Red;
                        cell.StrokeThickness = visMoves;
                    }
                }
            }
        }
        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            ListOfFigures.Add(new Rook("A8", true));
            ListOfFigures.Add(new Knight("B8", true));
            ListOfFigures.Add(new Bishop("C8", true));
            ListOfFigures.Add(new Queen("E8", true));
            ListOfFigures.Add(new King("D8", true));
            ListOfFigures.Add(new Bishop("F8", true));
            ListOfFigures.Add(new Knight("G8", true));
            ListOfFigures.Add(new Rook("H8", true));
            ListOfFigures.Add(new Rook("A1", false));
            ListOfFigures.Add(new Knight("B1", false));
            ListOfFigures.Add(new Bishop("C1", false));
            ListOfFigures.Add(new Queen("E1", false));
            ListOfFigures.Add(new King("D1", false));
            ListOfFigures.Add(new Bishop("F1", false));
            ListOfFigures.Add(new Knight("G1", false));
            ListOfFigures.Add(new Rook("H1", false));
            bool isWhite = true;
            int r = 7;
            for (int i = 0; i < 2; i++)
            {
                foreach (char ch in new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' })
                {
                    Pawn pawn = new Pawn(ch.ToString() + (r).ToString(), isWhite);
                    ListOfFigures.Add(pawn);
                    pawn.PawnChangeEvent += win.PawnChangeChallenge;
                }
                isWhite = false;
                r = 2;
            }
            int id = 0;
            foreach (Figure figure in ListOfFigures)
            {
                figure.Id = id;
                OnGameEvent(figure);
                Point p = CellSearch(figure.Cell);
                Grid.SetColumn(figure.ObjFig, (int)p.X);
                Grid.SetRow(figure.ObjFig, (int)p.Y);
                boardGrid.Children.Add(figure.ObjFig);
                HistoryOfGame.MoveCancelEvent += MoveCancel;
                id++;
            }
        }
        private void ContinuationGame()
        {
            foreach (Figure figure in ListOfFigures)
            {
                OnGameEvent(figure);
                Point p = CellSearch(figure.Cell);
                Grid.SetColumn(figure.ObjFig, (int)p.X);
                Grid.SetRow(figure.ObjFig, (int)p.Y);
                boardGrid.Children.Add(figure.ObjFig);
            }
            HistoryOfGame.MoveCancelEvent += MoveCancel;
        }
        private void MoveCancel(string cell, string name, string lastMove)
        {
            string id = name.Split('.')[1];
            for (int i = 0; i < ListOfFigures.Count; i++)
            {
                Figure figure = ListOfFigures[i];
                if (figure.Cell == cell && id == figure.Id.ToString())
                {
                    if (ListOfDeleteFigures.Count != 0)
                    {
                        for (int j = 0; j < ListOfDeleteFigures.Count; j++)
                        {
                            Figure fig = ListOfDeleteFigures[j];
                            if (fig.Cell == cell)
                            {
                                ListOfFigures.Add(fig);
                                ListOfDeleteFigures.Remove(fig);
                                Point e = CellSearch(fig.Cell);
                                Grid.SetColumn(fig.ObjFig, (int)e.X);
                                Grid.SetRow(fig.ObjFig, (int)e.Y);
                                boardGrid.Children.Add(fig.ObjFig);
                            }
                        }
                    }
                    boardGrid.Children.Remove(figure.ObjFig);
                    figure.Cell = lastMove;
                    Point p = CellSearch(figure.Cell);
                    Grid.SetColumn(figure.ObjFig, (int)p.X);
                    Grid.SetRow(figure.ObjFig, (int)p.Y);
                    boardGrid.Children.Add(figure.ObjFig);
                    if (isMoveWhite) isMoveWhite = false;
                    else isMoveWhite = true;
                }
            }
        }
        public static Point CellSearch(string cell)
        {
            Point point = new Point();
            for (int i = 0; i < ListCells.Count; i++)//через ячейки
            {
                if (cell == ListCells[i].Name)
                {
                    return point = new Point(Grid.GetColumn(ListCells[i]), Grid.GetRow(ListCells[i]));
                }
            }
            return point;
        }
        private static string NameCell(int col, int row)
        {
            string cell = "";
            char[] vs = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            for (int i = 0; i < vs.Length; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (i == col && j == row) cell = vs[i] + (8 - j).ToString();
                }
            }
            return cell;
        }
        private void Cells()
        {
            bool isFirst = true;
            for (int i = 0; i < boardGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = boardGrid.RowDefinitions.Count - 1; j >= 0; j--)
                {
                    if (j == boardGrid.RowDefinitions.Count - 1) { if (isFirst) isFirst = false; else isFirst = true; }
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 50;
                    rectangle.Height = 50;
                    rectangle.Name = NameCell(i, j);
                    if (isFirst) { rectangle.Fill = new SolidColorBrush(Color.FromRgb(186, 85, 54)); isFirst = false; }
                    else { rectangle.Fill = new SolidColorBrush(Color.FromRgb(70, 33, 26)); isFirst = true; }
                    ListCells.Add(rectangle);
                    Grid.SetColumn(rectangle, i);
                    Grid.SetRow(rectangle, j);
                    boardGrid.Children.Add(rectangle);
                }
            }
            boardGrid.MouseLeftButtonDown += board_MouseLeftButtonDown;
        }
    }
}
