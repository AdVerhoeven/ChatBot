using System;
using System.Collections.Generic;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the path of a file to be used: ");
            var filePath = Console.ReadLine();
            var bot = new SimpleChatBot("English", filePath);

            Console.WriteLine("Computer: I am a very simple chat bot.");
            var exit = false;
            while (!exit)
            {
                exit = Prompt(bot);
            }
        }

        static bool Prompt(SimpleChatBot bot)
        {
            Console.Write("User: ");
            var input = Console.ReadLine();
            Console.WriteLine($"Computer: {bot.RespondSimple(input)}");
            return input == "bye";
        }
    }
}
