using System;
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

                int action = Convert.ToInt32(Console.ReadLine());
                bool end = false;

                switch (action)
                {
                    case 1:
                        StartGame();
                        break;

                    case 2:
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
            Random random = new Random();
            int index = random.Next(0, words.Count);
            string selectedWord = words[index].ToLower();

            GameLoop(selectedWord);



        }

        static void GameLoop(string word)
        {
            int lives = 10;
            string hiddenWord = "";

            for(int i = 0; i < word.Length; i++)
            {
                hiddenWord += "_";
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gesuchtes Word: " + hiddenWord);
                Console.Write("Noch übrige Verusche: ");
                for (int i = 0; i < lives; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.Write("Buchstabe: ");
                char character = Convert.ToChar(Console.ReadLine().ToLower());

                bool foundCharInWord = false;
                for (int i = 0; i < word.Length; i++)
                { 
                    if (word[i] == character)
                    {
                        foundCharInWord = true;
                        break;
                    }
                } 

                string tempHiddenWord = hiddenWord;
                hiddenWord = "";

                if(foundCharInWord)
                {
                    for(int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == character)
                        {
                            hiddenWord += character;
                        }
                        else if (tempHiddenWord[i] != '_')
                        {
                            hiddenWord += tempHiddenWord[i];
                        }
                        else
                        {
                            hiddenWord += '_';
                        }
                    }

                    if(hiddenWord == word)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("GEWONNEN!!!!");
                        Console.WriteLine($"Das Wort war: {word}");
                        Console.ResetColor();
                        break;
                    }
                }
                else
                {
                    hiddenWord = tempHiddenWord;
                    if(lives > 0)
                    {
                        lives -= 1;
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("GAME OVER!!!");
                        Console.WriteLine($"Das gesuchte Word war: {word}");
                        Console.ReadKey();
                        Console.ResetColor();
                        break;
                    }
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

    }
}