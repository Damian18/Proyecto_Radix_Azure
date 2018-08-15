﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_RadixWeb.Models
{
    public class MultiplesClases
    {

        public IdentitySample.Models.RegisterViewModel ObjRegistrar { get; set; }
        public IdentitySample.Models.RoleViewModel ObjRoles { get; set; }
        public IdentitySample.Models.LoginViewModel ObjLogin { get; set; }
        
        public Proyecto_RadixWeb.Models.aspnetuserroles ObjAspnetUserRoles { get; set; }
        public Proyecto_RadixWeb.Models.empresas ObjEmpresas { get; set; }
        public Proyecto_RadixWeb.Models.login ObjLogin_v2 { get; set; }
        public Proyecto_RadixWeb.Models.subempresas ObjSubEmpresas { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.subempresas> ObjESubEmpresas { get; set; }

        public Proyecto_RadixWeb.Models.regiones ObjRegiones { get; set; }
        public Proyecto_RadixWeb.Models.provincias ObjProvincias { get; set; }
        public Proyecto_RadixWeb.Models.comunas ObjComunas { get; set; }
        public Proyecto_RadixWeb.Models.cargos ObjCargos { get; set; }
        public Proyecto_RadixWeb.Models.personas ObjPersonas { get; set; }


    }
}