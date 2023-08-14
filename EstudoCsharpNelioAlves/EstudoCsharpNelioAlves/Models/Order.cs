using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstudoCsharpNelioAlves.Models.Enums;

namespace EstudoCsharpNelioAlves.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Situação { get; set; }
        public OrderStatus Status { get; set; }


        public override string ToString()
        {
            return Id
                + ", "
                + Situação
                + ", "
                + Status;
        }

    }
}
