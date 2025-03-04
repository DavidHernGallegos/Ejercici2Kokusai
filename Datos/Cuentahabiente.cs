﻿using System;
using System.Collections.Generic;

namespace Datos;

public partial class Cuentahabiente
{
    public int Id { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public decimal Saldo { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
