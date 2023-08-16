using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Security.RightsManagement;

namespace Chess
{
    public abstract class Figure
    {
        public virtual string Cell { get; set; }
        public bool IsWhite { get; set; }
        public string Name { get; private set; }
        public int Id { get; set; }
        public bool IsSelected
        {
            get
            {
                if (ObjFig.Stroke == Brushes.Black) { AvailableMoves(); return true; }
                else return false;
            }
        }
        public Rectangle ObjFig { get; set; }
        public ObservableCollection<string> ListAvailableMoves { get { return listAvailableMoves; } }
        protected ObservableCollection<string> listAvailableMoves = new ObservableCollection<string>();
        protected ImageBrush image = new ImageBrush();
        protected static string Cols = "ABCDEFGH";
        protected abstract void AvailableMoves();
        protected Figure(string cell, bool isWhite)
        {
            Cell = cell;
            IsWhite = isWhite;
            ObjFig = new Rectangle();
            Name = NameFig(this);
        }
        protected int ColumnSearch(char col)//возвращаем индекс буквы 
        {
            for (int i = 0; i < Cols.Length; i++)
            {
                if (Cols[i] == col) return i;
            }
            return -1;
        }
        protected void RemoveAvMove() //удаляем доступный ход на фигуре
        {
            for (int i = 0; i < listAvailableMoves.Count; i++)
            {
                if (listAvailableMoves[i].Equals(Cell)) { listAvailableMoves.RemoveAt(i); }
            }
        }
        private static string NameFig(Figure figure)
        {
            if (figure is Rook) return "ладья";
            else if (figure is Knight) return "конь";
            else if (figure is Queen) return "ферзь";
            else if (figure is Pawn) return "пешка";
            else if (figure is Bishop) return "слон";
            else if (figure is King) return "король";
            else return " ";
        }
    }
    public class Rook : Figure//ладья
    {
        public Rook(string cell, bool isWhite) : base(cell, isWhite)
        {
            if (IsWhite)
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\WRook.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\BRook.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
        }
        protected override void AvailableMoves()
        {
            if (listAvailableMoves.Count != 0) listAvailableMoves.Clear();

            int col = 0, row = 0;
            while (row < 8)
            {
                row++;
                listAvailableMoves.Add(Cell[0] + row.ToString());
            }
            while (col < Cols.Length)
            {
                listAvailableMoves.Add(Cols[col].ToString() + Cell[1]);
                col++;
            }
            RemoveAvMove();
        }
    }
    public class Knight : Figure//конь
    {
        public Knight(string cell, bool isWhite) : base(cell, isWhite)
        {
            if (IsWhite)
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\WKnight.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\BKnight.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
        }
        protected override void AvailableMoves()
        {
            if (listAvailableMoves.Count != 0) listAvailableMoves.Clear();
            int r = Convert.ToInt32(Cell.Remove(0, 1));
            int col = ColumnSearch(Cell[0]), row = r - 2;
            while (row <= r + 2)
            {
                if (r == row) { row++; continue; }
                if (row == r - 2 && row > 0 || row == r + 2 && row <= 8)
                {
                    if (col - 1 >= 0)
                    {
                        listAvailableMoves.Add(Cols[col - 1].ToString() + row);
                    }
                    if (col + 1 < 8)
                    {
                        listAvailableMoves.Add(Cols[col + 1].ToString() + row);
                    }
                    row++;
                    continue;
                }
                if (row == r - 1 && row > 0 || row == r + 1 && row <= 8)
                {
                    if (col - 2 >= 0)
                    {
                        listAvailableMoves.Add(Cols[col - 2].ToString() + row);
                    }
                    if (col + 2 < 8)
                    {
                        listAvailableMoves.Add(Cols[col + 2].ToString() + row);
                    }
                    row++;
                    continue;
                }
                row++;
            }
        }
    }
    public class Queen : Figure
    {
        public Queen(string cell, bool isWhite) : base(cell, isWhite)
        {
            if (IsWhite)
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\WQueen.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\BQueen.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
        }
        protected override void AvailableMoves()
        {
            if (listAvailableMoves.Count != 0) listAvailableMoves.Clear();
            int r = Convert.ToInt32(Cell.Remove(0, 1));
            int col = ColumnSearch(Cell[0]), row = r - 8;
            while (row <= r + 8)
            {
                if (row == r)
                {
                    row++; continue;
                }
                int cl = col - Math.Abs(r - row);
                int cr = col + Math.Abs(r - row);
                if (row > 8) break;
                while (row < 0) row++;
                if (cl >= 0 && cl < 7)
                {
                    listAvailableMoves.Add(Cols[cl].ToString() + row);
                }
                if (cr > 0 && cr < 8)
                {
                    listAvailableMoves.Add(Cols[cr].ToString() + row);
                }
                row++;
            }
            col = 0; row = 0;
            while (row < 8)
            {
                row++;
                listAvailableMoves.Add(Cell[0] + row.ToString());
            }
            while (col < Cols.Length)
            {
                listAvailableMoves.Add(Cols[col].ToString() + Cell[1]);
                col++;
            }
            RemoveAvMove();
        }
    }
    public class Pawn : Figure//пешка
    {
        public delegate void PawnChangeDelegate(Pawn p);
        public event PawnChangeDelegate PawnChangeEvent;
        private void OnPawnChangeEvent(Pawn p)
        {
            if (PawnChangeEvent != null)
                PawnChangeEvent(p);
        }
        public override string Cell
        {
            get => base.Cell;
            set
            {
                base.Cell = value;
                if (Convert.ToInt32(value[1].ToString()) == 8 || Convert.ToInt32(value[1].ToString()) == 1)
                {
                    OnPawnChangeEvent(this);
                }
            }
        }
        public Pawn(string cell, bool isWhite) : base(cell, isWhite)
        {
            if (IsWhite)
            {
                image.ImageSource = new BitmapImage(new Uri(@"Resources\WPawn.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(@"Resources\BPawn.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
        }
        protected override void AvailableMoves()
        {
            if (listAvailableMoves.Count != 0) listAvailableMoves.Clear();
            int row = Convert.ToInt32(Cell.Remove(0, 1));
            if (IsWhite)
            {
                char startingCell='7';
                if (row + 1 > 8) return;
                if (startingCell == Cell[1])
                {
                    listAvailableMoves.Add(Cell[0] + (row - 1).ToString());
                    listAvailableMoves.Add(Cell[0] + (row - 2).ToString());
                }
                else { listAvailableMoves.Add(Cell[0] + (row - 1).ToString()); }
            }
            else
            {
                char startingCell = '2';
                if (row + 1 > 8) return;
                if (startingCell == Cell[1])
                {
                    listAvailableMoves.Add(Cell[0] + (row + 1).ToString());
                    listAvailableMoves.Add(Cell[0] + (row + 2).ToString());
                }
                else { listAvailableMoves.Add(Cell[0] + (row + 1).ToString()); }
            }
            RemoveAvMove();
        }
    }
    public class Bishop : Figure//слон
    {
        public Bishop(string cell, bool isWhite) : base(cell, isWhite)
        {
            if (IsWhite)
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\WBishop.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\BBishop.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
        }
        protected override void AvailableMoves()
        {
            if (listAvailableMoves.Count != 0) listAvailableMoves.Clear();
            int r = Convert.ToInt32(Cell.Remove(0, 1));
            int col = ColumnSearch(Cell[0]), row = r - 8;
            while (row <= r + 8)
            {
                if (row == r) { row++; continue; }
                int cl = col - Math.Abs(r - row);
                int cr = col + Math.Abs(r - row);
                if (row > 8) return;
                while (row < 0) row++;
                if (cl >= 0 && cl < 7)
                {
                    listAvailableMoves.Add(Cols[cl].ToString() + row);
                }
                if (cr > 0 && cr < 8)
                {
                    listAvailableMoves.Add(Cols[cr].ToString() + row);
                }
                row++;
            }
        }
    }
    public class King : Figure
    {
        public King(string cell, bool isWhite) : base(cell, isWhite)
        {
            if (IsWhite)
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\WKing.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
            else
            {
                image.ImageSource = new BitmapImage(new Uri(@"resources\BKing.png", UriKind.Relative));
                ObjFig.Fill = image;
            }
        }
        protected override void AvailableMoves()
        {
            if (listAvailableMoves.Count != 0) listAvailableMoves.Clear();
            int r = Convert.ToInt32(Cell.Remove(0, 1));
            int col = ColumnSearch(Cell[0]) - 1, row = r - 1;
            int c = col + 2;
            while (row <= r + 1)
            {
                int cl = col;
                while (cl <= c)
                {
                    if (row > 0 && row <= 8 && col >= 0 && col < 8)
                    {
                        listAvailableMoves.Add(Cols[cl] + row.ToString());
                    }
                    cl++;
                }
                row++;
            }
            RemoveAvMove();
        }
    }
}
