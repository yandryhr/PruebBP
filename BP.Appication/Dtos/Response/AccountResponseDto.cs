using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Appication.Dtos.Response
{
    public class AccountResponseDto
    {
        public required long NumeroCuenta { get; set; }
        public required string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public long ClienteId { get; set; }
        public string? EstadoCuenta { get; set; }
    }
}
