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
    public class ServiceUsuario
    {
        public static void InsertUsuario(UsuarioModelView model)
        {
            User objretorno = new User();

            //faz o de para: objModelView para objEntity 
            Mapper.CreateMap<UsuarioModelView, User>();
            var objtpprod = Mapper.Map<User>(model);
            objtpprod.DataIncl = DateTime.Now;

            ////pega o nome da Produto
            if (model.imagem != null)
            { objtpprod.imagem = model.imagem.FileName; }

            UserRepository tpprod = new UserRepository();
            tpprod.Add(objtpprod);
            tpprod.Save();
        }

        public static void UpdateUsuario(UsuarioModelView model)
        {
            User objretorno = new User();

            //faz o de para: objModelView para objEntity 
            Mapper.CreateMap<UsuarioModelView, User>();
            var objtpprod = Mapper.Map<User>(model);

            ////pega o nome da Produto
            if (model.imagem != null)
            {
                objtpprod.imagem= model.imagem.FileName;
            }
            else
            {
                objtpprod.imagem = model.arquivoimagem;
            }


            //objtpprod.dataincl = DateTime.Now;
            UserRepository tpprod = new UserRepository();
            tpprod.Edit(objtpprod);
            tpprod.Save();
        }


        
        public static List<User> getUsuario(Int16 id)
        {
            //busca no banco
            UserRepository tprep = new UserRepository();
            var lst = tprep.Search(x => x.UserId == id).ToList();

            return lst;
        }


        public static List<User> getUsuarioAll(Int16 repid)
        {
            //busca no banco
            UserRepository tprep = new UserRepository();
            List<User> lst = new List<User>();
            if (repid != 0)
            {
                lst = tprep.Search(x => x.usuariotipoid != 0).ToList();
            }
            else
            {
                lst = tprep.Search(x => x.usuariotipoid != 0).ToList();
            }
            var lstord = lst.OrderBy(y => y.Nome).ToList();

            return lstord;
        }


        //get produto ID
        public static UsuarioModelView GetUsuarioId(Int16 id)
        {
            User objretorno = new User();

            UserRepository tpprod = new UserRepository();
            objretorno = tpprod.Find(id);

            //faz o de para: objEntity para objModelView
            UsuarioModelView model = GetUserModelDePara(objretorno);

            return model;
        }


        
        public static List<User> getUsuariobyEmail(string email)
        {
            UserRepository tprep = new UserRepository();
            var lst = tprep.Search(x => x.Email == email).ToList();

            return lst;
        }

        private static UsuarioModelView GetUserModelDePara(User objprod)
        {
            UsuarioModelView model = new UsuarioModelView();
            model.userid = objprod.UserId;
            model.arquivoimagem = objprod.imagem;

            model.apelido = objprod.Apelido;
            model.dataalt = objprod.Dataalt;
            model.dataincl = objprod.DataIncl;
            model.email = objprod.Email;
            model.nome = objprod.Nome;
            model.senha = objprod.Senha;
            model.status = objprod.Status;
            model.usuariotipoid = objprod.usuariotipoid;
            

            return model;
        }


        //delete tipo produto
        public static void DeleteUsuarioId(Int16 id)
        {
            //busca o arquivo q sera apagado
            User objretorno = new User();
            UserRepository tpprod = new UserRepository();
            objretorno = tpprod.Find(id);

            //passa a entidade recuperada para deletar
            tpprod.Delete(objretorno);
            tpprod.Save();
        }

    }
}
