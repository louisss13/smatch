using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ICalculPoint
    {
        Score Calcul(ICollection<Point> points);
    }

}
