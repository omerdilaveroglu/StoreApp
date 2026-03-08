using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository _storeRepository;

    public int pageSize = 3;

    public HomeController(IStoreRepository repository)
    {
        _storeRepository = repository;
    }
 
    
    public IActionResult Index(int page = 1)
    {
        var products = _storeRepository
            .Products
            .Skip((page - 1) * pageSize)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category

            }).Take(pageSize);

        return View(new ProductListViewModel { Products = products ,
            PageInfo = new PageInfo
            {
                TotalItems = _storeRepository.Products.Count(),
                ItemsPerPage = pageSize
            }
        });
    }
}