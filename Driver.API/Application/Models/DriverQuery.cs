namespace Driver.API.Application.Models
{
    public record DriverQuery
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public DriverQuery() { }
        public DriverQuery(Domain.Entities.Driver driver) 
        {
            this.Id = driver.Id.ToString();
            this.firstName = driver.FirstName;
            this.lastName = driver.LastName;
            this.phoneNumber= driver.PhoneNumber.ToString();
            this.email = driver.Email.ToString();
        
        }
    }
}
