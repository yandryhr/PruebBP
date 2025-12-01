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
    public class AccountRepository : GenericRepository<Cuenta>, IAccountRepository
    {
        private readonly BpContext _context;

        public AccountRepository(BpContext context)
        {
            _context = context;
        }

        // Listar todas las cuentas con filtros opcionales y paginación
        public async Task<BaseEntityResponse<Cuenta>> ListAccounts(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Cuenta>();
            var accounts = _context.Cuenta.AsQueryable();

            if (filters != null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                accounts = accounts.Where(c => c.NumeroCuenta.Equals(filters.NumFilter));
            }
            if (filters.Sort == null || filters.Sort == "") filters.Sort = "NumeroCuenta";

            response.TotalRecords = await accounts.CountAsync();
            response.Items = await Ordering(filters, accounts, !(bool)filters.DownLoad).ToListAsync();
            return response;
        }

        // Obtener cuenta por número
        public async Task<Cuenta> AccountByNum(long numeroCuenta)
        {
            var account = await _context.Cuenta.FirstOrDefaultAsync(c => c.NumeroCuenta.Equals(numeroCuenta));
            return account!;
        }

        // Registrar nueva cuenta
        public async Task<bool> RegisterAccount(Cuenta account)
        {
            _context.Cuenta.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }

        // Editar cuenta
        public async Task<bool> EditAccount(Cuenta account)
        {
            _context.Cuenta.Update(account);
            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar cuenta
        public async Task<bool> DeleteAccount(long numeroCuenta)
        {
            var account = await _context.Cuenta.FindAsync(numeroCuenta);
            if (account == null) return false;

            _context.Cuenta.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
                
    }
}
