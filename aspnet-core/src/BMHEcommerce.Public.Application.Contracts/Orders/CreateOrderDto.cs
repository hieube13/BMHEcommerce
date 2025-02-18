﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Public.Orders
{
    public class CreateOrderDto
    {
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public Guid? CustomerUserId { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }
}
