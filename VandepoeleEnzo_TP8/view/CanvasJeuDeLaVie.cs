using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VandepoeleEnzo_TP8.model;

namespace VandepoeleEnzo_TP8.view
{
    public class CanvasJeuDeLaVie : Canvas
    {
        private JeuDeLaVie jeuDeLaVie = new JeuDeLaVie();

        public JeuDeLaVie JeuDeLaVie => jeuDeLaVie;
    
        private void DrawGridLine(double X1, double Y1, double X2, double Y2)
        {
            Line line = new Line();
            line.X1 = X1;
            line.Y1 = Y1;
            line.X2 = X2;
            line.Y2 = Y2;
            line.Stroke = Brushes.LightGray;
            this.Children.Add(line);
        }

        private void DrawGrid()
        {
            double height = this.ActualHeight / jeuDeLaVie.nbRow;
            double width = this.ActualWidth / jeuDeLaVie.nbCol;

            for (double i = 0; i <= jeuDeLaVie.nbRow; i++)
            {
                DrawGridLine(0, i * height, this.ActualWidth, i * height);
            }

            for (double i = 0; i <= jeuDeLaVie.nbCol; i++)
            {
                DrawGridLine(i * width, 0, i * width, this.ActualHeight);
            }
        }

        private void DrawRect(int X, int Y, int width, int height, Color color)
        {
            SolidColorBrush colorPen = new SolidColorBrush(color);
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = colorPen;
            rect.Fill = colorPen;
            rect.Width = width - 2;
            rect.Height = height - 2;
            Canvas.SetLeft(rect, X + 1);
            Canvas.SetTop(rect, Y + 1);
            this.Children.Add(rect);
        }

        private void Clear()
        {
            this.Children.Clear();
            DrawGrid();
        }

        public void DisplayNextGeneration()
        {
            List<Point> listAliveCell = this.jeuDeLaVie.getListAliveCellNextGeneration();
            int width = (int)this.ActualWidth / jeuDeLaVie.nbCol;
            int height = (int)this.ActualHeight / jeuDeLaVie.nbRow;

            foreach (Point item in listAliveCell)
            {
                DrawRect((int)item.X * width, (int)item.Y * height, width, height, Colors.Black);
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            DrawGrid();
            //DrawRect(0, 0, 50, 50, Colors.Black);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);
            int width = (int)this.ActualWidth / jeuDeLaVie.nbCol;
            int height = (int)this.ActualHeight / jeuDeLaVie.nbRow;
            int cellX = (int)position.X / width;
            int cellY = (int)position.Y / height;

            bool state = jeuDeLaVie.changeCellState(cellX, cellY);

            if (state) DrawRect(cellX * width, cellY * height, width, height, Colors.Black);
            else DrawRect(cellX * width, cellY * height, width, height, Colors.White);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.N) DisplayNextGeneration();
        }
    }
}
