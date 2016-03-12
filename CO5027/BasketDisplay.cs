using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CO5027
{
    public class BasketDisplay
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
    }
}