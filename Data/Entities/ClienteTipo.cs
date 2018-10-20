﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("clientetipo")]
    public class ClienteTipo
    {
        public Int16 clientetipoid { get; set; }
        public string descricao { get; set; }
        public int Status { get; set; }
        public DateTime? Dataalt { get; set; }
        public string User { get; set; }
    }
}
