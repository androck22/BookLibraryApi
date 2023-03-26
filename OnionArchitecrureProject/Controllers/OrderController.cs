﻿using AutoMapper;
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
        private readonly ILogger<BookController> _logger;

        public OrderController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<BookController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllOrders()
        {
            var repository = _unitOfWork.GetRepository<Order>() as OrderService;

            var orders = await Task.Run(() => repository.GetAllOrders());

            var request = _mapper.Map<List<Order>, List<AllOrdersDto>>(orders.ToList());

            _logger.LogDebug("Произведена выборка всех заказов");

            return StatusCode(200, request);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var repository = _unitOfWork.GetRepository<Order>() as OrderService;

            var order = await Task.Run(() => repository.GetOrderById(id));

            var request = _mapper.Map<Order, OrderDto>(order);

            _logger.LogDebug("Выборка прошла успешно. Выбран заказ с id: " + order.OrderId);

            return StatusCode(200, request);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder(AddOrderDto addOrder)
        {
            var orderRep = _unitOfWork.GetRepository<Order>() as OrderService;
            var bookRep = _unitOfWork.GetRepository<Book>() as BookService;

            var book = await Task.Run(() => bookRep.GetBookById(addOrder.BookId));

            if (book == null)
            {
                _logger.LogError($"Ошибка: Книга с id: {addOrder.BookId} не найдена. Проверьте корректность ввода!");
                return StatusCode(400, $"Ошибка: Книга с id: {addOrder.BookId} не найдена. Проверьте корректность ввода!");
            }

            var newOrder = new Order()
            {
                Description = $"Order: {book.Author} - {book.Title}",
                BookId = addOrder.BookId
            };


            await Task.Run(() => orderRep.AddOrder(newOrder));

            _unitOfWork.SaveChanges();

            _logger.LogDebug($"Заказ на книгу: {book.Author} - {book.Title} : {DateTime.Now.ToShortDateString()} добвлен успешно.");

            return StatusCode(200, $"Заказ на книгу: {book.Author} - {book.Title} : {DateTime.Now.ToShortDateString()} добвлен успешно.");
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
