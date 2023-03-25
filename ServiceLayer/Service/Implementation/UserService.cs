using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Service.Contract;

namespace ServiceLayer.Service.Implementation
{
    public class UserService : IUser
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(long id)
        {
            return _dbContext.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public string AddUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                SaveChanges();

                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateUser(User user)
        {
            try
            {
                var userValue = _dbContext.Users.Find(user.UserId);

                if (userValue != null)
                {
                    userValue.UserName = user.UserName;
                    userValue.UserPhone = user.UserPhone;
                    userValue.UserEmail = user.UserEmail;
                    _dbContext.Users.Update(userValue);
                    SaveChanges();
                    return "Successfully Updated";
                }
                else
                {
                    return "No Record(s) Found";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string RemoveUser(long id)
        {
            try
            {
                var user = _dbContext.Users.Where(u => u.UserId == id).FirstOrDefault();
                _dbContext.Users.Remove(user);
                SaveChanges();

                return "Successfully Removed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
