using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class GuestValidator : AbstractValidator<Guest>
    {
        public GuestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(2)
                .WithMessage("Name must be at least 2 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email must be a valid email address.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required.")
                .Matches(@"^\(?((11|12|13|14|15|16|17|18|19|21|22|24|27|28|
31|32|33|34|35|37|38|41|42|43|44|45|46|47|48|
49|51|53|54|55|61|62|64|63|65|66|67|68|69|71|73|74|75|77|79|
81|82|83|84|85|86|87|
88|89|91|92|93|94|95|96|97|98|99))\)? ?9?[0-9]{4}-?[0-9]{4}$")
                .WithMessage("Phone must be a valid phone number.");

            RuleFor(x => x.Cpf).NotEmpty()
                .WithMessage("CPF is required.")
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
                .WithMessage("CPF must be in the format XXX.XXX.XXX-XX.");
        }
    }
 
}
