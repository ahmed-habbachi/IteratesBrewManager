namespace IteratesBrewManager.Application.Common.Models;
public class QuoteRequestResult
{
    public bool Succeeded { get; set; }

    public QuoteDto? Quote { get; set; }

    public string? ErrorMessage { get; set; }

    public QuoteRequestResult(QuoteDto? quote)
    {
        Succeeded = true;
        Quote = quote;
    }

    public QuoteRequestResult(string errorMessage)
    {
        Succeeded = false;
        ErrorMessage = errorMessage;
    }
}
