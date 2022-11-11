using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Models
{
    public enum ToDoRabbitActions
    {
        Create = 0,
        Update,
        Delete,
    }

    [Serializable]
    public class ToDoRabbitMessage
    {
        public ToDoRabbitActions Action { get; set; }
        public ToDoItem? OldItem { get; set; }
        public ToDoItem? NewItem { get; set; }
    }
}
