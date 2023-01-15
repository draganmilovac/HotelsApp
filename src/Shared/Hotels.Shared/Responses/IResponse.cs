using HotelsApp.Shared.Enums;

namespace HotelsApp.Shared.Responses
{
    public interface IResponse
    {
        bool IsSuccess { get; }
        string Message { get; }
        ErrorCode ErrorCode { get; }
        public string FormatLogMessage()
        {
            return $"Message: {Message}" + (IsSuccess ? string.Empty : $"{Environment.NewLine}Error code: {ErrorCode}");
        }
    }
    public interface IResponse<T> : IResponse
    {
        T Result { get; }
    }
}
