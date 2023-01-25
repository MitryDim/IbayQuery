using Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Data
{


    public class ProductsDAL
    {

        DatabaseContext _context;


        public ProductsDAL(DatabaseContext context)
        {

            _context = context;

        }


        // GET: Products
        public List<ProductsEntities> GetAll()
        {

            return _context.Products.ToList();

        }
        public ProductsEntities SearchById(int Id)
        {
            return _context.Products.Where(p => p.Id == Id).FirstOrDefault();
        }

        // POST: Product
        public ProductsEntities Insert(ProductsEntities product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
               
        }

        public bool CheckProductExists(int id)
        {
           return _context.Products.Any(p => p.Id == id);
        }

        public bool CheckProductOwner(int id, int OwnedId)
        {
            return _context.Products.Any(p => p.Id == id && p.OwnedId == OwnedId);
        }

        // PUT : Product
        public ProductsEntities Update(ProductsEntities product)
        {
            var currentProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            if (currentProduct == null)
            {
                throw new Exception("Null");
            }
            product.ImageURL = currentProduct.ImageURL;
            product.Added_Hour = currentProduct.Added_Hour;
            product.OwnedId = currentProduct.OwnedId;

            _context.Entry(currentProduct).CurrentValues.SetValues(product);
            _context.SaveChangesAsync();

            return product;

        }

        // DELETE : Product
        public ProductsEntities Delete(int id)
        {
            var currentProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (currentProduct == null)
            {
                throw new Exception("Null");
            }

            _context.Products.Remove(currentProduct);
            _context.SaveChanges();

            return currentProduct;

        }


    }
}
