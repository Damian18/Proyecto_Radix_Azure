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
    
    public partial class Sectores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sectores()
        {
            this.Cuarteles = new HashSet<Cuarteles>();
        }
    
        public int sect_id { get; set; }
        public string sect_nom { get; set; }
        public Nullable<int> Sub_Id { get; set; }
    
        public virtual subempresas subempresas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cuarteles> Cuarteles { get; set; }
    }
}
