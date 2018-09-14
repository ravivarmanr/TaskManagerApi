using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entity
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public Nullable<int> ParentTaskId { get; set; }
        public string TaskName { get; set; }
        public Nullable<System.DateTime> StartDt { get; set; }
        public Nullable<System.DateTime> EndDt { get; set; }
        public Nullable<int> Priority { get; set; }
        public string TaskStatus { get; set; }
        public string ParentTaskName { get; set; }
    }
}
