using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public class UserInfo 
    {

        public long Id { get; set; }
        
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public Address Adresse { get; set; }
        public DateTime Birthday { get; set; }
        public Account CreatedBy { get; set; }


    }
}
