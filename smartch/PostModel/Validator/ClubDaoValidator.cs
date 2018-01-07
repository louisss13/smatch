using model;
using smartch.PostModel;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace smartch.PostModel.Validator
{
    static class ClubDaoValidator
    {
        public static List<Error> Validate(ClubDTO club, List<Error> errors)
        {
            errors = AddressValidator.IsAdresseCorrect(club.Adresse, errors);
            try
            {
                new MailAddress(club.ContactMail);
            }
            catch (ArgumentNullException)
            {
                errors.Add(new Error()
                {
                    Code = "NullMailAddress",
                    Description = "L'adresse mail ne peut pas être vide"
                }
                );
            }
            catch (FormatException )
            {
                errors.Add(new Error()
                {
                    Code = "IncorrectMailAddress",
                    Description = "Le format de l'adresse mail n'est pas reconnu"
                }
                );
            }
            if (club.Name == null || club.Name.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "NameRequired",
                    Description = "Le nom du club ne peux pas être vide"
                }
                );
            }
            
            if (club.Phone == null || club.Phone.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "PhoneRequired",
                    Description = "Le numero de telephone du club ne peux pas être vide"
                }
                );
            }
            return errors;
        }
    }
}