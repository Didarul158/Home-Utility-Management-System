//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeUtilityProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOCATION_TABLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOCATION_TABLE()
        {
            this.TECHNICIAN_TABLE = new HashSet<TECHNICIAN_TABLE>();
            this.USER_TABLE = new HashSet<USER_TABLE>();
        }
    
        public int LOCATION_ID { get; set; }
        public string LOCATION_NAME { get; set; }
        public string STATUS { get; set; }
        public int FLAG_1 { get; set; }
        public string FLAG_2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TECHNICIAN_TABLE> TECHNICIAN_TABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_TABLE> USER_TABLE { get; set; }
    }
}
