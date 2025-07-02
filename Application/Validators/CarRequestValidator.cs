using Application.Communication.Cars;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class CarRequestValidator : AbstractValidator<CarRequest>
    {
        public CarRequestValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is required.");
            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required.")
                .Must(BeAValidYear)
                .WithMessage("Year must be a valid year.");
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required.");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");
        }
        private bool BeAValidYear(int year)
        {
            return year > 1970 && year <= DateTime.UtcNow.Year;
        }
    }
}
