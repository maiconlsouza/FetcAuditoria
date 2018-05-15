using Interacao.Framework.MVC;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebAPI.Controllers
{
    public class BaseController : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        protected string getTemplateEmail(string templateName)
        {
            using (StringWriter sw = new StringWriter())
            {

                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, templateName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                var urlLogo = Path.Combine("http://", Request.UrlReferrer.Authority.ToString(), "Assets", "img", "qc_logo_topo.png");

                return sw.GetStringBuilder().ToString().Replace("[MSLOGOTIPO]", urlLogo);
            }
        }

        protected void Autentica(string Email, bool manterConectado)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    "Email",
                    DateTime.Now,
                    DateTime.Now.AddYears(1),
                    manterConectado,
                    Email,
                    FormsAuthentication.FormsCookiePath);

            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }

        protected void Logoff()
        {
            try
            {
                FormsAuthentication.SignOut();

                IMHelper.SetaCookie(this, "UID", string.Empty);
                IMHelper.SetaCookie(this, "SOB", string.Empty);
                IMHelper.SetaCookie(this, "CRU", string.Empty);
            }
            catch
            {
            }
        }
    }
}