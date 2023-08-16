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
    /// Логика взаимодействия для PawnChange.xaml
    /// </summary>
    public partial class PawnChange : Page
    {
        private Pawn pawn;
        private Win win;
        public PawnChange(Pawn p, Win wind)
        {
            pawn = p;
            win = wind;
            InitializeComponent();
        }

        private void buttonQueen_Click(object sender, RoutedEventArgs e)
        {
            Queen queen = new Queen(pawn.Cell, pawn.IsWhite);
            queen.Id = pawn.Id;
            win.board.Children.Remove(pawn.ObjFig);
            ChessBoard.ListOfFigures.Remove(pawn);
            ChessBoard.ListOfFigures.Add(queen);
            Point p = ChessBoard.CellSearch(queen.Cell);
            Grid.SetColumn(queen.ObjFig, (int)p.X);
            Grid.SetRow(queen.ObjFig, (int)p.Y);
            win.board.Children.Add(queen.ObjFig);
            NavigationService.Navigate(win);
        }

        private void buttonRook_Click(object sender, RoutedEventArgs e)
        {
            Rook rook = new Rook(pawn.Cell, pawn.IsWhite);
            rook.Id = pawn.Id;
            win.board.Children.Remove(pawn.ObjFig);
            ChessBoard.ListOfFigures.Remove(pawn);
            ChessBoard.ListOfFigures.Add(rook);
            Point p = ChessBoard.CellSearch(rook.Cell);
            Grid.SetColumn(rook.ObjFig, (int)p.X);
            Grid.SetRow(rook.ObjFig, (int)p.Y);
            win.board.Children.Add(rook.ObjFig);
            NavigationService.Navigate(win);
        }

        private void buttonBishop_Click(object sender, RoutedEventArgs e)
        {
            Bishop bishop = new Bishop(pawn.Cell, pawn.IsWhite);
            bishop.Id = pawn.Id;
            win.board.Children.Remove(pawn.ObjFig);
            ChessBoard.ListOfFigures.Remove(pawn);
            ChessBoard.ListOfFigures.Add(bishop);
            Point p = ChessBoard.CellSearch(bishop.Cell);
            Grid.SetColumn(bishop.ObjFig, (int)p.X);
            Grid.SetRow(bishop.ObjFig, (int)p.Y);
            win.board.Children.Add(bishop.ObjFig);
            NavigationService.Navigate(win);
        }

        private void buttonKnight_Click(object sender, RoutedEventArgs e)
        {
            Knight knight = new Knight(pawn.Cell, pawn.IsWhite);
            knight.Id = pawn.Id;
            win.board.Children.Remove(pawn.ObjFig);
            ChessBoard.ListOfFigures.Remove(pawn);
            ChessBoard.ListOfFigures.Add(knight);
            Point p = ChessBoard.CellSearch(knight.Cell);
            Grid.SetColumn(knight.ObjFig, (int)p.X);
            Grid.SetRow(knight.ObjFig, (int)p.Y);
            win.board.Children.Add(knight.ObjFig);
            NavigationService.Navigate(win);
        }
    }
}
