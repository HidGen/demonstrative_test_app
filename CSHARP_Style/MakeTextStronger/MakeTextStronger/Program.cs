using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MakeTextStronger
{
	class Program
	{
		static void Main(string[] args)
		{
			var makeTextStronger = new MakeTextStronger();
			
			makeTextStronger.HandleCommand(args);
		}
	}

	public class MakeTextStronger
	{
		private const string HelpCommand = "help";
		private readonly Dictionary<string, Action<string>> _commandHandlers;
		
		private readonly string _infoText = $"To see available commands, type /{HelpCommand}";

		public MakeTextStronger()
		{
			_commandHandlers = new Dictionary<string, Action<string>>(StringComparer.OrdinalIgnoreCase)
			{
				{HelpCommand, _ => PrintCommandHelp()},
				{"ls", PrintAllFilesInDirectory},
				{"mst", TextInFileToUpper}
			};
		}

		public void HandleCommand(string[] args)
		{
			// Проверяем, что ввелки хоть какой-то текст
			if (args.Length == 0)
			{
				PrintInfo();
				return;
			}
				
			if (!IsValid(args))
			{
				PrintWarning();
				return;
			};
			
			var commandName = args[0][1..].ToLower();
			if (_commandHandlers.TryGetValue(commandName, out var handler)) handler(args.Length > 1 ? args[1] : string.Empty);
			else
			{
				PrintWarning();
			}
		}

		private bool IsValid(string[] args)
		{
			// Проверяем, что изначально была введена команда
			var firstCommand = args[0];
			if (!firstCommand.StartsWith('/') && firstCommand.Length <= 1)
				return false;
			
			var command = _commandHandlers.Keys.FirstOrDefault(s => s == firstCommand[1..].ToLower());
			
			// Проверяем, что такая команда существует
			return command is not null;
		}

		private void PrintCommandHelp()
		{
			Console.WriteLine("Available commands:");
			foreach (var command in _commandHandlers.Keys)
			{
				Console.WriteLine($"/{command}");
			}
		}

		private void PrintAllFilesInDirectory(string directory)
		{
			if (!Directory.Exists(directory)) Console.WriteLine($"Directory not found: {directory}");
			else
			{
				Console.WriteLine($"Files found:");
				Directory.GetFiles(directory).ToList().ForEach(s => Console.WriteLine(Path.GetFileName(s)));
			}
		}

		private void TextInFileToUpper(string filePath)
		{
			if (!File.Exists(filePath)) Console.WriteLine($"File not found: {filePath}");
			else if (Path.GetExtension(filePath) != ".txt")
				Console.WriteLine($"File is not a txt file: {filePath}");
			else
			{
				var fileContent = File.ReadAllLines(filePath).ToList().ConvertAll(l => l.ToUpper());
				var fileName = Path.GetFileName(filePath).Replace(".txt", "");
				var rootPath = Path.GetDirectoryName(filePath);
				
				File.WriteAllText($"{rootPath}/{fileName}_COMPLETE.txt", string.Join(Environment.NewLine, fileContent));
				Console.WriteLine($"Process complete!");
			}
		}

		private void PrintWarning()
		{
			Console.WriteLine("WARNING: Please specify a valid command.");
			PrintInfo();
		}

		public void PrintInfo() => Console.WriteLine(_infoText);
	}
}