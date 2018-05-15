namespace BancoDeDados
{
    public class ArquivoView
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string ArquivoLocal { get; set; }
        public GrupoView Grupo { get; set; }
    }
}
