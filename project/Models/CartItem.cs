using System.Drawing;

namespace DoAnCoSo.Models
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

        public decimal Total
        {
            get { return ProductQuantity * ProductPrice; }
        }
        public string Image {  get; set; }
        
        public CartItem() 
        { 

        }

		public CartItem(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            ProductPrice = product.ProductPrice;
            ProductQuantity = 1;
            Image = product.ProductImage;
        }
	}
}
