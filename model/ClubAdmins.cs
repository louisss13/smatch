using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class ClubAdmins
    {
     
        public string AccountId { get; set; }
        public int ClubId { get; set; }
        public Account Account { get; set; }
        //public Club Club { get; set; }
    }
}
