using System.Collections.Generic;
using System.IO;

namespace ExtensionSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            List<string> fileNames = new List<string>();
            List<string> fileTypes = new List<string>();
            // Adding paths and extensions to their respective lists.
            foreach(string file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) != ".exe")
                {
                    fileNames.Add(file);
                }
                if (!fileTypes.Contains(Path.GetExtension(file)) && Path.GetExtension(file) != ".exe")
                {
                    fileTypes.Add(Path.GetExtension(file));
                }
            }
            // Create new folders for the different extensions.
            foreach(string type in fileTypes)
            {
                Directory.CreateDirectory(type);
            }
            // Moves files into their respective folders based on extension.
            foreach(string file in fileNames)
            {
                string destPath = Directory.GetCurrentDirectory();
                destPath += ("\\" + Path.GetExtension(file) + "\\" + Path.GetFileName(file));
                File.Move(file, destPath);
            }
        }
    }
}
