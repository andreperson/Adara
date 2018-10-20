using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("login")]
    public class Login
    {
        public Int16 loginid { get; set; }
        public Int16 userid { get; set; }
        public string apelido { get; set; }
        public string email { get; set; }
        public string origem { get; set; }
        public DateTime? Dataalt { get; set; }
    }
}
