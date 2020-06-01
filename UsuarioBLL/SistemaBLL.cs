using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SistemaBLL
    {
        SistemaDAL _sistemaDAL = new SistemaDAL();

        public List<SistemaDTO> ObterSistemas(int grupoId)
        {
            return _sistemaDAL.ObterSistemas(grupoId).ToList();
        }
    }
}
