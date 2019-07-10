/* Namespace: delParserGUI_V2
 * Description: This program is responsible for parsing through GOTM-B related files pertaining to watchlist-removed POIs.
 *              This program accepts .txt and .xml files for parsing. The parsed information is then
 *              appended to a master .cvs file for each respective file type. 
 *              .txt Master File: DelTCNMaster.csv
 *              .xml Master File: DelBIARMaster.csv
 * Author: Christian Lanning
 */
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace delParserGUI_V2
{
    public partial class MainWindow : Window
    {
        ListBox lBox1;
        // Responsible for initializing main GUI window and processing list box selections.
        public MainWindow()
        {
            InitializeComponent();
            // Setting ListBox variable to the ListBox on the GUI.
            lBox1 = pathList;
            string path = Directory.GetCurrentDirectory();
            // Inserting .txt and .xml files from current directory into their seperate arrays.
            string[] txtFilePaths = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
            string[] xmlFilePaths = Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories);
            // Adding files from arrays into the ListBox for selection.
            if (txtFilePaths != null && txtFilePaths.Length != 0)
            {
                foreach (string list in txtFilePaths)
                {
                    lBox1.Items.Add(list);
                }
            }
            if (xmlFilePaths != null && xmlFilePaths.Length != 0)
            {
                foreach (string list in xmlFilePaths)
                {
                    lBox1.Items.Add(list);
                }
            }
        }
        // Used for displaying console output during parsing.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        // Parse button. Once pressed, places selected items into an array.
        // Then the array is used in a ReadFile object.
        // ReadFile function, processPaths, is then called.
        private void parseClick(object sender, RoutedEventArgs e)
        {
            // Creates console window for output viewing.
            AllocConsole();
            // An array is used to collect user selection.
            string[] arrayPaths = new string[lBox1.Items.Count];
            for(int i = 0; i < lBox1.SelectedItems.Count; i++)
            {
                if (lBox1.Items[i] == null)
                {
                    break;
                }
                else
                {
                    arrayPaths[i] = lBox1.SelectedItems[i].ToString();
                }
            }
            // Array is used for ReadFile object construction, then the processPaths function is called.
            ReadFile f = new ReadFile(arrayPaths);
            f.processPaths();
            Close();
        }
        // Select all button.
        private void selAllClick(object sender, RoutedEventArgs e)
        {
            lBox1.SelectAll();
        }
        // Clear selections button.
        private void dselAllClick(object sender, RoutedEventArgs e)
        {
            lBox1.UnselectAll();
        }
    }
}
