using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel.Validator
{
    public static class PointDTOValidator
    {
        public static List<Error> Validate(PointDTO point, List<Error>errors)
        {
            bool isCorrectJoueur = false;
            foreach(EJoueurs joueur in Enum.GetValues(point.Joueur.GetType()))
            {
                if(joueur == point.Joueur)
                {
                    isCorrectJoueur = true;
                }
            }
            if (!isCorrectJoueur)
            {
                errors.Add(new Error()
                {
                    Code = "JoueurUnknow",
                    Description = "Ce joueur n'esxiste pas dans l'enumeration"
                });
            }
            return errors;
        }
    }
}
