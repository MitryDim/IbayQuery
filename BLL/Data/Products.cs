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
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public class Products
    {

        private Dal.Data.Products _DAL;

        private Mapper _ProductMapper;


        public Products(DatabaseContext context)
        {
            _DAL = new Dal.Data.Products(context);
            var _configProduct = new MapperConfiguration(cfg => cfg.CreateMap<ProductsEntities, ProductsModel>().ReverseMap());
            _ProductMapper = new Mapper(_configProduct);
        }


        // GET: Products
        public List<ProductsEntities> GetAll()
        {
            return _DAL.GetAll();
        }

        // GET: Products By ID
        public ProductsEntities GetProductById(int id)
        {

            return _DAL.GetAll().FirstOrDefault(p => p.Id == id);
        }

        // POST: Add product
        public ProductsEntities Insert(ProductsModel product)
        {


            //get the file extension
            var fileExtension = Path.GetExtension(product.Image.FileName);
            //generate a new file name
            var newFileName = Guid.NewGuid() + fileExtension;
            //get the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images\\Products", newFileName);

            //open the file stream
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                //copy the file to the server
                product.Image.CopyTo(fileStream);
            }

            ////save the other product's information
            product.ImageURL = newFileName;

            var productMap = _ProductMapper.Map<ProductsEntities>(product); 
            var data =_DAL.Insert(productMap);

            return data;
        }





    }
}
