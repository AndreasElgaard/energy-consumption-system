using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.AccountingService
{
    public class AccountingModel
    {
        public string ConsumptionType { get; set; }
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float Consumption { get; set; }
        public double Price { get; set; }
        public double TaxPrice { get; set; }
        [Key]
        public int AccountBillId { get; set; }
    }
}
