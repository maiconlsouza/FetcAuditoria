using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public int tipo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public int grupo { get; set; }
        public Grupo GrupoFK { get; set; }

        public virtual ICollection<UsuarioArquivo> Arquivos { get; set; }

    }

}