using Store_Manager.Services.UserService.UserRoles;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserVm>> GetAllUsersAsync();
        Task<UserVm> LoginUserAsync(string email, string password);
        Task<UserVm> CreateUserAsync(string name, string email, string password, UserRole role);
    }
}
