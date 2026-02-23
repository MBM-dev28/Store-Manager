namespace Store_Manager.Services.UserService
{
    public interface IUserService
    {
        Task LoginUserAsync(string email, string password);
    }
}
