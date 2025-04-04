using System.IO;
using System;
using System.Drawing;

namespace POE_PART_1
{
    public class logo
    {
        public logo()
        {
            //get app full path
            string paths = AppDomain.CurrentDomain.BaseDirectory;

            // replace the bin\Debug\\
            string new_path = paths.Replace("bin\\Debug\\", "");

            //combining the logo and the image
            string full_path = Path.Combine(new_path, "logo.jpg");
            Console.WriteLine(full_path);

            if (!File.Exists(full_path))
            {
                Console.WriteLine("Error: Image file not found at " + full_path);
                return;
            }

            //working on the ascii
            Bitmap Logo = new Bitmap(full_path);
            Logo = new Bitmap(Logo, new Size(60, 40));

            // for height
            for (int height = 0; height < Logo.Height; height++)
            {
                //for width
                for (int width = 0; width < Logo.Width; width++)
                {
                    Color pixelColor = Logo.GetPixel(width, height);
                    int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    char asciiChar = gray > 200 ? '.' : gray > 150 ? '*' : gray > 100 ? 'o' : gray > 50 ? '#' : '@';
                    Console.Write(asciiChar + "  ");

                }
                Console.WriteLine();

            }
        }
    }
}