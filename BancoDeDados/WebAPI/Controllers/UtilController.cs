using Interacao.Framework;
using Interacao.Framework.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using RegraDeNegocio;

namespace WebAPI.Controllers
{
    public class UtilController : BaseController
    {
        [Authorize]
		[HttpPost]
		public JsonResult UploadFiles()
		{
			var codproduto = IMHelper.GetCookie(this, "ArquivoID");
            var novo_arquivo = "UP_" + codproduto;

			string savedFileName = string.Empty;
			foreach (string file in Request.Files)
			{
				HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
				if (hpf.ContentLength == 0)
					continue;

				var extensao = Request.Files[file].FileName.Substring(Request.Files[file].FileName.LastIndexOf('.') + 1);

				savedFileName = Path.Combine(Server.MapPath("~/Upload"), novo_arquivo + "." + extensao);
				hpf.SaveAs(savedFileName);

                new ArquivoNegocio().AtualizaArquivo(int.Parse(codproduto), Request.Files[file].FileName);
			}

			return Json(new { foto = novo_arquivo });
		}
	}
}