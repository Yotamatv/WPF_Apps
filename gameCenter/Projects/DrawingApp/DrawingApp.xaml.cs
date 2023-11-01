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

namespace gameCenter.Projects.DrawingApp
{
  
    public partial class DrawingApp : Window
    {
        private bool isDrawing = false;
        private Point startPoint;
        private Brush currentBrush = Brushes.Black;
        private double penThickness = 2.0;
        public DrawingApp()
        {
            InitializeComponent();
        }
        private void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(drawingCanvas);
        }
        private void StopDrawing(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }
        private void Draw(object sender, MouseEventArgs e)
        {
            // This function is called when the mouse is moved while drawing. It updates the brush size label
            // and, if drawing is active, creates and adds Line objects to the drawingCanvas to create the drawing.
            Size.Text = "Brush Size " + penThickness.ToString();
            if (isDrawing)
            {
                Line line = new Line
                {
                    Stroke = currentBrush,
                    StrokeThickness = penThickness,
                    X1 = startPoint.X,
                    Y1 = startPoint.Y,
                    X2 = e.GetPosition(drawingCanvas).X,
                    Y2 = e.GetPosition(drawingCanvas).Y,
                };

                drawingCanvas.Children.Add(line);
                startPoint = e.GetPosition(drawingCanvas);
            }
        }

        //following functions set predetermined brush parameters
        private void SelectPenTool(object sender, RoutedEventArgs e)
        {
            currentBrush = Brushes.Black;
            penThickness = 2.0;
            Size.Text = "Brush Size " + penThickness.ToString();
        }

        private void SelectBrushTool(object sender, RoutedEventArgs e)
        {
            currentBrush = Brushes.Black;
            penThickness = 5.0;
            Size.Text = "Brush Size " + penThickness.ToString();
        }

        private void SelectEraserTool(object sender, RoutedEventArgs e)
        {
            penThickness = 15.0;
            currentBrush = Brushes.White;
            Size.Text = "Brush Size " + penThickness.ToString();
        }

        private void SelectColor(object sender, RoutedEventArgs e)
        {
            //show color pannel
            colorPanel.Visibility = Visibility.Visible;
        }

        private void ClearScreen(object sender, RoutedEventArgs e)
        {
            //clears canvas
            drawingCanvas.Children.Clear();
        }

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            // changes the current brush color and collapses the pannel
            if (sender is Button button)
            {
                string color = button.Name;
                switch (color)
                {
                    case "Blue":
                        {
                            currentBrush = Brushes.Blue;
                            colorPanel.Visibility = Visibility.Collapsed;

                            break;
                        }
                    case "Black":
                        {
                            currentBrush = Brushes.Black;
                            colorPanel.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case "Red":
                        {
                            currentBrush = Brushes.Red;
                            colorPanel.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case "Green":
                        {
                            currentBrush = Brushes.Green;
                            colorPanel.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case "Yellow":
                        {
                            currentBrush = Brushes.Yellow;
                            colorPanel.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case "Orange":
                        {
                            currentBrush = Brushes.Orange;
                            colorPanel.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case "Purple":
                        {
                            currentBrush = Brushes.Purple;
                            colorPanel.Visibility = Visibility.Collapsed;
                            break;
                        }
                }
            }
            
        }

        private void ChangeSize(object sender, RoutedEventArgs e)
        {
            //manualy adjust brush size
            if (sender is Button button)
            {
                string sign=button.Content as string;
                switch (sign)
                {
                    case "+":
                        {
                            penThickness++;
                            Size.Text = "Brush Size " + penThickness.ToString();
                            break;
                        }
                    case "-":
                        {
                            penThickness= penThickness > 1 ? --penThickness:1;
                            Size.Text = "Brush Size " + penThickness.ToString();
                            break;
                        }
                }
            }
        }
    }
}
