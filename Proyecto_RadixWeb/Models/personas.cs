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
    
    public partial class personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public personas()
        {
            this.login = new HashSet<login>();
            this.contratos = new HashSet<contratos>();
        }
    
        public string Per_Rut { get; set; }
        public string Per_Nom { get; set; }
        public string Per_ApePat { get; set; }
        public string Per_ApeMat { get; set; }
        public string Per_Dir { get; set; }
        public string Per_Tel { get; set; }
        public int Nac_Id { get; set; }
        public int Gen_Id { get; set; }
        public int EC_Id { get; set; }
        public int Car_Id { get; set; }
        public string Per_Suel { get; set; }
        public Nullable<int> TImp_Id { get; set; }
        public string Per_Des { get; set; }
        public Nullable<int> Desc_Id { get; set; }
        public Nullable<int> THor_Id { get; set; }
    
        public virtual estadosciviles estadosciviles { get; set; }
        public virtual fichadescuentos fichadescuentos { get; set; }
        public virtual generos generos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<login> login { get; set; }
        public virtual nacionalidades nacionalidades { get; set; }
        public virtual tiposhorasextras tiposhorasextras { get; set; }
        public virtual tipoimpuestos tipoimpuestos { get; set; }
        public virtual cargos cargos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contratos> contratos { get; set; }
    }
}
