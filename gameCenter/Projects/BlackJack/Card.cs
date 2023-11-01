using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace gameCenter.Projects.BlackJack
{
    class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ImgSource { get; set; }
        public Card(string name, int value, string imgSource) 
        {
            Name = name;
            Value = value;
            ImgSource = imgSource;
        }
    }
}
