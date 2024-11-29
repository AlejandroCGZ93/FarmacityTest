using AutoMapper;
using Core.Dtos;
using Core.Entities;


namespace Core.Automapper
{
    public class MapProfile : Profile
    {
        public MapProfile() 
        {
            CreateMap<Product, ProductDto>().ForMember(dto => dto.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(source => source.Price))
                .ForMember(dto => dto.QuantityStock, opt => opt.MapFrom(source => source.QuantityStock))
                .ForMember(dto => dto.IsActive, opt => opt.MapFrom((source) => source.IsActive));

            CreateMap<ProductDto, Product>().ForMember(dto => dto.Id, opt => opt.MapFrom(source => source.Id));

            CreateMap<Barcode, ProductDto>().ForMember(dto => dto.Barcode.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(dto => dto.Barcode.Code, opt => opt.MapFrom(source => source.Code))
                .ForMember(dto => dto.Barcode.IsActive, opt => opt.MapFrom(source => source.IsActive));
        }
    }
}
