using Core;
using Core.Logs;
using Model.Logs;

namespace Data.Repository
{
    public class UserLogRepository: Repository<UserLog>, IUserLogRepository
    {
        private CCDbContext _db;

        public UserLogRepository(CCDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
