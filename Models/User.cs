using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class User
    {
        public Guid MobileId { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string ProfilePhoTo { get; set; }
        public string MobileToken { get; set; }
    }
}
