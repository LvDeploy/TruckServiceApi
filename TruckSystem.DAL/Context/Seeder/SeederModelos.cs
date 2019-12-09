using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckSystem.Domain.Vehicles.Model;

namespace TruckSystem.DAL.Context.Seeder
{
    public class SeederModelos
    {
        private readonly SqlContext _context;

        public static readonly List<Model> listModel = new List<Model>() { new Model { Name = "PM" }, new Model { Name = "HP" }, new Model { Name = "OP" }, new Model { Name = "FH" }, new Model { Name = "FM" }, };

        public SeederModelos(SqlContext context)
        {
            _context = context;
        }

        public async Task InserirModels()
        {
            var models = await _context
                    .Set<Model>()
                    .ToListAsync();

            if (models.Count == 0)
            {
                foreach (var model in listModel)
                {                    
                    _context
                        .Set<Model>()
                        .Add(model);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
