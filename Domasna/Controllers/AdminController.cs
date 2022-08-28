
using Domasna.Domain.DomainModels;
using Domasna.Domain.Identity;
using Domasna.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domasna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IOrderService _orderService;
        private readonly UserManager<DomasnaApplicationUser> userManager;

        public AdminController(IOrderService service, UserManager<DomasnaApplicationUser> userManager)
        {
            _orderService = service;
            this.userManager = userManager;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return _orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetOrderDetails(BaseEntity model)
        {
            return _orderService.getOrderDetails(model);
        }

        [HttpPost("[action]")]
        public Boolean ImportAllUsers(List<UserRegistrationDto> model)
        {
            bool status = true;

            foreach(var item in model)
            {
                var userCheck =  userManager.FindByEmailAsync(item.Email).Result;

                if(userCheck == null)
                {
                    var user = new DomasnaApplicationUser
                    {
                        FirstName = item.Name,
                        LastName = item.LastName,
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = item.PhoneNumber,
                        UserCart = new ShoppingCart()
                    };
                    var result =  userManager.CreateAsync(user, item.Password).Result;

                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }
    }
}
