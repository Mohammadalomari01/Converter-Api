using System;
using System.Collections.Generic;

namespace Converter.Core.Models
{
    public partial class User
    {
        public decimal Userid { get; set; }
        public string? Fullname { get; set; }
        public decimal? Phonenumber { get; set; }
        public decimal? Userloginid { get; set; }

        public virtual Userlogin? Userlogin { get; set; }
    }
}
