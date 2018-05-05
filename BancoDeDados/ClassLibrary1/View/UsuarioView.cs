namespace BancoDeDados
{
    using System;

    public class UsuarioView
    {
        public int Id { get; set; }
        public string usr { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public TipoUsuarioView tipoUsuario { get; set; }
        public GrupoView grupo { get; set; }
        
    }
}
