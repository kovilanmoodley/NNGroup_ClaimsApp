using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class ClaimRequest
    {
        public int ClientID { get; set; }
        public string? ClaimDescription { get; set; }
        public double ClaimAmount { get; set; } = 0;

    }
}
