using Microsoft.EntityFrameworkCore;
using Store_Manager.Data;
using Store_Manager.Data.Entities;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _db;

        public StoreService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<StoreVm>> GetAllStoresAsync()
        {
            return await _db.Stores
                .Include(s => s.Chain)
                .Select(s => MapToVm(s))
                .ToListAsync();
        }

        public async Task<StoreVm> CreateStoreAsync(int number, string name)
        {
            var numberExists = await _db.Stores.AnyAsync(s => s.Number == number);
            if (numberExists)
                throw new InvalidOperationException($"A store with number {number} already exists.");

            var store = new Store
            {
                Id = Guid.NewGuid(),
                Number = number,
                Name = name,
                CreatedOn = DateTime.UtcNow
            };

            _db.Stores.Add(store);
            await _db.SaveChangesAsync();

            return MapToVm(store);
        }

        public async Task<StoreVm?> UpdateStoreAsync(Guid id, int number, string name, string? address, string? postalCode, string? city, string? phone, string? email, string? storeOwner)
        {
            var store = await _db.Stores
                .Include(s => s.Chain)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (store is null)
                return null;

            var numberExists = await _db.Stores.AnyAsync(s => s.Number == number && s.Id != id);
            if (numberExists)
                throw new InvalidOperationException($"A store with number {number} already exists.");

            store.Number = number;
            store.Name = name;
            store.Address = address;
            store.PostalCode = postalCode;
            store.City = city;
            store.Phone = phone;
            store.Email = email;
            store.StoreOwner = storeOwner;
            store.ModifiedOn = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return MapToVm(store);
        }

        public async Task<bool> DeleteStoreAsync(Guid id)
        {
            var store = await _db.Stores.FindAsync(id);

            if (store is null)
                return false;

            _db.Stores.Remove(store);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<StoreVm?> AssignToChainAsync(Guid storeId, Guid chainId)
        {
            var store = await _db.Stores
                .Include(s => s.Chain)
                .FirstOrDefaultAsync(s => s.Id == storeId);

            if (store is null)
                return null;

            var chain = await _db.Chains.FindAsync(chainId);
            if (chain is null)
                throw new InvalidOperationException("The specified chain does not exist.");

            store.ChainId = chainId;
            store.ModifiedOn = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            store.Chain = chain;

            return MapToVm(store);
        }

        public async Task<StoreVm?> RemoveFromChainAsync(Guid storeId)
        {
            var store = await _db.Stores
                .Include(s => s.Chain)
                .FirstOrDefaultAsync(s => s.Id == storeId);

            if (store is null)
                return null;

            store.ChainId = null;
            store.Chain = null;
            store.ModifiedOn = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return MapToVm(store);
        }

        private static StoreVm MapToVm(Store store) => new StoreVm
        {
            Id = store.Id,
            Number = store.Number,
            Name = store.Name,
            Address = store.Address,
            PostalCode = store.PostalCode,
            City = store.City,
            Phone = store.Phone,
            Email = store.Email,
            StoreOwner = store.StoreOwner,
            CreatedOn = store.CreatedOn,
            ModifiedOn = store.ModifiedOn,
            ChainName = store.Chain?.Name
        };
    }
}
