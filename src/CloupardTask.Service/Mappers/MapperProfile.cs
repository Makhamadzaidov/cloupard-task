using AutoMapper;
using CloupardTask.Api.DTO_s;
using CloupardTask.Api.Models;
using CloupardTask.Service.ViewModels.Products;

namespace CloupardTask.Api.Mappers
{
	public class MapperProfile : Profile
	{
        public MapperProfile()
        {
            CreateMap<ProductCreateDto, Product>().ReverseMap()
                .ForMember(dto => dto.Image, 
                expression => expression.MapFrom(entity => entity.ImageUrl));
                
            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<Product, ProductViewModel>() .ForMember(dest => dest.ImageUrl, opt =>
                opt.MapFrom(src => "C:\\Users\\k.maxamadzoidov\\Desktop\\cloupard-task\\src\\CloupardTask.Api\\wwwroot\\" + src.ImageUrl));
        }
    }
}
