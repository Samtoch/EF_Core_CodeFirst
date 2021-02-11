using EF_App.Models.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_Core_CodeFirst.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            using (var _context = new CustomerDbContext())
            {
                return _context.Customers.ToList();
            }
        }

        [HttpGet]
        public IEnumerable<Customer> Get(int id)
        {
            using (var _context = new CustomerDbContext())
            {
                return _context.Customers.Where(c => c.Id == id).ToList();
            }
        }

        [HttpPost]
        public IEnumerable<Customer> Create(Customer customer)
        {
            using (var _context = new CustomerDbContext())
            {
                //Customer customer = new Customer() { Age = 45, Name = "Ugochi Bush" };
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return _context.Customers.Where(c => c.Name == customer.Name).ToList();
            }
        }

        [HttpPost]
        public Customer Update(Customer customer)
        {
            using (var _context = new CustomerDbContext())
            {
                //Customer customer = new Customer() { Age = 45, Name = "Ugochi Bush" };
                Customer newRecord = _context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();
                newRecord.Name = customer.Name;
                newRecord.Age = customer.Age;

                _context.SaveChanges();

                return _context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();
            }
        }

        [HttpPost]
        public String Delete(Customer customer)
        {
            try
            {
                using (var _context = new CustomerDbContext())
                {
                    Customer newRecord = _context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();

                    _context.Remove(newRecord);
                    _context.SaveChanges();

                    return "Customer with id : " + customer.Id + " Successesfully Removed";
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.StackTrace);
                return "Deletion Failed";
                throw;
            }
        }
    }
}
