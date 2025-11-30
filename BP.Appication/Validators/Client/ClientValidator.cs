using BP.Application.Dtos.Request;
using FluentValidation;

namespace BP.Application.Validators.Client
{
    public class ClientValidator: AbstractValidator<ClientRequestDto>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacio");

            RuleFor(x => x.Identificacion)
                .NotNull().WithMessage("La identificación es requerida")
                .NotEmpty().WithMessage("La identificación es requerida");
        }
    }
}
