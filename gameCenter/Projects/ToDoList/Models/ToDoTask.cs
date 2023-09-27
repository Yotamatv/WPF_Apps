using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gameCenter.Projects.ToDoList.Models
{
    internal class ToDoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsComplete { get; set; }

        [JsonConstructor]
        public ToDoTask(int Id, string Description, DateTime CreatedDate, bool IsComplete)
        {
            this.Id = Id;
            this.Description = Description;
            this.CreatedDate = CreatedDate;
            this.IsComplete = IsComplete; // allows more flexibility in creating tasks

        }

        public ToDoTask(int id, string descript, bool isComp = false)
        {
            Id = id;
            Description = descript;
            CreatedDate = DateTime.Now;
            IsComplete = isComp; // allows more flexibility in creating tasks

        }
    }
}
