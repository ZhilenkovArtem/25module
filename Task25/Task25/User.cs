using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task25
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }

    public class UserRepository
    {
        AppContext db;

        public UserRepository(AppContext context)
        {
            db = context;
        }

        public User GetUserById(int id) => db.Users.FirstOrDefault(u => u.Id == id);

        public IEnumerable<User> GetAllUsers() => db.Users;

        public void AddUser(User user) => db.Users.Add(user);

        public void AddSeveralUser(User[] users) => db.Users.AddRange(users);

        public void RemoveUser(User user) => db.Users.Remove(user);

        public int UserHasBookCount(User user) => user.Books.Count;
    }
}
