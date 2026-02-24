using Store_Manager.Services.UserService.UserRoles;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public class UserSession : IUserSession
    {
        public UserVm? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser is not null;
        public bool IsAdmin() => CurrentUser?.Role == UserRole.Administrator;
        public void SetUser(UserVm user) => CurrentUser = user;
        public void ClearUser() => CurrentUser = null;
    }
}
