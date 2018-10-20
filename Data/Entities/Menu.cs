using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("menu")]
    public class Menu
    {
        public Int16 menuid { get; set; }
        public string descricao { get; set; }
        public string icone { get; set; }
        public string controller { get; set; }
        public string view { get; set; }
        public int ordem { get; set; }
        public int status { get; set; }
        public DateTime? dataalt { get; set; }
        public string user { get; set; }
    }
}
