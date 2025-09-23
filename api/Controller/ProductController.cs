using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Poduct;
using api.Interface;
using api.Mappes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IProductRepository _productRepository;
        public ProductController(AppDbContext appDbContext, IProductRepository productRepository)
        {
            _appDbContext = appDbContext;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productList = await _productRepository.GetAllAsync();
            var product = productList.Select(p => p.ToProductDto());
            return Ok(product);
        }

        [HttpGet("{ProductNo:int}")]
        public async Task<IActionResult> GetById([FromRoute] int ProductNo)
        {
            var product = await _productRepository.GetByIdAsync(ProductNo);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToProductDto());
        }

        [HttpGet("by-name/{Name}")]
        public async Task<IActionResult> GetByName([FromRoute] string Name)
        {
            var product = await _productRepository.GetByNameAsync(Name);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto createProductRequestDto)
        {
            var product = createProductRequestDto.ToProductFromCreateDto();
            product = await _productRepository.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { ProductNo = product.ProductNo }, product.ToProductDto());
        }

        [HttpPut]
        [Route("{Name}")]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequestDto updateProductRequestDto, [FromRoute] string Name)
        {
            var product = await _productRepository.UpdateAsync(updateProductRequestDto, Name);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToProductDto());
        }


        [HttpDelete]
        [Route("{Name}")]
        public async Task<IActionResult> Delete([FromRoute] string Name)
        {
            var product = await _productRepository.DeletelAsync(Name);

            if (product == null)
            {
                return NotFound();
            }

           
            return NoContent();
        }
    }
}