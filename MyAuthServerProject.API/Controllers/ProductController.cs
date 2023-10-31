using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Entities;
using MyAuthServerProject.Core.Services;

namespace MyAuthServerProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IServiceGeneric<Product, ProductDto> _genericService;

        public ProductController(IServiceGeneric<Product, ProductDto> genericService)
        {
            _genericService = genericService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var values = await _genericService.GetAllAsync();
            return ActionResultInstance(values);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto productDto)
        {
            var value = await _genericService.AddAsync(new ProductDto { Detail=productDto.Detail, Name=productDto.Name,Price=productDto.Price});
            return ActionResultInstance(value);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            var value = await _genericService.UpdateAsync(productDto, productDto.ProductId);
            return ActionResultInstance(value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var value = await _genericService.GetByFilterAsync(x=>x.ProductId==id);
            return ActionResultInstance(value);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var value = await _genericService.RemoveAsync(id);
            return ActionResultInstance(value);
        }
    }
}
