namespace TaskManager.Common.Entities
{
    /// <summary>
    /// Категория задачи
    /// </summary>
    public class Category : EntityBase
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит категория
        /// </summary>
        public string UserId { get; set; }
    }
}