using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperBenchmarks
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }

        public static implicit operator Product(ProductDto productDto) => new Product
        {
            ProductName = productDto.ProductName,
            ProductDescription = productDto.ProductDescription,
            Price = productDto.Price
        };

        public static explicit operator ProductDto(Product product) => new ProductDto
        {
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            Price = product.Price + product.Price * 100 / product.VatPercentage,
        };

        public static ProductDto ConvertFromProduct(Product product)
        {
            return new ProductDto
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price + product.Price * 100 / product.VatPercentage,
            };
        }
    }
}
