using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing; 
using Data.Repository;
using Domain.ModelView;
using Domain.Service;

namespace Admin.Controllers
{
    public class ListController : Controller
    {
        [HttpPost]
        public ActionResult Lista()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            return View();
        }

 

        [HttpGet]
        public ActionResult Lista(Int16 id = 0, Int16 id2 = 0, string id3 = "")
        {
            ViewBag.erro = id3;

            return View();
        }
        
    }
}
