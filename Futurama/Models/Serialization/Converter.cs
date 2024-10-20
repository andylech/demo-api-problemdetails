using System.Text.Json;
using ProblemDetailsApiDemo.Futurama.Models.Converters;

namespace ProblemDetailsApiDemo.Futurama.Models.Serialization;

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
