using BancoDeDados;
using Newtonsoft.Json.Linq;
using RegraDeNegocio;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("UsuarioArquivo")]
    public class UsuarioAquivoController : ApiController
    {
        [HttpPost]
        [ActionName("obtemporcodigo")]
        public async Task<IHttpActionResult> ObtemPorCodigo([FromBody] JObject jsonData)
        {
            dynamic json = jsonData;
            int codigo = json.Codigo;

            var resposta = new UsuarioArquivoNegocio().PegaPorCodigo(codigo);

            if (resposta == null) return Ok(new Resposta(false, "UsuarioArquivo não encontrado.", resposta));

            return Ok(new Resposta(true, objeto: resposta));
        }

        [HttpPost]
        [ActionName("obtem")]
        public async Task<IHttpActionResult> Obtem()
        {
            var resposta = new UsuarioArquivoNegocio().PegaTodas();

            if (resposta == null || resposta.Count == 0)
            {
                return Ok(new Resposta(false, "Nenhum registro encontrado.", resposta));
            }

            return Ok(new Resposta(true, "", resposta));
        }

        [HttpPost]
        [ActionName("salvar")]
        public async Task<IHttpActionResult> Salvar([FromBody] JObject jsonData)
        {
            UsuarioArquivoView usuarioArquivo = jsonData.SelectToken("usuarioArquivo").ToObject<UsuarioArquivoView>();

            return Ok((new UsuarioArquivoNegocio().Salvar(usuarioArquivo)));
        }

        [HttpPost]
        [ActionName("excluir")]
        public async Task<IHttpActionResult> Excluir([FromBody] JObject jsonData)
        {
            UsuarioArquivoView usuarioArquivo = jsonData.SelectToken("usuarioArquivo").ToObject<UsuarioArquivoView>();

            return Ok((new UsuarioArquivoNegocio().Excluir(usuarioArquivo)));
        }
    }
}