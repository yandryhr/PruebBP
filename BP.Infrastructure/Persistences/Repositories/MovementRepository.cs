using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using BP.Infrastructure.Persistences.Contexts;
using BP.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Infrastructure.Persistences.Repositories
{
    public class MovementRepository: IGenericRepository<Movimiento>, IMovementRepository
    {
        private readonly BpContext _context;

        public MovementRepository(BpContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterMovement(Movimiento movimiento)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Obtener la cuenta
                var account = await _context.Cuenta.FirstOrDefaultAsync(c => c.NumeroCuenta.Equals(movimiento.NumeroCuenta));


                if (account == null || !account.Estado)
                    throw new InvalidOperationException("Cuenta no existe o está inactiva.");

                // Ajustar valor según tipo de movimiento
                if (movimiento.TipoMovimiento.Equals("Debito", StringComparison.OrdinalIgnoreCase))
                {
                    if (account.SaldoInicial < movimiento.Valor)
                        throw new InvalidOperationException("Saldo no disponible.");

                    movimiento.Valor = -Math.Abs(movimiento.Valor); // débito negativo
                }
                else // Crédito
                {
                    movimiento.Valor = Math.Abs(movimiento.Valor); // positivo
                }

                // Actualizar saldo de la cuenta
                account.SaldoInicial += movimiento.Valor;

                // Registrar movimiento
                movimiento.Fecha = DateTime.Now;
                movimiento.Saldo = account.SaldoInicial;
                _context.Movimientos.Add(movimiento);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                
                return false;
            }   
            
        }

        public async Task<List<EstadoCuentaDto>> AccountStatusReport(AccounStatusRequest filters)
        {
            // Obtener el cliente
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.PersonaId == filters.ClientId);
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            // Obtener todas las cuentas del cliente
            var cuentas = await _context.Cuenta
                .Where(c => c.ClienteId == cliente.PersonaId)
                .ToListAsync();

            var reporte = new List<EstadoCuentaDto>();

            foreach (var cuenta in cuentas)
            {
                // Obtener los movimientos de la cuenta dentro del rango de fechas
                var movimientos = await _context.Movimientos
                    .Where(m => m.NumeroCuenta == cuenta.NumeroCuenta
                                && m.Fecha >= DateTime.Parse(filters.StartDate)
                                && m.Fecha <= DateTime.Parse(filters.EndDate))
                    .OrderBy(m => m.Fecha)
                    .ToListAsync();

                decimal saldoDisponible = cuenta.SaldoInicial;

                foreach (var mov in movimientos)
                {
                    
                    reporte.Add(new EstadoCuentaDto
                    {
                        Fecha = mov.Fecha.ToString("dd/MM/yyyy"),
                        Cliente = cliente.Nombre,
                        NumeroCuenta = cuenta.NumeroCuenta.ToString(),
                        Tipo = cuenta.TipoCuenta,
                        SaldoInicial = saldoDisponible- (mov.TipoMovimiento.ToLower() == "debito" ? -mov.Valor : mov.Valor),
                        Estado = cuenta.Estado,
                        Movimiento = mov.TipoMovimiento.ToLower() == "debito" ? -mov.Valor : mov.Valor,
                        SaldoDisponible = saldoDisponible
                    });
                }
            }

            return reporte;
        }
    }
}
