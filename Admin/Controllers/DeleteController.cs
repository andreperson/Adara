using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing; 
using Data.Entities;
using Data.Repository;
using Domain.ModelView;
using Domain.Service; 

namespace Admin.Controllers
{
    public class DeleteController : Controller
    {
        [HttpPost]
        public ActionResult Deletar(DeleteModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                return Redirect(Domain.Util.config.UrlSite + model.Control + "/" + model.Act + "/" + model.Id + "/" + model.Id2 + "/" + model.Id3);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Deletar(Int16 id = 0)
        {
            var model = new DeleteModelView();
            model.Id = id;

            if (id != 0)
            {
                //busca as informações para edição
               // return RedirectToAction("ThankYou", "Account", new { whatever = message });
            }

            return View(model);
        }

    }
}
