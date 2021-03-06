﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeDados.DB
{
    [Table("Tipo_Usuario")]
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        public string descricao { get; set; }
        public string sobe_arquivo { get; set; }
        public string cria_usuario { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }

    }

}