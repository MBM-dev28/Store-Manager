using Store_Manager.ViewModels;

namespace Store_Manager.Services.ChainService
{
    public interface IChainService
    {
        Task<List<ChainVm>> GetAllChainsAsync();
        Task<ChainVm> CreateChainAsync(string name);
        Task<ChainVm?> UpdateChainAsync(Guid id, string name);
        Task<bool> DeleteChainAsync(Guid id);
    }
}
