namespace CarSalesDataAccessLayer.Models
{
    public partial class Orders
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public int StatuID { get; set; }
        public bool IsActive { get; set; }
    }
}
