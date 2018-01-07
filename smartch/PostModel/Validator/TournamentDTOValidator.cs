using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel.Validator
{
    public static class TournamentDTOValidator
    {
        public static List<Error> Validate(TournamentDTO tournament, List<Error> errors)
        {
            errors = AddressValidator.IsAdresseCorrect(tournament.Address, errors);
            if (tournament.BeginDate > tournament.EndDate)
            {
                errors.Add(new Error()
                {
                    Code = "DateSequenceError",
                    Description = "La date de debut ne peut pas être après la date de fin"
                });
            }
            if (tournament.Name == null || tournament.Name.Length <= 0)
            {
                errors.Add(new Error()
                {
                    Code = "NameTournamentRequired",
                    Description = "Le nom du tournois ne peut pas être vide"
                });
            }

            return errors;
        }


    }
}
