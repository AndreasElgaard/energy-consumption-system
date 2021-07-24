using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.HeatStatusSubmissionService.Models
{
    public class StatusSubmission
    {
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime TimeOfStatus { get; set; }
    }
}
