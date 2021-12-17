using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Musical_Instrument_Store.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _appDBContext;
        private readonly Cart _cart;

        public OrderRepository(AppDBContext appDBContext, Cart cart)
        {
            _appDBContext = appDBContext;
            _cart = cart;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _appDBContext.orders.Include(s=>s.StatusOrder).Include(o=>o.orderDetails).ThenInclude(m=>m.musicalInstrument).ToListAsync();
        }
        public async Task<List<OrderDetail>> GetOrdersDetailsAsync()
        {
            return await _appDBContext.orderDetails.Include(m => m.musicalInstrument)
                .Include(o => o.order)
                    .ThenInclude(s => s.StatusOrder).ToListAsync();
        }

        public async Task CreateOrderAsync(Order order, string userId)
        {
            order.StatusOrder = await GetStatusOrderById(1);
            order.orderTime = DateTime.Now;
            order.userId = userId;
            _appDBContext.orders.Add(order);
            _appDBContext.SaveChanges();

            var items = _cart.cartLines;
            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {

                    musicalInstrument = el.musicalInstrument,
                    order = order,
                    price = el.musicalInstrument.price,
                    
                };
                _appDBContext.MusicalInstruments.FirstOrDefault(p => p.Id == orderDetail.musicalInstrument.Id).quantity--;
                _appDBContext.orderDetails.Add(orderDetail);
            }

            await _appDBContext.SaveChangesAsync();

        }

        public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
        {
            List<Order> orderDetails = await _appDBContext.orders.Include(s => s.StatusOrder).Include(o => o.orderDetails).ThenInclude(m => m.musicalInstrument).Where(o => o.userId == userId).ToListAsync();

            return orderDetails;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _appDBContext.orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<SelectListItem>> GetStatusForDropdownAsync()
        {
            List<SelectListItem> categories = await _appDBContext.StatusOrders.Select(n =>
            new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.statusName
            }).ToListAsync();

            return new SelectList(categories, "Value", "Text");
        }

        public async Task<IEnumerable<SelectListItem>> GetStatusForFilterForDropdownAsync()
        {
            List<SelectListItem> statusOrder = await _appDBContext.StatusOrders.Select(n =>
            new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.statusName
            }).ToListAsync();
            SelectListItem selectListItem = new SelectListItem { Value = "0", Text = "All" };
            statusOrder.Add(selectListItem);


            return new SelectList(statusOrder, "Value", "Text");
        }

        public async Task SetOrderStatusAsync(int orderId, int status)
        {
            var order = await GetOrderByIdAsync(orderId);
            order.StatusOrder = await GetStatusOrderById(status);
            _appDBContext.SaveChanges();
        }
        public async Task<StatusOrder> GetStatusOrderById(int id)
        {
            return await _appDBContext.StatusOrders.SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> FilterByStatusAsync(int? statusOrderId)
        {
            IQueryable<Order> orderDetails = _appDBContext.orders.Include(s => s.StatusOrder).Include(o => o.orderDetails).ThenInclude(m => m.musicalInstrument).Where(o =>o.statusOrderId == statusOrderId);

            return await orderDetails.ToListAsync();

        }

        public async Task<List<Order>> FilterByStatusUserOrderAsync(int? statusOrderId, string userId)
        {
            IQueryable<Order> orderDetails = _appDBContext.orders.Include(s => s.StatusOrder).Include(o => o.orderDetails).ThenInclude(m => m.musicalInstrument).Where(o => o.statusOrderId == statusOrderId).Where(o => o.userId == userId);

            return await orderDetails.ToListAsync();
        }

    }
}
