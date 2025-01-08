using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Loosely coupled (gevşek bağlılık) --> bir bağlılık var ama soyut bir bağlılık
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public List<Product> Get()
        {
            // IProductService productService = new ProductManager(new EfProductDal());  --> Dependency chain (Bağımlılık zinciri) --> iki somut bağlılık (ProductManager, EfProductDal)
            var result = _productService.GetAll();
            return result.Data;
        }
    }
}
