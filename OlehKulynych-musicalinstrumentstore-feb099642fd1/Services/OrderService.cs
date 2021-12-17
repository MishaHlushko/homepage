using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMusicalInstrumentRepository _musicalInstrumentRepository;

        public OrderService(IOrderRepository orderRepository, IMusicalInstrumentRepository musicalInstrumentRepository)
        {
            _orderRepository = orderRepository;
            _musicalInstrumentRepository = musicalInstrumentRepository;
        }

        public async Task<OrderDetailListViewModel> GetOrdersAsync()
        {
            return new OrderDetailListViewModel { orderDetailsList = await _orderRepository.GetOrdersDetailsAsync(), statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync() };
        }

        public async Task<OrderListViewModel> GetOrderListViewModelAsync()
        {
            return new OrderListViewModel
            {
                orders = await _orderRepository.GetOrdersAsync(),
                statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync()

            };

        }





        public async Task<List<OrderViewModel>> GetOrderListAsync()
        {
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            List<Order> order = await _orderRepository.GetOrdersAsync();
            foreach (var order1 in order)
            {
                OrderViewModel orderViewModel = new OrderViewModel
                {
                    order = order1,
                    statusOrderId = order1.statusOrderId,
                    statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync(),
                };
                orderViewModels.Add(orderViewModel);
            }

            return orderViewModels;
        }

        public async Task CreateOrderAsync(Order order, string userId)
        {
            await _orderRepository.CreateOrderAsync(order, userId);
        }

        public async Task<OrderListViewModel> GetOrderByUserIdAsync(string userId)
        {

            return new OrderListViewModel
            {

                orders = await _orderRepository.GetOrderByUserIdAsync(userId),
                statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync()
            };
        }



        public async Task<OrderViewModel> GetOrderDetailViewModelAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                surnameClient = order.surnameClient,
                nameClient = order.nameClient,
                emailClient = order.emailClient,
                addressClient = order.addressClient,
                phoneClient = order.phoneClient,
                orderTime = order.orderTime,
                statusOrderId = order.statusOrderId,
                statusOrders = await _orderRepository.GetStatusForDropdownAsync()

            };

            return orderViewModel;
        }

        public async Task SetOrderStatusAsync(int orderId, int status)
        {
            await _orderRepository.SetOrderStatusAsync(orderId, status);
        }

        //public async Task<OrderDetailListViewModel> FilterByStatusAsync(int? statusOrderId)
        //{
        //    OrderDetailListViewModel orderDetailListView = new OrderDetailListViewModel
        //    {
        //        orderDetailsList = await _orderRepository.FilterByStatusAsync(statusOrderId),
        //        statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync(),
        //    };

        //    return orderDetailListView;

        //}

        //public async Task<OrderDetailListViewModel> FilterByStatusUserOrderAsync(int? statusOrderId, string userId)
        //{
        //    OrderDetailListViewModel orderDetailListView = new OrderDetailListViewModel
        //    {
        //        orderDetailsList = await _orderRepository.FilterByStatusUserOrderAsync(statusOrderId, userId),
        //        statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync(),
        //    };

        //    return orderDetailListView;

        //}

        public async Task<OrderListViewModel> FilterByStatusAsync(int? statusOrderId)
        {
            OrderListViewModel orderListView = new OrderListViewModel
            {

                orders = await _orderRepository.FilterByStatusAsync(statusOrderId),
                statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync(),
            };

            return orderListView;

        }

        public async Task<OrderListViewModel> FilterByStatusUserOrderAsync(int? statusOrderId, string userId)
        {
            OrderListViewModel orderDetailListView = new OrderListViewModel
            {
                orders = await _orderRepository.FilterByStatusUserOrderAsync(statusOrderId, userId),
                statusOrders = await _orderRepository.GetStatusForFilterForDropdownAsync(),
            };

            return orderDetailListView;

        }
    }
}
