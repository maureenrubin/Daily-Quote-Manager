﻿
namespace DailyQuoteManager.Client.Common.Responses
{
    public record Response(
        bool IsSuccess = false,
        string ErrorMessage = "",
        string Message = "",
        string MessageType = "",
        object? Data = null,
        Dictionary<string, string[]>? ValidationErrors = null
        );
}
