using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.BusinessLayer;
using TaskManager.Entity;

namespace TaskManager.API.Controllers
{
    public class TaskController : ApiController
    {


        private ITaskManagerBL _TaskManagerService;

        public TaskController(ITaskManagerBL TaskManagerService)
        {
            _TaskManagerService = TaskManagerService;
        }


        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            var getAllTask = _TaskManagerService.GetAllTask();

            return Ok(getAllTask);
        }

        [Route("Get/{TaskId}")]
        public IHttpActionResult GetTask(int TaskId)
        {
            var getTask = _TaskManagerService.GetTaskByID(TaskId);

            return Ok(getTask);
        }

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddTask([FromBody]TaskEntity task)
        {
            _TaskManagerService.Add(task);

            return Ok();
        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult UpdateTask([FromBody]TaskEntity task)
        {
            _TaskManagerService.Update(task);

            return Ok();
        }

        [Route("End/{taskID}")]
        [HttpGet]
        public IHttpActionResult End(int taskId)
        {
            _TaskManagerService.EndTask(taskId);
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult Test()
        {
            return Ok("hi");
        }
    }
}
