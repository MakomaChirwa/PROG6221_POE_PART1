using System.ComponentModel.Design;
using System.IO;
using System.Media;
using System;

namespace POE_PART_1
{
    public class greeting
    {
     //Contructor
        public greeting()
        {
            //Getting full location of the project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            // 
            string new_path = full_location.Replace("bin\\Debug", "");

            // combining the paths
            string full_path = Path.Combine(new_path, "greeting.wav");

            // using try and catch
            try
            {
                //creating an intance for Soundplay class
                using (SoundPlayer play = new SoundPlayer(full_path))
                {
                    play.PlaySync();
                }

            }
            catch (Exception error)
            {
                // error message to be displayed when sound cant play
                Console.WriteLine(error.Message);
            }
        }
    }
}


