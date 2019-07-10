/* Namespace: delParserGUI_V2
 * Class: ReadFile
 * Description: This class is responsible for parsing through GOTM-B related files pertaining to watchlist-removed POIs.
 *              This class accepts .txt and .xml files for parsing. The parsed information is then
 *              appended to a master .cvs file for each respective file type.
 *              .txt Master File: DelTCNMaster.csv
 *              .xml Master File: DelBIARMaster.csv
 * Author: Christian Lanning
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace delParserGUI_V2
{
    class ReadFile
    {
        private readonly string[] files;
        private List<string> dupes = new List<string>();
        public ReadFile(string[] filePaths)
        {
            files = new string[filePaths.Length];
            filePaths.CopyTo(files, 0);
        }
        // Looks through an array of .txt files and parses their content into a master file.
        // Duplicate data points are skipped over.
        public void TextRead(string[] txtFiles)
        {
            bool read;
            foreach (string txtfile in txtFiles)
            {
                if (File.Exists("DelTCNMaster.csv"))
                {
                    dupes.Clear();
                    foreach (string line in File.ReadAllLines("DelTCNMaster.csv"))
                    {
                        if (!dupes.Contains(line))
                        {
                            dupes.Add(line);
                        }
                    }
                }
                read = false;
                Console.WriteLine("Parsing: " + txtfile);
                try
                {
                    using (StreamReader sr = new StreamReader(txtfile))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (!dupes.Contains(line))
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
        public void XmlRead(string[] xmlFiles)
        {
            bool read;
            foreach (string xmlfile in xmlFiles)
            {
                if (File.Exists("DelBIARMaster.csv"))
                {
                    dupes.Clear();
                    foreach (string biar in File.ReadAllLines("DelBIARMaster.csv"))
                    {
                        if (!dupes.Contains(biar))
                        {
                            dupes.Add(biar);
                        }
                    }
                }
                string line;
                read = false;
                Console.WriteLine("Parsing: " + xmlfile);
                try
                {
                    using (XmlReader reader = XmlReader.Create(xmlfile))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.Text || reader.NodeType == XmlNodeType.EndElement)
                            {
                                if (reader.Name.Equals("poi") && (reader.GetAttribute("op") == "del"))
                                {
                                    reader.ReadToDescendant("altid");
                                    line = reader.ReadElementContentAsString();
                                    if (!dupes.Contains(line))
                                    {
                                        File.AppendAllText("DelBIARMaster.csv", line + "\n");
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
                }
                else
                {
                    Console.WriteLine("Contents already in Master File: DelBIARMaster.csv");
                }
            }
        }
        // Looks into the files array and seperates .xml and .txt files into seperate lists.
        // These lists are then converted into arrays and passed onto their respective read functions.
        // Once the read functions are completed, a stopwatch displays the time elapsed and finishes out the program.
        public void processPaths()
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<string> xmls = new List<string>();
            List<string> txts = new List<string>();
            foreach (string path in files)
            {
                if(path == null)
                {
                    break;
                }
                if (path.Contains(".xml") && path != null)
                {
                    xmls.Add(path);
                } else if (path.Contains(".txt") && path != null)
                {
                    txts.Add(path);
                }
            }
            string[] xmlFiles = xmls.ToArray();
            string[] txtFiles = txts.ToArray();
            if (xmlFiles != null && xmlFiles.Length != 0)
            {
                XmlRead(xmlFiles);
            }
            if (txtFiles != null && txtFiles.Length != 0)
            {
                TextRead(txtFiles);
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
