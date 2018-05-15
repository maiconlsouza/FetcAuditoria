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
    public class UsuariosController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (!IMHelper.GetCookie(this, "CRU").Equals("99"))
            {
                return RedirectToAction("Index", "Home");
            }

            var lista = new UsuarioNegocio().PegaTodas();

            return View(lista);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            if (!IMHelper.GetCookie(this, "CRU").Equals("99"))
            {
                return RedirectToAction("Index", "Home");
            }

            var lista = new UsuarioNegocio().PegaPorCodigo(id);

            return View(lista);
        }

        [Authorize]
        [HttpPost]
        public JsonResult DoSave(int Id, string nome, string email, string usr, string senha, int grupo, int tipo)
        {
            var user = new UsuarioView
            {
                Id = Id,
                nome= nome,
                email = email,
                senha = senha,
                usr = usr,
                grupo = new GrupoView
                {
                    Id = grupo
                },
                tipoUsuario = new TipoUsuarioView
                {
                    Id = tipo
                }
            };
            return Json(new UsuarioNegocio().Salvar(user));
        }
    }
}