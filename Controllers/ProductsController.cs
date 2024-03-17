using ApiBeginner.Data;
using ApiBeginner.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBeginner.Controllers
{
    [ApiController]
    [ Route("[controller]")]
    [Authorize]

    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext dbContext,ILogger<ProductsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var GetAll =_dbContext.Set<Product>().ToList();
            return Ok(GetAll);
        }
        [HttpGet]
        [Route("{id}")]
        [SensitiveFilter]

        public ActionResult GetById( int id)
        {
            _logger.LogDebug("get product has #{id}", id);
            var ProductFromDb = _dbContext.Find<Product>(id);
            _logger.LogWarning("product wit #{id} not found-----------#{y}", id,DateTime.Now);
            return ProductFromDb== null? NotFound() :Ok(ProductFromDb);
        }
        [HttpPost]
        [Route("")]

        public  ActionResult<int> CreateProduct([FromQuery]Product product, 
            [FromHeader(Name = "Accept-Language")]string language)
        {
            product.Id = 0;
            _dbContext.Add(product);
            _dbContext.SaveChanges();
            return Ok(product.Id);
        }

        [HttpPut]
        [Route("")]
        public ActionResult UpdateProduct (Product product)
        {
            if(product != null) {
              
                var ProductFromDb= _dbContext.Find<Product>(product.Id);
                ProductFromDb.Name=product.Name;
                ProductFromDb.Sku=product.Sku;
                _dbContext.Set<Product>().Update(ProductFromDb);
                _dbContext.SaveChanges();
                return Ok("Updated");
            }
            return NotFound("Product Not Found");
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete (int id)
        {
            if (id != null)
            {

                var ProductFromDb = _dbContext.Find<Product>(id);
                _dbContext.Set<Product>().Remove(ProductFromDb);
                _dbContext.SaveChanges();
                return Ok("Deleted");
            }
            return NotFound("Product Not Found");

        }
    }
}
