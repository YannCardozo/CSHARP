using EstudoCsharpNelioAlves.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    abstract class Shape
    {
        public Color Color { get; set; }
        public abstract Double Area();
    }
}
