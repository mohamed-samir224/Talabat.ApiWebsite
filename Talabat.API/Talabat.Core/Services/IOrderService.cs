﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services
{
    public interface IOrderService 
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, AddressShipping Shippingaddress, int DeliveryMethodId);
    }
}
