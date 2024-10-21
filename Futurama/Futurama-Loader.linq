<Query Kind="Program">
  <Reference Relative="Shared\bin\Debug\net8.0\ProblemDetailsApiDemo.Futurama.Shared.dll">D:\Sync\Code\GitHub\andylech\demo-api-problemdetails-talk\Futurama\Shared\bin\Debug\net8.0\ProblemDetailsApiDemo.Futurama.Shared.dll</Reference>
</Query>

using ProblemDetailsApiDemo.Futurama.Shared.Models;
using ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Entities;
using System.Text.Json;

using static ProblemDetailsApiDemo.Futurama.Shared.DataPaths;

void Main()
{
	try
	{
		var dataFilePaths = new string[] {
			CastPath,
			CharactersPath,
			EpisodesPath,
			InfoPath,
			InventoryPath,
			QuestionsPath,
		};
		dataFilePaths.Dump();

		foreach (var dataFilePath in dataFilePaths)
		{
			if (!Path.Exists(dataFilePath))
			{
				Debug.WriteLine("ERROR: Invalid local file path '{dataFile}'");
	
				continue;
			}

			dataFilePath.Dump("Data File");
			
			var jsonString = File.ReadAllText(dataFilePath);
			//jsonString.Dump();
			
			var dataFilename = Path.GetFileName(dataFilePath);
			
			switch (dataFilename)
			{
				case "cast.json":
					var cast = CastMember.FromJson(jsonString);
					cast.Dump();

					break;
				case "characters.json":
					var characters = Character.FromJson(jsonString);
					characters.Dump();

					break;
				case "episodes.json":
					var episodes = EpisodeInfo.FromJson(jsonString);
					episodes.Dump();

					break;
				case "info.json":
					var info = SeriesInfo.FromJson(jsonString);
					info.Dump();

					break;
				case "inventory.json":
					var inventory = InventoryItem.FromJson(jsonString);
					inventory.Dump();

					break;
				case "questions.json":
					var questions = TriviaQuestion.FromJson(jsonString);
					questions.Dump();

					break;
			}
		}
	}
	catch (Exception exception)
	{
		exception.Dump();
	}
}
