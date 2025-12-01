using System;
using System.Collections.Generic;

namespace BP.Domain.Entities;

public partial class Movimiento
{
    public long MovimientoId { get; set; }

    public DateTime Fecha { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public decimal Valor { get; set; }

    public decimal Saldo { get; set; }

    public long NumeroCuenta { get; set; }    
}
