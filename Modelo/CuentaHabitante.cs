using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CuentaHabitante
    {
        public int? Id { get; set; }

        public string? NombreCompleto { get; set; }

        public decimal? Saldo { get; set; }

        public List<object>? CuentaHabitantes { get; set;}



    }
}
