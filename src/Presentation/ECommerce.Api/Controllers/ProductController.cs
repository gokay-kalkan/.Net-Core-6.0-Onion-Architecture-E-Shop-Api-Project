using Application.Dtos.CategoryDtos;
using Application.Dtos.ProductDtos;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiResponse;
using System.Threading;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHostingEnvironment _env;

        public ProductController(IMediator mediator, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet("ProductList")]
        public async Task<IActionResult> GetProducts()
        {
            var query = new ListProductQuery();
            var products = await _mediator.Send(query);

            return Ok(new Response<List<ProductListDto>>(200, products, "Ürünler başarıyla getirildi."));
        }

        [HttpGet("ProductListByCategory/{categoryId}")]
        public async Task<IActionResult> GetProductListByCategory(int categoryId)
        {
            var query = new ProductListByCategoryQuery() { CategoryId = categoryId };
            var products = await _mediator.Send(query);

            var categoryName = products.Select(x => x.CategoryName).FirstOrDefault();

            return Ok(new Response<List<ProductListByCategoryDto>>(200, products, $"{categoryName} ait Ürünler başarıyla getirildi."));
        }

        [HttpGet("ProductGetOne/{id}")]
        public async Task<IActionResult> ProductGetOne(int id)
        {
            var query = new GetByIdProductQuery() { Id = id };
            var product = await _mediator.Send(query);

            return Ok(new Response<ProductGetByIdDto>(200, product, "Ürün başarıyla getirildi."));
        }

        [HttpDelete("ProductDelete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);

            return Ok(new Response<ProductDeleteDto>(200, result, "Ürün başarıyla silindi."));
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm]UpdateProductCommand command)
        {
            if (command.newImage != null && command.newImage.Length > 0)
            {
                var path = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, command.newImage.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await command.newImage.CopyToAsync(stream);
                }

                var returnPath = "photos/" + command.newImage.FileName;

               
            }
            var updatedProductDto = await _mediator.Send(command);
            
            return Ok(new Response<ProductUpdateDto>(200, updatedProductDto, "Ürün Başarıyla Güncellendi"));
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand command)
        {
            if (command.image != null && command.image.Length > 0)
            {
                var path = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, command.image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await command.image.CopyToAsync(stream);
                }

                var returnPath = "photos/" + command.image.FileName;

            }

            var createdProductDto = await _mediator.Send(command);

            return Ok(new Response<ProductCreateDto>(201, createdProductDto, "Ürün Başarıyla Eklendi"));
        }



    }
}
