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
    public class ArquivosController : Controller
    {
        [Authorize]
        public ActionResult Index(int? id)
        {
            if (id == null) id = 9;
            var codigo = IMHelper.GetCookie(this, "UID").ConvertToInt();

            var resposta = new ArquivoNegocio().PegaMeusArquivosStatus(codigo, (int)id);

            return View(resposta);
        }

        [Authorize]
        [HttpPost]
        public JsonResult MarcarComoLido(int id)
        {
            var codigo = IMHelper.GetCookie(this, "UID").ConvertToInt();

            new ArquivoNegocio().MarcaArquivoLido(id, codigo);

            return Json(true);
        }
    }
}