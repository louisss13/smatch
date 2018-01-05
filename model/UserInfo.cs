using Microsoft.AspNetCore.Identity;
using model.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace model
{
    public class UserInfo 
    {

        public long Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String FirstName { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        [Phone]
        public String Phone { get; set; }
        [Required]
        public Address Adresse { get; set; }
        [Required]
        [DateMaxNow]
        public DateTime Birthday { get; set; }
        [Required]
        public Account CreatedBy { get; set; }
        
        public Account Owner { get; set; }


    }
}
