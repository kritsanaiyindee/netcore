using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class CCode
    {
        public int Id { get; set; }
        public string CCode1 { get; set; }
        public string CDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }
}
