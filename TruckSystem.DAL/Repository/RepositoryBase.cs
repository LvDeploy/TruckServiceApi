using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TruckSystem.DAL.Context;
using TruckSystem.Domain.Common;

namespace TruckSystem.DAL.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ILogger<RepositoryBase<TEntity>> Logger;
        protected SqlContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        public RepositoryBase(SqlContext context, ILogger<RepositoryBase<TEntity>> logger)
        {
            Logger = logger;
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await DbSet.FindAsync(id);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, $"Erro ao consultar objeto no repositório. Método: {nameof(GetByIdAsync)}, Rotina: {typeof(RepositoryBase<>).FullName}",
                    new
                    {
                        TAggregate = typeof(TEntity),
                        id
                    });
                throw;
            }; ;
        }

        public virtual async Task SaveAsync(TEntity entity)
        {
            try
            {
                await DbSet.AddAsync(entity);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, $"Erro ao salvar objeto no repositório. Método: {nameof(SaveAsync)}, Rotina: {typeof(RepositoryBase<>).FullName}",
                       new
                       {
                           TAggregate = typeof(TEntity),
                           entity
                       });
                throw;
            }
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            try
            {
                DbSet.Update(entity);
                return Task.FromResult(0);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, $"Erro ao atualizar objeto no repositório. Método: {nameof(UpdateAsync)}, Rotina: {typeof(RepositoryBase<>).FullName}",
                       new
                       {
                           TAggregate = typeof(TEntity),
                           entity
                       });
                throw;
            }
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            try
            {
                DbSet.Remove(entity);
                return Task.FromResult(0);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, $"Erro ao deletar objeto no repositório. Método: {nameof(DeleteAsync)}, Rotina: {typeof(RepositoryBase<>).FullName}",
                       new
                       {
                           TAggregate = typeof(TEntity),
                           entity
                       });
                throw;
            }
        }

        public virtual async Task SaveChanges()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, $"Erro ao Salvar Resultados. Método: {nameof(SaveChanges)}, Rotina: {typeof(RepositoryBase<>).FullName}",
                       new
                       {
                           TAggregate = typeof(TEntity)
                       });
                throw;
            }
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex, $"Erro ao consultar Resultados. Método: {nameof(SaveChanges)}, Rotina: {typeof(RepositoryBase<>).FullName}",
                       new
                       {
                           TAggregate = typeof(TEntity)
                       });
                throw;
            }
        }
    }
}
