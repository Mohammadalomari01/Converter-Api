using System;
using System.Collections.Generic;

namespace Converter.Core.Models
{
    public partial class Role
    {
        public Role()
        {
            Userlogins = new HashSet<Userlogin>();
        }

        public decimal Roleid { get; set; }
        public string? Rolename { get; set; }

        public virtual ICollection<Userlogin> Userlogins { get; set; }
    }
}
