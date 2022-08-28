using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domasna.Domain.DomainModels
{
    public class TicketProduct : BaseEntity
    {

        [Required]
        [Display(Name = "Product")]
        public string TicketProductName { get; set; }
        [Required]
        [Display(Name = "Product image")]
        public string TicketProductImage { get; set; }
        [Required]
        [Display(Name = "Product description")]
        public string TicketProductDescription { get; set; }
        [Required]
        [Display(Name = "Product macros on 100g")]
        public string TicketProductsMacros{ get; set; }


        public TicketProduct() { }

    }
}
