using DemoShop.Models.db;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoShop.ViewModels
{
    public class SaleBookCreateViewModel
    {
        public int SaleBookId { get; set; } // รหัสการขายหนังสือ
        public int CustomerID { get; set; }  // เลือกลูกค้า

        public int SaleId { get; set; }       // เลือก Sale (ถ้ามี)

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}


