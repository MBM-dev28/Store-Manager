using Store_Manager.Services.UserService.UserRoles;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public interface IUserService
    {
        Task<UserVm> LoginUserAsync(string email, string password);
        Task<UserVm> CreateUserAsync(string name, string email, string password, UserRole role);
    }
}
