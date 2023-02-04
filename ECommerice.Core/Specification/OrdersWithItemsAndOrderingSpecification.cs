using ECommerice.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.Specification
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<OrderItem>
    {
        //public OrdersWithItemsAndOrderingSpecification(string email) 
        //    : base(o => o.PuyerEmail == email)
        //{
        //    AddInclude(o => o.OrderItems);
        //    AddInclude(o => o.DeliveryMethod);
        //    AddOrderByDescending(o => o.OrderDate);
        //}

        //public OrdersWithItemsAndOrderingSpecification(int id, string email)
        //  : base(o => o.Id == id && o.PuyerEmail == email)
        //{
        //    AddInclude(o => o.OrderItems);
        //    AddInclude(o => o.DeliveryMethod);
        //}
        public OrdersWithItemsAndOrderingSpecification(int id) : base(p => p.Id == id)
        {

            //AddInclude(p => p.productSizes);
        }
    }
}
