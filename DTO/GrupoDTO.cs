using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Table("Grupos")]
    public class GrupoDTO
    {
        [Key]
        public int grupoId { get; set; }
        [Required]
        public string grupoNome { get; set; }  
        
        public List<SistemaDTO> grupoSistemas { get; set; }

    }
}
