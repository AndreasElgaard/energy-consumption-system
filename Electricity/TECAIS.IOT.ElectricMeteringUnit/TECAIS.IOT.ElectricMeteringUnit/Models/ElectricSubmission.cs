using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.IOT.ElectricMeteringUnit.Models
{
    public class ElectricSubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float ElectricityComsumption { get; set; }
    }
}
