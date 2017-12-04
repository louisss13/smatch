using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace model
{
    public class UserInfo 
    {
        //https://github.com/samiroquai/Henallux20172018/tree/master/BD%20Avancees%20et%20applications%20web/AspnetIdentityEFCore
        public String Id { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        
        public String Phone { get; set; }
        public Address Adresse { get; set; }
        



    }
}
