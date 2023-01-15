using FluentValidation;

namespace HotelsApp.Application.Dtos.Validation
{
    public class PaginationParamsDtoValidator : AbstractValidator<PaginationParamsDto>
    {
        #region Constructors
        public PaginationParamsDtoValidator()
        {
            RuleFor(x => x.SizePerPage)
                .GreaterThan(0)
                .WithMessage("Please add number of items per page");

            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Please add number of pages");
        }
        #endregion
    }
}
