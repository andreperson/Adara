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
    public class BookItensController : Controller
    {
        public ActionResult TalaoItensDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceTalaoItens.DeleteTalaoItensId(id); 
            }

            return Redirect(Domain.Util.config.UrlSite + "BookItens/TalaoItens");
        }

        [HttpGet]
        public ActionResult TalaoItens(Int16 id=0, Int16 id2=0, string id3="")//repreid, Talaoid, TalaoItensid
        {
            TalaoItensModelView model = new TalaoItensModelView();
            if (!string.IsNullOrEmpty(id3) & id3 != "0")
            {
                model = ServiceTalaoItens.GetTalaoItensId(Convert.ToInt16(id3)); 
            }

            if (id != 0 & id2 !=0)
            {
                model.Representantes = ServiceRepresentante.getRepresentante(id);
                model.TaloesItens = ServiceTalaoItens.GetTalaoItens(id2, id);
                model.TaloesItensStatus = ServiceTalaoItensStatus.getTalaoItensStatusCombo();
                model.Taloes = ServiceTalao.getTalaoid(id2);
            }

            //pega o representante
            ViewBag.repre = getRepre(model.Representantes);

            //pega o Talao ini e final
            getIntervalo(model.Taloes);

            ViewBag.situacao = Domain.Util.valida.getSituacao(model.talaoitensstatusid);
            return View(model);
        }


        //pega intervalod o Talao
        private void getIntervalo(List<Talao> lst)
        {
            foreach (var item in lst)
            {
                ViewBag.ini = item.ini;
                ViewBag.fin = item.fin;
            }
            
        }



        //pega o nome do representante
        private static string getRepre(List<Representante> lst)
        {
            string ret = string.Empty;

            foreach (var item in lst)
            {
                ret = item.Nome;
            }

            return ret;
        }

        

        [HttpPost]
        public ActionResult TalaoItens(TalaoItensModelView model)
        {
            if (ModelState.IsValid)
            {
                if (model.talaoitensid != 0)
                {
                    //update
                    //busca o ITempDataProvider completo
                    TalaoItensModelView msalvo = new TalaoItensModelView();
                    msalvo = ServiceTalaoItens.GetTalaoItensId(model.talaoitensid);

                    if (msalvo != null)
                    {
                        msalvo.obs = model.obs;
                        msalvo.status = Convert.ToInt16(model.statusb);
                        ServiceTalaoItens.UpdateTalaoItens(msalvo);

                        return Redirect(Domain.Util.config.UrlSite + "BookItens/TalaoItens/" + model.representanteid + "/" + model.talaoid);
                    }

                }
            }

        
            return View(model);
        }


        /// <summary>
        /// marca o financeiro como pago
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>

        public JsonResult StCheck(Int16 id = 0, Int16 id2 = 0, string id3 = "")//repreid, TalaoItensid
        {
            var newst = 0;

            if (id3 == "0")
                newst = 1;

            //busca o financeiro
            TalaoItensModelView model = new TalaoItensModelView();
            model.TaloesItens = ServiceTalaoItens.GetTalaoItemId(id2);

            if (model.TaloesItens.Count == 1)
            {
                foreach (var item in model.TaloesItens)
                {
                    model = new TalaoItensModelView();
                    model.talaoid = item.talaoid;
                    model.talaoitensid = item.talaoitensid;
                    model.dataalt = DateTime.Now;
                    model.dataincl = item.DataIncl;
                    model.numero = item.Numero;
                    model.obs = item.Obs;
                    model.representanteid = item.representanteid;
                    model.status = newst;
                    model.user = User.Identity.Name;

                    ServiceTalaoItens.UpdateTalaoItens(model);
                }

            }

            return Json(ServiceTalaoItens.GetTalaoItemId(id2), JsonRequestBehavior.AllowGet);
        }

    }
}
