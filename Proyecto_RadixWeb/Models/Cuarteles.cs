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
    
    public partial class Cuarteles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cuarteles()
        {
            this.GruposCuarteles = new HashSet<GruposCuarteles>();
        }
    
        public int cuar_id { get; set; }
        public string cuar_nom { get; set; }
        public Nullable<int> varfrut_id { get; set; }
        public Nullable<int> sect_id { get; set; }
    
        public virtual Sectores Sectores { get; set; }
        public virtual VariedadesFrutas VariedadesFrutas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GruposCuarteles> GruposCuarteles { get; set; }
    }
}