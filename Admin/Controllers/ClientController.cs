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
    public class ClientController : Controller
    {
        [HttpPost]
        public ActionResult Cliente(ClienteModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                if (model.clienteid != 0) //update
                {
                    ServiceCliente.UpdateCliente(model);
                }
                else //insert
                {
                    ServiceCliente.InsertCliente(model);
                }
                return Redirect(Domain.Util.config.UrlSite + "Client/Cliente");

            }
            //busca o representante
            //se for admin, é zero e escolhe pelo combo, se  não ja atribui no model
            Int16 repreid = 0;
            
            ViewBag.repreid = repreid;
            
            model.ClientesTipos = ServiceClienteTipo.getClienteTipoCombo();
            model.UFs = ServiceUF.getUFCombo();
            return View(model);
        }

        [HttpGet]
        public ActionResult Cliente(Int16 id = 0)
        {
            var model = new ClienteModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceCliente.GetClienteId(id);  
            }

            //busca o representante
            //se for admin, é zero e escolhe pelo combo, se  não ja atribui no model
            

            return View(model);
        }


        public ActionResult ClienteDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceCliente.DeleteClienteId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "Client/Cliente");
        }
    }
}
