using ECommerice.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.IRepository
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetOrdersSearch(int pageNo, int pageSize,DateTime? dateFrom, DateTime? dateTo);
        //Task<int> GetOrdersSearchCount(DateTime? dateFrom, DateTime? dateTo);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> CreateOrderAsync(string userId, string userName, List<OrderItem> OrderItems);
        Task<IEnumerable<Order>> GetOrderForUserAsync(string userId);
        Task<IEnumerable<OrderItem>> GetOrderItems(int orderId);
        Task<Order> GetOrderById(int orderId);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
