using AutoMapper;
using DomainLayer.DTO.BookDtos;
using DomainLayer.DTO.OrderDtos;
using DomainLayer.Models;

namespace WebAPI_Layer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, AllBooksDto>();
            CreateMap<Book, BookDto>();
            CreateMap<AddBookDto, Book>();
            CreateMap<EditBookDto, Book>();

            CreateMap<Order, AllOrdersDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<AddOrderDto, Order>();
        }
    }
}
