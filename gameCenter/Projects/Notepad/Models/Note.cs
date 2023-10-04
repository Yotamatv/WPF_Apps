using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.Notepad.Models
{
    internal class Note
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public Note(string text, int index)
        {
            Text = text;
            Index = index;
        }
    }
}
