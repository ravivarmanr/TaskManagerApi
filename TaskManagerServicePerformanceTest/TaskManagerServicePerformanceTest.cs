using System;
using NBench;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.BusinessLayer;
using TaskManager.DataLayer;
using TaskManager.Entity;

namespace TaskManagerServicePerformanceTest
{
    [TestClass]
    public class TaskManagerServicePerformanceTest
    {
        private ITaskManagerBL _taskManagerService;
        private TaskEntity _newTask;
        private TaskEntity _taskToBeUpdated;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            ITaskManagerDL _dbRepo = new TaskManagerDL();
            _taskManagerService = new TaskManagerBL(_dbRepo);
            _newTask = new TaskEntity { TaskId = 1, TaskName = "Test project Creation", ParentTaskId = 0, ParentTaskName = "null", Priority = 1, TaskStatus = "Y", StartDt = new DateTime(2018, 9, 9), EndDt = new DateTime(2018, 9, 9) };
            _taskToBeUpdated = new TaskEntity { TaskId = 2, TaskName = "Setup Creation", ParentTaskId = 1, ParentTaskName = "Test project Creation", Priority = 2, TaskStatus = "Y", StartDt = new DateTime(2018, 9, 9), EndDt = new DateTime(2018, 9, 9) };

        }

        [PerfBenchmark(NumberOfIterations =1, RunMode =RunMode.Throughput, TestMode =TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void GetAll_TaskList_Elapsed_Time()
        {
            var result = _taskManagerService.GetAllTask();
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds =1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated,MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        public void GetAll_TaskList_Memory_Consumed()
        {
            var result = _taskManagerService.GetAllTask();
        }


        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void Add_Task_Elapsed_Time()
        {
            _taskManagerService.Add(_newTask);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        public void Add_Task_Memory_Consumed()
        {
            _taskManagerService.Add(_newTask);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void Update_Task_Elapsed_Time()
        {
            _taskManagerService.Update(_taskToBeUpdated);
        }

        [PerfBenchmark(NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 1000)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        public void Update_Task_Memory_Consumed()
        {
            _taskManagerService.Update(_taskToBeUpdated);
        }
        
    }
}
