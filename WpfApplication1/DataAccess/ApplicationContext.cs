using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Models;

namespace WpfApplication1.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=ApplicationContext2")  
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
