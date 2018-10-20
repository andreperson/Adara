using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;  
using Data.Entities;

namespace Data.DataContext
{
    public class ConnDataContext : DbContext
    {
       

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteTipo> ClienteTipo { get; set; }

        public DbSet<Login> Login { get; set; }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuSub> MenuSub { get; set; }

        public DbSet<UF> UF { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UsuarioMenu> UsuarioMenu { get; set; }
        public DbSet<UsuarioTipo> UsuarioTipo { get; set; }
    }
}
