using System.Diagnostics.Tracing;
using System.Text.Json;

namespace Hangman
{

    public class Config
    {
        public required List<string> Words { get; set; }

        public static Config LoadFromJson(string path = "words.json")
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Die Datei {path} wurde nicht gefunden.");
            }

            string json = File.ReadAllText(path);
            Config? config = JsonSerializer.Deserialize<Config>(json);

            if (config == null || config.Words == null || config.Words.Count == 0)
                throw new Exception($" Die words.json darf nicht leer sein.");

            return config;
        }
    }
}
