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
    public class ServiceCliente
    {
        public static void InsertCliente(ClienteModelView model)
        {
            Cliente objretorno = new Cliente();

            //faz o de para: objModelView para objEntity 
            Mapper.CreateMap<ClienteModelView, Cliente>();
            var objtpprod = Mapper.Map<Cliente>(model);

            objtpprod.DataIncl = DateTime.Now;
            ClienteRepository tpprod = new ClienteRepository();
            tpprod.Add(objtpprod);
            tpprod.Save();
        }

        public static void UpdateCliente(ClienteModelView model)
        {
            Cliente objretorno = new Cliente();

            //faz o de para: objModelView para objEntity 
            Mapper.CreateMap<ClienteModelView, Cliente>();
            var objtpprod = Mapper.Map<Cliente>(model);

            objtpprod.Dataalt = DateTime.Now;
            ClienteRepository tpprod = new ClienteRepository();
            tpprod.Edit(objtpprod);
            tpprod.Save();
        }


        public static List<Cliente> getCliente(bool visivel)
        {
            //busca no banco
            ClienteRepository tprep = new ClienteRepository();
            var lst = tprep.Search(x => x.Status == 1).ToList();

            return lst;
        }

        public static List<Cliente> getCliente()
        {
            //busca no banco
            ClienteRepository tprep = new ClienteRepository();
            var lst = tprep.Search(x => x.Status != 0).ToList();

            return lst;
        }

        

        public static List<Cliente> getClienteCombo()
        {
            //busca no banco
            ClienteRepository tprep = new ClienteRepository();
            List<Cliente> lst = new List<Cliente>();
            Cliente obj = new Cliente();
            obj.Fantasia = "-";
            lst.Add(obj);

            return lst;
        }

        

        //get produto ID
        public static ClienteModelView GetClienteId(Int16 id)
        {
            Cliente objretorno = new Cliente();

            ClienteRepository tpprod = new ClienteRepository();
            objretorno = tpprod.Find(id);

            Mapper
                .CreateMap<Cliente, ClienteModelView>();
                //.ForMember(x => x.imagem, option => option.Ignore());
            var vretorno = Mapper.Map<ClienteModelView>(objretorno);

            //vretorno.arquivoimagem = img;

            return vretorno;
        }


        //delete tipo produto
        public static void DeleteClienteId(Int16 id)
        {
            //busca o arquivo q sera apagado
            Cliente objretorno = new Cliente();
            ClienteRepository tpprod = new ClienteRepository();
            objretorno = tpprod.Find(id);

            //passa a entidade recuperada para deletar
            tpprod.Delete(objretorno);
            tpprod.Save();
        }

    }
}
