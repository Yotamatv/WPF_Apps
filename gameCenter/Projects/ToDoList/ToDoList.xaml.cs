using gameCenter.Projects.ToDoList.Models;
using System;
using System.Collections.Generic;
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

namespace gameCenter.Projects.ToDoList
{
    public partial class ToDoList : Window
    {
        private string tasksFromFile = " ";
        private ToDoListModel _todolist;
        public ToDoList()
        {
            InitializeComponent();
            InitializeTasks();
        }

        private void InitializeTasks()
        {

            _todolist = new ToDoListModel();
            //checks if file exists, if not creates it
            if (File.Exists("tasks.txt"))
            {
                FileInfo fileInfo = new FileInfo("tasks.txt");
                //checks if file is empty, if not it takes the tasks
                if (fileInfo.Length != 0)
                {
                    tasksFromFile = File.ReadAllText("tasks.txt");
                    _todolist = JsonSerializer.Deserialize<ToDoListModel>(tasksFromFile);
                }
            }
            else
            {
                File.Create("tasks.txt");
            }
            listTasks.ItemsSource = _todolist.Tasks;
        }

        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            // This function is called when a task's checkbox is toggled. It updates the task's completion status,
            // serializes the updated to-do list, and writes it back to the "tasks.txt" file.
            if (sender is CheckBox checkBox && checkBox.DataContext is ToDoTask task)
            {
                _todolist.ToggleComplete(task.Id);
                tasksFromFile = JsonSerializer.Serialize<ToDoListModel>(_todolist);
                File.WriteAllText("tasks.txt", tasksFromFile);
            }
        }

        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;
                textBlock.Visibility = Visibility.Collapsed;
                editTextBox.Visibility = Visibility.Visible;
                editTextBox.Text = textBlock.Text;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            // This function is called when the "Save" button for editing a task is clicked.
            // It retrieves the edited description, updates the task in the to-do list, and saves the changes to the file.
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            btnSave.Visibility = Visibility.Collapsed;
            editTextBox.Visibility = Visibility.Collapsed;
            textBlock.Visibility = Visibility.Visible;
            textBlock.Text = editTextBox.Text;
            if (textBlock.DataContext is ToDoTask task)
            {
                _todolist.UpdateTask(task.Id, textBlock.Text);
                tasksFromFile = JsonSerializer.Serialize<ToDoListModel>(_todolist);
                File.WriteAllText("tasks.txt", tasksFromFile);
            }
        }

        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            // adds a new task to the to-do list if the input is not empty,
            // serializes the updated list, and saves it to the "tasks.txt" file.
            if (!string.IsNullOrEmpty(txtNewTask.Text))
            {
                _todolist.AddTask(new ToDoTask(_todolist.Tasks.Count, txtNewTask.Text));
                tasksFromFile = JsonSerializer.Serialize<ToDoListModel>(_todolist);
                File.WriteAllText("tasks.txt", tasksFromFile);
                txtNewTask.Clear();
            }
        }
    }
}
