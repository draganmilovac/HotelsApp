using HotelsApp.Application.Dtos.Validation;
using FluentValidation.TestHelper;
using HotelsApp.Application.Dtos;

namespace HotelsApp.Tests.Validators
{
    public class HotelDtoValidatorTests
    {
        #region Fields
        private readonly HotelDtoValidator _hotelDtoValidator;
        private readonly HotelDto _hotelDto;
        #endregion

        #region Constructors
        public HotelDtoValidatorTests()
        {
            _hotelDtoValidator = new HotelDtoValidator();
            _hotelDto = new HotelDto();
        }
        #endregion

        #region Public methods

        [Fact]
        public void GivenAnInvalidPriceNumberValue_ShouldHaveValidationError()
        {
            _hotelDto.Price = 0m;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldHaveValidationErrorFor(h => h.Price);
        }

        [Fact]
        public void GivenAnValidPriceNumberValue_ShouldNotHaveValidationError()
        {
            _hotelDto.Price = 100m;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldNotHaveValidationErrorFor(h => h.Price);
        }

        [Fact]
        public void GivenAnInvalidNameValue_ShoulHaveValidationError()
        {
            _hotelDto.Name = string.Empty;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldHaveValidationErrorFor(h => h.Name);
        }
        [Fact]
        public void GivenAnValidNameValue_ShoulNotHaveValidationError()
        {
            _hotelDto.Name = "Hotel";
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldNotHaveValidationErrorFor(h => h.Name);
        }

        [Fact]
        public void GivenAnInvalidLatitudeValue_ShoulHaveValidationError()
        {
            _hotelDto.Latitude = 0d;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldHaveValidationErrorFor(h => h.Latitude);
        }
        [Fact]
        public void GivenAnValidLatitudeValue_ShoulNotHaveValidationError()
        {
            _hotelDto.Latitude =50.1111d;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldNotHaveValidationErrorFor(h => h.Latitude);
        }
        [Fact]
        public void GivenAnInvalidLongitudeValue_ShoulHaveValidationError()
        {
            _hotelDto.Longitude = 0d;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldHaveValidationErrorFor(h => h.Longitude);
        }
        [Fact]
        public void GivenAnValidLongitudeValue_ShoulNotHaveValidationError()
        {
            _hotelDto.Longitude = 50.111d;
            var result = _hotelDtoValidator.TestValidate(_hotelDto);
            result.ShouldNotHaveValidationErrorFor(h => h.Longitude);
        }
        #endregion
    }
}
