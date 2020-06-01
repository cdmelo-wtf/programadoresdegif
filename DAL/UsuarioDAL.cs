using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL : AmbienteTelContext
    {
        public UsuarioDAL()  { }

        public bool VerificarLogin(string login, string senha)
        {
            try
            {
                return (Usuarios.Any(u => u.usuarioLogin == login && u.usuarioSenha == senha));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UsuarioDTO ObterUsuario(string login, string senha)
        {
            try
            {
                return Usuarios.FirstOrDefault(u => (u.usuarioLogin == login && u.usuarioSenha == senha));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
