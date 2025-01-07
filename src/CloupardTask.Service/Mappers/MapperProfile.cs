using AutoMapper;
using CloupardTask.Api.DTO_s;
using CloupardTask.Domain.Models;
using CloupardTask.Service.DTOs.Categories;
using CloupardTask.Service.DTOs.Customers;
using CloupardTask.Service.DTOs.Orders;
using CloupardTask.Service.ViewModels.Categories;
using CloupardTask.Service.ViewModels.Customers;
using CloupardTask.Service.ViewModels.OrderDetails;
using CloupardTask.Service.ViewModels.Orders;
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


			CreateMap<Product, ProductViewModel>()
				.ForMember(dest => dest.ImageUrl, opt =>
					opt.MapFrom(src =>
					string.IsNullOrEmpty(src.ImageUrl)
						? "/Images/placeholder.jpg" // Default image
				:		$"/{src.ImageUrl.Replace("\\", "/")}"));




			CreateMap<Category, CategoryViewModel>().ReverseMap();
			CreateMap<CategoryCreateDto, Category>().ReverseMap();

			CreateMap<Customer, CustomerViewModel>().ReverseMap();
			CreateMap<CustomerCreateDto, Customer>().ReverseMap();

			// Order mappings
			CreateMap<OrderCreateDto, Order>()
				.ForMember(o => o.OrderDetails, opt =>
					opt.MapFrom(dto => dto.OrderDetails.Select(od => new OrderDetail
					{
						ProductId = od.ProductId,
						Quantity = od.Quantity,
						UnitPrice = od.UnitPrice
					})))
				.ReverseMap();

			CreateMap<OrderUpdateDto, Order>()
				.ForMember(o => o.OrderDetails, opt =>
					opt.MapFrom(dto => dto.OrderDetails.Select(od => new OrderDetail
					{
						Quantity = od.Quantity ?? 0,
						UnitPrice = od.UnitPrice ?? 0m
					})))
				.ReverseMap();

			CreateMap<Order, OrderViewModel>()
				.ForMember(dest => dest.CustomerName, opt =>
					opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : string.Empty))
				.ForMember(dest => dest.OrderDetails, opt =>
					opt.MapFrom(src => src.OrderDetails.Select(od => new OrderDetailViewModel
					{
						ProductName = od.Product != null ? od.Product.Name : string.Empty,
						Quantity = od.Quantity,
						UnitPrice = od.UnitPrice
					})))
				.ReverseMap();

		}
	}
}
