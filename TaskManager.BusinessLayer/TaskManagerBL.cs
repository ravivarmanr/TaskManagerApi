using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataLayer;
using TaskManager.Entity;

namespace TaskManager.BusinessLayer
{

    public class TaskManagerBL : ITaskManagerBL
    {

        private ITaskManagerDL _taskDL;

        public TaskManagerBL (ITaskManagerDL taskDL)
        {
            _taskDL = taskDL;
        }
        
        public List<TaskEntity> GetAllTask()
        {
            return _taskDL.GetAllTask();
        }

        public TaskEntity GetTaskByID(int taskId)
        {
            return _taskDL.GetTaskByID(taskId);
        }

        public void Add(TaskEntity task)
        {
            _taskDL.Add(task);
        }

        public void Update(TaskEntity task)
        {
            _taskDL.Update(task);
        }

        public void EndTask(int taskId)
        {
            _taskDL.EndTask(taskId);
        }
    }
}
