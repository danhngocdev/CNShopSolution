using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CNShopSolution.App.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNShopSolution.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IPuclicProductService _publicProductService;

        public ProductController(IPuclicProductService puclicProductService)
        {
            _publicProductService = puclicProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }
    }
}