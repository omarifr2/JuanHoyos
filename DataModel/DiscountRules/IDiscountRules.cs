﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DiscountRules
{
    public interface IDiscountRule
    {
        decimal ApplyDiscount(Cart cart);
    }

}
