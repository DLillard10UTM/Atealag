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
        public MakeItEasy()
        {
            InitializeComponent();
            // On all of the following the "| DataDirectory |" pipeline might need to be removed and replaced with a "." before the "\[]Data.accdb"
            raceCn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =| DataDirectory |\RaceData.accdb");
            classCn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\ClassData.accdb");
        }

        private void LoadRaceBox(object sender, RoutedEventArgs e)
        {
            string query = "select * from RaceTable";
            OleDbCommand cmd = new OleDbCommand(query, raceCn);
            raceCn.Open();
            OleDbDataReader read = cmd.ExecuteReader();

            List<ComboBoxItem> racedata = new List<ComboBoxItem>(); // Reads the second column (Race) [1] and stores all information (even repeated races)
            int loopIteration = 0;

            while (read.Read()) // Fills the racedata list
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = read[1].ToString();
                racedata[loopIteration] = cbi;
                loopIteration++;
            }

            for (int i = 0; i < racedata.Count; i++) // Populates the RaceSelector ComboBox with elements of racedata with "unique" content (no repeated races)
            {

            }
        }

    }

}
