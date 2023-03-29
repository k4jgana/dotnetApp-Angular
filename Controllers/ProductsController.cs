using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController: Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IDutchRepository repository,ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get() 
        {
            try { return Ok(repository.GetProducts()); }
            catch {
                return BadRequest("Failed to get products");
            }
        }

    }
}
