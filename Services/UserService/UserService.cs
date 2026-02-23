using Microsoft.EntityFrameworkCore;
using Store_Manager.Data;
using Store_Manager.ViewModels;

namespace Store_Manager.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserVm> LoginUserAsync(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
                throw new InvalidOperationException("Invalid email or password.");

            var passwordHash = HashPassword(password);
            if (user.PasswordHash != passwordHash)
                throw new InvalidOperationException("Invalid email or password.");

            return MapToVm(user);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static UserVm MapToVm(Data.Entities.User user) => new UserVm
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            CreatedOn = user.CreatedOn
        };
    }
}
