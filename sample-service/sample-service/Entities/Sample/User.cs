using System;
using System.Collections.Generic;

namespace sample_service.Entities.Sample
{
    public partial class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
