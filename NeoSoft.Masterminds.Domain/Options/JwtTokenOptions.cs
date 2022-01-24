using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSoft.Masterminds.Domain.Models.Options
{
    public class JwtTokenOptions
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int LifetimeMin { get; set; }
    }
}
