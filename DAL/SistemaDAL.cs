using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SistemaDAL : AmbienteTelContext
    {
        public SistemaDAL()  { }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SistemaDTO>()
            .HasMany(s => s.sistemaGrupos);
        }

        public IQueryable<SistemaDTO> ObterSistemas(int grupoId)
        {
            try
            {  
                return Sistemas.Where(s => s.sistemaGrupos.Any(sg => sg.grupoId == grupoId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
