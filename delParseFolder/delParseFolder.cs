/* Namespace: delParseFolder.cs
 * Description: This program looks for GOTM-B files related to watchlist-removed POIs in its current directory
 *              and the folders within the current directory.
 *              This program accepts .txt and .xml files for parsing. The parsed information is then
 *              appended to a master .cvs file for each respective file type.
 *              .txt Master File: DelTCNMaster.csv
 *              .xml Master File: DelBIARMaster.csv
 * Author: Christian Lanning
 */
using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace delParseFolder
{
    // This class reads an array of file paths and parses through each file individually.
    // .txt files run through the TextRead function and .xml files run through the XmlRead function.
    class ReadFile
    {
        private readonly string[] files;
        public ReadFile(string[] filePaths)
        {
            files = new string[filePaths.Length];
            filePaths.CopyTo(files, 0);
        }
        // Looks through an array of .txt files and parses their content into a master file.
        // Duplicate data points are skipped over.
        public void TextRead()
        {
            bool read;
            foreach (string txtfile in files)
            {
                read = false;
                Console.WriteLine("Parsing: " + txtfile);
                try
                {
                    using (StreamReader sr = new StreamReader(txtfile))
                    {
                        string line;
                        // Checking if the master file already exists. 
                        // If it does, scans through for duplicate entries and only appends new data points.
                        if (File.Exists("DelTCNMaster.csv"))
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (!(File.ReadAllText("DelTCNMaster.csv").Contains(line)))
                                {
                                    File.AppendAllText("DelTCNMaster.csv", line + "\n");
                                    read = true;
                                }
                            }                    
                        }
                        // If the master file does not exist yet, creates a new file and appends as normal.
                        else
                        {
                            while ((line = sr.ReadLine()) != null)
                            {
                                File.AppendAllText("DelTCNMaster.csv", line + "\n");
                                read = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Error Has Occured!");
                    Console.WriteLine(e.Message);
                }
                if (read)
                {
                    Console.WriteLine("Collected from: " + txtfile);
                }
                else
                {
                    Console.WriteLine("Contents already in Master File: DelTCNMaster.csv");
                }
            }
        }
        // Looks through an array of .xml files and parses their content into a master file.
        // Duplicate data points are skipped over.
        public void XmlRead()
        {
            bool read;
            foreach (string xmlfile in files)
            {
                string line;
                read = false;
                Console.WriteLine("Parsing: " + xmlfile);
                try
                {
                    using (XmlReader reader = XmlReader.Create(xmlfile))
                    {
                        // Checking if the master file already exists. 
                        // If it does, scans through for duplicate entries and only appends new data points.
                        if (File.Exists("DelBIARMaster.csv"))
                        {
                            while(reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Text || reader.NodeType == XmlNodeType.EndElement)
                                {
                                    if (reader.Name.Equals("poi") && (reader.GetAttribute("op") == "del"))
                                    {
                                        reader.ReadToDescendant("altid");
                                        line = reader.ReadElementContentAsString();
                                        if (!(File.ReadAllText("DelBIARMaster.csv").Contains(line)))
                                        {
                                            File.AppendAllText("DelBIARMaster.csv", line + "\n");
                                            read = true;
                                        }
                                    }
                                }
                            }
                        }
                        // If the master file does not exist yet, creates a new file and appends as normal.
                        else
                        { 
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Text || reader.NodeType == XmlNodeType.EndElement)
                                {
                                    if (reader.Name.Equals("poi") && (reader.GetAttribute("op") == "del"))
                                    {
                                        reader.ReadToDescendant("altid");
                                        File.AppendAllText("DelBIARMaster.csv", reader.ReadElementContentAsString() + "\n");
                                        read = true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Error Has Occured!");
                    Console.WriteLine(e.Message);
                }
                if (read)
                {
                    Console.WriteLine("Collected from: " + xmlfile);
                } else
                {
                    Console.WriteLine("Contents already in Master File: DelBIARMaster.csv");
                }
            }
        }
    }

    class delParseFolder
    {
        // Locates .txt and .xml files within the current dirrectory and its folders.
        // The paths to these files are then placed in arrays and sent through a ReadFile object.
        // A Stopwatch is used to determine run time. This is displayed at the end to the user.
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string path = Directory.GetCurrentDirectory();
            string[] txtFilePaths = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
            string[] xmlFilePaths = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            if (txtFilePaths != null && txtFilePaths.Length != 0)
            {
                ReadFile t = new ReadFile(txtFilePaths);
                t.TextRead();
            }
            if (xmlFilePaths != null && xmlFilePaths.Length != 0)
            {
                ReadFile x = new ReadFile(xmlFilePaths);
                x.XmlRead();
            }
            Console.WriteLine("Procedure Complete");
            sw.Stop();
            int sec = (int)(sw.ElapsedMilliseconds / 1000) % 60;
            int min = (int)((sw.ElapsedMilliseconds / (1000 * 60)) % 60);
            Console.WriteLine("Total elapsed time: {0}m {1}s", min, sec);
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}