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
    public class BookItensStatusController : Controller
    {
        [HttpPost]
        public ActionResult TalaoItensStatus(TalaoItensStatusModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                if (model.talaoitensstatusid != 0) //update
                {
                    ServiceTalaoItensStatus.UpdateTalaoItensStatus(model);
                }
                else //insert
                {
                    ServiceTalaoItensStatus.InsertTalaoItensStatus(model);
                }
                return Redirect(Domain.Util.config.UrlSite + "BookItensStatus/TalaoItensStatus");

            }
            model.TaloesItensStatus = ServiceTalaoItensStatus.getTalaoItensStatus();

            return View(model);
        }

        [HttpGet]
        public ActionResult TalaoItensStatus(Int16 id = 0)
        {
            var model = new TalaoItensStatusModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceTalaoItensStatus.GetTalaoItensStatusId(id);  
            }

            model.TaloesItensStatus = ServiceTalaoItensStatus.getTalaoItensStatus();

            return View(model);
        }


        public ActionResult TalaoItensStatusDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceTalaoItensStatus.DeleteTalaoItensStatusId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "BookItensStatus/TalaoItensStatus");
        }
    }
}
