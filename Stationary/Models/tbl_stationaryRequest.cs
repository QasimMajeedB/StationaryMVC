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
    
    public partial class tbl_stationaryRequest
    {
        public int st_reqid { get; set; }
        public Nullable<int> s_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string item_name { get; set; }
        public Nullable<double> req_qty { get; set; }
        public Nullable<double> req_totalprice { get; set; }
        public Nullable<System.DateTime> request_date { get; set; }
        public string req_status { get; set; }
        public Nullable<System.DateTime> approvecancel_date { get; set; }
    
        public virtual tbl_user tbl_user { get; set; }
        public virtual tbl_stock tbl_stock { get; set; }
    }
}
