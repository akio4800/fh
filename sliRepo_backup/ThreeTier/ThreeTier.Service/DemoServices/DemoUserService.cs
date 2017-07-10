using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeTier.Data;
using ThreeTier.Data.DataAccess;
using ThreeTier.Data.DataAccess.DemoRepository;

namespace ThreeTier.Service.DemoServices
{
    public class DemoUserService : IUserService
    {
        private IUserRepository rep;

        public DemoUserService()
        {
            this.rep = new DemoUserRepository();
        }

        public bool Login(User loginUser)
        {
            User user = this.rep.GetUser(loginUser);
            if (user != null)
            {
                if (user.Login.Equals(loginUser.Login) && user.Password.Equals(loginUser.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public IList<User> GetFriendsFromUser(User user)
        {
            List<User> users = this.rep.GetUsers().ToList();
            List<User> returnUsers = new List<User>();
            if (user.Login.Equals("Robert"))
            {
                returnUsers.Add(users.Single(x => x.Login == "Oli"));
            }
            return returnUsers;
        }

    }
}
