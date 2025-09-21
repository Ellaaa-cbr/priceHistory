using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Poduct;

namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {
         private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return product;
            
        }

        public async Task<Product?> DeletelAsync(string Name)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Name == Name);

            if (product == null)
            {
                return null;
            }

             _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int ProductNo)
        {
            return await _appDbContext.Products
                               .SingleOrDefaultAsync(p => p.ProductNo == ProductNo);
        }

        public async Task<Product?> GetByNameAsync(string Name)
        {
            return await _appDbContext.Products
                               .SingleOrDefaultAsync(p => p.Name == Name);
        }

        public async Task<Product?> UpdateAsync(UpdateProductRequestDto updateProductRequestDto, string Name)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Name == Name);

            if (product == null)
            {
                return null;
            }

            product.Name = updateProductRequestDto.Name;
            product.price = updateProductRequestDto.price;
            product.date = updateProductRequestDto.date;
            product.price_text = updateProductRequestDto.price_text;
            product.weight = updateProductRequestDto.weight;
            product.Comment = updateProductRequestDto.Comment;
            product.ImageUrl = updateProductRequestDto.ImageUrl;

            await _appDbContext.SaveChangesAsync();
            return product;
        }

        
    }
}