using System;
using System.Collections.Generic;

namespace Converter.Core.Models
{
    public partial class Userlogin
    {
        public Userlogin()
        {
            Users = new HashSet<User>();
        }

        public decimal Userloginid { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public decimal? Roleid { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
