using Driver.API.Domain.ValueObjects;

namespace Driver.API.Domain.Interfaces
{
    public interface IDriverDbContext
    {
        List<Entities.Driver> FindAll(int take,int skip);
        Entities.Driver Find(DriverId id);
        Entities.Driver Find(EmailAddress email);
        Entities.Driver Find(PhoneAddress phoneNumber);
        bool Insert(Entities.Driver driver);

        bool Update(Entities.Driver driver);
        bool Delete(DriverId Id);


    }
}
