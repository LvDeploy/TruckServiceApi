using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckSystem.DAL.Context;
using TruckSystem.DAL.IRepository;
using TruckSystem.Domain.Vehicles.Model;
using TruckSystem.Domain.Vehicles.Validators;

namespace TruckSystem.DAL.Repository
{
    public class TruckRepository : RepositoryBase<Truck>, ITruckRepository
    {
        public readonly TruckValidator _validator;

        public TruckRepository(SqlContext context, ILogger<TruckRepository> logger) : base(context, logger)
        {
            _validator = new TruckValidator();
        }

        public async override Task DeleteAsync(Truck aggregate)
        {
           await base.DeleteAsync(aggregate);
           await this.SaveChanges();
        }

        public async override Task<Truck> GetByIdAsync(int id)
        {
           return await base.GetByIdAsync(id);
        }

        public async override Task SaveAsync(Truck aggregate)
        {
            _validator.ValidateAndThrow(aggregate);
            await base.SaveAsync(aggregate);
            await this.SaveChanges();
        }

        public async override Task SaveChanges()
        {
            await base.SaveChanges();
        }

        public async override Task UpdateAsync(Truck aggregate)
        {
            _validator.ValidateAndThrow(aggregate);
            await base.UpdateAsync(aggregate);
            await this.SaveChanges();
        }

        public async override Task<List<Truck>> GetAllAsync() {
            var data = await DbSet.Include(x => x.Model)
              .ToListAsync();

            return data;
        }
    }
}
