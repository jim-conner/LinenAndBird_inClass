using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird_inClass.Models;

namespace LinenAndBird_inClass.DataAccess
{
    public class OrdersRepository
    {
        static List<Order> _orders = new List<Order>();

        internal void Add(Order order)
        {
            order.Id = Guid.NewGuid();

            _orders.Add(order);
        }
    }
}
