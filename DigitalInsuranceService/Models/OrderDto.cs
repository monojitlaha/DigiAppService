using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalInsuranceService.Models
{
    public class OrderDto
    {
        public string Name { get; set; }
        public int Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public string Model { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public int Tenure { get; set; }
    }
}
