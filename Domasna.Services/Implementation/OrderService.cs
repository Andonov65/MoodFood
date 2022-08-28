
using Domasna.Domain.DomainModels;
using Domasna.Repository.Interface;
using Domasna.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domasna.Services.Implementation
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return _orderRepository.getAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return _orderRepository.getOrderDetails(model);
        }
    }
}
