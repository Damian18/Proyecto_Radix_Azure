//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class GrupoCuartelesDetalle
    {
        public int gcd_id { get; set; }
        public Nullable<int> gc_id { get; set; }
        public Nullable<int> Con_id { get; set; }
    
        public virtual contratos contratos { get; set; }
        public virtual GruposCuarteles GruposCuarteles { get; set; }
    }
}
