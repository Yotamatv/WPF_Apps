using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gameCenter.Projects.CarGame.Objects.GaneObject;
using System.Windows.Controls;

namespace gameCenter.Projects.CarGame.Objects
{
    internal class PlayerCar : GameObject
    {
        public bool rightPressed { get; set; }
        public bool leftPressed { get; set; }


        public PlayerCar(int x, int y, int speed, Image carImage) : base(x, y, speed, carImage)
        {


        }
        // This function is used to move an object. It checks for left and right key presses,
        // and if conditions are met, it updates the object's position (X coordinate) accordingly.
        public override void Move()
        {
            if (leftPressed && X > 0)
            {
                Console.WriteLine("left");
                X -= Speed + 5;
            }

            if (rightPressed && X < 800 - Representation.Width)
            {
                X += Speed + 5;
            }

            Draw();
        }

    }
}
