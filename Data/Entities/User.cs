using Store_Manager.Services.UserService.UserRoles;

namespace Store_Manager.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required UserRole Role { get; set; }

        public required DateTime CreatedOn { get; set; }
    }
}
