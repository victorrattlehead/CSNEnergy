using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Entities.Interface
{
    interface IStore
    {
        void Import(string catalogAsJson);
        int Quantity(string name);
        double Buy(params string[] basketByNames);
    }
}
