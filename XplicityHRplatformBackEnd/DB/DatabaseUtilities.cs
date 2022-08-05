using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.DB
{
    public class DatabaseUtilities
    {
        private readonly HRplatformDbContext _dbContext;
        public DatabaseUtilities(HRplatformDbContext context)
        {
            _dbContext = context;
        }
        public async Task EnsureDatabaseCreated()
        {
            await _dbContext.Database.MigrateAsync();
        }

        public async Task<T> GetById<T>(DbSet<T> dbSet, Guid id) where T : BaseEntity
        {
            var entry = await dbSet.FindAsync(id);
            return entry;
        }
        public async Task<List<T>> GetAll<T>(DbSet<T> dbSet) where T : BaseEntity
        {
            var allEntries = await dbSet.ToListAsync();
            return allEntries;
        }

        public async Task<Guid> AddEntry<T>(DbSet<T> dbSet, T entryData) where T : BaseEntity
        {
            var result = await dbSet.AddAsync(entryData);
            var entryId = result.Entity.Id;
            await _dbContext.SaveChangesAsync();
            return entryId;
        }

    }
}
