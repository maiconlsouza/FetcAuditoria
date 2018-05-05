using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("arquivio")]
    public class Arquivo
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}