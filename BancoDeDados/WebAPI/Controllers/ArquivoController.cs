using BancoDeDados;
using Newtonsoft.Json.Linq;
using RegraDeNegocio;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("arquivo")]
    public class ArquivoController : ApiController
    {
        [HttpPost]
        [ActionName("obtemporcodigo")]
        public async Task<IHttpActionResult> ObtemPorCodigo([FromBody] JObject jsonData)
        {
            dynamic json = jsonData;
            int codigo = json.Codigo;

            var resposta = new ArquivoNegocio().PegaPorCodigo(codigo);

            if (resposta == null) return Ok(new Resposta(false, "Arquivo não encontrado.", resposta));

            return Ok(new Resposta(true, objeto: resposta));
        }

        [HttpPost]
        [ActionName("obtem")]
        public async Task<IHttpActionResult> Obtem()
        {
            var resposta = new ArquivoNegocio().PegaTodas();

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
            ArquivoView arquivo = jsonData.SelectToken("Arquivo").ToObject<ArquivoView>();

            return Ok((new ArquivoNegocio().Salvar(arquivo)));
        }

        [HttpPost]
        [ActionName("excluir")]
        public async Task<IHttpActionResult> Excluir([FromBody] JObject jsonData)
        {
            ArquivoView arquivo = jsonData.SelectToken("Arquivo").ToObject<ArquivoView>();

            return Ok((new ArquivoNegocio().Excluir(arquivo)));
        }

        [HttpPost]
        [ActionName("meudashboard")]
        public async Task<IHttpActionResult> MeuDashboard([FromBody] JObject jsonData)
        {
            ArquivoView arquivo = jsonData.SelectToken("Arquivo").ToObject<ArquivoView>();

            return Ok((new ArquivoNegocio().Excluir(arquivo)));
        }
    }
}