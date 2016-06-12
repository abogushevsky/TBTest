using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;

namespace TaskManager.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает сервис для работы с пользовательскими данными
    /// </summary>
    [ContractClass(typeof(IUsersServiceContracts))]
    public interface IUsersService
    {
        /// <summary>
        /// Получение всех пользователей (для административной консоли)
        /// </summary>
        /// <returns>Все пользователи системы</returns>
        Task<UserInfo[]> GetAllUsers();

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Найденный пользователь, или null</returns>
        Task<UserInfo> GetUserById(string id);

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <returns>Идентификатор добавленного пользователя</returns>
        Task<string> AddUserAsync(UserInfo user);

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="user">Измененный пользователь</param>
        /// <returns>Признак успеха операции</returns>
        Task<bool> UpdateUserAsync(UserInfo user);

        /// <summary>
        /// Удаление пользователя (для административной консоли)
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Признак успеха операции</returns>
        Task<bool> DeleteUserAsync(string id);
    }

    [ContractClassFor(typeof(IUsersService))]
    internal abstract class IUsersServiceContracts : IUsersService
    {
        public Task<UserInfo[]> GetAllUsers()
        {
            Contract.Ensures(Contract.Result<UserInfo[]>() != null);

            throw new System.NotImplementedException();
        }

        public Task<UserInfo> GetUserById(string id)
        {
            Contract.Requires(id != null);

            throw new System.NotImplementedException();
        }

        public Task<string> AddUserAsync(UserInfo user)
        {
            Contract.Requires(user != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserInfo user)
        {
            Contract.Requires(user != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string id)
        {
            Contract.Requires(id != null);

            throw new System.NotImplementedException();
        }
    }
}