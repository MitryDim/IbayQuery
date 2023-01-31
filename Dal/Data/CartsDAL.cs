using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Data
{
    public class CartsDAL
    {

        private DatabaseContext _context;
        public CartsDAL(DatabaseContext context)
        {

            _context = context;

        }

        public CartsEntities SearchById(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }



        public bool AddToCart(CartsEntities cartItem)
        {
            try
            {
                _context.Carts.Add(cartItem);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CartsEntities GetCart(int userId)
        {
            return _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefault(c => c.UserId == userId && c.Status == "In Progress");
        }

        public bool Update(CartsEntities cart)
        {
            try
            {
                _context.Carts.Update(cart);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
