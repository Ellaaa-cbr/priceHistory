using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Poduct;
using api.Models;

namespace api.Mappes
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product p)
        {
            return new ProductDto
            {
                Name = p.Name,
                ProductNo = p.ProductNo,
                price = p.price,
                date = p.date,
                price_text = p.price_text,
                weight = p.weight,
                Comment = p.Comment,
                ImageUrl = p.ImageUrl
            };
        }


        public static Product ToProductFromCreateDto(this CreateProductRequestDto productModel)
        {
            return new Product
            {
                Name = productModel.Name,
                price = productModel.price,
                date = productModel.date,
                price_text = productModel.price_text,
                weight = productModel.weight,
                Comment = productModel.Comment,
                ImageUrl = productModel.ImageUrl

            };

        }
    }
}