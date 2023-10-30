using gameCenter.Projects.CarGame.Objects;
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
using System.Windows.Threading;

namespace gameCenter.Projects.CarGame
{
    /// <summary>
    /// Interaction logic for CarGame.xaml
    /// </summary>
    public partial class CarGame : Window
    {
        private PlayerCar playerCar;
        private List<Obstacle> obstacles;
        private Obstacle obstacle;
        private Random random;
        private Image bomb;
        public CarGame()
        {
            InitializeComponent();
            BackgroudVideo.Source = new Uri("Projects\\CarGame\\Images\\BackgroudVideo.mp4", UriKind.Relative);
            BackgroudVideo.Play();
            playerCar = new PlayerCar(200, 300, 1, playerCarImage);
            obstacles = new List<Obstacle>();
            random = new Random();
            DispatcherTimer gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            playerCar.Draw();
            gameTimer.Start();
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    playerCar.leftPressed = true;
                    break;
                case Key.Right:
                    playerCar.rightPressed = true;
                    break;
            }
        }
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:

                    playerCar.leftPressed = false;
                    break;

                case Key.Right:

                    playerCar.rightPressed = false;
                    break;
            }
        }
        private void GenerateBombs()
        {
            if (random.Next(30) == 1)
            {
                bomb = new Image();
                bomb.Source = new BitmapImage(new Uri("images/Bomb.png", UriKind.RelativeOrAbsolute));
                bomb.Width = 100;
                bomb.Height = 100;
                gameCanvas.Children.Add(bomb);
                obstacles.Add(new Obstacle(random.Next(750), 10, random.Next(5, 10), bomb));
            }

            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Move();
                consoleTextBox.Text = "Score:\n" + obstacles.Count.ToString();
            }

        }
        private void DrawObstacles()
        {
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Draw();
            }
            playerCar.Draw();

        }
        private void TestCollision()
        {
            double playerCarLeft = playerCar.X + 30;
            double playerCarRight = playerCar.X + 85;
            double playerCarTop = playerCar.Y;
            double playerCarBottom = playerCar.Y + 100;

            foreach (Obstacle obstacle in obstacles)
            {
                double obstacleLeft = obstacle.X;
                double obstacleRight = obstacle.X + 80;
                double obstacleTop = obstacle.Y;
                double obstacleBottom = obstacle.Y + 80;

                // Check for collision by comparing bounding boxes
                if (playerCarLeft < obstacleRight && playerCarRight > obstacleLeft &&
                    playerCarTop < obstacleBottom && playerCarBottom > obstacleTop)
                {
                    GameOver();
                    break;
                }


            }
        }
        private void GameOver()
        {
            consoleTextBox.Text = "Game Over";
            MessageBoxResult playAgain = MessageBox.Show($"Score:\n{obstacles.Count.ToString()}\nDo you want to play again?", "Game Over", MessageBoxButton.YesNo, MessageBoxImage.None,MessageBoxResult.No);
            switch(playAgain)
            {
                case MessageBoxResult.Yes:
                    obstacles.Clear();
                    gameCanvas.Children.Clear();
                    gameCanvas.Children.Add(playerCarImage);
                    gameCanvas.Children.Add(consoleTextBox);
                    playerCar.leftPressed = false;
                    playerCar.rightPressed = false;
                    consoleTextBox.Text = "Score:\n0";
                    break; 
                case MessageBoxResult.No:
                    obstacles.Clear();
                    gameCanvas.Children.Clear();
                    playerCar.Y = -200;
                    this.Close();
                    break;

            }
           
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroudVideo.Position = TimeSpan.Zero;
            BackgroudVideo.Play();
        }
        private void GameLoop(object sender, EventArgs e)
        {
         
            DrawObstacles();
            playerCar.Move();
            GenerateBombs();
            TestCollision();
        }
    }
}

