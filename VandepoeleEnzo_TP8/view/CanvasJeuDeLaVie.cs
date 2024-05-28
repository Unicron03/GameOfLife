using System.Windows;
using System.Windows.Controls;
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

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            DrawGridLine(0, 0, 100, 100);
        }
    }
}
