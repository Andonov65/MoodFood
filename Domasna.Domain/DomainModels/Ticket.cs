using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domasna.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        
        [Required]
        [Display(Name = "Recipe")]
        public string TicketName { get; set; }
        [Required]
        [Display(Name = "Recipe image")]
        public string TicketImage { get; set; }
        [Required]
        [Display(Name = "Recipe description")]
        public string TicketDescription { get; set; }
        [Required]
        [Display(Name = "Recipe cost")]
        public int TicketPrice { get; set; }
        [Required]
        [Display(Name = "Recipe rating")]
        public int Rating{ get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }

        public virtual ICollection<TicketInOrder> Orders { get; set; }

        public Ticket() { }
    }
}
