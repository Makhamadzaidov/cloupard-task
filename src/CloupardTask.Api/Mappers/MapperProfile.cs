using AutoMapper;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Models;

namespace CloupardTask.Api.Mappers
{
	public class MapperProfile : Profile
	{
        public MapperProfile()
        {
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
