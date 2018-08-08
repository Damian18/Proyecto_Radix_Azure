using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_RadixWeb.Models
{
    public class MultiplesClases
    {

        public IdentitySample.Models.RegisterViewModel objRegistrar { get; set; }
        public IdentitySample.Models.RoleViewModel objRoles { get; set; }
        public IdentitySample.Models.LoginViewModel objLogin { get; set; }
        public Proyecto_RadixWeb.Models.empresas objEmpresas { get; set; }
        public Proyecto_RadixWeb.Models.login objLogin_v2 { get; set; }
        public Proyecto_RadixWeb.Models.subempresas objSubEmpresas { get; set; }
        public Proyecto_RadixWeb.Models.regiones objRegiones { get; set; }
        public Proyecto_RadixWeb.Models.provincias objProvincias { get; set; }
        public Proyecto_RadixWeb.Models.comunas objComunas { get; set; }
        public Proyecto_RadixWeb.Models.cargos objCargos { get; set; }
        public Proyecto_RadixWeb.Models.personas objPersonas { get; set; }


    }
}