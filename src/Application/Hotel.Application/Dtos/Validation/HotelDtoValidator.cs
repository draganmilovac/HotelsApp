using FluentValidation;

namespace HotelsApp.Application.Dtos.Validation
{
    public class HotelDtoValidator : AbstractValidator<HotelDto>
    {
        #region Consturcotrs
        public HotelDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please add Hotel name");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be a positive number");

            RuleFor(x => x.Latitude)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please add Latitude");

            RuleFor(x => x.Longitude)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please add Longitude");
        }
        #endregion
    }
}
