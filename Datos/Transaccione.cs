using System;
using System.Collections.Generic;

namespace Datos;

public partial class Transaccione
{
    public int Id { get; set; }

    public int IdCuentahabiente { get; set; }

    public decimal CantidadRetirada { get; set; }

    public DateTime FechaHora { get; set; }

    public virtual Cuentahabiente IdCuentahabienteNavigation { get; set; } = null!;
}
