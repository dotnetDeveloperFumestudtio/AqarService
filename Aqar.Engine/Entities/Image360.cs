//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aqar.Engine.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Image360
    {
        public int Id { get; set; }
        public Nullable<int> PropertyId { get; set; }
        public Nullable<int> CategoryType { get; set; }
        public string Thumb { get; set; }
        public string Image { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
