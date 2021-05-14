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

    public class ResultModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
    public class MyEntity
    {
        public string LogicalName { get; set; }
        public Guid Id { get; set; }
        //public Dictionary<string, Dictionary<string, object>> Fields { get; set; }
        public string Fields { get; set; }
    }
}
