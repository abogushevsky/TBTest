using System;
using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;

namespace TaskManager.Web.Models
{
    public class TaskModel
    {
        public TaskModel()
        {
            
        }

        public TaskModel(UserTask task)
        {
            Contract.Requires(task != null);

            this.Id = task.Id;
            this.Title = task.Title;
            this.Category = task.Category;
            this.DueDate = task.DueDate;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public DateTime DueDate { get; set; }
    }
}