using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class CarValidation : AbstractValidator<Car>
    {
        public CarValidation()
        {
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is required.");
            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required.")
                .Must(BeAValidYear)
                .WithMessage("Year must be a valid year.");
        }
        private bool BeAValidYear(int year)
        {
            return year > 1970 && year <= DateTime.UtcNow.Year;
        }
    }
}
