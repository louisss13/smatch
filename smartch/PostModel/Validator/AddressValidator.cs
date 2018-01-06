using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel.Validator
{
    public static class AddressValidator
    {
        public static List<Error> IsAdresseCorrect(Address adresse, List<Error> errors)
        {
            if(adresse == null)
            {
                errors.Add(new Error()
                {
                    Code = "AddressRequired",
                    Description = "l'adresse ne peut pas etre vide"
                }
                );
                return errors;
            }
            if (adresse.City == null || adresse.City.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "AddressRequiredCity",
                    Description = "La ville doit être remplie"
                }
                );
            }
            if (adresse.Number == null || adresse.Number.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "AddressRequiredNumber",
                    Description = "Le numéro doit être remplis"
                }
                );
            }
            if (adresse.Street == null || adresse.Street.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "AddressRequiredStreet",
                    Description = "La rue doit être remplie"
                }
                );
            }
            if (adresse.ZipCode == null || adresse.ZipCode.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "AddressRequiredZipCode",
                    Description = "Le code postal doit être remplis"
                }
                );
            }
            return errors;
        }
    }
}
