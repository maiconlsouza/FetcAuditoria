using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("usuarios_arquivo")]
    public class UsuarioArquivo
    {
        [Key]
        public int id { get; set; }
        public int lido { get; set; }

        public int id_arquivo { get; set; }
        public Arquivo Arquivo { get; set; }

        public int id_usuario { get; set; }
        public Usuario Usuario { get; set; }

        public int id_grupo { get; set; }
        public Grupo GrupoFK { get; set; }
    }

}