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
    
    public partial class bancostiposcuentas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bancostiposcuentas()
        {
            this.pago = new HashSet<pago>();
        }
    
        public int BTC_Id { get; set; }
        public int Ban_Id { get; set; }
        public int TCuen_Id { get; set; }
    
        public virtual bancos bancos { get; set; }
        public virtual tiposcuentas tiposcuentas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pago> pago { get; set; }
    }
}
