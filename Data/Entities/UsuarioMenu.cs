using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("usuariomenu")]
    public class UsuarioMenu
    {
        public Int16 usuariomenuid { get; set; }

        public Int16 usuariotipoid { get; set; }
        [ForeignKey("usuariotipoid")]
        public virtual UsuarioTipo UsuarioTipo { get; set; }

        public Int16 menuid { get; set; }
        [ForeignKey("menuid")]
        public virtual Menu Menu { get; set; }

        public int status { get; set; }
        public DateTime? Dataalt { get; set; }
        public string user { get; set; }
    }
}
