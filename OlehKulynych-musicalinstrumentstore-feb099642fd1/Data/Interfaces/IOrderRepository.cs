using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order, string userId);
        Task<List<Order>> GetOrderByUserIdAsync(string userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task SetOrderStatusAsync(int orderId, int status);
        Task<List<Order>> GetOrdersAsync();
        Task<IEnumerable<SelectListItem>> GetStatusForDropdownAsync();
        Task<StatusOrder> GetStatusOrderById(int id);
        Task<List<OrderDetail>> GetOrdersDetailsAsync();
        Task<List<Order>> FilterByStatusAsync(int? statusOrderId);
        Task<List<Order>> FilterByStatusUserOrderAsync(int? statusOrderId, string userId);
        Task<IEnumerable<SelectListItem>> GetStatusForFilterForDropdownAsync();
    }
}