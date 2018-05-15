using BancoDeDados;
using Interacao.Framework.MVC;
using RegraDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class EnvioController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            if (!IMHelper.GetCookie(this, "SOB").Equals("99"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public JsonResult DoSave(string descricao, int grupo)
        {
            return Json(new ArquivoNegocio().Salvar(descricao, grupo));
        }

        [Authorize]
        public ActionResult Upar(int id)
        {
            if (!IMHelper.GetCookie(this, "SOB").Equals("99"))
            {
                return RedirectToAction("Index", "Home");
            }

            IMHelper.SetaCookie(this, "ArquivoID", id.ToString());

            var objeto = new ArquivoNegocio().PegaPorCodigo(id);

            return View(objeto);
        }
    }
}