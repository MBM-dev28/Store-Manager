using Store_Manager.Services.UserService.UserRoles;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public interface IUserSession
    {
        UserVm? CurrentUser { get; }
        bool IsLoggedIn { get; }
        bool IsAdmin();
        void SetUser(UserVm user);
        void ClearUser();
    }
}
