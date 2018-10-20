using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ModelView;
using Data.Entities;
using Domain.Consumo;
using AutoMapper;


namespace Domain.Service
{
    public class ServiceMenuSub
    {
        public static void InsertMenuSub(MenuSubModelView model)
        {
            MenuSub objretorno = new MenuSub();

            //faz o de para: objModelView para objEntity 
            Mapper.CreateMap<MenuSubModelView, MenuSub>();
            var objtpprod = Mapper.Map<MenuSub>(model);

            MenuSubRepository tpprod = new MenuSubRepository();
            tpprod.Add(objtpprod);
            tpprod.Save();
        }

        public static void UpdateMenuSub(MenuSubModelView model)
        {
            MenuSub objretorno = new MenuSub();

            //faz o de para: objModelView para objEntity 
            Mapper.CreateMap<MenuSubModelView, MenuSub>();
            var objtpprod = Mapper.Map<MenuSub>(model);

            //objtpprod.dataincl = DateTime.Now;
            MenuSubRepository tpprod = new MenuSubRepository();
            tpprod.Edit(objtpprod);
            tpprod.Save();
        }


        
        public static List<MenuSub> getMenuSubByMenuId(Int16 id)
        {
            //busca no banco
            MenuSubRepository tprep = new MenuSubRepository();
            var lst = tprep.Search(x => x.menuid == id).ToList();
            var result = lst.OrderBy(y => y.ordem).ToList();
            return result;
        }

        
        public static List<MenuSub> getMenuSubByController(string ctr)
        {
            //busca no banco
            MenuSubRepository tprep = new MenuSubRepository();
            var lst = tprep.Search(x => x.controller == ctr).ToList();
            var result = lst.OrderBy(y => y.ordem).ToList();
            return result;
        }

        //get produto ID
        public static MenuSubModelView GetMenuSubId(Int16 id)
        {
            MenuSub objretorno = new MenuSub();

            MenuSubRepository tpprod = new MenuSubRepository();
            objretorno = tpprod.Find(id);

            //faz o de para: objEntity para objModelView
            Mapper
                .CreateMap<MenuSub, MenuSubModelView>();
                //.ForMember(x => x.imagem, option => option.Ignore());
            var vretorno = Mapper.Map<MenuSubModelView>(objretorno);

            return vretorno;
        }


        //delete tipo produto
        public static void DeleteMenuSubId(Int16 id)
        {
            //busca o arquivo q sera apagado
            MenuSub objretorno = new MenuSub();
            MenuSubRepository tpprod = new MenuSubRepository();
            objretorno = tpprod.Find(id);

            //passa a entidade recuperada para deletar
            tpprod.Delete(objretorno);
            tpprod.Save();
        }

    }
}
