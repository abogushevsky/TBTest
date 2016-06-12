using System.Data;
using System.Diagnostics.Contracts;

namespace TaskManager.DataLayer.MsSql
{
    /// <summary>
    /// Структура представялет команду SQL
    /// </summary>
    public struct CrudCommand
    {
        public CrudCommand(string command, CommandType commandType) : this()
        {
            Contract.Requires(!string.IsNullOrEmpty(command));

            this.Command = command;
            this.CommandType = commandType;
        }

        /// <summary>
        /// Текст команды (название хранимой процедуры или запрос)
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Тип команды
        /// </summary>
        public CommandType CommandType { get; private set; }
    }
    
    /// <summary>
    /// Полный набор CRUD-команд, объединённый в одну связку
    /// </summary>
    public class CrudCommandsBundle
    {
        /// <summary>
        /// Команда для получения всех сущностей репозитория
        /// </summary>
        public CrudCommand GetAllCommand { get; set; }

        /// <summary>
        /// Команда для получения сущности по идентификатору
        /// </summary>
        public CrudCommand GetByIdCommand { get; set; }

        /// <summary>
        /// Команда для добавления новой сущности
        /// </summary>
        public CrudCommand CreateCommand { get; set; }

        /// <summary>
        /// Команда для обновления сущности
        /// </summary>
        public CrudCommand UpdateCommand { get; set; }

        /// <summary>
        /// Команда для удаления сущности
        /// </summary>
        public CrudCommand DeleteCommand { get; set; }
    }
}