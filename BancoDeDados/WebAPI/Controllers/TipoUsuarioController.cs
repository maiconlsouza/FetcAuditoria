using BancoDeDados;
using Newtonsoft.Json.Linq;
using RegraDeNegocio;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("tipoUsuario")]
    public class TipoUsuarioController : ApiController
    {
        [HttpPost]
        [ActionName("obtemporcodigo")]
        public async Task<IHttpActionResult> ObtemPorCodigo([FromBody] JObject jsonData)
        {
            dynamic json = jsonData;
            int codigo = json.Codigo;

            var resposta = new TipoUsuarioNegocio().PegaPorCodigo(codigo);

            if (resposta == null) return Ok(new Resposta(false, "Tipo de Usuario não encontrado.", resposta));

            return Ok(new Resposta(true, objeto: resposta));
        }

        [HttpPost]
        [ActionName("obtem")]
        public async Task<IHttpActionResult> Obtem()
        {
            var resposta = new TipoUsuarioNegocio().PegaTodas();

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
            TipoUsuarioView tipoUsuario = jsonData.SelectToken("TipoUsuario").ToObject<TipoUsuarioView>();

            return Ok((new TipoUsuarioNegocio().Salvar(tipoUsuario)));
        }

        [HttpPost]
        [ActionName("excluir")]
        public async Task<IHttpActionResult> Excluir([FromBody] JObject jsonData)
        {
            TipoUsuarioView tipoUsuario = jsonData.SelectToken("TipoUsuario").ToObject<TipoUsuarioView>();

            return Ok((new TipoUsuarioNegocio().Excluir(tipoUsuario)));
        }
    }
}