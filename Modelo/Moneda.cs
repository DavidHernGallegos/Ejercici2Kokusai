using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Moneda
    {
        public int MonedaTipo { get; set; }
        public int Cantidad { get; set; }

        public List<object> Monedas { get; set; }

    }
}
