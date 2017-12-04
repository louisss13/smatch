using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class Account :  IdentityUser
    {
        public DateTime Birthday { get; set; }
        public DateTime DateInscription { get; set; }
        public DateTime DateDerniereConnection { get; set; }
        public int Active { get; set; }
    }
}
