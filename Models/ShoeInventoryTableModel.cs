using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Porfolio6_1.Models
{
    public class ShoeInventoryTableModel
    {
        public int id { get; set; }
        public string shoeName { get; set; }
        public string shoeDescription { get; set; }
        public string shoePrice { get; set; }
        public int shoeSize { get; set; }
        public int quantity { get; set; }

    }
}