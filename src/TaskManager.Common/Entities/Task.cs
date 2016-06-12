using System;

namespace TaskManager.Common.Entities
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Task : EntityBase
    {
        /// <summary>
        /// Название задачи или краткое описание
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Детальное описание задачи
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Крайний срок выполнения задачи
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Категория задачи
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача
        /// </summary>
        public string UserId { get; set; }
    }
}