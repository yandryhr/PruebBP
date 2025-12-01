using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Infrastructure.Commons.Bases.Request
{
    public class AccounStatusRequest
    {
        public long ClientId { get; set; }
        public string EndDate { get; set; } = null;
        public string StartDate { get; set; } = null;
    }
}
