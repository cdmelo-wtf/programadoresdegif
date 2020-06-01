using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Table("Usuarios")]
    public class UsuarioDTO
    {
        [Key]
        public int usuarioId { get; set; }
        [Required]
        public string usuarioLogin { get; set; }
        [Required]
        public string usuarioSenha { get; set; }
        [Required]
        public int usuarioGrupo { get; set; }

    }
}
