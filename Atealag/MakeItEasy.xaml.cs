using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace Atealag
{
    /// <summary>
    /// Interaction logic for MakeItEasy.xaml
    /// </summary>
    public partial class MakeItEasy : Window
    {
        // Need to add a LoadData function and a number of derived functions to access information from SQL databases
        // There need to be the following databases: RACE, CLASS, BACKGROUND
        // These three databases will determine the available skill proficiencies, subrace, and subclass options

        OleDbConnection raceCn;
        OleDbConnection classCn;
        OleDbConnection bgCn;
        Dictionary<string, List<string>> raceDict = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> classDict = new Dictionary<string, List<string>>();
        public MakeItEasy()
        {
            InitializeComponent();
            // On all of the following the "| DataDirectory |" pipeline might need to be removed and replaced with a "." before the "\[]Data.accdb"
            raceCn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = .\RaceData.accdb");
            classCn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\ClassData.accdb");
            LoadRaces();
        }

        private void LoadRaces()
        {
            string query = "select * from RaceTable";
            OleDbCommand cmd = new OleDbCommand(query, raceCn);
            raceCn.Open();
            OleDbDataReader read = cmd.ExecuteReader();

            string race;

            while (read.Read())                               // Populating the race dictionary 
            {
                race = read[1].ToString();                    // The key is chosen for the loop
                if (!raceDict.ContainsKey(race))              // If the key is not present, create a new list of subraces to serve as that key's definition
                {
                    raceDict[race] = new List<string>();
                }
                raceDict[race].Add(read[2].ToString());       // Add the subrace to the definition of the key
            }

            // Using the completed dictionary, create a ComboBoxItem with content corresponding with the keys of the dictionary. This is the race selector.
            foreach (string key in raceDict.Keys)
            {
                ComboBoxItem keyVal = new ComboBoxItem();
                keyVal.Content = key;
                keyVal.Name = RemoveSpecialCharacters(key);
                RaceSelector.Items.Add(keyVal);
            }
            // The contents of the subrace selector will be covered in the ComboBoxItem upon SelectionChanged()
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '.') || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void RaceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubraceSelector.Items.Clear(); // Clears the content of the SubraceSelector.

            string comp = ((ComboBoxItem)RaceSelector.SelectedItem).Content.ToString(); // The current content of the RaceSelector after change.

            foreach (var entry in raceDict) // For each entry in the RaceDictionary...
            {
                if (entry.Key == comp) // ... if the 'key' for that entry is equal to the current content of the RaceSelector...
                {
                    List<string> subraces = entry.Value; // ... the list of subraces corresponds to the entry's value.

                    foreach(string subrace in subraces) // For each subrace in the list of subraces...
                    {
                        ComboBoxItem nSub = new ComboBoxItem(); // Create a new combobox item to hold the subrace...
                        nSub.Content = subrace;
                        // ... assign its value to be the current subrace...

                        nSub.Name = RemoveSpecialCharacters(entry.Key) + "_" + subrace;
                        // ... assign its name to be the race from which the subrace is derived, concatenated with an underscore and the subrace...

                        SubraceSelector.Items.Add(nSub);
                        // ... and add the combobox item to the SubraceSelector as an option.
                    }
                }
            }
        }
    }

}
