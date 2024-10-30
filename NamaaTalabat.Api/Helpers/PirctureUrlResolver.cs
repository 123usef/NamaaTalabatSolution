using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using NamaaTalabat.Api.DTOS;
using NamaaTalabat.Core.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace NamaaTalabat.Api.Helpers
{
    public class PirctureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;
        //public IConfiguration Configuration { get; }

        public PirctureUrlResolver(IConfiguration configuration)
        {
          
            this.configuration = configuration;
        }


        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            string url = source.PictureUrl;
            if (!string.IsNullOrEmpty(url))
            {
                return $"{configuration["ApiBaseUrl"]}/{url}";
                 
            }
            return string.Empty;
        }
    }
}
