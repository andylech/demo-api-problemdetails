namespace ProblemDetailsApiDemo.Futurama.Shared;

public static class DataPaths
{
    private const string DataPath =
        @"D:\Sync\Code\GitHub\andylech\demo-api-problemdetails-talk\Futurama\Data";

    public static readonly string CastPath =
        Path.Combine(DataPath, "cast.json");

    public static readonly string CharactersPath =
        Path.Combine(DataPath, "characters.json");

    public static readonly string EpisodesPath =
        Path.Combine(DataPath, "episodes.json");

    public static readonly string InfoPath =
        Path.Combine(DataPath, "info.json");

    public static readonly string InventoryPath =
        Path.Combine(DataPath, "inventory.json");

    public static readonly string QuestionsPath =
        Path.Combine(DataPath, "questions.json");
}
