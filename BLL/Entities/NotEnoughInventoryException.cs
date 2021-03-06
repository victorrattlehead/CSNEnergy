﻿using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class NotEnoughInventoryException: Exception
    {
        public NotEnoughInventoryException()
        {
            Missing = new List<INameQuantity>();
        }
        public IEnumerable<INameQuantity> Missing { get; }
    }
}
