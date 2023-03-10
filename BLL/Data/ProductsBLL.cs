using AutoMapper;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public class ProductsBLL
    {

        private Dal.Data.ProductsDAL _DAL;

        private Mapper _ProductMapper;


        public ProductsBLL(DatabaseContext context)
        {
            _DAL = new Dal.Data.ProductsDAL(context);
            var _configProduct = new MapperConfiguration(cfg => cfg.CreateMap<ProductsEntities, ProductsModel>().ReverseMap());
            _ProductMapper = new Mapper(_configProduct);
        }


        // GET: Products
        public List<ProductsModel> GetAll(string sortBy, int limit, string query)
        {
            try
            {
            var productsEntities = _DAL.GetAll();
            var products = _ProductMapper.Map<List<ProductsModel>>(productsEntities);

            products = products.Where(p => p.Name.ToLower().Contains(query.ToLower()) || p.Added_Hour.ToString("dd/MM/yyyy").Equals(query) || p.Price.ToString().TrimEnd('0', ',').Equals(query)).ToList();


            switch (sortBy.ToLower())
            {
                case "name":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "available":
                    products = products.OrderBy(p => p.Available).ToList();
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "date":
                    products = products.OrderBy(p => p.Added_Hour).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
            }

            
                return products.Take(limit).ToList();



            }
            catch(Exception ex) { 
                throw new Exception(ex.Message);
            }
            
        }

        // GET: Products By ID
        public ProductsEntities GetProductById(int id)
        {
            try
            {
                return _DAL.SearchById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        // POST: Add product
        public ProductsModel Insert(ProductsModel product)
        {


            try
            {
                //get the file extension
                var fileExtension = Path.GetExtension(product.Image.FileName);
            //generate a new file name
            var newFileName = Guid.NewGuid() + fileExtension;
            //get the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images\\Products", newFileName);


                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images\\Products")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images\\Products"));
                    Console.WriteLine("Le répertoire a été créé.");
                }



                //open the file stream
                using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                //copy the file to the server
                product.Image.CopyTo(fileStream);
            }

            ////save the other product's information
            product.ImageURL = filePath;


                var productMap = _ProductMapper.Map<ProductsEntities>(product);
                return _ProductMapper.Map <ProductsModel>(_DAL.Insert(productMap));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message); 
            }

        }

        private bool CheckProductExists(int id)
        {
            try
            {
                return _DAL.CheckProductExists(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public bool CheckProductOwner(int id, int OwnedId)
        {

            try
            {
                return _DAL.CheckProductOwner(id, OwnedId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




        // PUT : Update Product

        public bool Update(ProductsModel product)
        {
            var productExist = _DAL.CheckProductExists(product.Id);

            if (productExist == false)
            {
                throw new Exception("Produit Introuvable");
            }

            try
            {
                return _DAL.Update(_ProductMapper.Map<ProductsEntities>(product));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Erreur lors de l'update");

            }
        }

        // DELETE : Delete Product
        public bool? Delete(int id)
        {
            try
            {
                var productExist = _DAL.CheckProductExists(id);

            if (productExist == false)
            {
                    return null;
            }

            
                return _DAL.Delete(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Erreur lors du Delete");

            }
        }
    }
}
