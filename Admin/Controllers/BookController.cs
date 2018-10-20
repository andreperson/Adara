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
    public class BookController : Controller
    {
        [HttpPost]
        public ActionResult talao(TalaoModelView model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", null);
            }

            Int16 repreid = Domain.Util.valida.getRepresentanteID(User.Identity.Name);

            if (ModelState.IsValid)
            {
                model.user = User.Identity.Name;
                model.status = Convert.ToInt16(model.statusb);
                var msg = string.Empty;
                if (model.talaoid != 0) //update
                {
                    //busca os Taloes conforme inicio e final para fazer alteração - so pode alterar o status e a obs
                    List<Talao> lst = new List<Data.Entities.Talao>();
                    TalaoModelView msalvo = new TalaoModelView();
                    msalvo = ServiceTalao.GetTalaoId(model.talaoid);
                    msg = Validatalao(msalvo);
                    if (string.IsNullOrEmpty(msg))
                    {
                        if (msalvo != null)
                        {
                            //alteracoes no pai
                            msalvo.dataalt = DateTime.Now;
                            msalvo.user = User.Identity.Name;
                            msalvo.obs = model.obs;
                            msalvo.status = model.status;
                            ServiceTalao.UpdateTalao(msalvo);

                            //altera o status dos filhos
                            List<TalaoItens> lstItens = new List<TalaoItens>();
                            lstItens = ServiceTalaoItens.GetTalaoItens(model.talaoid);

                            TalaoItensModelView mitens = new TalaoItensModelView();
                            foreach (var item in lstItens)
                            {
                                mitens = new TalaoItensModelView();
                                mitens.talaoitensid = item.talaoitensid;
                                mitens.talaoitensstatusid = item.talaoitensstatusid;
                                mitens.talaoid = item.talaoid;
                                mitens.dataalt = DateTime.Now;
                                mitens.dataincl = item.DataIncl;
                                mitens.numero = item.Numero;
                                mitens.obs = item.Obs;
                                mitens.representanteid = item.representanteid;
                                mitens.status = model.status;
                                mitens.user = User.Identity.Name;
                                ServiceTalaoItens.UpdateTalaoItens(mitens);
                            }

                        }

                    }
                }
                else //insert
                {
                    string valida = Validatalao(model);
                    if (!string.IsNullOrEmpty(valida))
                    {
                        return Redirect(Domain.Util.config.UrlSite + "Book/talao/0/0/" + valida);
                    }
                    else
                    {
                        //insere o pai
                        ServiceTalao.InsertTalao(model);
                        //insere os filhos conforme numeração informada.
                        TalaoItensModelView mitens = new TalaoItensModelView();

                        //pega o ultimo talaoid
                        model.talaoid = ServiceTalao.getTalaoMax(model.representanteid);

                        if (model.talaoid != 0)
                        {
                            for (int i = model.ini; i <= model.fin; i++)
                            {
                                mitens = new TalaoItensModelView();
                                mitens.numero = i;
                                mitens.talaoid = model.talaoid;
                                mitens.representanteid = model.representanteid;
                                mitens.status = 1;
                                mitens.user = User.Identity.Name;
                                mitens.talaoitensstatusid = 1;
                                ServiceTalaoItens.InsertTalaoItens(mitens);
                            }

                            msg = "talao-inserido-com-sucesso!";
                        }
                        
                    }
                }
                return Redirect(Domain.Util.config.UrlSite + "Book/talao/0/0/" + msg);

            }

            ViewBag.repreid = repreid;
            model.Representantes = ServiceRepresentante.getRepresentante(repreid);
            model.Taloes = ServiceTalao.getTalao(repreid, false);

            return View(model);
        }

        /// <summary>
        /// verifica se o intervalo não compreende um já existente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static string Validatalao(TalaoModelView model)
        {
            string ret = string.Empty;
            //verifica se este intervalo está em uso.
            List<Talao> lst = new List<Data.Entities.Talao>();
            lst = ServiceTalao.getTalao(model.representanteid, true);

            foreach (var item in lst)
            {
                if (item.talaoid != model.talaoid)
                {
                    if (model.fin < item.ini | model.ini > item.fin)
                    {
                    }
                    else
                    {
                        //intervalo errado
                        ret = "talaoID-" + item.talaoid + "-de-" + item.ini + "-a-" + item.fin + "-ocupa-o-intervalo-informado";
                        break;
                    }
                }
            }

            return ret;

        }


        [HttpGet]
        public ActionResult talao(Int16 id = 0, Int16 id2 = 0, string id3 = "")//repreid, talaoid, inicio
        {
            var model = new TalaoModelView();
            Int16 repreid = Domain.Util.valida.getRepresentanteID(User.Identity.Name);
            if (!string.IsNullOrEmpty(id3))
            {
                ViewBag.erro = id3.Replace("-", " ");
            }

            if (id != 0 & id2 != 0)
            {
                //busca as informações para edição
                model = ServiceTalao.GetTalaoId(id2);
                model.Representantes = ServiceRepresentante.getRepresentante(id);
                model.Taloes = GetTaloes(id);
            }
            else
            {
                model.Representantes = ServiceRepresentante.getRepresentante(repreid);
                model.Taloes = GetTaloes(repreid);
            }

            ViewBag.repreid = repreid;

            return View(model);
        }

        //public JsonResult GetTaloes(Int16 id = 0)//repre
        //{
        //    return Json(Servicetalao.gettalao(id,false), JsonRequestBehavior.AllowGet);
        //}

        //pega os Taloes
        private static List<Talao> GetTaloes(Int16 repreid )//repre
        {
            return ServiceTalao.getTalao(repreid, false);
        }

        public JsonResult GetIntervalo(Int16 id = 0, Int16 id2 = 0, string id3 = "")//repre
        {
            TalaoModelView model = new TalaoModelView();
            model.representanteid = id;
            model.ini = id2;
            model.fin = Convert.ToInt32(id3);

            return Json(Validatalao(model), JsonRequestBehavior.AllowGet);
        }

        public ActionResult talaoDelete(Int16 id = 0)
        {
            if (id != 0)
            {
                //exclui registro
                ServiceTalao.DeleteTalaoId(id);
            }

            return Redirect(Domain.Util.config.UrlSite + "Book/talao");
        }
    }
}
