using System;
using System.Collections.Generic;

#nullable disable

namespace netcoreapi.Models
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public string ImageUrl { get; set; }
        public string ProfilePhoto { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public DateTime? Time { get; set; }
    }
}
