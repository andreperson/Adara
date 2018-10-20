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
    public class ReportStatusController : Controller
    {
        [HttpPost]
        public ActionResult RelatorioStatus(RelatorioStatusModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                if (model.Relatoriostatusid != 0) //update
                {
                    ServiceRelatorioStatus.UpdaterelatorioStatus(model);
                }
                else //insert
                {
                    ServiceRelatorioStatus.InsertrelatorioStatus(model);
                }
                return Redirect(Domain.Util.config.UrlSite + "ReportStatus/RelatorioStatus");

            }
            model.RelatoriosStatus = ServiceRelatorioStatus.getrelatorioStatus();

            return View(model);
        }

        [HttpGet]
        public ActionResult RelatorioStatus(Int16 id = 0)
        {
            var model = new RelatorioStatusModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceRelatorioStatus.GetrelatorioStatusId(id);  
            }

            model.RelatoriosStatus = ServiceRelatorioStatus.getrelatorioStatus();

            return View(model);
        }


        public ActionResult RelatorioStatusDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceRelatorioStatus.DeleterelatorioStatusId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "ReportStatus/RelatorioStatus");
        }
    }
}
