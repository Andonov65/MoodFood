using Domasna.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domasna.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        
        public string OwnerId { get; set; }
        public DomasnaApplicationUser Owner { get; set; }
        public  virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
