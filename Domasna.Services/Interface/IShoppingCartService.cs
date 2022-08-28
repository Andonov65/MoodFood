
using Domasna.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domasna.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid id);
        bool order(string userId);
    }
}
