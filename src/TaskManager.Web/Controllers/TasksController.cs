using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using TaskManager.Common.Interfaces;
using TaskManager.Web.Models;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            Contract.Requires(taskService != null);

            this.taskService = taskService;
        }

        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            ApplicationUser currentUser = await GetCurrentUser();
            if (currentUser == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            return this.Request.CreateResponse(await this.taskService.GetAllUserTasksAsync(currentUser.Id));
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            if (this.User.Identity.IsAuthenticated)
                return await this.UserManager.FindByNameAsync(this.User.Identity.Name);

            return null;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
    }
}