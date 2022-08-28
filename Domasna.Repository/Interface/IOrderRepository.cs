
using Domasna.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domasna.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();
        Order getOrderDetails(BaseEntity model);
    }
}
