using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly Cart _cart;
        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, ICartService cartService, Cart cart, UserManager<User> userManager)
        {
            _userManager = userManager;
            _orderService = orderService;
            _cartService = cartService;
            _cart = cart;
        }


        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                User user = await _userManager.GetUserAsync(User);
                Order order = new Order
                {
                    surnameClient = user.Surname,
                    nameClient = user.Name,
                    addressClient = user.Address,
                    phoneClient = user.Address,
                    emailClient = user.Email
                };
                return View(order);
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(Order order)
        {
            User user = await _userManager.GetUserAsync(User);
            _cart.cartLines = await _cartService.GetCartLines(_cart);
            if(_cart.cartLines.Count==0)
            {
                ModelState.AddModelError("","Немає товарів в корзині");
            }
            else
            {
                if(ModelState.IsValid)
                {
                     await _orderService.CreateOrderAsync(order, user.Id);
                    return RedirectToAction("Complete");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders(int? statusOrderId)
        {

            if(statusOrderId != null && statusOrderId !=0)
            {
                return View(await _orderService.FilterByStatusAsync(statusOrderId));
            }
            return View(await _orderService.GetOrderListViewModelAsync());

        }
        
        [HttpGet]
        public async Task<IActionResult> UserOrder(int? statusOrderId, string userId)
        {
            if (statusOrderId != null && statusOrderId != 0)
            {
                User user = await _userManager.GetUserAsync(User);
                return View(await _orderService.FilterByStatusUserOrderAsync(statusOrderId, user.Id));
            }
            return View(await _orderService.GetOrderByUserIdAsync(userId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStatusOrder(int Id)
        {
       
            return View(await _orderService.GetOrderDetailViewModelAsync(Id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditStatusOrder(OrderViewModel model)
        {   
            try
            {
                await _orderService.SetOrderStatusAsync(model.Id, model.statusOrderId);
                return RedirectToAction("Index", "MusicalInstrument");
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "MusicalInstrument");
            }
            

        }
        public IActionResult Complete()
        {
            ViewBag.Message = "Successful";
            return View();
        }
    }
}
