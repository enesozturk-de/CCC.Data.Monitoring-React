﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CCC.Data.Monitoring.Concrete.Entities
{
    [Serializable]
    public class LoginEntity
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
