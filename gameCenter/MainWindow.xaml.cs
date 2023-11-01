using gameCenter.Projects;
using gameCenter.Projects.BlackJack;
using gameCenter.Projects.Calculator;
using gameCenter.Projects.CarGame;
using gameCenter.Projects.CurrencyConvertorView;
using gameCenter.Projects.DrawingApp;
using gameCenter.Projects.Notepad;
using gameCenter.Projects.Project1;
using gameCenter.Projects.TicTacToe;
using gameCenter.Projects.ToDoList;
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
        // This function is called when the mouse enters an Image control.
        // It sets the opacity of the Image to 0.7 and updates the GameText Content
        // based on the name of the Image control that triggered the event.
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
                "Image7" => "To Do List",
                "Image8" => "Calculator",
                "Image9" => "Note Taking App",
                _ => "please pick an app"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "please pick a game";
        }

        //shuts down the app on close
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //opens a window for each selected prpject
        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            Project1 project = new();
            page.OnStartUp("User Management System designed to keep track of added users and their infornmation. Uses local storage to save added and removed users", Image1.Source, project, "Users Manager");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            TicTacToe project = new();
            page.OnStartUp("This is a 2 player Tic Tac Toe experience", Image2.Source, project, "Tic Tac Toe");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            CurrencyConvertorView project = new();
            page.OnStartUp("Convert all currencies swiftly and accurately using up to date API!", Image3.Source, project, "Currency Convertor");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            CarGame project = new();
            page.OnStartUp("Use the car and escape the bombs, the more bombs you dodge the higher the score!", Image4.Source, project, "Car Game");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            DrawingApp project = new();
            page.OnStartUp("Paint App for artists, options to change brush size and color as well as erasing any mistakes", Image5.Source, project, "Paint");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            BlackjackGame project = new();
            page.OnStartUp("A Game of Blackjack: you against the dealer\nYou are dealt two cards, and can choose to \"hit\" " +
                "to receive additional cards or \"stand\" to keep your current total. Face cards are worth 10 points, numbered cards " +
                "are worth their face value, and aces can be worth 1 or 11 points.\nYou win if your hand is closer to 21 than the dealer's " +
                "without exceeding 21.", Image6.Source, project, "BlackJack");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            ToDoList project = new();
            page.OnStartUp("Add tasks, mark their complition and keep track in the future using local storage", Image7.Source, project, "To Do List");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            Calculator project = new();
            page.OnStartUp("Simple Calculator with options to add, subtract, chaining operators and more!", Image8.Source, project, "Calculator");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }

        private void Image9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProjectPresentation page = new();
            NoteApp project = new();
            page.OnStartUp("Write, search and manage notes, all stored in local storage ", Image9.Source, project, "Note Taking App");
            Hide();
            page.ShowDialog();
            ShowDialog();
            project.Close();
        }
    }
}
