using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductSearchDemoNET8.Models;
using System.Diagnostics;
using System.Linq;

namespace ProductSearchDemoNET8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<ProductViewModel> _productViewModels;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PrepareProductViewModels();

            return View(_productViewModels);
        }

        private void PrepareProductViewModels()
        {
            _productViewModels = Products.ProductList.Select(p => new ProductViewModel
            {
                Description = p.Description,
                ImagePath = p.ImagePath
            }).ToList();
        }

        [HttpGet]
        public IActionResult SearchProducts(string searchString)
        {
            PrepareProductViewModels();
            var searchResults = _productViewModels
                .Where(p => string.IsNullOrEmpty(searchString) || p.Description.ToLower().Contains(searchString.ToLower()))
                .Select(p => new ProductViewModel
                {
                    Description = p.Description,
                    ImagePath = p.ImagePath
                })
                .ToList();

            return Json(searchResults);
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
