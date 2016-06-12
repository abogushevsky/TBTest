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
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    transaction = connection.BeginTransaction();
                    Task<IEnumerable<TResult>> result = connection.QueryAsync<TResult>(command.Command, param: param,
                        commandType: command.CommandType);

                    transaction.Commit();
                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                if (transaction != null) transaction.Rollback();
                if (sqlEx.ErrorCode == ConcurrentUpdateException.ERROR_CODE)
                {
                    throw new ConcurrentUpdateException();
                }

                //TODO: log
                throw new RepositoryException();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                //TODO: log
                throw new RepositoryException();
            }
        }
    }
}