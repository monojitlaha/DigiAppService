using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigitalInsuranceService.Models;
using Microsoft.EntityFrameworkCore;


namespace DigitalInsuranceService.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly DatabaseContext _context;

        public OrderController(DatabaseContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IEnumerable<Order> orderDetails = _context.Order
                                                .Include(x=> x.Customer)
                                                .Include(y=>y.Product)
                                                .ToList();

            if (orderDetails == null)
                return NotFound();

            var orderDtoDetails = new List<OrderDto>();
            foreach(var orderDetail in orderDetails)
            {
                orderDtoDetails.Add(PopulateDtoObject(orderDetail));
            }

            return Ok(orderDtoDetails);
        }       
   

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetOrder([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Order orderDetail = _context.Order
                                .Include(x => x.Customer)
                                .Include(y => y.Product)
                                .FirstOrDefault(x=> x.OrderId==id);

            if (orderDetail == null)
                return NotFound();

            var orderDtoDetail = PopulateDtoObject(orderDetail);

            return Ok(orderDtoDetail);
        }

        // POST api/values
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody]OrderDto orderDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderInfo = PopulateData(orderDetails);

            _context.Order.Add(orderInfo);

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetOrderDetails", orderDetails);
        }       

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult UpdateOrder([FromRoute]int id, [FromBody]OrderDto orderDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderInfo = PopulateData(orderDetails);

            _context.Entry(orderInfo).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetOrderDetails", orderDetails);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Order orderDetail = _context.Order.FirstOrDefault(x => x.OrderId == id);

            if (orderDetail == null)
                return NotFound();

            _context.Order.Remove(orderDetail);
            _context.SaveChanges();

            return CreatedAtAction("GetOrderDetails", orderDetail);
        }

        private Order PopulateData(OrderDto orderDetails)
        {
            var orderInfo = new Order
            {
                Amount = orderDetails.Amount,
                DateOPurchase = DateTime.Now,
                Tenure = orderDetails.Tenure,
                Product = new Product
                {
                    Type = orderDetails.Type,
                    Subtype = orderDetails.Subtype,
                    Model = orderDetails.Model
                },
                Customer = new Customer
                {
                    Name = orderDetails.Name,
                    Contact = orderDetails.Contact,
                    Email = orderDetails.Email,
                    Address = orderDetails.Address,
                    City = orderDetails.City,
                    State = orderDetails.State,
                    Zipcode = orderDetails.Zipcode
                }
            };
            return orderInfo;
        }

        private OrderDto PopulateDtoObject(Order orderDeail)
        {
            var orderDto = new OrderDto
            {
                Amount = orderDeail.Amount,
                DateOfPurchase = orderDeail.DateOPurchase,
                Tenure = orderDeail.Tenure
            };
            if(orderDeail.Customer!=null)
            {
                orderDto.Name = orderDeail.Customer.Name;
                orderDto.Contact = orderDeail.Customer.Contact;
                orderDto.Email = orderDeail.Customer.Email;
                orderDto.Address = orderDeail.Customer.Address;
                orderDto.City = orderDeail.Customer.City;
                orderDto.State = orderDeail.Customer.State;
                orderDto.Zipcode = orderDeail.Customer.Zipcode;
            }
            if(orderDeail.Product!=null)
            {
                orderDto.Type = orderDeail.Product.Type;
                orderDto.Subtype = orderDeail.Product.Subtype;
                orderDto.Model = orderDeail.Product.Model;
            }
            return orderDto;
        }
    }
}
