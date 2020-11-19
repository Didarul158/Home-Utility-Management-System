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
    
    public partial class ORDER_TABLE
    {
        public int ID { get; set; }
        public int TECHNICIAN_ID { get; set; }
        public int USER_ID { get; set; }
        public int SERVICE_ID { get; set; }
        public string REQUESTED_TIME { get; set; }
        public string FINISHED_TIME { get; set; }
        public string WORKING_STATUS { get; set; }
        public int PROMO_ID { get; set; }
        public int CALCULATED_WAGE { get; set; }
        public int NET_WAGE { get; set; }
        public int FLAG_1 { get; set; }
        public int FLAG_2 { get; set; }
        public string FLAG_3 { get; set; }
        public string FLAG_4 { get; set; }
    
        public virtual PROMO_TABLE PROMO_TABLE { get; set; }
        public virtual SERVICE_TABLE SERVICE_TABLE { get; set; }
        public virtual TECHNICIAN_TABLE TECHNICIAN_TABLE { get; set; }
        public virtual USER_TABLE USER_TABLE { get; set; }
    }
}