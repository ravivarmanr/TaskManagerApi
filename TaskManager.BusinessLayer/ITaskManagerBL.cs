using System.Collections.Generic;
using TaskManager.Entity;

namespace TaskManager.BusinessLayer
{
    public interface ITaskManagerBL
    {

        List<TaskEntity> GetAllTask();

        TaskEntity GetTaskByID(int taskId);

        void Add(TaskEntity task);

        void Update(TaskEntity task);

        void EndTask(int taskId);

    }
}
