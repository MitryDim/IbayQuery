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

            return _context.Products.Where(p => p.Available == true).ToList();

        }
        public ProductsEntities SearchById(int Id)
        {
            return _context.Products.Where(p => p.Id == Id && p.Available == true).FirstOrDefault();
        }

        // POST: Product
        public ProductsEntities Insert(ProductsEntities product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

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
        public bool Update(ProductsEntities product)
        {
            var currentProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            if (currentProduct == null)
            {
                throw new Exception("Produit non existant !");
            }

            product.ImageURL = currentProduct.ImageURL;
            product.Added_Hour = currentProduct.Added_Hour;
            product.OwnedId = currentProduct.OwnedId;
            try
            {
                _context.Entry(currentProduct).CurrentValues.SetValues(product);
                _context.SaveChangesAsync();
            }catch(Exception)
            {
                return false;
            }
           
            return true;

        }

        // DELETE : Product
        public bool Delete(int id)
        {
            try
            {
            var currentProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (currentProduct == null)
            {
                throw new Exception("Null");
            }
                currentProduct.Available = false;
                _context.Products.Update(currentProduct);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Erreur lors de la suppression");
            }

            return true;

        }


    }
}
