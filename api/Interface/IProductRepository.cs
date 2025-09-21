using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Poduct;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int ProductNo);
        Task<Product?> GetByNameAsync(string Name);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(UpdateProductRequestDto updateProductRequestDto, string Name);
        Task<Product?> DeletelAsync(string Name);
    }
}