﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeTier.Data;

namespace ThreeTier.Service
{
    public interface IUserService
    {
        bool Login(User user);
        IList<User> GetFriendsFromUser(User user);
    }
}
