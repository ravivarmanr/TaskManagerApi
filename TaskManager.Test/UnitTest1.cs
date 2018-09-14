using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using TaskManager.DataLayer;
using TaskManager.Entity;
using TaskManager.BusinessLayer;


namespace TaskManager.UnitTest.BusinessLayer
{
    [TestFixture]
    public class UnitTestTaskManagerBL
    {
        private ITaskManagerDL _mockRepository;
        private List<TaskEntity> _taskList;

        [SetUp]
        public void initialize()
        {
            // Initialise repository
            var mockRepo = new Mock<ITaskManagerDL>();

            //create sample data
            _taskList = new List<TaskEntity>()
            {
                new TaskEntity{TaskId=1, TaskName="Test project Creation", ParentTaskId=0, ParentTaskName="null", Priority=1, TaskStatus="Y", StartDt= new DateTime(2018,9,9), EndDt=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=2, TaskName="Setup Creation", ParentTaskId=1, ParentTaskName="Test project Creation", Priority=2, TaskStatus="Y", StartDt= new DateTime(2018,9,9), EndDt=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=3, TaskName="Repository Creation", ParentTaskId=1, ParentTaskName="Test project Creation", Priority=2, TaskStatus="Y", StartDt= new DateTime(2018,9,9), EndDt=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=4, TaskName="Test case creation", ParentTaskId=3, ParentTaskName="Repository Creation", Priority=3, TaskStatus="Y", StartDt= new DateTime(2018,9,9), EndDt=new DateTime(2018,9,10) },
                new TaskEntity{TaskId=5, TaskName="Test case execution", ParentTaskId=3, ParentTaskName="Repository Creation", Priority=4, TaskStatus="N", StartDt= new DateTime(2018,9,9), EndDt=new DateTime(2018,9,9) }
            };

            // Setup mocking behavior

            //Get the list of all the tasks
            mockRepo.Setup(p => p.GetAllTask()).Returns(_taskList);

            //Get the one particular task
            mockRepo.Setup(p => p.GetTaskByID(It.IsAny<int>()))
            .Returns((int id) => _taskList.Find(p => p.TaskId.Equals(id)));

            //Add the task
            mockRepo.Setup(p => p.Add(It.IsAny<TaskEntity>()))
                .Callback((TaskEntity task) => _taskList.Add(task));

            //Update the task
            mockRepo.Setup(p => p.Update(It.IsAny<TaskEntity>()))
                .Callback((TaskEntity updatedTask) =>
                {
                    var actualTask = _taskList.Find(p => p.TaskId.Equals(updatedTask.TaskId));

                    actualTask.TaskName = updatedTask.TaskName;
                    actualTask.ParentTaskId = updatedTask.ParentTaskId;
                    actualTask.Priority = updatedTask.Priority;
                    actualTask.StartDt = updatedTask.StartDt;
                    actualTask.EndDt = updatedTask.EndDt;
                    actualTask.TaskStatus = updatedTask.TaskStatus;
                }
                );

            //End the Task
            mockRepo.Setup(p => p.EndTask(It.IsAny<int>()))
                .Callback((int taskId) =>
                {
                    var actualTask = _taskList.Find(p => p.TaskId.Equals(taskId));

                    actualTask.TaskStatus = "N";
                }
                );
            _mockRepository = mockRepo.Object;
        }

        [Test]
        //Test GetAll tasks to return all tasks
        public void ShouldReturnAllTask()
        {
            List<TaskEntity> taskList = _mockRepository.GetAllTask();

            Assert.IsTrue(taskList.Count() == 5);
            Assert.IsTrue(taskList.ElementAt(0).TaskName == "Test project Creation");
            Assert.IsTrue(taskList.ElementAt(1).ParentTaskId == 1);
            Assert.IsTrue(taskList.ElementAt(4).TaskStatus == "N" );
            Assert.IsTrue(taskList.ElementAt(2).Priority == 2);
            Assert.IsTrue(taskList.ElementAt(3).StartDt == new DateTime(2018, 9, 9));
            Assert.IsTrue(taskList.ElementAt(3).EndDt == new DateTime(2018, 9, 10));

        }

        [Test]
        //Test GetTask to return the task details of requested task id
        public void ShouldReturnSingleTask()
        {
            var taskId = 2;

            TaskEntity taskDetail = _mockRepository.GetTaskByID(taskId);
            
            Assert.IsNotNull(taskDetail);
            Assert.IsTrue(taskDetail.TaskName == "Setup Creation");
            Assert.IsTrue(taskDetail.ParentTaskName == "Test project Creation");
            Assert.IsTrue(taskDetail.Priority == 2);
            Assert.IsTrue(taskDetail.TaskStatus== "Y");
            Assert.IsTrue(taskDetail.StartDt == new DateTime(2018, 9, 9));
            Assert.IsTrue(taskDetail.EndDt == new DateTime(2018, 9, 9));
        }

        [Test]
        // Test Add Task
        public void ShouldAddTask()
        {
            var taskId = _taskList.Count() + 1;
            var taskDetail = new TaskEntity
            {
                TaskId = taskId,
                TaskName = "testing the Test case",
                ParentTaskId = 3,
                ParentTaskName = "Repository Creation",
                Priority = 5,
                TaskStatus = "Y",
                StartDt = new DateTime(2018, 9, 10),
                EndDt = new DateTime(2018, 9, 11)
            };

            _mockRepository.Add(taskDetail);

            TaskEntity addedTask = _mockRepository.GetTaskByID(taskId);
            Assert.IsTrue(_taskList.Count() == 6);
            Assert.IsNotNull(addedTask);
            Assert.AreSame(addedTask.GetType(), typeof(TaskEntity));
            Assert.AreEqual(taskId, addedTask.TaskId);
            Assert.IsTrue(taskDetail.TaskName == addedTask.TaskName);
        }

        [Test]
        // Test update task functionality
        public void ShouldUpdateTask()
        {
            var taskId = 3;
            var taskDetail = new TaskEntity
            {
                TaskId = taskId,
                TaskName = "Update Repository Creation", //"Repository Creation",
                ParentTaskId = 2, //1
                ParentTaskName = "Test project Creation",
                Priority = 3, //2
                TaskStatus = "Y",
                StartDt = new DateTime(2018, 9, 10), //DateTime(2018, 9, 9),
                EndDt = new DateTime(2018, 10, 9) //DateTime(2018, 9, 9)
            };

            _mockRepository.Update(taskDetail);

            var UpdatedTask = _mockRepository.GetTaskByID(taskId);

            Assert.IsTrue(UpdatedTask.TaskName == "Update Repository Creation");
            Assert.IsTrue(UpdatedTask.ParentTaskId == 2);
            Assert.IsTrue(UpdatedTask.Priority == 3);
            Assert.IsTrue(UpdatedTask.StartDt == new DateTime(2018,9,10));
            Assert.IsTrue(UpdatedTask.EndDt == new DateTime(2018, 10, 9));
        }
        
        [Test]
        //Test the end task functionality
        public void ShouldEndTask()
        {
            var taskId = 3;

            _mockRepository.EndTask(taskId);

            var updatedTask = _mockRepository.GetTaskByID(taskId);

            Assert.IsTrue(updatedTask.TaskStatus == "N");
        }

        [TearDown]
        public void CleanUp()
        {
            _taskList.Clear();
        }

    }
}
