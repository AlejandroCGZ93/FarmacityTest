using Core.Entities.Base;

namespace Core.Entities
{
    public class Barcode : Entity<int>
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime DateUpdate { get; set; }

        public Product Product { get; set; }
    }
}
