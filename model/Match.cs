using model.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace model
{
    public class Match
    {
        public long Id { get; set; }
        [Required]
        [Range(typeof(int), "0", "10000")]
        public int Phase { get; set; }
        [Required]
        [DateMaxNow]
        public TimeSpan DebutPrevu { get; set; }
        [Required]

        public UserInfo Joueur1 { get; set; }
        [Required]
        public UserInfo Joueur2 { get; set; }
        [Required]
        public Account Arbitre { get; set; }
        [Required]
        public String Emplacement { get; set; }
        public ICollection<Point> Score { get; set; }
    }
}