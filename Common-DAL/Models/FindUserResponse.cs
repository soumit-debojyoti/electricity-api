﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Electricity_DAL.Models
{
    public class FindUserResponse
    {
        public bool IsUserExist { get; set; }
        public int role_id { get; set; } = 0;
    }
}
