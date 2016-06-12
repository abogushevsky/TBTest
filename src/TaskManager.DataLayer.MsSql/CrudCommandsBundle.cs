using System.Data;
using System.Diagnostics.Contracts;

namespace TaskManager.DataLayer.MsSql
{
    public struct CrudCommand
    {
        public CrudCommand(string command, CommandType commandType) : this()
        {
            Contract.Requires(!string.IsNullOrEmpty(command));

            this.Command = command;
            this.CommandType = commandType;
        }

        public string Command { get; private set; }

        public CommandType CommandType { get; private set; }
    }
    
    public class CrudCommandsBundle
    {
        public CrudCommand GetAllCommand { get; set; }

        public CrudCommand GetByIdCommand { get; set; }

        public CrudCommand CreateCommand { get; set; }

        public CrudCommand UpdateCommand { get; set; }

        public CrudCommand DeleteCommand { get; set; }
    }
}