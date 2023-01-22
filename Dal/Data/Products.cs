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


    public class Products
    {



        DatabaseContext _context;


        public Products(DatabaseContext context)
        {

            _context = context;

        }


        // GET: Products
        public List<ProductsEntities> GetAll()
        {

            return _context.Products.ToList();

        }

        // POST: Product
        public ProductsEntities Insert(ProductsEntities product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
               
        }


    }
}
