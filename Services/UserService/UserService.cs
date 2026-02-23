using Store_Manager.Data;

namespace Store_Manager.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task LoginUserAsync(string email, string password)
        {

        }
    }
}
