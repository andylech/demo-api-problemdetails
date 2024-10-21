using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Enums;

namespace ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Converters;

public class GenderConverter : JsonConverter<Gender>
{
    public override bool CanConvert(Type t) => t == typeof(Gender);

    public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return value switch
        {
            "Female" => Gender.Female,
            "Male" => Gender.Male,
            _ => throw new Exception("Cannot unmarshal type Gender")
        };
    }

    public override void Write(Utf8JsonWriter writer, Gender value,
        JsonSerializerOptions options)
    {
        switch (value)
        {
            case Gender.Female:
                JsonSerializer.Serialize(writer, "Female", options);

                return;
            case Gender.Male:
                JsonSerializer.Serialize(writer, "Male", options);

                return;
            default:
                throw new Exception("Cannot marshal type Gender");
        }
    }

    public static readonly GenderConverter Singleton = new();
}
