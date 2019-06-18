namespace SULS.Services
{
    using SULS.Data;
    using SULS.Models;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly SULSContext context;

        public UsersService(SULSContext sulsContext)
        {
            this.context = sulsContext;
        }

        public string CreateUser(string username, string email, string password)
        {
            User userInDB = context.Users.Where(x => x.Username == username).FirstOrDefault();

            if (userInDB != null)
            {
                return null;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = this.HashPassword(password),
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();
            return user.Id;
        }

        public User GetUserOrNull(string username, string password)
        {
            var passwordHash = this.HashPassword(password);
            var user = this.context.Users.FirstOrDefault(
                x => x.Username == username
                && x.Password == passwordHash);
            return user;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
