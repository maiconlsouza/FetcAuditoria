using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("usuario_arquivo")]
    public class UsuarioArquivo
    {
        [Key]
        public int id { get; set; }
        public int lido { get; set; }

        //[ForeignKey("Arquivo")]
        public int id_arquivo { get; set; }
        public Arquivo Arquivo { get; set; }

        //[ForeignKey("Usuario")]
        public int id_usuario { get; set; }
        public Usuario Usuario { get; set; }

        //[ForeignKey("Grupo")]
        public int id_grupo { get; set; }
        public Grupo Grupo { get; set; }
    }

}