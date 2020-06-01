using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GrupoBLL
    {
        GrupoDAL _grupoDAL = new GrupoDAL();

        public GrupoDTO ObterGrupo(int grupoId)
        {
            return _grupoDAL.ObterGrupo(grupoId);
        }
    }
}
