using HotelsApp.Application.Dtos.Validation;
using HotelsApp.Application.Dtos;
using FluentValidation.TestHelper;

namespace HotelsApp.Tests.Validators
{
    public class PaginationParamsDtoValidatorTests
    {
        #region Fields
        private readonly PaginationParamsDtoValidator _paginationParamsDtoValidator;
        private readonly PaginationParamsDto _paginationParamsDto;
        #endregion

        #region Constructors
        public PaginationParamsDtoValidatorTests()
        {
            _paginationParamsDtoValidator = new PaginationParamsDtoValidator();
            _paginationParamsDto = new PaginationParamsDto();
        }
        #endregion

        #region Public methods

        [Fact]
        public void GivenAnInvalidSizePerPageValue_ShouldHaveValidationError()
        {
            _paginationParamsDto.SizePerPage=0;
            var result = _paginationParamsDtoValidator.TestValidate(_paginationParamsDto);
            result.ShouldHaveValidationErrorFor(p => p.SizePerPage);
        }

        [Fact]
        public void GivenAnValidSizePerPageValue_ShouldNotHaveValidationError()
        {
            _paginationParamsDto.SizePerPage = 10;
            var result = _paginationParamsDtoValidator.TestValidate(_paginationParamsDto);
            result.ShouldNotHaveValidationErrorFor(p => p.SizePerPage);
        }
        [Fact]
        public void GivenAnInvalidPageValue_ShouldHaveValidationError()
        {
            _paginationParamsDto.Page = 0;
            var result = _paginationParamsDtoValidator.TestValidate(_paginationParamsDto);
            result.ShouldHaveValidationErrorFor(p => p.Page);
        }
        [Fact]
        public void GivenAnValidPageValue_ShouldNotHaveValidationError()
        {
            _paginationParamsDto.Page = 10;
            var result = _paginationParamsDtoValidator.TestValidate(_paginationParamsDto);
            result.ShouldNotHaveValidationErrorFor(p => p.Page);
        }
        #endregion
    }
}
