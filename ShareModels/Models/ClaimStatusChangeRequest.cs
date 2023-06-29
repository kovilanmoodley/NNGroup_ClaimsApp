using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class ClaimStatusChangeRequest
    {
        public int ClaimID { get; set; }  
        public int ID { get; set; }  //can be either ClientID or EmployeeID

    }
}
