using NetCoreTest.Models.DataModel;

namespace NetCoreTest.Models.Reposity
{
    public interface IUserReposity
    {
        User GetUser(string userName, string password);

        bool AddUser(User user);
    }
}
