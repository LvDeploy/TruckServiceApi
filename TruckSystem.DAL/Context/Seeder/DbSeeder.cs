using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckSystem.DAL.Context.Seeder
{
    public class DbSeeder
    {
        private readonly SqlContext _context;

        public DbSeeder(SqlContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!await ExisteMigrationsPendentes())
            {
                var seederModelos = new SeederModelos(_context);
                await seederModelos.InserirModels();
            }
        }

        private async Task<bool> ExisteMigrationsPendentes()
        {
            var migrationsPendentes = (await _context.Database.GetPendingMigrationsAsync())?.Count() ?? 0;
            return migrationsPendentes > 0;
        }
    }
}
