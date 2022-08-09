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

        public async Task<bool> UpdateEntry<T>(DbSet<T> dbSet, T entryData) where T : BaseEntity
        {
            dbSet.Update(entryData);
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> RemoveEntry<T>(DbSet<T> dbSet, T entryData, bool checkIfExists) where T : BaseEntity
        {
            if (checkIfExists)
            {
                var entry = dbSet.Find(entryData.Id);
                if (entry == null)
                    return false;
                dbSet.Remove(entry);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
