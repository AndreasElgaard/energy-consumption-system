using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.IOT.HeatMeteringUnit.Models
{
    public class HeatSubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public double HeatComsumption { get; set; }
    }
}
