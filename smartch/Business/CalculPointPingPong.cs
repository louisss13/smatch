using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace Business
{
    public class CalculPointPingPong : ICalculPoint
    {
        private static readonly int POINT_PAR_MANCHE = 11;
        private static readonly int POINT_ECART_MANCHE = 2;
        private int currentPosition = 0;

        public Score Calcul(ICollection<Point> points)
        {
            currentPosition = 0;
            if (points == null)
                return null;
            Score score = new Score();
            score.Joueurs = new List<EJoueurs>();
            List<Point> orderedPoint = points.OrderBy(p => p.Order).ToList();
            foreach(Point point in orderedPoint)
            {
                if (!score.Joueurs.Contains(point.Joueur))
                    score.Joueurs.Add(point.Joueur);
                score = CalculPoint(point.Joueur, score);
            }
            return score;
        }
        private Score CalculPoint(EJoueurs joueur, Score score)
        {
            score = AddPoint( joueur,  score);
            int newPointJoueur = score.PointLevel[currentPosition][joueur];
            int pointMaxManche = score.PointLevel[currentPosition].Max(p=>p.Value);
            if (newPointJoueur >= POINT_PAR_MANCHE && newPointJoueur == pointMaxManche)
            {
                if(IsPointEcart(joueur, score))
                {
                    //score = AddSet(joueur, score);
                    currentPosition++;
                    
                }
            }
            return score;
        }
        private Dictionary<EJoueurs, int> InitDict()
        {
            Dictionary<EJoueurs, int> dictionnaire = new Dictionary<EJoueurs, int>();
            foreach (EJoueurs joueurE in Enum.GetValues(typeof(EJoueurs)))
            {
                dictionnaire.Add(joueurE, 0);
            }
            return dictionnaire;
        }
        private Score AddSet(EJoueurs joueur, Score score)
        {
            if (score.PointLevel.Count() < 2)
            {
                score.PointLevel.Add(InitDict());
                score.PointLevel[1][joueur]+= 1;
            }
            else if (!score.PointLevel[1].ContainsKey(joueur))
            {
                score.PointLevel[1].Add(joueur, 1);
            }
            else
            {
                score.PointLevel[1][joueur] += 1;
            }
            
            return score;
        }

        private Score AddPoint(EJoueurs joueur, Score score)
        {
            if (score.PointLevel == null)
            {
                score.PointLevel = new List<Dictionary<EJoueurs, int>>()
                {
                    InitDict()
                };
                score.PointLevel[currentPosition][joueur] += 1;
            }
            else if (score.PointLevel.Count() <= currentPosition)
            {
                score.PointLevel.Add(InitDict());
                score.PointLevel[currentPosition][joueur] += 1;
            }
            else if (!score.PointLevel[currentPosition].ContainsKey(joueur))
            {
                score.PointLevel[currentPosition].Add(joueur, 1);
            }
            else
            {
                score.PointLevel[currentPosition][joueur]+=1;
                
            }
            return score;
        }

        private Score RemiseAZeroManche(EJoueurs joueur, Score score)
        {
            foreach (EJoueurs joueurE in Enum.GetValues(typeof(EJoueurs)))
            {
                if(score.PointLevel[currentPosition].ContainsKey(joueurE))
                    score.PointLevel[currentPosition][joueurE] = 0;
            }
            return score;
        }

        private bool IsPointEcart(EJoueurs joueur, Score score)
        {
            bool isUnPointEcart = true;
            foreach (EJoueurs joueurE in Enum.GetValues(typeof(EJoueurs)))
            {
                if (joueurE != joueur &&
                    score.PointLevel[currentPosition][joueur] - score.PointLevel[currentPosition][joueurE] < POINT_ECART_MANCHE)
                {
                    isUnPointEcart = false;
                }
            }
            return isUnPointEcart;
        }
    }
}
