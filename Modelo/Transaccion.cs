using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Transaccion
    {

        public int? Id { get; set; }

        public int? IdCuentahabiente { get; set; }

        public decimal? CantidadRetirada { get; set; }

        public DateTime? FechaHora { get; set; }
    }
}
