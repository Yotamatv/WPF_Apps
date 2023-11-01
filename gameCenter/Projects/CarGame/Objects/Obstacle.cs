using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gameCenter.Projects.CarGame.Objects.GaneObject;
using System.Windows.Controls;

namespace gameCenter.Projects.CarGame.Objects
{
    internal class Obstacle : GameObject
    {
        private int direction;
        public Obstacle(int x, int y, int speed, Image bombImage) : base(x, y, speed, bombImage)
        {
            Random rnd = new Random();
            direction = rnd.Next(-1, 2);
        }
        public override void Move()
        {
            //moves the object according to his direction and starting location
            if (!(X < -100 || X > 800 || Y > 450))
            {

                Y += Speed;
                if (direction == -1)
                {
                    X -= Speed;
                }
                if (direction == 1)
                {
                    X += Speed;
                }

                Draw();

            }
        }
    }
}
