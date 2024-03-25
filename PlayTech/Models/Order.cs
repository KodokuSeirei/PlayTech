using PlayTech.Services;
using System.Collections.Generic;

namespace PlayTech.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }

        private List<ShoppingCart> positions = new List<ShoppingCart>();
        public List<ShoppingCart> Positions
        {
            get
            {
                if (Id > 0)
                    positions = ShoppingCartService.GetShoppingCartsByOrder(Id);
                return positions;
            }
        }

    }
}
