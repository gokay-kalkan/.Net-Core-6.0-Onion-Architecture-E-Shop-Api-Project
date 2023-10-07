using Application.Dtos.CategoryDtos;
using Application.Exceptions;
using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiResponse;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("CategoryList")]
        public async Task<IActionResult> GetCategories()
        {
            var query = new ListCategoryQuery();
            var categories = await _mediator.Send(query);

           
            return Ok(new Response<List<CategoryListDto>>(200, categories, "Kategoriler başarıyla getirildi."));
        }

        [HttpGet("CategoryGetOne/{id}")]
        public async Task<IActionResult> CategoryGetOne(int id)
        {
            var query = new GetByIdCategoryQuery() { Id = id };
            var category = await _mediator.Send(query);

            return Ok(new Response<CategoryGetByIdDto>(200, category, "Kategori başarıyla getirildi."));
        }


        [HttpPost("CreateCategory")]
        public async Task<ActionResult<Response<CategoryCreateDto>>> Create(CreateCategoryCommand command)
        {
           

            var createdCategoryDto = await _mediator.Send(command);

           

            return Ok(new Response<CategoryCreateDto>(201, createdCategoryDto));
        }

        [HttpDelete("CategoryDelete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);

           

            return Ok(new Response<CategoryDeleteDto>(200, result, "Kategori başarıyla silindi."));
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
        {
            
           
            var updatedCategoryDto = await _mediator.Send(command);

            return Ok(new Response<CategoryUpdateDto>(200, updatedCategoryDto, "Kategori Başarıyla Güncellendi"));
        }



    }
}
