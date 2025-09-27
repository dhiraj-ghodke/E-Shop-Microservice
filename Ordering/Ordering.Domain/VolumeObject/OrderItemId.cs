using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.VolumeObject
{
    public record OrderItemId
    {
        public Guid Value { get;}
        private OrderItemId(Guid value)=> Value = value;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if(value == Guid.Empty)
            {
                throw new DomainException("OrderItemId Connot be empty. ");

            }
            return new OrderItemId(value);
        }


    }
}
