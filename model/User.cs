using System;
using System.Collections.Generic;

namespace model
{
    public class User
    {
        //https://github.com/samiroquai/Henallux20172018/tree/master/BD%20Avancees%20et%20applications%20web/AspnetIdentityEFCore
        public int Id { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public Address Adresse { get; set; }
        public DateTime birthday { get; set; }
        public DateTime dateInscription { get; set;}
        public DateTime dateDerniereConnection { get; set; }
        public int Active { get; set; }



    }
}
