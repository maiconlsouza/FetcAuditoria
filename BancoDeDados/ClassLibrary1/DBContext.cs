using BancoDeDados.DB;
using System.Data.Entity;

namespace BancoDeDados
{
    public class BDContext : DbContext
    {
        public BDContext() : base("Database")
        {
        }

        public virtual DbSet<Usuario> usuario { get; set; }
        public virtual DbSet<Arquivo> arquivo { get; set; }
        public virtual DbSet<Grupo> grupo{ get; set; }
        public virtual DbSet<TipoUsuario> tipoUsuarios { get; set; }
        public virtual DbSet<UsuarioArquivo> usuarioArquivo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(k => k.Id)
                .HasMany(f => f.Arquivos)
                .WithRequired(r => r.Usuario)
                .HasForeignKey(f => f.id_arquivo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasRequired(f => f.GrupoFK)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(f => f.grupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasRequired(f => f.TipoUsuario)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(f => f.tipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsuarioArquivo>()
                .HasRequired(f => f.Arquivo)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(f => f.id_arquivo)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UsuarioArquivo>()
                .HasRequired(f => f.GrupoFK)
                .WithMany(r => r.Arquivos)
                .HasForeignKey(f => f.id_grupo)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
