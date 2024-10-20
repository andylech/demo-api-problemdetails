namespace ProblemDetailsApiDemo.Futurama.Models.Structs;

public struct CorrectAnswer
{
    public long? Integer;
    public string? String;

    public static implicit operator CorrectAnswer(long integer) => new() { Integer = integer };
    public static implicit operator CorrectAnswer(string @string) => new() { String = @string };
}
