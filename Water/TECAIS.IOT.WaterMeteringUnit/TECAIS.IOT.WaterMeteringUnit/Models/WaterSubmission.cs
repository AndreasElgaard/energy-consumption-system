using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.IOT.WaterMeteringUnit.Models
{
    public class WaterSubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float WaterComsumption { get; set; }
    }
}
