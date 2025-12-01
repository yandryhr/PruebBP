using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Appication.Dtos.Request
{
    public class MovementRequestDto
    {
        
        public DateTime Fecha { get; set; }
        public required string TipoMovimiento { get; set; }
        public required decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public required long NumeroCuenta { get; set; }
    }
}
