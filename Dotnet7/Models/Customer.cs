namespace Dotnet7.Models
{
    public class Customer
    {
        public int CustomerId { get;set; }
        public string CustomerName { get;set; }
        public bool CustomerActivated { get;set; }
        
        public Decimal Balance { get;set; }
        public DateTime CreatedDate {get;set;}
    }
}
