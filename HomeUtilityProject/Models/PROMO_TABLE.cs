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
    
    public partial class PROMO_TABLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROMO_TABLE()
        {
            this.ORDER_TABLE = new HashSet<ORDER_TABLE>();
        }
    
        public int PROMO_ID { get; set; }
        public string PROMO_NAME { get; set; }
        public int DISCOUNT { get; set; }
        public string PURPOSE { get; set; }
        public string EXPIRED_DATE { get; set; }
        public int TIMES { get; set; }
        public string STATUS { get; set; }
        public int FLAG_1 { get; set; }
        public string FLAG_2 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_TABLE> ORDER_TABLE { get; set; }
    }
}
