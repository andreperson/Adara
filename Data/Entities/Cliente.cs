using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("cliente")]
    public class Cliente
    {
        public Int16 clienteid { get; set; }
        public Int16 clientetipoid { get; set; }
        [ForeignKey("clientetipoid")]
        public virtual ClienteTipo ClienteTipo { get; set; }

        public string NomeContato { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string DDDCelular { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Compl { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string URL { get; set; }
        public string EmailCom { get; set; }
        public string DDDFoneComercial { get; set; }
        public string FoneComercial { get; set; }
        public string obs { get; set; }
        public int Status { get; set; }
        public DateTime? Dataalt { get; set; }
        public DateTime DataIncl { get; set; }
        public string user { get; set; }
    }
}
