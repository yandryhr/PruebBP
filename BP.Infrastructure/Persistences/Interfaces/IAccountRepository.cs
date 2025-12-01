using BP.Domain.Entities;
using BP.Infrastructure.Commons.Bases.Request;
using BP.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Infrastructure.Persistences.Interfaces
{
    public interface IAccountRepository
    {
        Task<BaseEntityResponse<Cuenta>> ListAccounts(BaseFiltersRequest filters);  
        Task<Cuenta> AccountByNum(long numeroCuenta);
        Task<bool> RegisterAccount(Cuenta account);       
        Task<bool> EditAccount(Cuenta account);    
        Task<bool> DeleteAccount(long numeroCuenta);            
    }
}
