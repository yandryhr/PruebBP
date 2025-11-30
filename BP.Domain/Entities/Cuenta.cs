using System;
using System.Collections.Generic;

namespace BP.Domain.Entities;

public partial class Cuenta
{
    public long NumeroCuenta { get; set; }

    public string TipoCuenta { get; set; } = null!;

    public decimal SaldoInicial { get; set; }

    public bool Estado { get; set; }

    public long ClienteId { get; set; }
}
