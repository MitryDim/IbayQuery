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

        public void Insert(CartsEntities cart)
        {
            _context.Carts.AddAsync(cart);
            _context.SaveChanges();    
        }


    }
}
