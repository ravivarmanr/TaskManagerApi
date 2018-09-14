using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.DataLayer
{
    public interface ITaskManagerDL
    {
        List<TaskEntity> GetAllTask();

        TaskEntity GetTaskByID(int taskId);

        void Add(TaskEntity task);

        void Update(TaskEntity task);

        void EndTask(int taskId);
    }
}
