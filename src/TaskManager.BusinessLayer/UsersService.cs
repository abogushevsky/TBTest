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
    public class UsersService : EntityServiceBase<UserInfo, string>, IUsersService
    {
        public UsersService(IRepository<UserInfo, string> usersRepository) : base(usersRepository)
        {
            Contract.Requires(usersRepository != null);
        }

        /// <summary>
        /// Получение всех пользователей (для административной консоли)
        /// </summary>
        /// <returns>Все пользователи системы</returns>
        public async Task<UserInfo[]> GetAllUsers()
        {
            return await ExecOnRepositoryAsync(r => r.GetAllAsync());
        }

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Найденный пользователь, или null</returns>
        public async Task<UserInfo> GetUserById(string id)
        {
            return await ExecOnRepositoryAsync(r => r.GetByIdAsync(id));
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <returns>Идентификатор добавленного пользователя</returns>
        public async Task<string> AddUserAsync(UserInfo user)
        {
            return await ExecOnRepositoryAsync(r => r.CreateAsync(user));
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="user">Измененный пользователь</param>
        /// <returns>Признак успеха операции</returns>
        public async Task<bool> UpdateUserAsync(UserInfo user)
        {
            return await ExecOnRepositoryAsync(r => r.UpdateAsync(user));
        }

        /// <summary>
        /// Удаление пользователя (для административной консоли)
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Признак успеха операции</returns>
        public async Task<bool> DeleteUserAsync(string id)
        {
            return await ExecOnRepositoryAsync(r => r.DeleteAsync(id));
        }
    }
}