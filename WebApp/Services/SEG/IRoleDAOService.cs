using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IRoleDAOService
    {
        Result<List<Role>> GetListRoles();
       
        Result<List<Role>> GetListUserRoles(long userId);
        
        Result<UserRole> InsUserRole(UserRole role);
        
        Result DelUserRole(UserRole role);
       
        Result<Role> InsRole(Role role);

    }
}
