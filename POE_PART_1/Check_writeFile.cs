using System.Collections.Generic;
using System.IO;
using System;

namespace POE_PART_1
{
    public class Check_writeFile
    {
private string path_return()
        {
            // Get the base directory of the current application
            string fullpath = AppDomain.CurrentDomain.BaseDirectory;

            // Remove "bin\\Debug\\" to go to the project root folder
            string new_path = fullpath.Replace("bin\\Debug\\", "");

            // Combine the cleaned-up path with the file name
            string path = Path.Combine(new_path, "memory.txt");

            return path;
        }

        //Check if memory.txt exists, create if not
        public void check_file()
        {
            string path = path_return();

            if (!File.Exists(path))
            {
                // Create an empty memory file
                File.CreateText(path).Close(); 
            }
            else
            {
                Console.WriteLine("File is found...");
            }
        }

        // Read all lines from memory.txt and return as a list
        public List<string> return_memory()
        {
            string path = path_return();

            // Read all lines and return them as a list
            return new List<string>(File.ReadAllLines(path));
        }

        // Save new memory list into memory.txt (overwrite existing)
        public void save_memory(List<string> save_new)
        {
            string path = path_return();

            // Write all lines to file, replacing existing content
            File.WriteAllLines(path, save_new);
        }
    }
}