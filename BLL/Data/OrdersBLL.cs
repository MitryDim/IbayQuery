using AutoMapper;
using BLL.Models;
using Dal;
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

        private Mapper _OrdersMapper;


        public OrdersBLL(DatabaseContext context)
        {
            _DAL = new Dal.Data.OrdersDAL(context);
            var _configOrders = new MapperConfiguration(cfg => cfg.CreateMap<OrdersEntities, OrdersModel>().ReverseMap());
            _OrdersMapper = new Mapper(_configOrders);
        }


        // Add an Order
        public OrdersEntities Insert(OrdersModel order)
        {
            try
            {
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
