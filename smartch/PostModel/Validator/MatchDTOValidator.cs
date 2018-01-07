using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel.Validator
{
    public static class MatchDTOValidator
    {
        public static List<Error> Validate(MatchDTO match, List<Error>errors)
        {
            if(match.Arbitre == null)
            {
                errors.Add(new Error()
                {
                    Code = "ArbitreRequired",
                    Description = "Un arbitre est requi pour un match"
                });
            }
            if (match.DebutPrevu == null)
            {
                errors.Add(new Error()
                {
                    Code = "DebutPrevuRequired",
                    Description = "Une heure de début est requise pour un match"
                });
            }
            if (match.Emplacement == null)
            {
                errors.Add(new Error()
                {
                    Code = "EmplacementRequired",
                    Description = "Un emplacement est requi pour un match"
                });
            }
            if (match.Phase > 0)
            {
                errors.Add(new Error()
                {
                    Code = "PhaseRequired",
                    Description = "Une phaseplus grande que 0 est requise pour un match"
                });
            }
            if (match.Joueur1 == null)
            {
                errors.Add(new Error()
                {
                    Code = "Joueur1Required",
                    Description = "Un Joueur1 est requis pour un match"
                });
            }
            if (match.Joueur2 == null)
            {
                errors.Add(new Error()
                {
                    Code = "Joueur2Required",
                    Description = "Un Joueur1 est requis pour un match"
                });
            }
            return errors;
        }
    }
}
