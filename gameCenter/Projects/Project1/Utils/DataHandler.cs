using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace gameCenter.Projects.Project1.Utilities
{
    public static class DataHandler
    {
        static readonly string directory = Directory.GetParent(Environment.CurrentDirectory)!.ToString();
        static readonly string path = directory + @"../../../Projects/Project1/Data/Users.json";
        static readonly string jsonString=File.ReadAllText(path);
        public static List<User> GetUsersList()
        {
            return JsonSerializer.Deserialize<List<User>>(jsonString)!;
        }

        public static List<User> UpdateList(List<User> usersList)
        {
            var serilized = JsonSerializer.Serialize(usersList);
            File.WriteAllText(path, serilized);
            return usersList;
        }
    }
}
