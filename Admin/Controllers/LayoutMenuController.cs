using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Service;
using Data.Entities;


namespace Admin.Controllers
{
    public class LayoutMenuController : Controller
    {
        [ChildActionOnly]
        public ActionResult MenuHeader()
        {
            var msg = "Verifique-Permissão";
            List<User> lstUser = new List<Data.Entities.User>();
            lstUser = ServiceUsuario.getUsuariobyEmail(User.Identity.Name);
            Int16 tipoid=0;

            if (lstUser.Count ==1)
            {
                foreach (var item in lstUser)
                {
                    tipoid = item.usuariotipoid;
                }
            }

            //busca o menu
            List<Data.Entities.Menu> lstMn = new List<Data.Entities.Menu>();
            lstMn = Domain.Service.ServiceMenu.getMenu();

            string path = Request.Path;
            int posic = path.IndexOf("/", 1);
            if (posic > 0)
            {
                string actSolic = path.Substring(1, posic - 1);


                //busca os usuariostipos que contém os menus permitidos
                List<Data.Entities.UsuarioMenu> lstTp = new List<UsuarioMenu>();
                lstTp = ServiceUsuarioMenu.getUsuarioMenuByTipoId(tipoid);

                //pega somente os ids
                var lstMenuIdValido = from id in lstTp select id.menuid;

                //compara os ids permitidos com os todos o menus e pega somenteos permitidos
                var lstFinal = (from c in lstMn where lstMenuIdValido.Contains(c.menuid) select c).ToList();

                ViewData["result"] = lstFinal;

                //verifica se o usuário pode acessar esta página ou se está forçando na mão
                var permissao = from lst in lstFinal where lst.controller == actSolic select lst;

                //se não tem permissao, verifica se é um submenu 
                if (!permissao.Any())
                {
                    //verifica se é um submenu no qual ele tem permissão no meu principal
                    //se tiver, deixa entrar.
                    List<MenuSub> lstSub = new List<MenuSub>();
                    lstSub = ServiceMenuSub.getMenuSubByController(actSolic);
                    if (lstSub.Count == 0)
                    {
                        //se não encontrar aquele submenu, porque a solicitação é de um menu normal e não tem acesso
                        Response.Redirect(@Domain.Util.config.UrlSite + "Home/Index/0/0/" + msg);
                    }
                    else 
                    {
                        //se encontrou é porque realmente é um submenu, verifica se o usuário possui acesso ao menu daquele submenu
                        //pega o action do menu principal
                        var actmenu = (from lst1 in lstSub select lst1.menuact).FirstOrDefault();
                        
                        //verifica se o action está na lista de permissões - se não estiver é  porque não tem permissão e forçou acesso.
                        var permissaosub = (from lst2 in lstFinal where lst2.controller == actmenu select lst2.controller).FirstOrDefault();
                        if (string.IsNullOrEmpty(permissaosub))
                        {
                            Response.Redirect(@Domain.Util.config.UrlSite + "Home/Index/0/0/" + msg);
                        }
                    }
                }
            }
            else
            {
                Response.Redirect(@Domain.Util.config.UrlSite + "Home/Index/0/0/" + msg);
            }
            return PartialView("MenuHeader");
        }

        [ChildActionOnly]
        public ActionResult VencimentoBuyHeader()
        {
            //List<Compra> lst = new List<Compra>();
            //Int16 repreid = Domain.Util.valida.getRepresentanteID(User.Identity.Name);
            //lst = ServiceCompra.getCompra(repreid,"a");

            //string dthj = Convert.ToString(DateTime.Now).Substring(0, 10);
            //var lsthj = (from x in lst where Convert.ToString(x.DtVencimento).Substring(0,10) == dthj select x.DtVencimento).ToList();

            //ViewBag.Vencimento = lst.Count;
            //ViewBag.VencimentoHoje = lsthj.Count;
            //ViewBag.repreid = repreid;

            return PartialView("VencimentoBuyHeader");
        }

        [ChildActionOnly]
        public ActionResult VencimentoClosingHeader()
        {
            //List<Fechamento> lst = new List<Fechamento>();
            //Int16 repreid = Domain.Util.valida.getRepresentanteID(User.Identity.Name);
            //lst = ServiceFechamento.getFechamento(repreid,"a");

            //string dthj = Convert.ToString(DateTime.Now).Substring(0, 10);
            //var lsthj = (from x in lst where Convert.ToString(x.dataRecebto).Substring(0, 10) == dthj select x.dataRecebto).ToList();

            //ViewBag.Vencimento = lst.Count;
            //ViewBag.VencimentoHoje = lsthj.Count;
            //ViewBag.repreid = repreid;

            return PartialView("VencimentoClosingHeader");
        }

        [ChildActionOnly]
        public ActionResult ApelidoHeader()
        {

            List<User> lstUser = new List<Data.Entities.User>();
            lstUser = ServiceUsuario.getUsuariobyEmail(User.Identity.Name);
            string user = "";

            if (lstUser.Count == 1)
            {
                foreach (var item in lstUser)
                {
                    user = item.Apelido;
                }
            }

            ViewBag.Apelido = user;

            return PartialView("ApelidoHeader");
        }

        [ChildActionOnly]
        public ActionResult ImagemHeader()
        {

            List<User> lstUser = new List<Data.Entities.User>();
            lstUser = ServiceUsuario.getUsuariobyEmail(User.Identity.Name);
            string img = "";

            if (lstUser.Count == 1)
            {
                foreach (var item in lstUser)
                {
                    img = item.imagem;
                }
            }

            if (string.IsNullOrEmpty(img))
            {
                img = "user-default.jpg";            
            }

            ViewBag.Img = img;

            return PartialView("ImagemHeader");
        }

        [ChildActionOnly]
        public ActionResult DataInclHeader()
        {

            List<User> lstUser = new List<Data.Entities.User>();
            lstUser = ServiceUsuario.getUsuariobyEmail(User.Identity.Name);
            string dataincl = "";
            string mes = "";
            int userid = 0;
            if (lstUser.Count == 1)
            {
                foreach (var item in lstUser)
                {
                    mes = Convert.ToString(item.DataIncl.ToString("MMMM"));
                    mes = (char.ToUpper(mes[0]) + mes.Substring(1));
                    dataincl = Convert.ToString(mes + " de " + item.DataIncl.Year);

                    userid = item.UserId;
                }
            }

            ViewBag.dataincl = dataincl;

            return PartialView("DataInclHeader");
        }

        //pega o usuárioid logado
        [ChildActionOnly]
        public ActionResult UserIdHeader()
        {
            List<User> lst = new List<Data.Entities.User>();

            lst = ServiceUsuario.getUsuariobyEmail(User.Identity.Name);
            int userid = 0;
            
            foreach (var item in lst)
            {
                userid = item.UserId;
            }

            ViewBag.userid = userid;

            return PartialView("UserIdHeader");
        }


    }
}
