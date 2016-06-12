using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.BusinessLayer
{
    public class TaskService : EntityServiceBase<UserTask, int>, ITaskService
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="repository">Репозитория для сущностей типа <see cref="UserTask"/></param>
        public TaskService(IRepository<UserTask, int> repository) : base(repository)
        {
            Contract.Requires(repository != null);
        }

        /// <summary>
        /// Получение всех задач пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Найденные задачи пользователя, или пустой массив</returns>
        public async Task<UserTask[]> GetAllUserTasksAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получение всех задач по категории
        /// </summary>
        /// <param name="categoryId">Идентификатор категории</param>
        /// <returns>Найденные задачи по категории, или пустой массив</returns>
        public async Task<UserTask[]> GetTasksByCategoryAsync(int categoryId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получение задачи по id
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Найденная задача или null</returns>
        public async Task<UserTask> GetTaskByIdAsync(int id)
        {
            return await ExecOnRepositoryAsync(r => r.GetByIdAsync(id));
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task">Новая задача</param>
        /// <returns>Идентификатор добавленной задачи</returns>
        public async Task<int> AddTaskAsync(UserTask task)
        {
            return await ExecOnRepositoryAsync(r => r.CreateAsync(task));
        }

        /// <summary>
        /// Редактирование задачи
        /// </summary>
        /// <param name="task">Измененная задача</param>
        /// <returns>Признак успеха операции</returns>
        public async Task<bool> UpdateTaskAsync(UserTask task)
        {
            return await ExecOnRepositoryAsync(r => r.UpdateAsync(task));
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Призак успеха операции</returns>
        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await ExecOnRepositoryAsync(r => r.DeleteAsync(id));
        }
    }
}