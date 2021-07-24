using System;
using System.Collections.Generic;
using System.Text;

namespace TECAIS.IOT.HeatMeteringUnit.Models
{
    public class StatusSubmission
    {
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime TimeOfStatus { get; set; }
    }
}
