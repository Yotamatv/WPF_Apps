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
        int index;
        private string notesFromFile = " ";
        private NotesModel notesModel;
        public NoteApp()
        {
            InitializeComponent();
            initNotes();
        }
        private void initNotes()
        {
            notesModel = new NotesModel();
            if (File.Exists("notes.txt"))
            {
                FileInfo fileInfo = new FileInfo("notes.txt");
                if (fileInfo.Length > 0)
                {
                    notesFromFile = File.ReadAllText("notes.txt");
                    notesModel = JsonSerializer.Deserialize<NotesModel>(notesFromFile);
                }
            }
            else
            {
                File.Create("notes.txt");
            }
            noteListBox.ItemsSource = notesModel.notes;
        }
        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            string newNote = noteTextBox.Text;
            int index = notesModel.notes.Count;
            if (!string.IsNullOrWhiteSpace(newNote))
            {
                notesModel.notes.Add(new Note(newNote, index));
                updateFile();
                noteTextBox.Clear();
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = noteListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                notesModel.notes.RemoveAt(selectedIndex);
                updateFile();
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
            string note = notesModel.notes[noteListBox.SelectedIndex].Text;
            noteTextBox.Text = note;
            index = noteListBox.SelectedIndex;
            updateFile();
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (noteTextBox.Text.Length > 0)
            {
                notesModel.notes[index].Text = noteTextBox.Text;
                noteListBox.ItemsSource = null;
                noteListBox.ItemsSource = notesModel.notes;
                updateFile();
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                EditNote_Click(sender, e);
            }
        }
        private void updateFile()
        {
            notesFromFile = JsonSerializer.Serialize<NotesModel>(notesModel);
            File.WriteAllText("notes.txt", notesFromFile);
        }

        private void SearchBox_Update(object sender, TextChangedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(SearchBox.Text)))
            {
                noteListBox.ItemsSource = null;
                noteListBox.ItemsSource = notesModel.notes.Where(note => note.Text.Contains(SearchBox.Text)).ToList();
                PlaceHolder.Visibility = Visibility.Collapsed;
            }
            else
            {
                noteListBox.ItemsSource = null;
                noteListBox.ItemsSource = notesModel.notes;
                PlaceHolder.Visibility = Visibility.Visible;
            }
        }


    }
}
