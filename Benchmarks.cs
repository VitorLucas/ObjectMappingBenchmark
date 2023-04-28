using AutoMapper;
using BenchmarkDotNet.Attributes;

namespace MapperBenchmarks
{
    public class Benchmarks
    {

        private Product[] _products;
        private IMapper _mapper; 

        //[Params(10, 100, 1000)]
        [Params(10, 100)]
        public int NumberOfElements { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.ProductName, s => s.MapFrom(src => src.ProductName))
                    .ForMember(dest => dest.ProductDescription, s => s.MapFrom(src => src.ProductDescription))
                    .ForMember(dest => dest.Price, s => s.MapFrom(src => src.Price + src.Price * 100 / src.VatPercentage));
            });
            
            _mapper = config.CreateMapper();

            _products = Enumerable.Range(1, NumberOfElements)
                                    .Select(x => new Product 
                                    {
                                        Id = x,
                                        ProductCategoryName = $"Product  Number {x}",
                                        ProductDescription = $"Product  Description {x}",
                                        Price = 45.5m,
                                        VatPercentage = 19
                                    }).ToArray();
        }

        [Benchmark]
        public void WithAutoMapper() 
        {
            foreach (var product in _products)
            {
                var productDto = _mapper.Map<ProductDto>(product);
            }
        }

        [Benchmark]
        public void WithDirectAssignment()
        {
            foreach (var product in _products)
            {
                var productDto = ProductDto.ConvertFromProduct(product);
            }
        }

        [Benchmark]
        public void WithExplicitCast()
        {
            foreach (var product in _products)
            {
                var productDto = (ProductDto)product;
            }
        }
    }
}
