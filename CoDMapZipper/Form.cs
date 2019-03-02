using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.Win32; //Registry stuff
using JWC; //MRU List
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO.Compression;

/*
 * Todo list:
 * 
 * Date Created is not a valid check for stock files, its dependent on the date of install for the given user which makes it an irrelevant date. Need to create a file index.
 * Need a way to save preferences?
 * Checkbox for zone source files (default: unchecked)
 * Checkbox for mod.csv (needs warning that it must be the last mod.ff you compiled with Launcher.
 * 
 */

namespace CoDMapZipper
{
    public partial class Form1 : Form
    {
        class dataType
        { //dataType is an object representing each type of file recorded in the assetLists (eg. image, material, menufile). datTypeName is a string storing the name of the dataType, and dataList is a list of all file locations contained in the loaded assetList which match the dataType's name.
            public string dataTypeName { get; set; }
            public int engineLimit { get; set; }
            public ConcurrentStack<string> dataList { get; set; }
        };

        class assetLine
        {
            public string type { get; set; }
            public string name { get; set; }
        };

        string[] filePaths;
        static List<dataType> dataTypes;
        static List<assetLine> _assets = new List<assetLine>();
        static BackgroundWorker updateChecker = new BackgroundWorker();

        private delegate void openFileDelegate(string fileName);
        private openFileDelegate openFileD;
        List<Task> tasks;
        bool currentlyBusy;
        bool cancelOperationFlag;
        bool operationCanceled;
        
        protected MruStripMenu mruMenu;
        static string mruRegKey = "SOFTWARE\\UGX\\CoDMapZipper";

        public Form1()
        {
            InitializeComponent();
            this.Text = "CoDWaW Map Zipper v" + Application.ProductVersion.Substring(0, 5);
            Version nonBeta = new Version(1, 0, 0, 0);
            if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version < nonBeta)
                this.Text += " BETA";

            mruMenu = new MruStripMenuInline(fileToolStripMenuItem, menuRecentFile, new MruStripMenu.ClickedHandler(OnMruFile), mruRegKey + "\\MRU", 16);

            openFileD = new openFileDelegate(this.openFile);

            tasks = new List<Task>();
            currentlyBusy = false;
            cancelOperationFlag = false;
            operationCanceled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                openToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                savePresetToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.R))
            {
                refreshToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Q))
            {
                exitToolStripMenuItem_Click(null, null);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void initializeDataTypes()
        {
            dataTypes = new List<dataType>()
            {
                new dataType() { dataTypeName = "image", engineLimit = 2400, dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "material", engineLimit = 2048, dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "loaded_sound", engineLimit = 1600, dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "weapon", engineLimit = 128, dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "fx", engineLimit = 400, dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "techset", dataList = new ConcurrentStack<string>() },

                new dataType() { dataTypeName = "csv", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "aitype", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "character", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "col_map_sp", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "com_map", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "game_map_sp", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "gfx_map", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "lightdef", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "map_ents", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "menu", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "menufile", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "mptype", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "physpreset", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "rawfile", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "xanim", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "xmodel", dataList = new ConcurrentStack<string>() },
                new dataType() { dataTypeName = "xmodelalias", dataList = new ConcurrentStack<string>() }
            };
        }

        #region File IO
        private void openFileGroup(string[] fileNames)
        {
           
            //Clear the data
            initializeDataTypes();
            _assets.Clear();
            filePaths = fileNames; //update the global var in case they want to refresh

            openFile(filePaths[0]);

            mruMenu.SaveToRegistry();
        }
        private void openFile(string fileLoc)
        {
            if (!button1.Enabled)
                return;
            button1.Enabled = false;
            openMenuItem.Enabled = false;
            // Code to read the contents of the text 
            if (Path.GetExtension(fileLoc) != ".map")
                MessageBox.Show("File does not have the correct extension for a map file! (*.map required)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (File.Exists(fileLoc))
            {
                //Console.WriteLine(fileLoc);
                string[] directories = fileLoc.Split(Path.DirectorySeparatorChar);

                if (directories.Count() < 2)
                {
                    MessageBox.Show("File is not located in a valid location! Must be within the root of the map_source folder of a valid installation of CoDWaW!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (directories[directories.Count() - 2] != "map_source")
                {
                    MessageBox.Show("File is not located in a valid location! Must be within the root of the map_source folder of a valid installation of CoDWaW!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string rootDir = Path.GetFullPath(Path.Combine(fileLoc, @"..\..\"));
                string mapName = Path.GetFileNameWithoutExtension(fileLoc);
                string targetPath = Path.Combine(Directory.GetCurrentDirectory(), mapName);

                //Console.WriteLine(rootDir);

                string mapnameCSV = Path.Combine(rootDir, @"zone_source\english\assetlist\" + mapName + ".csv");

                if (!File.Exists(mapnameCSV))
                {
                    MessageBox.Show("Could not find an assetlist CSV for your map at " + mapnameCSV + ". Make sure you've compiled this map in Launcher!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Path.GetFileName(fileLoc).Contains("_patch"))
                {
                    MessageBox.Show("It appears you chose the _patch map file for a map instead of the map file for your map! Please choose the correct file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                mruMenu.AddFile(fileLoc);

                if (!System.IO.Directory.Exists(targetPath))
                    System.IO.Directory.CreateDirectory(targetPath);

                //Copy the map file to export directory
                if (mapCheck.Checked)
                {
                    if (!System.IO.Directory.Exists(targetPath + @"\map_source\"))
                        System.IO.Directory.CreateDirectory(targetPath + @"\map_source\");
                    System.IO.File.Copy(fileLoc, mapName + @"\map_source\" + mapName + ".map", true);

                    if (File.Exists(rootDir + @"map_source\" + mapName + "_patch.map"))
                        System.IO.File.Copy(rootDir + @"map_source\" + mapName + "_patch.map", mapName + @"\map_source\" + mapName + "_patch.map", true);

                    if (File.Exists(rootDir + @"map_source\" + mapName + "_load.map"))
                        System.IO.File.Copy(rootDir + @"map_source\" + mapName + "_load.map", mapName + @"\map_source\" + mapName + "_load.map", true);
                }

                if (prefabsCheck.Checked)
                {
                    if (!System.IO.Directory.Exists(targetPath + @"\map_source\_prefabs"))
                        System.IO.Directory.CreateDirectory(targetPath + @"\map_source\_prefabs");

                    foreach (string line in File.ReadLines(fileLoc))
                    {
                        if (line.Contains("_prefabs"))
                        {
                            string prefabPath = line.Remove(0, 9);
                            prefabPath = prefabPath.Trim('"');
                            prefabPath = prefabPath.Replace('/', '\\');

                            if (File.Exists(rootDir + @"map_source\" + prefabPath))
                            {
                                if (!System.IO.Directory.Exists(targetPath + @"\map_source\" + Path.GetDirectoryName(prefabPath)))
                                    System.IO.Directory.CreateDirectory(targetPath + @"\map_source\" + Path.GetDirectoryName(prefabPath));

                                System.IO.File.Copy(rootDir + @"map_source\" + prefabPath, mapName + @"\map_source\" + prefabPath, true);
                            }
                        }
                    }
                }

                if (launcherCfg.Checked)
                {
                    if (!System.IO.Directory.Exists(targetPath + @"\bin\Launcher\map_settings"))
                        System.IO.Directory.CreateDirectory(targetPath + @"\bin\Launcher\map_settings");

                    if (File.Exists(rootDir + @"\bin\Launcher\map_settings\" + mapName + ".cfg"))
                        System.IO.File.Copy(rootDir + @"\bin\Launcher\map_settings\" + mapName + ".cfg", mapName + @"\bin\Launcher\map_settings\" + mapName + ".cfg", true);
                    if (File.Exists(rootDir + @"\bin\Launcher\map_settings\" + mapName + "_load.cfg"))
                        System.IO.File.Copy(rootDir + @"\bin\Launcher\map_settings\" + mapName + "_load.cfg", mapName + @"\bin\Launcher\map_settings\" + mapName + "_load.cfg", true);
                    if (File.Exists(rootDir + @"\bin\Launcher\map_settings\" + mapName + "_patch.cfg"))
                        System.IO.File.Copy(rootDir + @"\bin\Launcher\map_settings\" + mapName + "_patch.cfg", mapName + @"\bin\Launcher\map_settings\" + mapName + "_patch.cfg", true);
                }

                if (zonesource.Checked)
                {
                    if (!System.IO.Directory.Exists(targetPath + @"\zone_source"))
                        System.IO.Directory.CreateDirectory(targetPath + @"\zone_source");

                    if (File.Exists(rootDir + @"\zone_source\" + mapName + ".csv"))
                        System.IO.File.Copy(rootDir + @"\zone_source\" + mapName + ".csv", mapName + @"\zone_source\" + mapName + ".csv", true);
                    if (File.Exists(rootDir + @"\zone_source\" + mapName + "_load.csv"))
                        System.IO.File.Copy(rootDir + @"\zone_source\" + mapName + "_load.csv", mapName + @"\zone_source\" + mapName + "_load.csv", true);
                    if (File.Exists(rootDir + @"\zone_source\" + mapName + "_patch.csv"))
                        System.IO.File.Copy(rootDir + @"\zone_source\" + mapName + "_patch.csv", mapName + @"\zone_source\" + mapName + "_patch.csv", true);
                }

                foreach (var line in File.ReadLines(mapnameCSV))
                {
                    var values = line.Split(',');
                    assetLine asset = new assetLine();
                    asset.type = Convert.ToString(values[0]);
                    asset.name = Convert.ToString(values[1]);

                    if (imagesCheck.Checked && asset.type == "image")
                    {
                        string imagePath = Path.Combine(rootDir, @"raw\images\" + asset.name + ".iwi");

                        if (!System.IO.Directory.Exists(targetPath + @"\raw\images\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\raw\images\");

                        if (!System.IO.Directory.Exists(targetPath + @"\mods\" + mapName + @"\images\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\mods\" + mapName + @"\images\");

                        if (File.Exists(imagePath) && !isFileTooOld(imagePath))
                        {
                            System.IO.File.Copy(imagePath, mapName + @"\raw\images\" + asset.name + ".iwi", true);
                            System.IO.File.Copy(imagePath, mapName + @"\mods\" + mapName + @"\images\" + asset.name + ".iwi", true);
                        }
                        //Console.WriteLine(imagePath);
                    }
                    if (materialsCheck.Checked && asset.type == "material")
                    {
                        string[] split = asset.name.Split('/');
                        string name = asset.name;
                        if (split.Count() > 1) name = split[split.Count() - 1];

                        if (name.Contains('*')) //invalid path character
                            continue;

                        string materialPath = Path.Combine(rootDir, @"raw\materials\" + name);
                        string materialPropPath = Path.Combine(rootDir, @"raw\material_properties\" + name);

                        if (!System.IO.Directory.Exists(targetPath + @"\raw\materials\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\raw\materials\");
                        if (!System.IO.Directory.Exists(targetPath + @"\raw\material_properties\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\raw\material_properties\");

                        if (File.Exists(materialPath) && !isFileTooOld(materialPath))
                            System.IO.File.Copy(materialPath, mapName + @"\raw\materials\" + name, true);
                        if (File.Exists(materialPropPath) && !isFileTooOld(materialPropPath))
                            System.IO.File.Copy(materialPropPath, mapName + @"\raw\material_properties\" + name, true);
                        //Console.WriteLine(materialPath);
                    }
                    if (xmodelsCheck.Checked && asset.type == "xmodel")
                    {
                        string xmodelPath = Path.Combine(rootDir, @"raw\xmodel\" + asset.name);
                        //Console.WriteLine(xmodelPath);

                        if (!System.IO.Directory.Exists(targetPath + @"\raw\xmodel\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\raw\xmodel\");
                        if (!System.IO.Directory.Exists(targetPath + @"\raw\xmodelsurfs\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\raw\xmodelsurfs\");
                        if (!System.IO.Directory.Exists(targetPath + @"\raw\xmodelparts\"))
                            System.IO.Directory.CreateDirectory(targetPath + @"\raw\xmodelparts\");

                        if (File.Exists(xmodelPath) && !isFileTooOld(xmodelPath))
                            System.IO.File.Copy(xmodelPath, mapName + @"\raw\xmodel\" + asset.name, true);
                        else
                            continue; //dont bother looking for the rest if the xmodel wasn't found or is too old

                        List<string> xmodelstuff = getStringsFromXmodel(xmodelPath); //all of the texture filenames associated with this material
                        if (xmodelstuff != null)
                        {
                            foreach (string stuff in xmodelstuff)
                            {
                                string xmodelSurf = Path.Combine(rootDir, @"raw\xmodelsurfs\" + stuff);
                                if (File.Exists(xmodelSurf))
                                {
                                    System.IO.File.Copy(xmodelSurf, mapName + @"\raw\xmodelsurfs\" + stuff, true);
                                    //Console.WriteLine(xmodelSurf);
                                }

                                string xmodelPart = Path.Combine(rootDir, @"raw\xmodelparts\" + stuff);
                                if (File.Exists(xmodelPart))
                                {
                                    System.IO.File.Copy(xmodelPart, mapName + @"\raw\xmodelparts\" + stuff, true);
                                    //Console.WriteLine(xmodelPart);
                                }
                            }
                        }
                    }

                    _assets.Add(asset);
                }

                if (zipCheck.Checked)
                {
                    if (File.Exists(mapName + ".zip"))
                        File.Delete(mapName + ".zip");
                    System.IO.Compression.ZipFile.CreateFromDirectory(targetPath, mapName + ".zip", CompressionLevel.Fastest, true);
                    Directory.Delete(targetPath, true);
                }
                MessageBox.Show(mapName + " exported sucessfully to current program directory.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("File could not be opened (unknown error)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            currentlyBusy = false;
            button1.Enabled = true;
            openMenuItem.Enabled = true;
        }

        private void dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void dragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                openFileGroup(filePaths);
            }
        }
        #endregion

        #region UI Population
        private List<string> getStringsFromXmodel(string path)
        {
            try
            {
                StreamReader MyStreamReader = new StreamReader(path);
                
                //Grab the first 100 characters of the xmodel file, the stuff I need is more than likely there.
                int count = 0;
                string exceprt = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0 && count < 100)
                    {
                        count++;
                        exceprt = exceprt + (char)sr.Read();
                    }
                }

                //Filter out alphanumeric word formations which have a length of at least 3
                MatchCollection names = Regex.Matches(exceprt, @"([a-zA-Z _ 0-9]+)");
                List<string> results = new List<string>();
                foreach (Match name in names)
                {
                    if (name.Length > 2)
                        results.Add(name.Value.ToString());
                        //results.Add(name.ToString().Remove(0, 1));
                }

                //Go through list of matches and see if files by that name actually exist.
                return results;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Menu Items
        private void OnMruFile(int number, string filename)
        {
            openFileGroup(new List<string> { filename }.ToArray());
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "map file|*.map";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                openFileGroup(openFileDialog1.FileNames);
        }
        private void savePresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePaths != null)
                openFileGroup(filePaths);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ugx-mods.com/forum/index.php?topic=2756");
        }
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //updateChecker.RunWorkerAsync();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About AboutDialog = new About();
            AboutDialog.Show();
        }
        #endregion

        #region Updating
        static void checkForUpdates(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("======== CHECKING FOR UPDATES ========");
            // in newVersion variable we will store the  
            // version info from xml file  
            Version newVersion = null;
            // and in this variable we will put the url we  
            // would like to open so that the user can  
            // download the new version  
            // it can be a homepage or a direct  
            // link to zip/exe file  
            string url = "http://ugx-mods.com/downloads/assetcounter/version.xml";
            string desc = "";
            try
            {
                XmlTextReader reader = new XmlTextReader(url);
                string elementName = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                        elementName = reader.Name;
                    else
                    {
                        // for text nodes...  
                        if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                        {
                            // we check what the name of the node was  
                            switch (elementName)
                            {
                                case "version":
                                    Console.WriteLine("version found: " + reader.Value);
                                    newVersion = new Version(reader.Value);
                                    break;
                                case "description":
                                    Console.WriteLine("description: " + reader.Value);
                                    desc = reader.Value.ToString();
                                    break;
                                case "url":
                                    Console.WriteLine("url found: " + reader.Value);
                                    url = reader.Value;
                                    break;
                                default:
                                    Console.WriteLine("Unrecognized Element: " + elementName);
                                    break;
                            }
                        }
                    }
                }
                reader.Close();
            }
            catch
            {
            }

            // get the running version  
            Version curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            // compare the versions  
            if (curVersion.CompareTo(newVersion) < 0)
            {
                string title = "Update is available!";
                string question = "An update for UGX Asset Counter is available!\n\nv" + newVersion + " Changelog: " + desc + "\n\nView the new version now?";
                if (DialogResult.Yes == MessageBox.Show(question, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
        }
        #endregion

        private string getAssignedImage(int index)
        {
            string assetType = "";
            switch (index)
            {
                case 0:
                    assetType = "csv";
                    break;
                case 1:
                    assetType = "image";
                    break;
                case 2:
                    assetType = "material";
                    break;
                case 3:
                    assetType = "xmodel";
                    break;
                case 4:
                    assetType = "weapon";
                    break;
                case 5:
                    assetType = "loaded_sound";
                    break;
                case 6:
                    assetType = "fx";
                    break;
                case 7:
                    assetType = "ai";
                    break;
                case 8:
                    assetType = "menu";
                    break;
                case 9:
                    assetType = "menufile";
                    break;
                case 10:
                    assetType = "techset";
                    break;
                case 11:
                    assetType = "xanim";
                    break;
                case 12:
                    assetType = "font";
                    break;
                case 13:
                    assetType = "rawfile";
                    break;
                case 14:
                    assetType = "sound";
                    break;
                default:
                    assetType = "none";
                    break;
            }
            return assetType;
        }
        private string getAssetExtension(assetLine asset)
        {
            if (asset.type == "image")
                return ".iwi";
            if (asset.type == "csv")
                return ".csv";
            return "";
        }

        private bool isFileTooOld(string path)
        {
            if (!File.Exists(path))
                return false;

            if (!excludeOldCheck.Checked)
                return false;

            DateTime dt = File.GetLastWriteTime(path);
            if (dt.Year <= 2012) 
                return true;
            return false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "map file|*.map";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Process input if the user clicked OK.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                openFileGroup(openFileDialog1.FileNames);
        }

        private void mapCheck_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
