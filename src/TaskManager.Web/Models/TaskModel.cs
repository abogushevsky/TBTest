﻿using System;
using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;

namespace TaskManager.Web.Models
{
    public class TaskListItemModel
    {
        public TaskListItemModel()
        {
            
        }

        public TaskListItemModel(UserTask task)
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

    public class TaskModel : TaskListItemModel
    {
        public TaskModel()
        {

        }

        public TaskModel(UserTask task) : base(task)
        {
            Contract.Requires(task != null);

            this.Details = task.Details;
        }

        public string Details { get; set; }

        public UserTask ToUserTask(ApplicationUser user)
        {
            Contract.Requires(user != null);

            return new UserTask()
            {
                Id = this.Id,
                Title = this.Title,
                Details = this.Details,
                DueDate = this.DueDate,
                Category = this.Category,
                User = new UserInfo()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }
    }
}