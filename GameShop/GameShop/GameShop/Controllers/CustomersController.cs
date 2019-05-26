using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameShop.Models;
using GameShop.ViewModels;
using System.Data.Entity;

namespace GameShop.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            //Data will be retrieved using WebAPI, no need for this now
            // var customers = _context.Customers.Include(c => c.MembershipType).ToList();// GetCustomers();

            //return View(customers);
            return View();
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.CustomerType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var customerTypes = _context.CustomerTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                CustomerTypes = customerTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            //Validations
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    CustomerTypes = _context.CustomerTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //Using AutoMapper tool
                //Mapper.map(customer,customerInDb)
                customerInDb.Name = customer.Name;
                customerInDb.CustomerTypeId = customer.CustomerTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //Not recommended way because of magic strings and not reliable 
                //TryUpdateModel(customerInDb,"",new string[] { "Name", "Email" });
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                CustomerTypes = _context.CustomerTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }


    }
}