using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string usr { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        [ForeignKey("TipoUsuario")]
        public int id_tipo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        [ForeignKey("Grupo")]
        public int id_grupo { get; set; }
        public Grupo grupo { get; set; }

    }

}