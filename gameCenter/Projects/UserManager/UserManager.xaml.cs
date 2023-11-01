using gameCenter.Projects.UserManager.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace gameCenter.Projects.UserManager
{
    public partial class UserManager : Window
    {
        User _user;
        List<User> users = DataHandler.GetUsersList();
        public UserManager()
        {
            InitializeComponent();
            if (users == null || users.Count == 0)
            {
                List<User> initialList = new()
                {
                    new User("Bob", "bob@email.com", "Qaz123!123Qaz"),
                    new User("Sara", "Sara@email.com", "Qaz123!123Qaz"),
                    new User("Neomi", "Neomi@email.com", "Qaz123!123Qaz"),
                    new User("Abed", "Abed@email.com", "Qaz123!123Qaz")
                };
                DataHandler.UpdateList(initialList);
                users = DataHandler.GetUsersList();
            }
            UpdateDataGrid();
        }
        private void UpdtaeJsonData()
        {
                List<User> tempList = users;
                users = DataHandler.UpdateList(tempList);
        }
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (Validate.UserName(Input_UserName))
            {
                users.Add(new User(Input_UserName.Text, Input_Email.Text, "Qaz123!123Qaz"));
                UpdtaeJsonData();
                UpdateDataGrid();
            }
        }

        private void UpdateDataGrid()
        {
            DataGrid1.ItemsSource = users.ToList();
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var idCell = DataGrid1.SelectedCells[0];
            var nameCell = DataGrid1.SelectedCells[1];
            var emailCell = DataGrid1.SelectedCells[2];

            try
            {
                string id = ((TextBlock)idCell.Column.GetCellContent(idCell.Item)).Text;
                Input_UserName.Text = ((TextBlock)nameCell.Column.GetCellContent(nameCell.Item)).Text;
                Input_Email.Text = ((TextBlock)emailCell.Column.GetCellContent(emailCell.Item)).Text;
                _user = users.Single(item => item.Id.ToString() == id);
            }
            catch
            {

            }
        }

        private void Btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            users.Remove(_user);
            UpdtaeJsonData();
            UpdateDataGrid();
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            UpdtaeJsonData();
            UpdateDataGrid();
        }
    }
}
