using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Data
{
    public class OrdersDAL
    {
        DatabaseContext _context;


        public OrdersDAL(DatabaseContext context)
        {

            _context = context;

        }

        // POST: Order
        public bool Insert(OrdersEntities order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
           
            return true;

        }

    }
}
