using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IPermissionDAOService
    {

        Result<IEnumerable<Permission>> GetListPermissions();
        
        Result<List<Permission>> GetListUserAssignedPermissions(long userId);

        Result<List<Permission>> GetListUserPermissions(long userId);

        Result<UserPermision> InsUserPermission(UserPermision permission);

        Result DelUserPermission(UserPermision permission);
        

    }
}
