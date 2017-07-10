using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{
    public class UsersBL
    {
        private UsersDAO usersDao = new UsersDAO();
        public List<String> getUsers()
        {
            return usersDao.selectUserNames();
        }
        public bool verifyPassword(String username, String password)
        {
            if (password != null)
            {
                String dbPassword = usersDao.selectPassword(username);
                if (password.Equals(dbPassword))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
