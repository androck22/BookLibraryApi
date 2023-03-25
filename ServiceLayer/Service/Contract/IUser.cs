using DomainLayer.Models;

namespace ServiceLayer.Service.Contract
{
    public interface IUser
    {
        List<User> GetAllUsers();
        User GetUserById(long id);
        string AddUser(User user);
        string UpdateUser(User user);
        string RemoveUser(long id);
        void SaveChanges();
    }
}
