using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class Rating
    {
        public Guid RatingsId { get; set; }
        public decimal? Score { get; set; }
        public string Comment { get; set; }
        public Guid? CaseId { get; set; }
    }
}
