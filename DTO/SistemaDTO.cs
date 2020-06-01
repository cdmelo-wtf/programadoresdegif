using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Table("Sistemas")]
    public class SistemaDTO
    {        
        [Key]
        public int sistemaId { get; set; }
        [Required]
        public string sistemaNome { get; set; }
        [Required]
        public string sistemaCaminho { get; set; }
        [Required]
        public bool sistemaExecutavel { get; set; }

        public List<GrupoDTO> sistemaGrupos { get; set; }
    }
}
