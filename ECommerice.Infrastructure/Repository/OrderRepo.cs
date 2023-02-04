using ECommerice.Core.Entities;
using ECommerice.Core.Entities.OrderAggregate;
using ECommerice.Core.IRepository;
using ECommerice.Core.IUniteOfWork;
using ECommerice.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Infrastructure.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly IUniteOfWork _uniteOfWork;
        public readonly StoreContext _context;

        public OrderRepo(IUniteOfWork uniteOfWork, StoreContext context)
        {
            _uniteOfWork = uniteOfWork;
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersSearch(int pageNo,int pageSize,DateTime? dateFrom, DateTime? dateTo)
        {
            var query = await _uniteOfWork.Repository<Order>()
                    .GetWhere(o => o.OrderDate >= dateFrom && o.OrderDate <= dateTo);

            return query;
        }

        //public async Task<int> GetOrdersSearchCount(DateTime? dateFrom, DateTime? dateTo)
        //{
        //    var query = await _uniteOfWork.Repository<Order>()
        //        .GetWhere(o => o.OrderDate >= dateFrom && o.OrderDate <= dateTo);
        //    return query.Count;
        //}

        public async Task<Order> CreateOrderAsync(string userId, string userName, List<OrderItem> OrderItems)
        {


            //foreach (var item in OrderItems)
            //{
            //    var basketitem = new BasketItem();
            //    basketitem.ProductName= item.ProductName;
            //    basketitem.PictureUrl= item.PictureUrl;
            //    basketitem.ProductSizes = item.ProductSizes;
            //    _uniteOfWork.Repository<BasketItem>().Add(basketitem);

            //}







            //foreach (var item in OrderItems)
            //{
            //    foreach (var item2 in item.ProductSizes)

            //    {
            //        var size = new Size();

            //        size = item2;
            //        size.OrderItemId = null;

            //    }



            //}








            foreach (var item in OrderItems)
            {
                var size = await _uniteOfWork.Repository<Size>().GetWhereObject(p => p.ProductId == item.ProductId);

                foreach (var element in item.productSizes)
                {

                    if (element.value == size.value)
                    {
                        size.Quantity = size.Quantity - element.Quantity;


                    }




                }

                _uniteOfWork.Repository<Size>().Update(size);



            }



            //calc subtotal
            //var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);
            var subTotal = 20;
            //create order
          
  var order = new Order();
            order.OrderDate = DateTime.Now;
            order.UserId = userId;
            order.Username = userName;
            order.TotalAmount = subTotal;
            //order.OrderItems = OrderItems;
            //var i = 0;
            //var y = new List<OrderItem>();
            //foreach (var item in OrderItems)
            //{
            //    y.Add(new OrderItem()
            //    {
            //        Id = OrderItems[i].Id,
            //        OrderId = OrderItems[i].OrderId,
            //        PictureUrl = OrderItems[i].PictureUrl,
            //        ProductId = OrderItems[i].ProductId,
            //        ProductName = OrderItems[i].ProductName,
            //        ProductSizes = OrderItems[i].ProductSizes
            //    });
            //    i++;

            //}
            //order.OrderItems = y;
            foreach (var item in OrderItems)
            {
                foreach (var element in item.productSizes)
                {

                    element.Id = 0;
                }
            }
            order.OrderItems = OrderItems;


            _uniteOfWork.Repository<Order>().Add(order);










            //save in db
            var result = await _uniteOfWork.Complete();
            if (result <= 0) return null;

            //var c = order.Id;
            //var query = await _uniteOfWork.Repository<OrderItem>()
            //  .GetWhere(o => o.OrderId == c);
            //foreach (var item in OrderItems)
            //{
            //    foreach (var item2 in item.ProductSizes)

            //    {
            //        var size = new Size();

            //        size = item2;
            //        size.OrderItemId = ;
            //        _uniteOfWork.Repository<Size>().Add(size);

            //    }



            //}
            //retun order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _uniteOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _uniteOfWork.Repository<Order>().ListAllAsync();
        }
        
        public async Task<IEnumerable<Order>> GetOrderForUserAsync(string userId)
        {
            var query = await _uniteOfWork.Repository<Order>().GetWhere(o => o.UserId == userId);
            return query;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems(int orderId)
        {
            var query = await _uniteOfWork.Repository<OrderItem>()
                .GetWhere(o => o.OrderId == orderId);
            return query;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var query = await _uniteOfWork.Repository<Order>().GetWhereObject(o => o.Id == orderId);
            return query;
        }
    }
}
