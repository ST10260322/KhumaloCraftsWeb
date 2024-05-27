namespace KhumaloCraftsWeb.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime? Date { get; set; }
    }
}
