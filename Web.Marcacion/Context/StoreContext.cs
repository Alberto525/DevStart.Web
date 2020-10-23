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
        public DbSet<T_TipoJornada> t_TipoJornadas{ get; set; }

        public DbSet<T_EmpleadoJornada> t_EmpleadoJornadas { get; set; }

        public DbSet<T_Persona> T_Personas { get; set; }
        public DbSet<T_TipoDocumento> T_TipoDocumentos { get; set; }
        public DbSet<T_Cargo> T_Cargos { get; set; }

    }
}