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
    public class ClientTypeController : Controller
    {
        [HttpPost]
        public ActionResult ClienteTipo(ClienteTipoModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                if (model.clientetipoid != 0) //update
                {
                    ServiceClienteTipo.UpdateClienteTipo(model);
                }
                else //insert
                {
                    ServiceClienteTipo.InsertClienteTipo(model);
                }
                return Redirect(Domain.Util.config.UrlSite + "ClientType/ClienteTipo");

            }
            model.ClienteTipos = ServiceClienteTipo.getClienteTipo();

            return View(model);
        }

        [HttpGet]
        public ActionResult ClienteTipo(Int16 id = 0)
        {
            var model = new ClienteTipoModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceClienteTipo.GetClienteTipoId(id);  
            }

            model.ClienteTipos = ServiceClienteTipo.getClienteTipo();

            return View(model);
        }


        public ActionResult ClienteTipoDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceClienteTipo.DeleteClienteTipoId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "ClientType/ClienteTipo");
        }
    }
}
