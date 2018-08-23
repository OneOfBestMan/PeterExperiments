using System.Linq;
using Core.Users;
using Model.Users;

namespace Data.Repository
{

    public class UserRepository : Repository<User>, IUserRepository
    {
        private CCDbContext _db;

        public UserRepository(CCDbContext db) : base(db)
        {
            _db = db;
        }
        public User GetUser(string userName, string password)
        {
            return _db.Users.Where(a => a.UserName == userName && a.Password == password).FirstOrDefault();
        }

        public User AddUser(User user)
        {
            if (user == null)
            {
                throw new System.Exception("user参数为空");
            }
            try
            {
                var existing = _db.Users.Where(
                    pc => pc.UserName == user.UserName).SingleOrDefault();
                if (existing == null)
                {
                    AddFullAuditDefault<User>(user);
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
