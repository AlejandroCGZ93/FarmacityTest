
namespace Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUpdate { get; set; }
        public BarcodeDto Barcode { get; set; }

    }

    public class BarcodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUpdate { get; set; }

    }

}
