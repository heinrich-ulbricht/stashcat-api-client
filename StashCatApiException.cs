namespace StashCat.Api;

public class StashCatApiException : Exception
{
    public string? ApiStatusCode { get; }
    public string? ApiShortMessage { get; }
    public string? ApiMessage { get; }
    public bool IsNewApiClientKeyNeeded { get; }

    public StashCatApiException(string exceptionMessage, string? apiStatusCode, string? apiShortMessage, string? apiMessage) : base(exceptionMessage)
    {
        ApiStatusCode = apiStatusCode;
        ApiShortMessage = apiShortMessage;
        ApiMessage = apiMessage;
        IsNewApiClientKeyNeeded = apiShortMessage == "app_auth_not_ok";
    }
}