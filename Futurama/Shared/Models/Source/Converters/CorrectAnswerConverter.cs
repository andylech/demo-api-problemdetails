using ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Structs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Converters;

public class CorrectAnswerConverter : JsonConverter<CorrectAnswer>
{
    public override bool CanConvert(Type t) => t == typeof(CorrectAnswer);

    public override CorrectAnswer Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader)
        {
            case { TokenType: JsonTokenType.Number }:
                var integerValue = reader.GetInt64();

                return new CorrectAnswer { Integer = integerValue };
            case { TokenType: JsonTokenType.String }:
                var stringValue = reader.GetString();

                return new CorrectAnswer { String = stringValue };
            default:
                throw new Exception("Cannot unmarshal type CorrectAnswer");
        }
    }

    public override void Write(Utf8JsonWriter writer, CorrectAnswer value,
        JsonSerializerOptions? options)
    {

        if (value.Integer != null)
        {
            JsonSerializer.Serialize(writer, value.Integer.Value, options);

            return;
        }

        if (value.String != null)
        {
            JsonSerializer.Serialize(writer, value.String, options);

            return;
        }

        throw new Exception("Cannot marshal type CorrectAnswer");
    }

    public static readonly CorrectAnswerConverter Singleton = new();
}
