﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_RadixWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class radixEntities : DbContext
    {
        public radixEntities()
            : base("name=radixEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<asistencias> asistencias { get; set; }
        public virtual DbSet<aspnetroles> aspnetroles { get; set; }
        public virtual DbSet<aspnetuserclaims> aspnetuserclaims { get; set; }
        public virtual DbSet<aspnetuserlogins> aspnetuserlogins { get; set; }
        public virtual DbSet<aspnetusers> aspnetusers { get; set; }
        public virtual DbSet<bancos> bancos { get; set; }
        public virtual DbSet<bancostiposcuentas> bancostiposcuentas { get; set; }
        public virtual DbSet<cargos> cargos { get; set; }
        public virtual DbSet<comunas> comunas { get; set; }
        public virtual DbSet<contratos> contratos { get; set; }
        public virtual DbSet<departamentos> departamentos { get; set; }
        public virtual DbSet<documentos> documentos { get; set; }
        public virtual DbSet<empresa_cargo> empresa_cargo { get; set; }
        public virtual DbSet<empresas> empresas { get; set; }
        public virtual DbSet<estadosciviles> estadosciviles { get; set; }
        public virtual DbSet<fichadescuentos> fichadescuentos { get; set; }
        public virtual DbSet<fichasasistencias> fichasasistencias { get; set; }
        public virtual DbSet<generos> generos { get; set; }
        public virtual DbSet<horario_laboral> horario_laboral { get; set; }
        public virtual DbSet<login> login { get; set; }
        public virtual DbSet<nacionalidades> nacionalidades { get; set; }
        public virtual DbSet<pago> pago { get; set; }
        public virtual DbSet<personas> personas { get; set; }
        public virtual DbSet<planillascontratos> planillascontratos { get; set; }
        public virtual DbSet<provincias> provincias { get; set; }
        public virtual DbSet<regiones> regiones { get; set; }
        public virtual DbSet<subempresa_cargo> subempresa_cargo { get; set; }
        public virtual DbSet<subempresas> subempresas { get; set; }
        public virtual DbSet<tipoformaspago> tipoformaspago { get; set; }
        public virtual DbSet<tipoimpuestos> tipoimpuestos { get; set; }
        public virtual DbSet<tiposcontratos> tiposcontratos { get; set; }
        public virtual DbSet<tiposcuentas> tiposcuentas { get; set; }
        public virtual DbSet<tiposdescuentos> tiposdescuentos { get; set; }
        public virtual DbSet<tiposhorasextras> tiposhorasextras { get; set; }
        public virtual DbSet<tiposperiodos> tiposperiodos { get; set; }
    }
}
