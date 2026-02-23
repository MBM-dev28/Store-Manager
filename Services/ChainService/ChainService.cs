using Microsoft.EntityFrameworkCore;
using Store_Manager.Data;
using Store_Manager.Data.Entities;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.ChainService
{
    public class ChainService : IChainService
    {
        private readonly ApplicationDbContext _db;

        public ChainService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<ChainVm>> GetAllChainsAsync()
        {
            return await _db.Chains
                .Include(c => c.Stores)
                .Select(c => MapToVm(c))
                .ToListAsync();
        }

        public async Task<ChainVm> CreateChainAsync(string name)
        {
            var nameExists = await _db.Chains.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            if (nameExists)
                throw new InvalidOperationException($"A chain with the name '{name}' already exists.");

            var chain = new Chain
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedOn = DateTime.UtcNow
            };

            _db.Chains.Add(chain);
            await _db.SaveChangesAsync();

            return MapToVm(chain);
        }

        public async Task<ChainVm?> UpdateChainAsync(Guid id, string name)
        {
            var chain = await _db.Chains
                .Include(c => c.Stores)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chain is null)
                return null;

            var nameExists = await _db.Chains.AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != id);
            if (nameExists)
                throw new InvalidOperationException($"A chain with the name '{name}' already exists.");

            chain.Name = name;
            chain.ModifiedOn = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return MapToVm(chain);
        }

        public async Task<bool> DeleteChainAsync(Guid id)
        {
            var chain = await _db.Chains
                .Include(c => c.Stores)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chain is null)
                return false;

            if (chain.Stores.Count != 0)
                throw new InvalidOperationException($"Cannot delete '{chain.Name}' because it still has {chain.Stores.Count} store(s) connected to it.");

            _db.Chains.Remove(chain);
            await _db.SaveChangesAsync();

            return true;
        }

        private static ChainVm MapToVm(Chain chain) => new ChainVm
        {
            Id = chain.Id,
            Name = chain.Name,
            CreatedOn = chain.CreatedOn,
            ModifiedOn = chain.ModifiedOn,
            StoreCount = chain.Stores.Count
        };
    }
}
