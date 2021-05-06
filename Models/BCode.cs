using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class BCode
    {
        public int Id { get; set; }
        public string BCode1 { get; set; }
        public string BDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }
}
