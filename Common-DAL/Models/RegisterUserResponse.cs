﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class RegisterUserResponse
    {
        public string message { get; set; }
        public string user_security_stamp { get; set; }
    }
}
