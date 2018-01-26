using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult Index()
        {
            var customers = new List<Customer>
            {
                    new Customer {Id = 1, Name = "John Smith"},
                    new Customer {Id = 2, Name = "Mary Williams"}
            };


            return View( customers );
        }

        public ActionResult Details( int id )
        {
            if( id != 1 && id != 2 )
            {
                return HttpNotFound();
            }

            var customer = new Customer
            {
                Id = id,
                Name = id == 1 ? "John Smith" : "Mary Williams"
            };

            return View( customer );
        }
    }
}