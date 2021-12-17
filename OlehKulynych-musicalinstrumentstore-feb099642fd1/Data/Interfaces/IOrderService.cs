using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order, string userId);
        Task<OrderListViewModel> GetOrderByUserIdAsync(string userId);
        Task<OrderViewModel> GetOrderDetailViewModelAsync(int id);
        Task SetOrderStatusAsync(int orderId, int status);
        Task<OrderDetailListViewModel> GetOrdersAsync();
        Task<OrderListViewModel> FilterByStatusAsync(int? statusOrderId);
        Task<OrderListViewModel> FilterByStatusUserOrderAsync(int? statusOrderId, string userId);
        Task<OrderListViewModel> GetOrderListViewModelAsync();
        Task<List<OrderViewModel>> GetOrderListAsync();
    }
}
