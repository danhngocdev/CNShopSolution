using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CNShopSolution.App.Catalog.Products;
using CNShopSolution.ViewModel.Catalog.ProductImages;
using CNShopSolution.ViewModel.Catalog.Products;
using CNShopSolution.ViewModel.Catalog.Products.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CNShopSolution.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {

        private readonly IPuclicProductService _publicProductService;
        private readonly IManagedproductService _managedproductService;
        public ProductsController(IPuclicProductService puclicProductService,IManagedproductService managedproductService)
        {
            _publicProductService = puclicProductService;
            _managedproductService = managedproductService;
        }

      

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaing(string languageId,[FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(languageId,request);
            return Ok(products);
        }


        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId,string languageId)
        {
            var product = await _managedproductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _managedproductService.Create(request);

            if (productId == 0)
                return BadRequest();

            var product = await _managedproductService.GetById(productId, request.LanguageId);
            return CreatedAtAction(nameof(GetById),new { id = productId }, product);


        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _managedproductService.Update(request);

            if (affectedResult == 0)
                return BadRequest();

            return Ok();

        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {

            var affectedResult = await _managedproductService.Delete(productId);

            if (affectedResult == 0)
                return BadRequest();

            return Ok();

        }

        [HttpPatch("{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {

            var isSuccessful = await _managedproductService.UpdatePrice(id,newPrice);

            if (isSuccessful)
                return Ok ();

            return BadRequest();

        }


        //IMAGES



        [HttpGet("{productId}/image/{iamgeId}")]
        public async Task<IActionResult> GetImageById(int productId, int iamgeId)
        {
            var product = await _managedproductService.GetImageById(iamgeId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId,[FromForm]ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _managedproductService.AddImage(productId, request);

            if (imageId == 0)
            {
                return BadRequest();

            }
            var image = await _managedproductService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);

        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image= await _managedproductService.UpdateImage(imageId, request);

            if (image == 0)
            {
                return BadRequest();

            }


            return Ok();

        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image = await _managedproductService.RemoveImage(imageId);

            if (image == 0)
            {
                return BadRequest();

            }


            return Ok();

        }











    }
}