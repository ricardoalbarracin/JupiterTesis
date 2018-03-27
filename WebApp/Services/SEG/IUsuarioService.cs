using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IUsuarioService
    {
        Result GetUsuarioByUserName(string userName);

        Result GetUsuarioById(int id);

        Result GetListUsuarios();

        Result GetUsuarioEditById(int id);
    }
}
