using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalInsuranceService.Models
{
    public class Order
    {
        private Customer _customer;
        private Product _product;

        //public Order()
        //{
        //    _customer = new Customer();
        //    _product = new Product();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOPurchase { get; set; }
        public int Tenure { get; set; }
    }
}
