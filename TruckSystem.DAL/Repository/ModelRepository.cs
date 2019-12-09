using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckSystem.DAL.Context;
using TruckSystem.DAL.IRepository;
using TruckSystem.Domain.Vehicles.Model;

namespace TruckSystem.DAL.Repository
{
    public class ModelRepository : RepositoryBase<Model>, IModelRepository
    {
        public ModelRepository(SqlContext context, ILogger<ModelRepository> logger) : base(context, logger)
        {
        }

        public override Task DeleteAsync(Model aggregate)
        {
            throw new NotImplementedException();
        }

        public override Task<Model> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async override Task<List<Model>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override Task SaveAsync(Model aggregate)
        {
            throw new NotImplementedException();
        }

        public override Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public override Task  UpdateAsync(Model aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
