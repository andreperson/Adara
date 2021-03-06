﻿using System;
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
    public class UserMeniController : Controller
    {
        [HttpPost]
        public ActionResult UsuarioMenu(UsuarioMenuModelView model)
        {
            //insere via ajaxa, não faz nada por aqui
            return Redirect(Domain.Util.config.UrlSite + "Meni/Menu");
        }

        [HttpGet]
        public ActionResult UsuarioMenu(Int16 id = 0)
        {
            var model = new UsuarioMenuModelView();

            if (id != 0)
            {
                //busca as informações para edição
                model = ServiceUsuarioMenu.GetUsuarioMenuId(id);  
            }

            model.UsuariosTipos = ServiceUsuarioTipo.getUsuarioTipo();
            model.Menus = ServiceMenu.getMenu();
            model.UsuariosMenus = ServiceUsuarioMenu.getUsuarioMenu();
            return View(model);
        }


        public ActionResult UsuarioMenuDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceUsuarioMenu.DeleteUsuarioMenuId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "UserMeni/UsuarioMenu");
        }


        //insere as atividades do participante
        public void InsertAjax(List<string> data)
        {
            //posição 0 = tipoid - admin, padrão, etc
            //posição 1 = menuid
            UsuarioMenuModelView model = new UsuarioMenuModelView();
            string usuario = User.Identity.Name;
            //apaga todos
            Domain.Consumo.UsuarioMenuRepository.DeleteUsuarioMenuAll();
            for (int i = 0; i < data.Count; i++)
            {
                string[] menuarray = data[i].Split(new Char[] { ':' });
                model.usuariotipoid = Convert.ToInt16(menuarray[0]);
                model.menuid = Convert.ToInt16(menuarray[1]);
                model.status = 1;
                model.user = usuario;
                //insere um de cada vez.
                ServiceUsuarioMenu.InsertUsuarioMenu(model);
            }
        }

        //busca as permissoes 
        public JsonResult ListaPermissoes()
        {
            return Json(ServiceUsuarioMenu.getUsuarioMenu(), JsonRequestBehavior.AllowGet);
        }

    }
}
