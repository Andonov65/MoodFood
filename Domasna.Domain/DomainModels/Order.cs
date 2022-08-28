
using Domasna.Domain.Identity;
using System.Collections.Generic;

namespace Domasna.Domain.DomainModels
{
    public class Order : BaseEntity
    {
 
        public string UserId { get; set; }
        public DomasnaApplicationUser User { get; set; }

        public List<TicketInOrder> Tickets { get; set; }
    }
}
