using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Data
{
    public class CartsItemsDAL
    {

        private DatabaseContext _context;

        public CartsItemsDAL(DatabaseContext context)
        {

            _context = context;

        }

        public bool Insert(CartsItemsEntities cart)
        {
            try
            {
                _context.CartsItems.Add(cart);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(CartsItemsEntities cartItem)
        {
            try
            {
                _context.CartsItems.Update(cartItem);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int productId, int userId)
        {
            try
            {
                var cart = _context.Carts
                            .Include(c => c.CartItems)
                            .FirstOrDefault(c => c.UserId == userId);
                if (cart == null)
                    return false;

                var cartItemToRemove = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItemToRemove != null)
                {
                    cart.CartItems.Remove(cartItemToRemove);
                    _context.SaveChanges();
                    return true;
                }

                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
