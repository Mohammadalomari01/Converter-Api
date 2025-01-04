using System;
using System.Collections.Generic;

namespace Converter.Core.Models
{
    public partial class Conversionhistory
    {
        public decimal Conversionid { get; set; }
        public decimal? Userid { get; set; }
        public string Sourcefile { get; set; } = null!;
        public string Outputfile { get; set; } = null!;
        public decimal? Filesize { get; set; }
        public string Status { get; set; } = null!;
    }
}
