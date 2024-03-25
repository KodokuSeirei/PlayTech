namespace PlayTech.Models
{
    //Модель для позиций в заказе
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int StockShelfId { get; set; }
        public string StockShelfName { get; set; }
        public int Count { get; set; }
    }
}
