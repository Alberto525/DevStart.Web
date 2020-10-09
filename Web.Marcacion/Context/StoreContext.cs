using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Marcacion.Models;

namespace Web.Marcacion.Context
{
    public class StoreContext : DbContext
    {

        public DbSet<T_Usuario> t_Usuarios { get; set; }
        public DbSet<T_Perfil> t_Perfils { get; set; }
        public DbSet<T_Empresa> t_empresas { get; set; }

    }
}