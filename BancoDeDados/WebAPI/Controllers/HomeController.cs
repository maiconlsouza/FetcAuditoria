using BancoDeDados;
using Interacao.Framework;
using Interacao.Framework.MVC;
using RegraDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "DashBoard";

            var lista = new List<RelatorioAuditoriaView>();
            if (IMHelper.GetCookie(this, "SOB").Equals("99"))
            {
                lista = new ArquivoNegocio().RelatorioAuditoria();
            }

            return View(lista);
        }

        [HttpPost]
        public JsonResult MeuDashboard()
        {
            var codigo = IMHelper.GetCookie(this, "UID").ConvertToInt();
            var retorno = new ArquivoNegocio().MeuDasboard(codigo);

            return Json(new MeuDashboardView
            {
                Lidos = retorno.Item1,
                NaoLidos = retorno.Item2
            });
            
        }
    }
}
