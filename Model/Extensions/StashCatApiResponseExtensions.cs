namespace StashCat.Model;
#nullable enable

using System;
using StashCat.Api;

public partial class StashCatTopLevelResponse
{
    public bool IsOkStatus()
    {
        return Status?.Value == "OK";
    }

    public void ThrowForNonOkStatus()
    {
        if (!IsOkStatus())
        {
            throw new StashCatApiException($"StashCat API returned non-OK status '{Status?.Value}', short message: '{Status?.ShortMessage}', message: '{Status?.Message}'", Status?.Value, Status?.ShortMessage, Status?.Message);
        }
    }
}