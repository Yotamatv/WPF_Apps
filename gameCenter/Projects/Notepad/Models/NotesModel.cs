using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.Notepad.Models
{
    internal class NotesModel
    {
        public ObservableCollection<Note> notes { get; set; }
        public NotesModel() 
        {
            notes = new ObservableCollection<Note>();
        }
    }
}
