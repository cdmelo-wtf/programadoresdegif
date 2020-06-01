using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioBLL
    {
        UsuarioDAL _usuarioDAL = new UsuarioDAL();

        public bool VerificarLogin(string login, string senha)
        {
            return _usuarioDAL.VerificarLogin(login, senha);
        }

        public UsuarioDTO ObterUsuario(string login, string senha)
        {
            UsuarioDTO _usuarioDTO = new UsuarioDTO();
            _usuarioDTO = _usuarioDAL.ObterUsuario(login, senha);

            return _usuarioDTO;
            
        }
    }
}
