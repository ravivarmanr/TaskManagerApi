using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entity;

namespace TaskManager.DataLayer
{
    public class TaskManagerDL : ITaskManagerDL
    {

        TaskManagerEntities _context;

        public TaskManagerDL()
        {
            _context = new TaskManagerEntities();
        }

    
        public List<TaskEntity> GetAllTask()
        {
            var task = (from t in _context.Task_Tbl
                        join p in _context.Task_Tbl on t.Parent_ID equals p.Task_ID into Ta
                        from pTask in Ta.DefaultIfEmpty()


            select new TaskEntity
                        {
                            ParentTaskName = (pTask.Task ==null ? string.Empty : pTask.Task),
                            ParentTaskId = t.Parent_ID,
                            TaskId = t.Task_ID,
                            TaskName = t.Task,
                            Priority = t.Priority,
                            EndDt = t.End_Date,
                            StartDt = t.Start_Date,
                            TaskStatus = t.Task_Status

                        }).ToList();    

            return task;
        }

        public TaskEntity GetTaskByID(int taskId)
        {
            var task = (from t in _context.Task_Tbl
                        where t.Task_ID == taskId
                        select new TaskEntity
                        {
                            ParentTaskId = t.Parent_ID,
                            TaskId = t.Task_ID,
                            TaskName = t.Task,
                            Priority = t.Priority,
                            EndDt = t.End_Date,
                            StartDt = t.Start_Date,
                            TaskStatus = t.Task_Status
                        }).SingleOrDefault();

            return task;
        }

        public void Add(TaskEntity task)
        {
            var taskParam = new Task_Tbl();

            var taskId = _context.Task_Tbl.Max(t => t.Task_ID) + 1;

            taskParam.Parent_ID = task.ParentTaskId;
            taskParam.Task_ID = taskId;
            taskParam.Task = task.TaskName;
            taskParam.Priority = task.Priority;
            taskParam.End_Date = task.EndDt;
            taskParam.Start_Date = task.StartDt;
            taskParam.Task_Status = task.TaskStatus; 

            _context.Task_Tbl.Add(taskParam);
            _context.SaveChanges();
        }

        public void Update(TaskEntity task)
        {
            var taskParam = (from t in _context.Task_Tbl
                             where t.Task_ID == task.TaskId
                             select t
                             ).FirstOrDefault();


            taskParam.Parent_ID = task.ParentTaskId;
            taskParam.Task_ID = task.TaskId;
            taskParam.Task = task.TaskName;
            taskParam.Priority = task.Priority;
            taskParam.End_Date = task.EndDt;
            taskParam.Start_Date = task.StartDt;
            taskParam.Task_Status = task.TaskStatus;

            _context.SaveChanges();
        }

        public void EndTask(int taskId)
        {
            var taskParam = (from t in _context.Task_Tbl
                             where t.Task_ID == taskId
                             select t
                             ).FirstOrDefault();

            taskParam.End_Date = DateTime.Now;
            taskParam.Task_Status = "N";

            _context.SaveChanges();
        }

    }
}
