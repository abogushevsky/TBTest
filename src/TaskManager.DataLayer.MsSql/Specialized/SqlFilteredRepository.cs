using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql.Specialized
{
    /// <summary>
    /// Реализация фильтрующего репозитория, работающая с MS SQL Server
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    public class SqlFilteredRepository<TEntity, TFilter> : SqlRepositoryBase, IFilteredRepository<TEntity, TFilter>
    {
        private readonly SqlCommandInfo command;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="command">Сведения о запросе, который должен быть выполнен в БД</param>
        /// <param name="connectionStringName">Имя строки подключения в конфигурационном файле</param>
        public SqlFilteredRepository(SqlCommandInfo command, string connectionStringName) : base(connectionStringName)
        {
            Contract.Requires(!string.IsNullOrEmpty(command.Command));
            Contract.Requires(!string.IsNullOrEmpty(connectionStringName));

            this.command = command;
        }

        /// <summary>
        /// Выполняет фильтрующий запрос
        /// </summary>
        /// <param name="filter">Данные фильтра</param>
        /// <returns>Найденные сущности или пустой массив</returns>
        public async Task<TEntity[]> FilterAsync(TFilter filter)
        {
            return (await UsingConnectionAsync<TEntity>(this.command, filter)).ToArray();
        }
    }
}