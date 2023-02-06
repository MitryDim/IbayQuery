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
        private Dal.Data.UsersDAL _DALUsers;
        private CartsItemsDAL _DALCartItems;


        private Mapper _CartsMapper;


        public CartsBLL(DatabaseContext context)
        {
            _DALProducts = new Dal.Data.ProductsDAL(context);
            _DALUsers = new Dal.Data.UsersDAL(context);
            _DAL = new Dal.Data.CartsDAL(context);
            _DALCartItems = new CartsItemsDAL(context);
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

                var CartItemExist = CartExist.CartItems.FirstOrDefault(ci => ci.ProductId == cart.CartItems.First().ProductId && ci.Status == cart.CartItems.First().Status );
                
               
                if (CartItemExist != null)
                {
                    CartItemExist.Quantity += cart.CartItems.First().Quantity;
                  return _DALCartItems.Update(CartItemExist);
                }
                else
                {
                    CartItemExist = new CartsItemsEntities
                    {
                        CartId = CartExist.Id,
                        ProductId = cart.CartItems.First().ProductId,
                        Status = cart.CartItems.First().Status
                    };

                    return _DALCartItems.Insert(CartItemExist);
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
                        Status = ci.Status,
                        Product = ci.Product
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

        public bool RemoveFromCart(int productId, int userId)
        {
            try
            {
                return _DALCartItems.Delete(productId, userId);

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
