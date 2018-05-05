namespace BancoDeDados
{
    public class Resposta
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Objeto { get; set; }

        public Resposta()
        {

        }

        public Resposta(bool sucesso, string mensagem = "", object objeto = null)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Objeto = objeto;
        }
    }
}
