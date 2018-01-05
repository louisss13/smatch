using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model
{
    public class Club
    {
        public long Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public Address Adresse{ get; set; }
        [Required]
        [EmailAddress]
        public String ContactMail { get; set; }
        [Required]
        [Phone]
        public String Phone { get; set; }
        public List<ClubAdmins> Admins { get; set; } = new List<ClubAdmins>();
        public List<ClubMember> Members { get; set; }
        public List<Tournament> Tournaments { get; set; }

        public Club() {
           
        }

        public override bool Equals(object obj)
        {
            var club = obj as Club;
            return club != null &&
                   Id == club.Id;
        }

        public override int GetHashCode()
        {
            return 1774844376 + Id.GetHashCode();
        }
    }
}
