using System.Collections.Generic;
using System.IO;
using System;

namespace POE_PART_1
{
    public class User_Memory
    {
        private readonly string memoryFilePath;
        public string Username { get; set; }
        public string FavoriteTopic { get; set; }

        private List<string> memoryData = new List<string>();

        public User_Memory()
        {
            memoryFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", ""), "memory.txt");
            LoadMemory();
        }

        private void LoadMemory()
        {
            if (File.Exists(memoryFilePath))
            {
                memoryData = new List<string>(File.ReadAllLines(memoryFilePath));
                foreach (string line in memoryData)
                {
                    if (line.StartsWith("name:"))
                        Username = line.Substring(5);
                    else if (line.StartsWith("favorite:"))
                        FavoriteTopic = line.Substring(9);
                }
            }
        }

        public void SaveMemory()
        {
            // Do not overwrite unrelated memory lines (like "Interest:")
            var filtered = new List<string>();

            foreach (var line in memoryData)
            {
                if (!line.StartsWith("name:") && !line.StartsWith("favorite:"))
                    filtered.Add(line);
            }

            if (!string.IsNullOrEmpty(Username))
                filtered.Add("name:" + Username);
            if (!string.IsNullOrEmpty(FavoriteTopic))
                filtered.Add("favorite:" + FavoriteTopic);

            File.WriteAllLines(memoryFilePath, filtered);
            memoryData = filtered;
        } 
    } // end of class
} // end of file