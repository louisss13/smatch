using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ICalculPoint
    {
        List<List<int>> Calcul(List<Point>);
    }
}
