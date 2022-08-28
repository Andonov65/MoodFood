
using Domasna.Domain.DomainModels;
using Domasna.Domain.DTO;
using Domasna.Repository.Interface;
using Domasna.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domasna.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<EmailMessage> mailRepository, IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<TicketInOrder> ticketInOrderRepository)
        {
            _mailRepository = mailRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
        }


        public bool deleteTicketFromSoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.TicketInShoppingCarts.Where(z => z.TicketId.Equals(id)).FirstOrDefault();

                userShoppingCart.TicketInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                var allTickets = userCard.TicketInShoppingCarts.ToList();

                var allTicketsPrices = allTickets.Select(z => new
                {
                    TicketPrice = z.Ticket.TicketPrice,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allTicketsPrices)
                {
                    totalPrice += item.Quantity * item.TicketPrice;
                }

                var reuslt = new ShoppingCartDto
                {
                    Tickets = allTickets,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDto();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Successfully created order";
                message.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var result = userCard.TicketInShoppingCarts.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Ticket.Id,
                    SelectedTicket = z.Ticket,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Your order is completed. The order contains: ");

                var totalPrice = 0.0;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    sb.AppendLine(i.ToString() + " " + item.SelectedTicket.TicketName + " with price of: " + item.SelectedTicket.TicketPrice + " and quantity of: " + item.Quantity); ;
                    totalPrice += item.Quantity * item.SelectedTicket.TicketPrice;
                }

                sb.AppendLine("Total Price: " + totalPrice.ToString());

                message.Content = sb.ToString();


                ticketInOrders.AddRange(result);

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.TicketInShoppingCarts.Clear();

                this._mailRepository.Insert(message);

                this._userRepository.Update(loggedInUser);

                return true;
            }

            return false;
        }
    }
}
