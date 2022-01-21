namespace StashCat.Tools;
#nullable enable

using StashCat.Model;
using Newtonsoft.Json;

public static class HttpClientExtension
{      
    public static async Task<(HttpResponseMessage, string, StashCatTopLevelResponse?)> PostWithGuaranteedSuccess(this HttpClient httpClient, string? requestUri, HttpContent? content)
    {
        var result = await httpClient.PostAsync(requestUri, content);
        if (!result.IsSuccessStatusCode)
        {
            throw new Exception($"Getting private key failed with HTTP status {result.StatusCode}; ReasonPhrase: {result.ReasonPhrase}");
        }
        var resultString = await result.Content.ReadAsStringAsync();
        var stashCatApiResponse = JsonConvert.DeserializeObject<StashCatTopLevelResponse>(resultString);
        stashCatApiResponse?.ThrowForNonOkStatus();

        return (result, resultString, stashCatApiResponse);
    }
}