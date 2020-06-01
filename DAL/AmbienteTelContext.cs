using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AmbienteTelContext : DbContext
    {
        public AmbienteTelContext() : base("name = AmbienteTel") { } 
        
        public virtual DbSet<GrupoDTO> Grupos { get; set; }
        public virtual DbSet<SistemaDTO> Sistemas { get; set; }
        public virtual DbSet<UsuarioDTO> Usuarios { get; set; }
    }
}
