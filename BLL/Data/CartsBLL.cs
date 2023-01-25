using AutoMapper;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public class CartsBLL
    {
        private Dal.Data.CartsDAL _DAL;


        private Dal.Data.ProductsDAL _DALProducts;
        private Dal.Data.Users _DALUsers;


        private Mapper _CartsMapper;


        public CartsBLL(DatabaseContext context)
        {
            _DALProducts = new Dal.Data.ProductsDAL(context);
            _DALUsers = new Dal.Data.Users(context);
            _DAL = new Dal.Data.CartsDAL(context);
            var _configUser = new MapperConfiguration(cfg => cfg.CreateMap<CartsEntities, CartsModel>().ReverseMap());
            _CartsMapper = new Mapper(_configUser);

        }
        public CartsModel SearchById(int Id)
        {
            return _CartsMapper.Map<CartsModel>(_DAL.SearchById(Id));
        }

        public CartsModel Insert(int IdUser, int IdProduit)
        {

            ProductsEntities currentProduct = _DALProducts.SearchById(IdProduit);

            if (currentProduct == null)
                throw new Exception("Une erreur s'est produite, produit introuvable ! ");

            if (currentProduct.Available != true)
                 throw new Exception("Le produit n'est plus disponible !");


            UsersEntities currentUser = _DALUsers.SearchUser(IdUser);

            if (currentUser == null)
                throw new Exception("Une erreur s'est produite, Utilisateur introuvable ! ");


            CartsEntities carts = new CartsEntities { Product = currentProduct, User = currentUser, ProductId = currentProduct.Id, UserId = currentUser.Id };


            try
            {

                 _DAL.Insert(carts);

            } catch(Exception ex)
            {
                
                throw new Exception("Une erreur s'est produite lors de l'insertion dans le panier");
            }
           

            return _CartsMapper.Map<CartsModel>(carts);

        }

    }
}
