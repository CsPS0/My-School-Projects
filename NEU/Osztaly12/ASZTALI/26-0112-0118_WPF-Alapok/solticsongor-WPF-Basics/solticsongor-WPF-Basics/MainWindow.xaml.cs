using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace solticsongor_WPF_Basics
{
    public partial class MainWindow : Window
    {
        private Polyline? currentPolyline;
        private Point lastPoint;
        private bool isDrawing = false;
        private bool isDrawingEnabled = false;
        private double hue = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PencilDown_Click(object sender, RoutedEventArgs e)
        {
            isDrawingEnabled = true;
            PencilDownBtn.IsEnabled = false;
            PencilUpBtn.IsEnabled = true;
        }

        private void PencilUp_Click(object sender, RoutedEventArgs e)
        {
            isDrawingEnabled = false;
            PencilDownBtn.IsEnabled = true;
            PencilUpBtn.IsEnabled = false;
            currentPolyline = null;
        }

        private System.Windows.Media.Color HsvToRgb(double h, double s, double v)
        {
            double r, g, b;
            int i = (int)Math.Floor(h / 60);
            double f = (h / 60) - i;
            double p = v * (1 - s);
            double q = v * (1 - s * f);
            double t = v * (1 - s * (1 - f));
            switch (i)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                default: r = v; g = p; b = q; break;
            }
            return System.Windows.Media.Color.FromRgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        private void DrawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isDrawingEnabled || e.ButtonState != MouseButtonState.Pressed) return;

            lastPoint = e.GetPosition(DrawCanvas);
            currentPolyline = new Polyline
            {
                StrokeThickness = 5,
                Stroke = new SolidColorBrush(HsvToRgb(hue % 360, 1, 1))
            };
            currentPolyline.Points.Add(lastPoint);
            currentPolyline.Points.Add(lastPoint);
            DrawCanvas.Children.Add(currentPolyline);
            isDrawing = true;
            hue += 10;
        }

        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || !isDrawingEnabled || e.LeftButton != MouseButtonState.Pressed) return;

            Point newPoint = e.GetPosition(DrawCanvas);
            if (Math.Abs(newPoint.X - lastPoint.X) > 2 || Math.Abs(newPoint.Y - lastPoint.Y) > 2)
            {
                currentPolyline!.Points.Add(lastPoint);
                var segmentColor = HsvToRgb(hue % 360, 1, 1);
                currentPolyline.Stroke = new SolidColorBrush(segmentColor);
                lastPoint = newPoint;
                hue += 15;
            }
        }

        private void DrawCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            currentPolyline = null;
        }
    }
}