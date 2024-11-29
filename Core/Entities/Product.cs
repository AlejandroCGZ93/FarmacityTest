using Core.Entities.Base;

namespace Core.Entities
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUpdate { get; set; }

        public Barcode Barcode { get; set; }
    }
}
