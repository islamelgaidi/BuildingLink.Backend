using Driver.API.Domain.ValueObjects;

namespace Driver.API.Domain.Entities
{
    public  class Driver
    {   
        public DriverId Id{ get; set; } 
        public string FirstName { get;private set; }

        public string LastName { get;private set;}

        public EmailAddress Email { get; private set; }

        public PhoneAddress PhoneNumber { get; private set; }

        public Driver(DriverId id, string firstName, string lastName, EmailAddress email, PhoneAddress phoneNumber)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id), $"{nameof(id)} can't be empty.");
            //
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName), $"{nameof(firstName)} can't be empty.");
            //
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName), $"{nameof(lastName)} can't be empty.");
            //
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public Driver(string firstName, string lastName, EmailAddress email, PhoneAddress phoneNumber) : this((DriverId) Guid.NewGuid(), firstName, lastName, email, phoneNumber)
        {
           
        }
    }
}
