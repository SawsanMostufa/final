using AutoMapper;
using ECommerice.Api.DTOs;
using ECommerice.Core.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerice.Api.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemsDTO, string>
    {
        public readonly IConfiguration _config;
        public OrderItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemsDTO destination, string destMember, ResolutionContext context)
        {
            //if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            //{
            //    return _config["ApiUrl"] + source.ItemOrdered.PictureUrl;
            //}
            return null;
        }
    }
}
