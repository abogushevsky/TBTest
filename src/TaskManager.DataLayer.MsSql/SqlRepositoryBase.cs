using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Exceptions;

namespace TaskManager.DataLayer.MsSql
{
    public abstract class SqlRepositoryBase
    {
        protected Task<IEnumerable<TResult>> UsingConnectionAsync<TResult>(SqlCommandInfo command, object param)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    return connection.QueryAsync<TResult>(command.Command, param: param,
                        commandType: command.CommandType);
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.ErrorCode == ConcurrentUpdateException.ERROR_CODE)
                {
                    throw new ConcurrentUpdateException();
                }

                //TODO: log
                throw new RepositoryException();
            }
            catch (Exception ex)
            {
                //TODO: log
                throw new RepositoryException();
            }
        }
    }
}