using AutoMapper;
using DomainLayer.DTO.OrderDtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Implementation;
using ServiceLayer.Service.UoW;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public OrderController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<List<AllOrdersDto>> GetAllOrders()
        {
            var repository = _unitOfWork.GetRepository<Order>() as OrderService;

            var orders = await Task.Run(() => repository.GetAllOrders());

            var request = _mapper.Map<List<Order>, List<AllOrdersDto>>(orders.ToList());

            return request;
        }

        [HttpGet]
        [Route("get")]
        public async Task<OrderDto> GetOrder(int id)
        {
            var repository = _unitOfWork.GetRepository<Order>() as OrderService;

            var order = await Task.Run(() => repository.GetOrderById(id));

            var request = _mapper.Map<Order, OrderDto>(order);

            return request;
        }

        [HttpPost("add")]
        public async Task AddOrder(AddOrderDto addOrder)
        {
            var orderRep = _unitOfWork.GetRepository<Order>() as OrderService;
            var bookRep = _unitOfWork.GetRepository<Book>() as BookService;

            var book = await Task.Run(() => bookRep.GetBookById(addOrder.BookId));

            var newOrder = new Order()
            {
                Description = $"Order: {book.Author} - {book.Title}",
                BookId = addOrder.BookId
            };


            await Task.Run(() => orderRep.AddOrder(newOrder));

            _unitOfWork.SaveChanges();
        }

        //[HttpPut("edit")]
        //public async Task UpdateBook(Order book)
        //{
        //    var repository = _unitOfWork.GetRepository<Order>() as OrderService;


        //    await Task.Run(() => repository.UpdateOrder(book));

        //    _unitOfWork.SaveChanges();
        //}

        //[HttpDelete]
        //public async Task DeleteBook(long id)
        //{
        //    var repository = _unitOfWork.GetRepository<Order>() as OrderService;

        //    await Task.Run(() => repository.RemoveOrder(id));
        //    _unitOfWork.SaveChanges();
        //}
    }
}
