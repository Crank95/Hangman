using System.Text.Json;

namespace Hangman
{

    public class Config
    {
        public List<string> words { get; set; }

        public static Config LoadFromJson(string path = "words.json")
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Die Datei {path} wurde nicht gefunden.");
            }

            string json = File.ReadAllText(path);
            Config config = JsonSerializer.Deserialize<Config>(json);

            if (config == null || config.words == null || config.words.Count == 0)
                throw new Exception("Keine Wörter in der Konfigurationsdatei gefunden.");

            return config;
        }
    }
}
