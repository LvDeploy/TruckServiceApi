using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruckSystem.Domain.Common
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task SaveAsync(TEntity aggregate);

        Task UpdateAsync(TEntity aggregate);

        Task DeleteAsync(TEntity aggregate);

        Task SaveChanges();

        Task<List<TEntity>> GetAllAsync();
    }
}
