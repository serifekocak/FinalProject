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
        // Çalışırken Business/DependebcyResolvers/AutoFac/AutoFacBusinessModule classı IProductService için bir ProductManager nesnesi, (ProductManager, 
        // IPrductDal nesnesine bağlı olduğu için) IProductDal için de bir EfProductDal nesnesi verir. 
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        } 

        // Allias --> Birden fazla GET metodu olduğunda istekte bulunurken api hangi metodu çalıştıracağını bilemez. Bunun için allias veririz.
        // API çağırırken --> https://localhost:44335/api/products/getall
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            // IProductService productService = new ProductManager(new EfProductDal());  --> Dependency chain (Bağımlılık zinciri) --> iki somut bağlılık (ProductManager, EfProductDal)
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // API çağırırken --> https://localhost:44335/api/products/getbyid?id=1
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
