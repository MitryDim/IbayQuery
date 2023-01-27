using AutoMapper;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
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


        public  bool AddToCart(CartsModel cart)
        {
            try
            {
                var CartExist = _DAL.GetCart(cart.UserId);

                if (CartExist == null)
                {

                    CartExist = new CartsEntities
                    {
                        UserId = cart.UserId,
                        Status = cart.Status,
                        CartItems = cart.CartItems,
                    };
                    return _DAL.AddToCart(CartExist);
                }

                var CartItemExist = CartExist.CartItems.FirstOrDefault(ci => ci.ProductId == cart.CartItems[0].ProductId && ci.Status == cart.CartItems[0].Status );
                
               
                if (CartItemExist != null)
                {
                    CartItemExist.Quantity += cart.CartItems[0].Quantity;
                  return  _DAL.Update(CartItemExist);
                }
                else
                {
                    CartItemExist = new CartsItemsEntities
                    {
                        CartId = CartExist.Id,
                        ProductId = cart.CartItems[0].ProductId,
                        Status = cart.CartItems[0].Status
                    };

                    return  _DAL.AddToCart(CartItemExist);
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public CartsModel GetCarts(int userId)
        {
            try
            {
                var cartEntity = _DAL.GetCart(userId);

                if (cartEntity == null)
                    return null;

                CartsModel cart = new CartsModel
                {
                    Id = cartEntity.Id,
                    UserId = cartEntity.UserId,
                    Status= cartEntity.Status,
                    CartItems = cartEntity.CartItems.Select(ci => new CartsItemsEntities
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Status = ci.Status
                    }).ToList()
                };
                return cart;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool Update(CartsModel cart)
        {
            try
            {
                CartsEntities cartEntity = new CartsEntities
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CartItems = cart.CartItems.Select(ci => new CartsItemsEntities
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Status = ci.Status
                    }).ToList()
                };
                _DAL.Update(cartEntity);
                return true;


            } catch(Exception)
            {
                return false;
            }
        }

        public bool RemoveFromCart(int productId,int userId)
        {
            try
            {
                return _DAL.RemoveFromCart(productId,userId);
            
            }catch(Exception)
            {
                return false;
            }
        }


        //public CartsModel Insert(int IdUser, int IdProduit)
        //{

        //    ProductsEntities currentProduct = _DALProducts.SearchById(IdProduit);

        //    if (currentProduct == null)
        //        throw new Exception("Une erreur s'est produite, produit introuvable ! ");

        //    if (currentProduct.Available != true)
        //         throw new Exception("Le produit n'est plus disponible !");


        //    UsersEntities currentUser = _DALUsers.SearchUser(IdUser);

        //    if (currentUser == null)
        //        throw new Exception("Une erreur s'est produite, Utilisateur introuvable ! ");


        //    CartsEntities carts = new CartsEntities { Product = currentProduct, User = currentUser, ProductId = currentProduct.Id, UserId = currentUser.Id };


        //    try
        //    {

        //         _DAL.Insert(carts);

        //    } catch(Exception ex)
        //    {
                
        //        throw new Exception("Une erreur s'est produite lors de l'insertion dans le panier");
        //    }
           

        //    return _CartsMapper.Map<CartsModel>(carts);

        //}

    }
}
