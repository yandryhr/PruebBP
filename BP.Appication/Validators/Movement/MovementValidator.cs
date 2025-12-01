using BP.Appication.Dtos.Request;
using BP.Domain.Entities;
using FluentValidation;

namespace BP.Appication.Validators.Movement
{
    public class MovementValidator : AbstractValidator<MovementRequestDto>
    {
        public MovementValidator()
        {
            RuleFor(x => x.TipoMovimiento)
                .NotNull().WithMessage("El tipo de movimiento es requerido")
                .NotEmpty().WithMessage("El tipo de movimiento es requerido");

            RuleFor(x => x.Valor)
                .NotNull().WithMessage("El valoe del movimiento es requerido")
                .NotEmpty().WithMessage("El valoe del movimiento es requerido");
        }
    }
}
