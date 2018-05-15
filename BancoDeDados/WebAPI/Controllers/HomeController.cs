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

            return View();
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
