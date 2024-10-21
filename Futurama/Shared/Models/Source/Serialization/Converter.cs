using System.Text.Json;
using ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Converters;

namespace ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Serialization;

public static class Converter
{
    public static readonly JsonSerializerOptions? Settings = new(JsonSerializerDefaults.General)
    {
        Converters =
        {
            CorrectAnswerConverter.Singleton,
            GenderConverter.Singleton,
        },
    };
}
