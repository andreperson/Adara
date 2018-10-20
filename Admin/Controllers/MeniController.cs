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
    public class MeniController : Controller
    {
        [HttpPost]
        public ActionResult Menu(MenuModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                if (model.menuid != 0) //update
                {
                    ServiceMenu.UpdateMenu(model);
                }
                else //insert
                {
                    ServiceMenu.InsertMenu(model);
                }
                return Redirect(Domain.Util.config.UrlSite + "Meni/Menu");

            }

            model.menus = ServiceMenu.getMenu();

            return View(model);
        }

        [HttpGet]
        public ActionResult Menu(Int16 id = 0)
        {
            var model = new MenuModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceMenu.GetMenuId(id);  
            }

            model.menus = ServiceMenu.getMenu();

            return View(model);
        }


        public ActionResult MenuDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceMenu.DeleteMenuId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "Meni/Menu");
        }

    }
}
