namespace DailyQuoteManager.Application.Common.Responses
{
    public record Response(

        bool Success = false,
        string ErrorMessage = "",
        string Message = "",
        string MessageType = "",
        object? Data = null,
        Dictionary<string, string[]>? ValidationErrors = null);
}
