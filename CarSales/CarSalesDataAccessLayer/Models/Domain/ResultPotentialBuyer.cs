using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSalesDataAccessLayer.Models.Domain
{
    public class ResultPotentialBuyer
    {
        public string BuyerName { get; set; }
        public decimal Amount { get; set; }
        public string StatusName { get; set; }
        public DateTime LogDate { get; set; }
        public string RegistrationNumber { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }

    }
}
