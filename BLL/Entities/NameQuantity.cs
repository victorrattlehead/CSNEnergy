using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class NameQuantity : INameQuantity
    {
        public string Name {get;set;}

        public int Quantity { get; set; }
    }
}
