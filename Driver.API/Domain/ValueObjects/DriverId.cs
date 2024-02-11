using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.API.Domain.ValueObjects
{
    public record DriverId
    {
        private readonly Guid _value;
        private DriverId(Guid value)
        {
            _value = value;
        }
        public static DriverId New()=>new DriverId(Guid.NewGuid());
        public static implicit  operator Guid(DriverId id)=>id._value;
        public static explicit operator DriverId(Guid guid) => new DriverId(guid);
        public static explicit operator DriverId(string id)=>new DriverId(Guid.Parse(id));
       
        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
