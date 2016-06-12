using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.BusinessLayer.Properties;
using TaskManager.Common.Exceptions;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Exceptions;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.BusinessLayer
{
    public abstract class EntityServiceBase<TEntity, TKey> where TEntity: IEntityWithId<TKey>
    {
        private readonly IRepository<TEntity, TKey> repository;

        protected EntityServiceBase(IRepository<TEntity, TKey> repository)
        {
            Contract.Requires(repository != null);

            this.repository = repository;
        }

        protected Task<TResult> ExecWithRepositoryAsync<TResult>(Func<IRepository<TEntity, TKey>, Task<TResult>> asyncFunc)
        {
            try
            {
                return asyncFunc(this.repository);
            }
            catch (ConcurrentUpdateException)
            {
                throw new BusinessException(Resources.ConcurrentEditExText);
            }
            catch (RepositoryException)
            {
                throw new BusinessException(Resources.CommonRepositoryExText);
            }
            catch (Exception)
            {
                throw new BusinessException();
            }
        }
    }
}