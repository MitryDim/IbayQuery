using AutoMapper;
using BLL.Models;
using Dal;
using Dal.Data;
using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public class OrdersBLL
    {
        private Dal.Data.OrdersDAL _DAL;
        private CartsDAL _DALCart;

        private Mapper _OrdersMapper;


        public OrdersBLL(DatabaseContext context)
        {
            _DALCart = new CartsDAL(context);
            _DAL = new Dal.Data.OrdersDAL(context);
            var _configOrders = new MapperConfiguration(cfg => cfg.CreateMap<OrdersEntities, OrdersModel>().ReverseMap());
            _OrdersMapper = new Mapper(_configOrders);
        }


        // Add an Order
        public bool Insert(OrdersModel order)
        {
            try
            {

                var cart = _DALCart.GetCart(order.UserId);

                if (cart == null)
                    return false;

                order.CartId = cart.Id;
                order.Added_Hour = DateTime.Now;
                order.Payements = new List<PayementsEntities> {
                    new PayementsEntities
                    {
                     Amount= order.Payements[0].Amount,
                     Status = ((order.Status == "cancel")  ? "failed" : "success"),
                     Type = order.Payements[0].Type,
                     CreatedAt= DateTime.Now
                    } };

                var orderMap = _OrdersMapper.Map<OrdersEntities>(order);

                return _DAL.Insert(orderMap);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
