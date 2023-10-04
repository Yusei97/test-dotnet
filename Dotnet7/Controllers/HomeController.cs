using Dotnet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Dotnet7.Controllers
{
    public class HomeController : Controller
    {
        private DbContextOptionsBuilder<TestContext> optionBuilder;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            optionBuilder = new DbContextOptionsBuilder<TestContext>();
            //optionBuilder.UseSqlServer(DBConn.connString);
            optionBuilder.UseNpgsql(DBConn.connString);

            _logger = logger;
        }

        public IActionResult Index()
        {
            var newProd = new Product();
            List<Product> productList = new List<Product>();

            using (TestContext dbContext = new TestContext(optionBuilder.Options))
            {
                var prodList = dbContext.Product.ToArray();
                foreach (var prod in prodList)
                {
                    productList.Add(prod);
                }


               
                newProd.ProductName = "TEST";
                dbContext.Product.Add(newProd);
                dbContext.SaveChanges();

                //Customer customer = new Customer();
                //customer.CustomerName = "CY";
                //customer.CustomerActivated = true;
                //customer.CreatedDate = DateTime.UtcNow;
                //customer.Balance = 22.2M;

                //dbContext.Customer.Add(customer);
                //dbContext.SaveChanges();


            }


            return View("Index", productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}