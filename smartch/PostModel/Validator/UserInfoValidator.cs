using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace smartch.PostModel.Validator
{
    public static class UserInfoValidator
    {
        public static List<Error> Validate(UserInfo user, List<Error> errors)
        {
            errors = AddressValidator.IsAdresseCorrect(user.Adresse, errors);
            try {
                new MailAddress(user.Email);
            }
            catch (ArgumentNullException)
            {
                errors.Add(new Error()
                {
                    Code = "EmailRequired",
                    Description = "le mail ne peut pas être vide"
                });
            }
            catch (FormatException)
            {
                errors.Add(new Error()
                {
                    Code = "EmailFormatError",
                    Description = "le mail n'est pas valide"
                });
            }
            if(user.FirstName == null || user.FirstName.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "FirstNameRequired",
                    Description = "Le prenom ne doit pas etre vide"
                });
            }
            if (user.Name == null || user.Name.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "NameRequired",
                    Description = "Le nom ne doit pas etre vide"
                });
            }
            if (user.Birthday == null || user.Birthday > DateTime.Now)
            {
                errors.Add(new Error()
                {
                    Code = "BirtdayRequired",
                    Description = "La date de naissance n'est pas valide"
                });
            }
            return errors;
        }
    }
}
