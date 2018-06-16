using NetCoreTest.Models.DataModel;
using System.Linq;

namespace NetCoreTest.Models.Reposity
{
    public class UserReposity : Repository<User>, IUserReposity
    {
        private BlogDbContext _db;

        public UserReposity(BlogDbContext db) : base(db)
        {
            _db = db;
        }

        public bool AddUser(User user)
        {
            if (user==null)
            {
                throw new System.Exception("user参数为空");
            }
            try
            {
                var existing = _db.Users.Where(
                    pc => pc.UserName == user.UserName).SingleOrDefault();

                if (existing == null)
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User GetUser(string userName,string password)
        {
            return _db.Users.Where(a => a.UserName == userName && a.Password==password).FirstOrDefault();
        }
    }
}
