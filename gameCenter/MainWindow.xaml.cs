using gameCenter.Projects;
using gameCenter.Projects.BlackJack;
using gameCenter.Projects.CarGame;
using gameCenter.Projects.CurrencyConvertorView;
using gameCenter.Projects.DrawingApp;
using gameCenter.Projects.Project1;
using gameCenter.Projects.TicTacToe;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace gameCenter
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            DateLabel.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            DispatcherTimer clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clock.Tick += Tick!;
            clock.Start();

        }

        private void Tick(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.7;
            GameText.Content = (image.Name) switch
            {
                "Image1" => "User Management System",
                "Image2" => "Tic Tac Toe",
                "Image3" => "Currency Convertor",
                "Image4" => "Car Game",
                "Image5" => "Paint",
                "Image6" => "BlackJack",
                _ => "please pick an app"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "please pick a game";
        }

        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            Project1 project = new();
            page.OnStartUp("bla bla bla", Image1.Source, project,"Users Manager");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            TicTacToe project = new();
            page.OnStartUp("bla bla bla", Image2.Source, project,"Tic Tac Toe");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            CurrencyConvertorView project = new();
            page.OnStartUp("bla bla bla", Image3.Source, project, "Currency Convertor");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            CarGame project = new();
            page.OnStartUp("bla bla bla", Image4.Source, project, "Car Game");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            DrawingApp project = new();
            page.OnStartUp("bla bla bla", Image5.Source, project, "Paint");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            BlackjackGame project = new();
            page.OnStartUp("bla bla bla", Image6.Source, project, "BlackJack");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }
    }
}
