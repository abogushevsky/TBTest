using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Exceptions;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql
{
    /// <summary>
    /// Реализация репозитория, работающая с MS SQL Server
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public abstract class SqlRepositoryBase<TEntity, TKey, TDto> : IRepository<TEntity, TKey> 
        where TEntity : IEntityWithId<TKey> 
        where TDto : class
    {
        private readonly IEntityDtoConverter<TEntity, TDto> converter;

        protected SqlRepositoryBase(IEntityDtoConverter<TEntity, TDto> converter)
        {
            Contract.Ensures(converter != null);

            this.converter = converter;
        }

        /// <summary>
        /// Возвращает все сущности репозитория
        /// </summary>
        public async Task<TEntity[]> GetAllAsync()
        {
            TDto[] result = (await UsingConnectionAsync<TDto>(this.Commands.GetAllCommand, null)).ToArray();
            return result.Select(this.converter.Convert).ToArray();
        }

        /// <summary>
        /// Возвращает сущность по её идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная сущность или null</returns>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            TDto[] result = (await UsingConnectionAsync<TDto>(this.Commands.GetByIdCommand, new {Id = id})).ToArray();
            return result.Any() ? this.converter.Convert(result.First()) : default(TEntity);
        }

        /// <summary>
        /// Добавляет новую сущность в репозиторий и возвращает её идентификатор
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        /// <returns>Идентификатор новой сущности</returns>
        /// <exception cref="EntityCreateException">Если не удалось добавить новую сущность</exception>
        public async Task<TKey> CreateAsync(TEntity entity)
        {
            try
            {
                IEnumerable<TKey> result =
                    (await UsingConnectionAsync<TKey>(this.Commands.CreateCommand, this.converter.Convert(entity)))
                        .ToArray();

                if (result.Any())
                    return result.First();
                throw new EntityCreateException();
            }
            catch (SqlException ex)
            {
                throw new EntityCreateException(ex);
            }
        }

        /// <summary>
        /// Обновляет сущность в репозитории
        /// </summary>
        /// <param name="entity">Изменённая сущность</param>
        /// <returns>true, если операция затронула > 0 сущностей. false в противном случае</returns>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return (await UsingConnectionAsync<int>(this.Commands.UpdateCommand, this.converter.Convert(entity))).FirstOrDefault() > 0;
        }

        /// <summary>
        /// Удаляет сущность из репозитория
        /// </summary>
        /// <param name="id">Идентификатор сущности, которую необходимо удалить</param>
        /// <returns>true, если операция затронула > 0 сущностей. false в противном случае</returns>
        public async Task<bool> DeleteAsync(TKey id)
        {
            return (await UsingConnectionAsync<int>(this.Commands.DeleteCommand, new { Id = id })).FirstOrDefault() > 0;
        }

        private Task<IEnumerable<TResult>> UsingConnectionAsync<TResult>(CrudCommand command, object param)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                return connection.QueryAsync<TResult>(command.Command, param: param, commandType: command.CommandType);
            }
        }

        protected abstract CrudCommandsBundle Commands { get; }
    }
}