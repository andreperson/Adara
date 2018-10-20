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
    public class FactoryTypeController : Controller
    {
        [HttpPost]
        public ActionResult FabricaTipo(FabricaTipoModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                if (model.Fabricatipoid != 0) //update
                {
                    ServiceFabricaTipo.UpdateFabricaTipo(model);
                }
                else //insert
                {
                    ServiceFabricaTipo.InsertFabricaTipo(model);
                }
                return Redirect(Domain.Util.config.UrlSite + "FactoryType/FabricaTipo");

            }
            model.FabricaTipos = ServiceFabricaTipo.getFabricaTipo();

            return View(model);
        }

        [HttpGet]
        public ActionResult FabricaTipo(Int16 id = 0)
        {
            var model = new FabricaTipoModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceFabricaTipo.GetFabricaTipoId(id);  
            }

            model.FabricaTipos = ServiceFabricaTipo.getFabricaTipo();

            return View(model);
        }


        public ActionResult FabricaTipoDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceFabricaTipo.DeleteFabricaTipoId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "FactoryType/FabricaTipo");
        }
    }
}
