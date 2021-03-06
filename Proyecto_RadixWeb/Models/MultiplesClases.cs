﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_RadixWeb.Models
{
    public class MultiplesClases
    {
        //logeo
        //resgitrar
        public IdentitySample.Models.RegisterViewModel ObjRegistrar { get; set; }
        public IdentitySample.Models.RoleViewModel ObjRoles { get; set; }
        public IdentitySample.Models.LoginViewModel ObjLogin { get; set; }

        public Proyecto_RadixWeb.Models.AspNetUserRoles ObjAspnetUserRoles { get; set; }
        public Proyecto_RadixWeb.Models.empresas ObjEmpresas { get; set; }
        public Proyecto_RadixWeb.Models.login ObjLogin_v2 { get; set; }
        public Proyecto_RadixWeb.Models.subempresas ObjSubEmpresas { get; set; }
        public Proyecto_RadixWeb.Models.CuentasAndroid ObjCuentasAndroid { get; set; }
     
        //insertar
        public Proyecto_RadixWeb.Models.regiones ObjRegiones { get; set; }
        public Proyecto_RadixWeb.Models.provincias ObjProvincias { get; set; }
        public Proyecto_RadixWeb.Models.comunas ObjComunas { get; set; }
        public Proyecto_RadixWeb.Models.cargos ObjCargos { get; set; }
        public Proyecto_RadixWeb.Models.personas ObjPersonas { get; set; }
        public Proyecto_RadixWeb.Models.asistencias ObjAsistencia { get; set; }
        public Proyecto_RadixWeb.Models.contratos ObjContrato { get; set; }
        public Proyecto_RadixWeb.Models.empresa_cargo ObjEmpresa_Cargo { get; set; }
        public Proyecto_RadixWeb.Models.planillascontratos ObjPlantillascontratos { get; set; }
        public Proyecto_RadixWeb.Models.documentos ObjDocumentos { get; set; }
        public Proyecto_RadixWeb.Models.Sectores ObjSectores { get; set; }
        public Proyecto_RadixWeb.Models.PlanesEmpresas ObjPlanesEmpresa { get; set; }
        public Proyecto_RadixWeb.Models.PrivilegiosCuentas ObjPrivilegiosCuentas { get; set; }
        public Proyecto_RadixWeb.Models.Cuarteles ObjCuarteles { get; set; }
        public Proyecto_RadixWeb.Models.GruposCuarteles ObjGruposCuarteles { get; set; }
        public Proyecto_RadixWeb.Models.GrupoCuartelesDetalle ObjGruposCuartelesDetalle { get; set;}
        //listar
        public IEnumerable<Proyecto_RadixWeb.Models.subempresas> ObjESubEmpresas { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.personas> ObjEPersonas { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.asistencias> ObjEAsistencia { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.contratos> ObjEContrato { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.empresa_cargo> ObjEEmpresa_Cargo { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.Sectores> ObjESectores { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.cargos> ObjECargos { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.planillascontratos> ObjEPlantillascontratos { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.PlanesEmpresas> ObjEPlanesEmpresas { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.empresas> ObjEEmpresas { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.PrivilegiosCuentas> ObjEPrivilegiosCuentas { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.Cuarteles> ObjECuarteles { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.GruposCuarteles> ObjEGruposCuarteles { get; set; }
        public IEnumerable<Proyecto_RadixWeb.Models.GrupoCuartelesDetalle> ObjEGruposCuartelesDetalle { get; set; }
    }
}