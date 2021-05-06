using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class RoUser
    {
        public int Id { get; set; }
        public string UserMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Token { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }
    }
}
