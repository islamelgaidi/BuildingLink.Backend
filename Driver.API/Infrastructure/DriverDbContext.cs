using Driver.API.Domain.Entities;
using Driver.API.Domain.Interfaces;
using Driver.API.Domain.ValueObjects;
using System.Data;

namespace Driver.API.Infrastructure
{
    public class DriverDbContext : IDriverDbContext
    {        
        private readonly IDbContext _dbContext;
        private readonly ILogger<IDriverDbContext> _logger;
        public DriverDbContext(IDbContext dbContext,ILogger<DriverDbContext> logger)
        {            
            _dbContext = dbContext;
            _logger = logger;
        }
        public List<Domain.Entities.Driver> FindAll(int take, int skip)
        {
            List<Domain.Entities.Driver> res = new List<Domain.Entities.Driver>();
            string sql = @"SELECT  * FROM driver LIMIT @pageSize OFFSET @offset;";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@pageSize", take },
                { "@offset", skip }
            };
            //
            DataTable dt = _dbContext.GetDataTable(sql, parameters);
            foreach (DataRow dr in dt.Rows)
            {
                var driver = MapDataRowToDriver(dr);
                if (driver != null)
                {
                    res.Add(driver);
                }
            }
            //
            return res;
        }
        public Domain.Entities.Driver Find(DriverId Id)
        {
            Domain.Entities.Driver res = null;
            string sql = @"SELECT  * FROM driver WHERE Id=@Id LIMIT 1";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", Id.ToString() }
            };
            //
            DataTable dt = _dbContext.GetDataTable(sql, parameters);
            if(dt.Rows.Count > 0 )
            {
                res = MapDataRowToDriver(dt.Rows[0]);
            }
            //
            return res;
        }
        public Domain.Entities.Driver Find(EmailAddress email)
        {
            Domain.Entities.Driver res = null;
            string sql = @"SELECT  * FROM driver WHERE Email=@Email LIMIT 1";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Email", email.ToString() }
            };
            //
            DataTable dt = _dbContext.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                res = MapDataRowToDriver(dt.Rows[0]);
            }
            //
            return res;
        }

        public Domain.Entities.Driver Find(PhoneAddress phoneNumber)
        {
            Domain.Entities.Driver res = null;
            string sql = @"SELECT  * FROM driver WHERE PhoneNumber=@PhoneNumber LIMIT 1";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@PhoneNumber", phoneNumber.ToString() }
            };
            //
            DataTable dt = _dbContext.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                res = MapDataRowToDriver(dt.Rows[0]);
            }
            //
            return res;
        }
      
        private Domain.Entities.Driver MapDataRowToDriver(DataRow dr)
        {
            Domain.Entities.Driver driver = null;
            try
            {
                driver = new Domain.Entities.Driver(
                     id: (DriverId)dr["Id"].ToString() ,
                     firstName: dr["FirstName"] as string,
                     lastName: dr["LastName"] as string,
                     phoneNumber:(PhoneAddress) dr["PhoneNumber"].ToString() ,
                     email:(EmailAddress) dr["Email"].ToString()
                     );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"MapDataRowToDriver id={dr["Id"]} , firstName={dr["FirstName"]} , LastName={dr["LastName"]} ,PhoneNumber={dr["PhoneNumber"]} , Email={dr["Email"]}");
            }
            return driver;
        }
        public bool Insert(Domain.Entities.Driver driver)
        {
            string sql = @"INSERT INTO driver 
                            (Id,FirstName,LastName,PhoneNumber,Email)  
                     VALUES (@Id,@FirstName,@LastName,@PhoneNumber,@Email);";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", driver.Id.ToString() },
                { "@FirstName", driver.FirstName },
                { "@LastName", driver.LastName },
                { "@PhoneNumber", driver.PhoneNumber.ToString() },
                { "@Email", driver.Email.ToString() }
            };
            return _dbContext.Execute(sql, parameters) > 0;
        }

        public bool Update(Domain.Entities.Driver driver)
        {
            string sql = @"UPDATE driver  SET
                                FirstName=@FirstName,
                                LastName=@LastName,
                                PhoneNumber=@PhoneNumber,
                                Email=@Email
                            WHERE Id=@Id
                            ";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Id", driver.Id.ToString() },
                { "@FirstName", driver.FirstName },
                { "@LastName", driver.LastName },
                { "@PhoneNumber", driver.PhoneNumber.ToString() },
                { "@Email", driver.Email.ToString() }
            };
            return _dbContext.Execute(sql, parameters) > 0;
        }
        public bool Delete(DriverId Id)
        {
            string sql = @"DELETE FROM driver                               
                            WHERE Id=@Id
                            ";
            //
            Dictionary<string, object> parameters = new Dictionary<string, object>
            { { "@Id", Id.ToString()} };
            return _dbContext.Execute(sql, parameters) > 0;
        }

     
    }
}
