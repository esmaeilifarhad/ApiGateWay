﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Model
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }


        public string firstName { get; set; }
        public string lastName { get; set; }
        public string token { get; set; }


        public ICollection<UserRole> UserRoles { get; set; }
    }
}
