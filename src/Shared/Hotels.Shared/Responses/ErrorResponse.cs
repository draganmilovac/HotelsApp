using HotelsApp.Shared.Enums;
using System.Text.Json.Serialization;

namespace HotelsApp.Shared.Responses
{
    public class ErrorResponse : IResponse
    {
        #region Properties
        public bool IsSuccess
        {
            get
            {
                return false;
            }
        }
        public string Message { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorCode ErrorCode { get; set; }
        #endregion

        #region Constructors

        public ErrorResponse() { }

        public ErrorResponse(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public ErrorResponse(ErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
        #endregion

        #region Public methods
        public static ErrorResponse Error(ErrorCode errorCode, string message)
            => new ErrorResponse(errorCode, message);

        public static ErrorResponse<T> Error<T>(ErrorCode errorCode, string message)
            => new ErrorResponse<T>(errorCode, message);
        #endregion
    }

    public class ErrorResponse<T> : ErrorResponse, IResponse<T>
    {
        #region Properties
        public T Result { get; }
        #endregion

        #region Constructors
        public ErrorResponse(ErrorCode errorCode) : base(errorCode)
        {
        }
        public ErrorResponse(ErrorCode errorCode, string message) : base(errorCode, message)
        {
        }
        #endregion

    }
}
