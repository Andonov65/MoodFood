
using Domasna.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domasna.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Ticket SelectedTicket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
