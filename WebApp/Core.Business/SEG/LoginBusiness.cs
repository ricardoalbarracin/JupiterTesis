using Core.Models.SEG;
using Core.Models.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.Business.SEG
{
    public class LoginBusiness
    {
        public Result ValidarPassword(Usuario usuario, string password)
        {
            var result = new Result();
            PasswordHasher<string> pw = new PasswordHasher<string>();

            var resultVerify = pw.VerifyHashedPassword(usuario.Username, usuario.Password, password);
            if (resultVerify == PasswordVerificationResult.Failed)
            {
                result.Message = "Verifique sus credenciales de acceso previamente";
                return result;
            }
            result.Success = true;
            return result;
        }
    }
}
