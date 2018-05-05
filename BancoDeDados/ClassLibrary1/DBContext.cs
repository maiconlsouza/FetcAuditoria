using BancoDeDados.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDados
{
    public class BDContext : DbContext
    {
        public BDContext() : base()
        {
        }

        public virtual DbSet<Arquivo> arquivo { get; set; }
        public virtual DbSet<Grupo> grupo{ get; set; }
        public virtual DbSet<TipoUsuario> tipoUsuarios { get; set; }
        public virtual DbSet<Usuario> usuario { get; set; }
        public virtual DbSet<UsuarioArquivo> usuarioArquivo { get; set; }
    }
}
