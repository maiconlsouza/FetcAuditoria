using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BancoDeDados;
using BancoDeDados.DB;
using Interacao.Framework.MVC;
using RegraDeNegocio;

namespace WebAPI.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index(string ReturnUrl)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(IMHelper.GetCookie(this, "UID")))
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public JsonResult DoLogin(string usuario, string senha)
        {
            var core = new UsuarioNegocio();
            var resultado = core.Login(usuario, senha);

            if (resultado.Sucesso)
            {
                IMHelper.SetaCookie(this, "UID", ((UsuarioView)resultado.Objeto).Id.ToString());
                IMHelper.SetaCookie(this, "SOB", (((UsuarioView)resultado.Objeto).tipoUsuario.sobe_arquivo) == "S" ? "99" : "25");

                Autentica(usuario, true);
            }

            return Json(new { situacao = resultado.Sucesso, mensagem = resultado.Mensagem });
        }

        [HttpPost]
        public JsonResult Logout(string id)
        {
            try
            {
                Logoff();
                return Json(new { situacao = true, mensagem = "FEITO" });
            }
            catch
            {
                return Json(new { situacao = false, mensagem = "FEITO" });
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Logoff();

            return RedirectToAction("Index", "Login");
        }

    }
}