using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace gameCenter.Projects.Project1.Utilities
{
    public class Validate
    {
        public static bool UserName(TextBox box)
        {
            Regex regex = new(@"^[A-Za-z].{2,20}");
            Match match = regex.Match(box.Text);

            if (!match.Success)
            {
                MessageBox.Show("please enter at least 3 characters");
                box.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            box.BorderBrush = new SolidColorBrush(Colors.Black);
            return true;
        }
    }
}
