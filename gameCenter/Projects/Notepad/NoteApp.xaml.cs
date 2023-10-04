using gameCenter.Projects.Notepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace gameCenter.Projects.Notepad
{
    /// <summary>
    /// Interaction logic for NoteApp.xaml
    /// </summary>
    public partial class NoteApp : Window
    {
        ObservableCollection<Note> notes = new ObservableCollection<Note>();
        int index;
        private string notesFromFile = " ";
        string str;
        public NoteApp()
        {
            InitializeComponent();

            noteListBox.ItemsSource = notes;
        }
        private void initNotes()
        {
            if(File.Exists("notes.txt"))
            {
                FileInfo fileInfo = new FileInfo("notes.txt");
                if(fileInfo.Length > 0)
                {
                    notesFromFile = File.ReadAllText("notes.txt");
                    str = JsonSerializer.Deserialize<string>(notesFromFile);
                }
            }
        }
        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            string newNote = noteTextBox.Text;
            int index = notes.Count;
            if (!string.IsNullOrWhiteSpace(newNote))
            {
                notes.Add(new Note(newNote,index));
                noteTextBox.Clear();
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = noteListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                notes.RemoveAt(selectedIndex);
            }
        }
        private void noteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Get the current caret index
                int caretIndex = noteTextBox.CaretIndex;

                // Insert a newline character at the caret index
                noteTextBox.Text = noteTextBox.Text.Insert(caretIndex, "\n");

                // Move the caret to the beginning of the new line
                noteTextBox.CaretIndex = caretIndex + 1;

                // Mark the event as handled to prevent a new line from being added
                e.Handled = true;
            }
        }



        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Text = noteListBox.SelectedItem.ToString();
            index = noteListBox.SelectedIndex;

        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                notes[noteListBox.SelectedIndex] = noteTextBox.Text;

            }
            catch
            {
                notes[index] = noteTextBox.Text;
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                EditNote_Click (sender, e);
            }
        }
    }
}
