<Query Kind="Program">
  <Reference Relative="Models\bin\Debug\net8.0\ProblemDetailsApiDemo.Futurama.Models.dll">D:\Sync\Code\GitHub\andylech\demo-api-problemdetails-talk\Futurama\Models\bin\Debug\net8.0\ProblemDetailsApiDemo.Futurama.Models.dll</Reference>
</Query>

using ProblemDetailsApiDemo.Futurama.Models;
using ProblemDetailsApiDemo.Futurama.Models.Entities;
using System.Text.Json;

void Main()
{
	try
	{
		var dataPath = @"D:\Sync\Code\GitHub\andylech\demo-api-problemdetails-talk\Futurama\Data";
		
		if (!Path.Exists(dataPath))
		{
			Debug.WriteLine("ERROR: Invalid local folder path '{dataPath}'");
			
			return;
		}		
		
		var dataFilenames = new string[] {
			"cast.json", 
			"characters.json", 
			"episodes.json", 
			"info.json", 
			"inventory.json", 
			"questions.json",
		};
		//dataFiles.Dump();

		foreach (var dataFilename in dataFilenames)
		{
			var dataFilePath = Path.Combine(dataPath, dataFilename);
			
			if (!Path.Exists(dataFilePath))
			{
				Debug.WriteLine("ERROR: Invalid local file path '{dataFile}'");
	
				continue;
			}

			dataFilePath.Dump("Data File");
			
			var jsonString = File.ReadAllText(dataFilePath);
			//jsonString.Dump();
			
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
