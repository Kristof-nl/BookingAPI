using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Api.Services.Abstractions
{
    public interface IOperation
    {
        Guid Guid { get; set; }
    }
}
