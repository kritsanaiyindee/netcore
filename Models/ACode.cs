using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class ACode
    {
        public int Id { get; set; }
        public string ACode1 { get; set; }
        public string ADesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }
}
