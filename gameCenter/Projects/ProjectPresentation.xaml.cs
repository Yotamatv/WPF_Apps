﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace gameCenter.Projects
{
    public partial class ProjectPresentation : Window
    {
        private Window Project;

        public ProjectPresentation()
        {
            InitializeComponent();
            //adds UTC time clock
            DateLabel.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            DispatcherTimer clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            clock.Tick += Tick!;
            clock.Start();

            Project = new Window();

        }

        public void OnStartUp(string projectInfo, ImageSource imageSource, Window project, string projectName)
        {
            //sets the texts on screen to the project information
            InfoTextBox.Text = projectInfo;
            ProjectImage.Source = imageSource;
            Project = project;
            ProjectTitle.Content = projectName;
        }

        private void Tick(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }


        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
            Project.ShowDialog();
            Project.Close();
        }

    }
}

