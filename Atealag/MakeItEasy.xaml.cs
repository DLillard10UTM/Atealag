﻿using System;
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

            ComboBoxItem defaultValue = new ComboBoxItem(); // Creates the default option in the RaceSelector Combobox ("Select a race")
            defaultValue.Content = "Select a race";
            defaultValue.IsSelected = true;
            RaceSelector.Items.Add(defaultValue);

            List<ComboBoxItem> selectRaces = new List<ComboBoxItem>(); // Used for string equality comparisons, identical data inserted to RaceSelector
            int sri = 0; // "select races index"
            bool equalChecker;

            for (int i = 0; i < racedata.Count; i++) // Populates the RaceSelector ComboBox with elements of racedata with "unique" content (no repeated races)
            {
                if (i == 0)
                {
                    RaceSelector.Items.Add(racedata[0]);
                    selectRaces[sri] = racedata[0];
                }
                else
                {
                    for (int j = 0; j < selectRaces.Count; j++) // Loops over all existing data in RaceSelector to compare existing strings
                    {
                        equalChecker = Equals(selectRaces[j].Content, racedata[i].Content);
                    }
                }
                sri++;
            }
        }

    }

}