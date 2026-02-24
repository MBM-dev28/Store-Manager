using Store_Manager.ViewModels;

namespace Store_Manager.Services.StoreService
{
    public interface IStoreService
    {
        Task<List<StoreVm>> GetAllStoresAsync();
        Task<StoreVm> CreateStoreAsync(int number, string name, string? address, string? postalCode, string? city, string? phone, string? email, string? storeOwner);
        Task<StoreVm?> UpdateStoreAsync(Guid id, int number, string name, string? address, string? postalCode, string? city, string? phone, string? email, string? storeOwner);
        Task<bool> DeleteStoreAsync(Guid id);
        Task<StoreVm?> AssignToChainAsync(Guid storeId, Guid chainId);
        Task<StoreVm?> RemoveFromChainAsync(Guid storeId);
    }
}
