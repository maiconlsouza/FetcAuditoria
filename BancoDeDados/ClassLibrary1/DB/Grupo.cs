using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("grupo")]
    public class Grupo
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ativo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<UsuarioArquivo> Arquivos { get; set; }
    }
}

