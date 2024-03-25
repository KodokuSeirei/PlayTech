using PlayTech.Services;
using System.Collections.Generic;

namespace PlayTech.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        private List<StockShelf> stockShelfs = new List<StockShelf>();
        public List<StockShelf> StockShelfs
        {
            get
            {
                if (Id > 0)
                {
                    stockShelfs = StockShelfService.GetStockShelfsByProduct(Id);
                }
                return stockShelfs;
            }
        }
    }
}
