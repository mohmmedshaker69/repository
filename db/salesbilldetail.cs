//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iti.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class salesbilldetail
    {
        public int id { get; set; }
        public Nullable<int> productid { get; set; }
        public Nullable<int> salesbillid { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> totalprice { get; set; }
    
        public virtual salesbill salesbill { get; set; }
    }
}
