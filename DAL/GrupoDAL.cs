using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GrupoDAL : AmbienteTelContext
    {
        public GrupoDAL() { }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GrupoDTO>()
            .HasMany(g => g.grupoSistemas);
        }

        public GrupoDTO ObterGrupo(int grupoId)
        {
            try
            {
                return Grupos.FirstOrDefault(g => g.grupoId == grupoId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
