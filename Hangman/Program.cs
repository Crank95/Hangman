using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Hangman
{
    class Program
    {
        public static Config config = Config.LoadFromJson();
        static void Main()
        {
            MainMenue();
        }

        static void MainMenue()
        {
            while (true)
            {
                Headline();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char actionChar = keyInfo.KeyChar;
                bool end = false;

                switch (actionChar)
                {
                    case '1':
                        StartGame();
                        break;

                    case '2':
                        end = true;
                        break;
                }

                if (end)
                {
                    break;
                }
                Console.Clear();
            }
        }

        static void StartGame()
        {
            List<string> words = config.words;
            Random random = new ();
            int index = random.Next(0, words.Count);
            string selectedWord = words[index].ToLower();

            GameLoop(selectedWord);
        }

        static void GameLoop(string word)
        {
            int lives = 10;
            string hiddenWord = CreateHiddenWord(word.Length);

            while (true)
            {
                DisplayGameState(hiddenWord, lives);

                char character = GetValidInput();

                bool foundCharInWord = word.Contains(character);

                if(foundCharInWord)
                {
                    hiddenWord = UpdateHiddenWord(word, hiddenWord, character);
                }
                else
                {
                    lives--;
                }

                if(CheckGameEnd(word, hiddenWord, lives))
                {
                    break;
                }
            }
        }

        static void Headline()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("### Hangman ###");
            Console.WriteLine("###############");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Wähle eine Aktion aus:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] Spielen");
            Console.WriteLine("[2] Beenden");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Aktion: ");
        }

        static string CreateHiddenWord(int length)
        {
            string hidden = "";
            for (int i = 0; i < length; i++)
            {
                hidden += "_";
            }
            return hidden;
        }

        static void DisplayGameState(string hiddenWord, int lives)
        {
            Console.Clear();
            Console.WriteLine($"Gesuchtes Wort: {hiddenWord}");
            Console.Write("Noch übrige Versuche: ");
            for (int i = 0; i < lives; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        static char GetValidInput()
        {
            Console.Write("Buchstabe: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            char character = char.ToLowerInvariant(keyInfo.KeyChar);

            while(!char.IsLetter(character))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nBitte einen gültigen Buchstaben drücken: ");
                keyInfo = Console.ReadKey();
                character = char.ToLowerInvariant(keyInfo.KeyChar);
                Console.ResetColor();
            }
            Console.WriteLine($"{character}");
            return character;
        }

        static string UpdateHiddenWord(string word, string currentHidden, char character)
        {
            char[] hiddenChars = currentHidden.ToCharArray();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == character)
                {
                    hiddenChars[i] = character;
                }
            }
            return new string(hiddenChars);
        }

        static bool CheckGameEnd(string word, string hiddenWord, int lives)
        {
            if (hiddenWord == word)
            {
                Console.Clear();
                Console.Write($"Gesuchtes Word: {hiddenWord}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GEWONNEN!!!!");
                Console.WriteLine($"Das Wort war: {word}");
                Console.ResetColor();
                Console.ReadKey();
                return true;
            }

            if (lives <= 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GAME OVER!!!");
                Console.WriteLine($"Das gesuchte Word war: {word}");
                Console.ReadKey();
                Console.ResetColor();
                return true;
            }
            return false;
        }
    }
}