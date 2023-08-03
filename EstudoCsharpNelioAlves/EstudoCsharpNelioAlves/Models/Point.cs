using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    struct Point
    {
        public double X;
        public double Y;
        public double? C = null;

        public Point()
        {
        }

        public override string ToString()
        {
            Console.WriteLine("double null vale: " + C.GetValueOrDefault());
            return "(" + X + ", " + Y + ")"; 
        }

    }
}
