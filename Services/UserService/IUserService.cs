using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public interface IUserService
    {
        Task<UserVm> LoginUserAsync(string email, string password);
    }
}
