using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartch.PostModel
{
    public class PointDTO
    {
        public long Id { get; set; }
        public EJoueurs Joueur { get; set; }
        public int Order { get; set; }

        public PointDTO() { }
        public PointDTO(Point point)
        {
            Id = point.Id;
            Joueur = point.Joueur;
            Order = point.Order;
        }
        public Point GetPoint()
        {
            return new Point()
            {
                Id = Id,
                Joueur = Joueur,
                Order = Order
            };
        }
    }
}
