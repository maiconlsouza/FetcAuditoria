namespace BancoDeDados
{
    using System;

    public class UsuarioArquivoView
    {
        public int Id { get; set; }
        public int lido { get; set; }

        public ArquivoView arquivo { get; set; }
        public UsuarioView usuario { get; set; }
        public GrupoView grupo { get; set; }
     
    }
}
