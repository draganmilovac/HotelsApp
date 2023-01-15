using HotelsApp.Shared.Enums;

namespace HotelsApp.Shared.Responses
{
    public class SuccessResponse : IResponse
    {
        #region Properties
        public bool IsSuccess
        {
            get
            {
                return true;
            }
        }

        public string Message { get; }

        public ErrorCode ErrorCode { get; }
        #endregion

        #region Public methods
        public static SuccessResponse Success()
        {
            return new SuccessResponse();
        }
        public static SuccessResponse<T> Success<T>(T result) 
        { 
            return new SuccessResponse<T>(result);
        }
        #endregion
    }
    public class SuccessResponse<T> : SuccessResponse, IResponse<T>
    {
        #region Constructors
        public SuccessResponse()
        {
        }
        public SuccessResponse(T data)
        {
            Result = data;
        }
        #endregion

        #region Properties
        public T Result { get; set; }
        #endregion
    }
}
