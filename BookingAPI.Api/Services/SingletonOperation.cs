﻿using BookingAPI.Api.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Api.Services
{
    public class SingletonOperation : ISingletonOperation
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }


}
