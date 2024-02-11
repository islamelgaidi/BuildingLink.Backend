namespace Driver.API.Application.Models
{
    public record DriverCommand
    {      
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
