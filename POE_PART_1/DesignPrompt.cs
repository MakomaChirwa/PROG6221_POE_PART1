using System;

namespace POE_PART_1
{
    public class DesignPrompt
    {
        private string user_name = string.Empty;
        private filter filter;

        public DesignPrompt()
        {
            filter = new filter(); // Create instance of Filter

            // UI Setup
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   |   Welcome to Broad Box AI   |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            // Ask for user name
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("BroadBox:-> ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Please Enter your name.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("You:-> ");
            Console.ForegroundColor = ConsoleColor.White;
            user_name = Console.ReadLine();

            // Welcome the user
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("BroadBox:-> ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Hey {user_name}, how can I assist you today?");

            // Start the question loop
            RunChat();
        }
        private void RunChat()
        {
            string user_asking;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(user_name + ":-> ");
                Console.ForegroundColor = ConsoleColor.White;
                user_asking = Console.ReadLine()?.Trim();

                if (string.Equals(user_asking, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Thank you {user_name}, for using BroadBox AI, bye!");
                    return;
                }

                string response = filter.ProcessQuestions(user_asking);
                PrintResponse(response);

            } while (true);
        }
        private void PrintResponse(string response)
        {
            Console.Write("ChatBot:-> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}