using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DTO
{
    public class DiscountDetail
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }

    public class DiscountResponse
    {
        public decimal OriginalTotal { get; set; }
        public List<DiscountDetail> Discounts { get; set; } = new List<DiscountDetail>();
        public decimal FinalTotal { get; set; }
        public string Error { get; set; } // Add this property
    }

}
