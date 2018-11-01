using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IsecurityService
    {
        Result<UserIdentity> Login(User user);
        
        Result UpdUserRolesPermissions(IDictionary<string, object> dataSections);
        
        Result InsUserRolesPermissions(IDictionary<string, object> dataSections);
        
        Result<string> ResetPassword(User user);
        
        Result ValidatePassword(User usuario, string password);
        
        Result<Dictionary<string, string>> GenerarHashRandomPassword(string Username);
        string GenerateRandomPassword();
        
        Result ValidateUserCreate(User user);
        
        Result ValidateUserUpdate(User user);
    }
}
