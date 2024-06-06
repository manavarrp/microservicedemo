using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(c => c.UserName).NotEmpty()
                .WithMessage("UserName no puede estar vacio")
                .NotNull()
                .EmailAddress().WithMessage("Username debe ser un correo valido");
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Primer nombre no puede estar vacio")
                .MaximumLength(100).WithMessage("No puede exceder los 50 caracteres");
            RuleFor(c => c.TotalPrice).GreaterThan(0).WithMessage("Debe ser mayor 0.");
            RuleFor(c => c.EmailAddress).EmailAddress().WithMessage("Emailaddress  debe ser un correo valido.");

        }
    }
}
