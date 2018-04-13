using Core.Models.SEG;
using Core.Models.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Result GenerarHashRandomPassword(string Username)
        {
            var result = new Result();
            try
            {
                var password = GenerateRandomPassword();
                PasswordHasher<string> pw = new PasswordHasher<string>();
                var data = new Dictionary<string, string>();
                data.Add("Password", password);
                data.Add("Hash", pw.HashPassword(Username, password));
                result.Data = data;
                result.Success = true;
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public string GenerateRandomPassword()
        {
            var opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "0123456789"                   // digits
                
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
