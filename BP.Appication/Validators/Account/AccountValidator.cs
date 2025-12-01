using BP.Appication.Dtos.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Appication.Validators.Account
{
    public class AccountValidator: AbstractValidator<AccountRequestDto>
    {
        public AccountValidator()
        {
            RuleFor(x => x.NumeroCuenta)
                .NotNull().WithMessage("El número de cuenta no puede ser nulo")
                .NotEmpty().WithMessage("El número de cuenta puede ser vacio");

            RuleFor(x => x.TipoCuenta)
                .NotNull().WithMessage("El tipo de cuenta es requerida")
                .NotEmpty().WithMessage("El tipo de cuenta es requerida");
        }
    }
}
