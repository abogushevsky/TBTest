using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.BusinessLayer
{
    /// <summary>
    /// Реализация сервиса для работы с пользователями
    /// </summary>
    public class UsersService : IUsersService
    {
        private readonly IRepository<UserInfo, string> usersRepository;

        public UsersService(IRepository<UserInfo, string> usersRepository)
        {
            Contract.Requires(usersRepository != null);

            this.usersRepository = usersRepository;
        }

        /// <summary>
        /// Получение всех пользователей (для административной консоли)
        /// </summary>
        /// <returns>Все пользователи системы</returns>
        public Task<UserInfo[]> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Найденный пользователь, или null</returns>
        public Task<UserInfo> GetUserById(string id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <returns>Идентификатор добавленного пользователя</returns>
        public Task<string> AddUserAsync(UserInfo user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="user">Измененный пользователь</param>
        /// <returns>Признак успеха операции</returns>
        public Task<bool> UpdateUserAsync(UserInfo user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Удаление пользователя (для административной консоли)
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Признак успеха операции</returns>
        public Task<bool> DeleteUserAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}