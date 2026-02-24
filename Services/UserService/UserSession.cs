using Store_Manager.Services.UserService.UserRoles;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public class UserSession : IUserSession
    {
        public UserVm? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser is not null;
        public bool IsAdmin() => CurrentUser?.Role == UserRole.Administrator;
        // Any component can subscribe to this event to be notified when the session changes.
        // For example, NavMenu subscribes so it can re-render when the user logs in or out.
        public event Action? OnChange;

        public void SetUser(UserVm user)
        {
            CurrentUser = user;
            OnChange?.Invoke(); // notify all subscribers that the session has changed
        }

        public void ClearUser()
        {
            CurrentUser = null;
            OnChange?.Invoke(); // notify all subscribers that the session has changed
        }
    }
}
