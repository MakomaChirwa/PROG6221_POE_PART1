using System;
using System.Collections.Generic;
using System.Drawing.Text;

namespace POE_PART_1
{
    public class DesignPrompt
    {
        private string user_name = string.Empty;
        private filter filter;
        private Check_writeFile memoryManager;
        private List<string> memoryList;
        filter myFilter = new filter();
        User_Memory memory = new User_Memory();

        public DesignPrompt()
        {
            filter = new filter(); // Create instance of Filter
            memoryManager = new Check_writeFile();

            memoryManager.check_file();
            memoryList = memoryManager.return_memory();

            // calling welcome user 
            Welcome_user();

            //starts the question loop 
            RunChat();

        }

        private void Welcome_user() { 
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

            // Save to memory
            memory.Username = user_name;
            memory.SaveMemory();

            // Welcome the user
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("BroadBox:-> ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Hey {user_name}, how can I assist you today?");
          
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

                CheckForKeyWordsAndSave(user_asking);
                string response = myFilter.ProcessQuestions(user_asking, memory);
                PrintResponse(response);

            } while (true);
        }

        // method to check key words and remember the users favourite topic
        public void CheckForKeyWordsAndSave(string input)
        {

            string lower = input.ToLower();

            if (lower.Contains("interested in") || lower.Contains("favourite"))
            {
                string topic = ExtractTopic(input);
                if (!string.IsNullOrEmpty(topic))
                {
                    SaveToMemory($"Interest:{topic}");

                    memory.FavoriteTopic = topic;
                    memory.SaveMemory();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"BroadBox:-> Got it! I’ll remember you're interested in '{topic}'.");
                }
            }
            else if (lower.Contains("what am i interested in"))
            {
                if (!string.IsNullOrEmpty(memory.FavoriteTopic))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"BroadBox:-> You mentioned earlier you're interested in {memory.FavoriteTopic}.");
                }
                else
                {
                    Console.WriteLine("BroadBox:-> I don't have a record of your interests yet.");
                }
            }
        }
        private string ExtractTopic(string sentence)
        {
            // Simple logic to extract after keyword
            int index = sentence.ToLower().IndexOf("interested in");
            if (index != -1)
                return sentence.Substring(index + "interested in".Length).Trim();

            index = sentence.ToLower().IndexOf("favourite");
            if (index != -1)
                return sentence.Substring(index + "favourite".Length).Trim();

            return string.Empty;
        }

        private void SaveToMemory(string entry)
        {
            memoryList.Add(entry);
            memoryManager.save_memory(memoryList);
        }

        private void PrintResponse(string response)
        {
            Console.Write("ChatBot:-> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(response);
            Console.ForegroundColor = ConsoleColor.White;
        } 
    } // end of class
}// end of file