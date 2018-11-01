using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IUserDAOService
    {
        Result<List<UserIdentity>> GetListUser();

        Result<UserIdentity> GetUserById(long id);

        Result<User> GetUserByUserName(string userName);

        Result<UserEdit> GetUserEditById(long id);

        Result<bool> UpdUser(User usuar);

        Result<User> InsUser(User usuar);

        Result<User> UserByPersonId(long personId);

        Result<User> UserByUserIdPersonId(long userId, long personId);

        Result<User> GetUserByUserIdUserName(long userId, string userName);
        

    }
}
