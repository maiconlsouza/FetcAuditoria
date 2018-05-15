using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("arquivo")]
    public class Arquivo
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string ArquivoLocal { get; set; }

        public virtual ICollection<UsuarioArquivo> Usuarios { get; set; }
    }
}