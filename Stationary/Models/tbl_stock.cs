//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stationary.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_stock
    {
        public tbl_stock()
        {
            this.tbl_stationaryRequest = new HashSet<tbl_stationaryRequest>();
        }
    
        public int s_id { get; set; }
        public string s_name { get; set; }
        public Nullable<double> s_price { get; set; }
        public Nullable<double> s_qty { get; set; }
        public Nullable<System.DateTime> s_date { get; set; }
        public string s_des { get; set; }
        public string s_status { get; set; }
    
        public virtual ICollection<tbl_stationaryRequest> tbl_stationaryRequest { get; set; }
    }
}
