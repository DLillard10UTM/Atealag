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
            raceCn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =| DataDirectory |\RaceData.accdb");
            classCn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\ClassData.accdb");
        }

        private void LoadRaces(object sender, RoutedEventArgs e)
        {
            string query = "select * from RaceTable";
            OleDbCommand cmd = new OleDbCommand(query, raceCn);
            raceCn.Open();
            OleDbDataReader read = cmd.ExecuteReader();

            List<string> raceData = new List<string>(); // Human, Elf, Dwarf, Halfling, Gnome, Half-Elf, Half-Orc, Dragonborn, Tiefling (9 entries base)
            List<string> raceTotal = new List<string>(); // Human, Human, Elf, Elf, Elf, Dwarf, Dwarf... (24 entries base)
            List<string> subraceTotal = new List<string>(); // Default, Variant, High, Wood, Drow, Hill, Mountain... (24 entries base)

            while (read.Read()) // Creating the three lists necessary to construct the dictionary
            {
                if (!raceData.Contains(read[1].ToString()))
                {
                    raceData.Add(read[1].ToString());
                }
                raceTotal.Add(read[1].ToString());
                subraceTotal.Add(read[2].ToString());
            }

            foreach (string item in raceData) // For each of the [9] races...
            {
                if (raceTotal.Contains(item)) // If the raceTotal list contains an instance of the race...
                {
                    // Find the first index where that race occurs and make note of it...
                    // Find any additional indices where the race occurs...
                    // Create a new list of strings containing the subraces at the aforementioned indicies...
                    // Add the race as the key to a dictionary entry and add the list of subraces as the corresponding value
                }
                // If the raceTotal list does not contain and instance of the race... do nothing (even though there should be at least one)
                // Repeat for each of the [9] races and the dictionary of entries should be complete.
            }

            ComboBoxItem defaultValue = new ComboBoxItem(); // Creates the default option in the RaceSelector Combobox ("Select a race")
            defaultValue.Content = "Select a race";
            defaultValue.IsSelected = true;
            RaceSelector.Items.Add(defaultValue);

            // Using the completed dictionary, create a ComboBoxItem with content corresponding with the keys of the dictionary. This is the race selector.
            // The contents of the subrace selector will be covered in the ComboBoxItem upon SelectionChanged()
        }

    }

}
