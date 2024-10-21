using System.Text.Json.Serialization;

namespace ProblemDetailsApiDemo.Futurama.Shared.Models.Requests.Authentication;

public class NameSearchParameters
{
    [JsonPropertyName("first")]
    public string? First { get; set; }

    [JsonPropertyName("middle")]
    public string? Middle { get; set; }

    [JsonPropertyName("last")]
    public string? Last { get; set; }

    // HACK
    public string FullName => GetFullName(First, Middle, Last);

    // HACK
    private static string GetFullName(string? first = "", string? middle = "",
        string? last = "")
    {
        var fullName =
            string.Join(" ",
                new[] { first!.Trim(), middle!.Trim(), last!.Trim() }
                    .Where(str => !string.IsNullOrEmpty(str)));

        return fullName;
    }
}
