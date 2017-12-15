
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace model
{
    public class Point
    {
        public long Id { get; set; }
        public EJoueurs Joueur { get; set; }
        public int Value { get; set; }
        public int Order { get; set; }
    }
}