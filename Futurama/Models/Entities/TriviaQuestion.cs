﻿// <auto-generated />

#nullable enable

using ProblemDetailsApiDemo.Futurama.Models.Serialization;
using ProblemDetailsApiDemo.Futurama.Models.Structs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProblemDetailsApiDemo.Futurama.Models.Entities;

public class TriviaQuestion
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("question")]
    public required string Question { get; set; }

    [JsonPropertyName("possibleAnswers")]
    public required List<string> PossibleAnswers { get; set; }

    [JsonPropertyName("correctAnswer")]
    public CorrectAnswer CorrectAnswer { get; set; }

    public static List<TriviaQuestion>? FromJson(string json) =>
        JsonSerializer.Deserialize<List<TriviaQuestion>>(json, Converter.Settings);
}

public static class TriviaQuestionExtensions
{
    public static string ToJson(this List<TriviaQuestion> self) =>
        JsonSerializer.Serialize(self, Converter.Settings);
}