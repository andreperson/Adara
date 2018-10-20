using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using System.Web;


namespace Domain.ModelView
{
    public class ClienteModelView
    {
        public Int16 clienteid { get; set; }
        public Int16 clientetipoid { get; set; }

        [Required(ErrorMessage = "Nome do Contato")]
        [Display(Name = "Nome do Contato")]
        public string NomeContato { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "RG")]
        public string RG { get; set; }

        [Display(Name = "DDD")]
        public string DDDCelular { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Fantasia")]
        [Display(Name = "Fantasia")]
        public string Fantasia { get; set; }

        [Display(Name = "Segmento")]
        public string Segmento { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Compl")]
        public string Compl { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        public string UF { get; set; }

        [Display(Name = "URL")]
        public string URL { get; set; }

        [Display(Name = "Email Comercial")]
        public string EmailCom { get; set; }

        [Display(Name = "DDD Fone Comercial")]
        public string DDDFoneComercial { get; set; }

        [Display(Name = "Fone Comercial")]
        public string FoneComercial { get; set; }

        [Display(Name = "Data Fundação da Empresa")]
        public DateTime dtFundacao { get; set; }

        [Display(Name = "Obs")]
        public string obs { get; set; }

        public DateTime? Dataalt { get; set; }
        public DateTime DataIncl { get; set; }
        public string user { get; set; }

        public int status { get; set; }
        public bool statusb { get; set; }

        public List<Cliente> Clientes { get; set; }
        public List<ClienteTipo> ClientesTipos { get; set; }
        public List<UF> UFs { get; set; }
        
    }
}
