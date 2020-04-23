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
        struct charVals
        {
            public string charRace;             // Race
            public string charClass;            // Class
            public string charSClass;           // Subclass
            public string charBG;               // Background
            public int[] charAS;                // Ability Scores (with Racial Modifiers)
            public List<string> charSProf;      // List of All Skill Proficiencies
        };
        
        // Need to add a LoadData function and a number of derived functions to access information from SQL databases
        // There need to be the following databases: RACE, CLASS, BACKGROUND
        // These three databases will determine the available skill proficiencies, subrace, and subclass options

        OleDbConnection raceCn;
        OleDbConnection classCn;
        OleDbConnection bgCn;

        Dictionary<string, List<string>> raceDict = new Dictionary<string, List<string>>();     // Race --> Subrace
        Dictionary<string, List<string>> classDict = new Dictionary<string, List<string>>();    // Class --> Subclass
        Dictionary<string, List<string>> bgDict = new Dictionary<string, List<string>>();       // Background --> Skill Proficiencies

        Dictionary<string, int[]> modDict = new Dictionary<string, int[]>();                    // Subrace --> Array of Ability Score Racial Modifiers
        Dictionary<string, List<string>> cProfDict = new Dictionary<string, List<string>>();    // Class-Subclass Pair --> List of Skill Proficiencies for that Class-Subclass Pair
        Dictionary<string, int> cProfCount = new Dictionary<string, int>();                     // Class-Subclass Pair --> Number of possible Skill Proficiencies for that Class-Subclass Pair
        Dictionary<string, int> cProfChoiceCount = new Dictionary<string, int>();               // Class-Subclass Pair --> Number of actual Skill Proficiencies able to be chosen for that Class-Subclass Pair

        List<string> classHead = new List<string>();                                            // List of Subclass headings
        List<string> charBGSProf = new List<string>();                                          // List of Skill Proficiencies received from Background

        charVals character = new charVals();                // Creating a new character

        public MakeItEasy()
        {
            InitializeComponent();
            // On all of the following, the "| DataDirectory |" pipeline needed to be removed and replaced with a "." before the "\[]Data.accdb"
            raceCn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = .\RaceData.accdb");
            classCn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= .\ClassData.accdb");
            bgCn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= .\BackgroundData.accdb");

            character.charAS = new int[] { 10, 10, 10, 10, 10, 10 };    // Assigning default values to the ability scores
            character.charSProf = new List<string>();                   // Initializing the skill proficiency list

            LoadRaces();
            LoadClasses();
            LoadBGs();
        }

        private void LoadRaces()
        {
            string query = "select * from RaceTable";
            OleDbCommand cmd = new OleDbCommand(query, raceCn);
            raceCn.Open();
            OleDbDataReader read = cmd.ExecuteReader();

            string race;
            string subrace;
            string mSub;

            while (read.Read())                               // Populating the race dictionary 
            {
                race = read[1].ToString();                    // The raceDict key is chosen for the loop
                if (!raceDict.ContainsKey(race))              // If the key is not present, create a new list of subraces to serve as that key's definition
                {
                    raceDict[race] = new List<string>();
                }
                subrace = read[2].ToString();
                raceDict[race].Add(subrace);                 // Add the subrace to the definition of the key

                mSub = RemoveSpecialCharacters(race) + RemoveSpecialCharacters(subrace);
                // mSub (or Modifier Subrace) has the title "RaceSubrace" with no special characters. Default Half-Orc becomes HalfOrcDefault.
                // There are multiple "Default" races, so this ensures that everything after the first Default isn't filtered out.

                if (!modDict.ContainsKey(mSub))          // If the modifier dictionary does not already contain the subrace
                {
                    modDict[mSub] = new int[]
                        {Convert.ToInt32(read[3]),          // Strength
                         Convert.ToInt32(read[4]),          // Dexterity
                         Convert.ToInt32(read[5]),          // Constitution
                         Convert.ToInt32(read[6]),          // Intelligence
                         Convert.ToInt32(read[7]),          // Wisdom
                         Convert.ToInt32(read[8])           // Charisma
                        };
                }
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

            raceCn.Close();
        }

        private void LoadClasses()
        {
            string query = "select * from ClassTable";
            OleDbCommand cmd = new OleDbCommand(query, classCn);
            classCn.Open();
            OleDbDataReader read = cmd.ExecuteReader();

            string vclass;
            string subclass;
            string classPair;
            int profCount;
            int choiceCount;

            while (read.Read())                                 // Populating the class dictionary and list
            {
                vclass = read[1].ToString();                    // The key is chosen for the loop
                if (!classDict.ContainsKey(vclass))             // If the key is not present, create a new list of subclasses to serve as the definition and add the heading to the classHead list
                {
                    classDict[vclass] = new List<string>();
                    classHead.Add(read[2].ToString());          // Archetype heading: "School, Path of, etc." (flavor text)
                }
                subclass = read[3].ToString();
                classDict[vclass].Add(subclass);                // Add the subclass to the definition list

                choiceCount = Convert.ToInt32(read[4]);
                profCount = Convert.ToInt32(read[5]);

                classPair = RemoveSpecialCharacters(vclass) + RemoveSpecialCharacters(subclass);

                if (!cProfChoiceCount.ContainsKey(classPair))
                {
                    cProfChoiceCount[classPair] = choiceCount;
                }

                if (!cProfCount.ContainsKey(classPair))
                    cProfCount[classPair] = profCount;

                if (!cProfDict.ContainsKey(classPair))          // If the Dictionary does not contain a given class-subclass combo as a key...
                {
                    cProfDict[classPair] = new List<string>();

                    for (int i = 6; i < profCount + 5; i++)     // Proficiencies start at column 6 and go until column 23, maxing out with a profCount of 18
                    {
                        cProfDict[classPair].Add(read[i].ToString());   // BardLore has all 18, but BarbarianTotemWarrior just has 6
                    }
                }
            }

            foreach (string key in classDict.Keys)
            {
                ComboBoxItem keyVal = new ComboBoxItem();
                keyVal.Content = key;
                keyVal.Name = RemoveSpecialCharacters(key);
                ClassSelector.Items.Add(keyVal);
            }
            // The contents of the subclass selector will be covered in the ComboBoxItem upon SelectionChanged()

            classCn.Close();
        }

        private void LoadBGs() // Load Backgrounds
        {
            string query = "select * from BGTable";
            OleDbCommand cmd = new OleDbCommand(query, bgCn);
            bgCn.Open();
            OleDbDataReader read = cmd.ExecuteReader();

            string bg;

            while (read.Read())
            {
                bg = read[1].ToString();
                                                                // No conditional is necessary as every default background has a unique value.
                                                                // In the event that not every background has a unique value, add a conditional

                bgDict[bg] = new List<string>();                // Create a new dictionary entry with the key being 'bg' and the value being a list.

                bgDict[bg].Add(read[2].ToString());             // Add the first skill proficiency to the list
                bgDict[bg].Add(read[3].ToString());             // Add the second skill proficiency to the list
            }

            foreach (string key in bgDict.Keys)
            {
                ComboBoxItem keyVal = new ComboBoxItem();
                keyVal.Content = key;
                keyVal.Name = RemoveSpecialCharacters(key);
                BackgroundSelector.Items.Add(keyVal);
            }

            bgCn.Close();
        }

        public string RemoveSpecialCharacters(string str)
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

        private void SubraceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string baseRace = ((ComboBoxItem)RaceSelector.SelectedItem).Content.ToString();
            string subRace = ((ComboBoxItem)SubraceSelector.SelectedItem).Content.ToString();

            if (subRace != "Default") character.charRace = subRace + " " + baseRace;
            else character.charRace = baseRace;
        }

        private void ClassSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubclassSelector.Items.Clear(); // Clears the content of the SubclassSelector.
            ProficiencySelector.Items.Clear();
            MaxClassSkills.Text = "";
            ProficiencyTotalList.Text = "";

            string comp = ((ComboBoxItem)ClassSelector.SelectedItem).Content.ToString(); // The current content of the ClassSelector after change.

            foreach (var entry in classDict) // For each entry in the ClassDictionary...
            {
                if (entry.Key == comp)// ... if the 'key' for that entry is equal to the current content of the ClassSelector...
                {
                    List<string> subclasses = entry.Value; // ... the list of subclasses corresponds to the entry's value.

                    int scpi = classDict.Keys.ToList().IndexOf(entry.Key); // Find the "index" of the key - SCPI stands for SubClass Preface Index

                    // As the list and dictionary were constructed at the same time, the order of the dictionary should not matter when searching for the index

                    SubclassPreface.Text = classHead[scpi]; // Changes the preface text above the subclass according to the class selected: "School, Path, Way, etc."

                    foreach (string subclass in subclasses) // For each subclass in the list of subclasses...
                    {
                        ComboBoxItem nSub = new ComboBoxItem(); // Create a new combobox item to hold the subclass...
                        nSub.Content = subclass;
                        // ... assign its value to be the current subclass...

                        nSub.Name = RemoveSpecialCharacters(entry.Key) + "_" + RemoveSpecialCharacters(subclass);
                        // ... assign its name to be the class from which the subclass is derived, concatenated with an underscore and the subclass...
                        SubclassSelector.Items.Add(nSub);
                        // ... and add the combobox item to the SubclassSelector as an option.
                    }
                }
            }
        }

        private void SubclassSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string baseClass = ((ComboBoxItem)ClassSelector.SelectedItem).Content.ToString();
            ProficiencySelector.Items.Clear();

            if (SubclassSelector.SelectedItem != null)
            {
                string subClass = ((ComboBoxItem)SubclassSelector.SelectedItem).Content.ToString();

                character.charClass = baseClass;
                character.charSClass = subClass;

                string classPair = RemoveSpecialCharacters(baseClass) + RemoveSpecialCharacters(subClass);

                
                MaxClassSkills.Text = cProfChoiceCount[classPair].ToString();

                foreach (var entry in cProfDict) // For each Class-Subclass pair...
                {
                    if (entry.Key == classPair) // Find the one where it matches.
                    {
                        List<string> proficiencies = entry.Value;   // The class's proficiencies equal the Value.
                        int choices = cProfChoiceCount[classPair];  // cProfChoiceCount and cProfDict have identical keys

                        foreach (string proficiency in proficiencies)
                        {
                            ListBoxItem nProf = new ListBoxItem();
                            nProf.Content = proficiency;

                            nProf.Name = RemoveSpecialCharacters(entry.Key) + "_" + RemoveSpecialCharacters(proficiency); // "BarbarianTotemWarrior_AnimalHandling"

                            ProficiencySelector.Items.Add(nProf);
                        }
                    }
                }
            }
        }

        private void BackgroundSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string background = ((ComboBoxItem)BackgroundSelector.SelectedItem).Content.ToString();

            character.charBG = background;

            charBGSProf.Clear();                // Clears the list of background skill proficiencies

            foreach (var entry in bgDict)
            {
                if (entry.Key == background)
                {
                    List<string> proficiencies = entry.Value;

                    foreach (string proficiency in proficiencies)
                    {
                        charBGSProf.Add(proficiency);
                        // Add these proficiencies to the TextBlock for Proficiencies and the character skill list
                    }
                }
            }

            if (ClassSelector.SelectedItem != null && SubclassSelector.SelectedItem != null)
            {
                string baseClass = ((ComboBoxItem)ClassSelector.SelectedItem).Content.ToString();
                string subClass = ((ComboBoxItem)SubclassSelector.SelectedItem).Content.ToString();

                string classPair = RemoveSpecialCharacters(baseClass) + RemoveSpecialCharacters(subClass);

                int maxSelections = cProfChoiceCount[classPair];

                StringBuilder sb = new StringBuilder();

                foreach (ListBoxItem item in ProficiencySelector.SelectedItems)
                {
                    sb.Append(item.Content + ", ");
                }

                foreach (string proficiency in charBGSProf)
                {
                    sb.Append(proficiency + ", ");
                }

                ProficiencyTotalList.Text = sb.ToString();

            }
        }

        private void ProficiencySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string baseClass = ((ComboBoxItem)ClassSelector.SelectedItem).Content.ToString();
            if (SubclassSelector.SelectedItem != null)
            {
                string subClass = ((ComboBoxItem)SubclassSelector.SelectedItem).Content.ToString();

                string classPair = RemoveSpecialCharacters(baseClass) + RemoveSpecialCharacters(subClass);

                int maxSelections = cProfChoiceCount[classPair];

                StringBuilder sb = new StringBuilder();

                foreach (ListBoxItem item in ProficiencySelector.SelectedItems)
                {
                    sb.Append(item.Content + ", ");
                }

                foreach (string proficiency in charBGSProf)
                {
                    sb.Append(proficiency + ", ");
                }

                ProficiencyTotalList.Text = sb.ToString();

                if (ProficiencySelector.SelectedItems.Count > maxSelections)
                {
                    ProficiencyTotalList.Text = "";
                    ProficiencySelector.SelectedIndex = -1;                     // Need to test, should clear selections and text box of selections
                }
            }
        }

        private void strBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(strBox.Text, out value))
            {
                abilityWarning.Visibility = Visibility.Collapsed;
                character.charAS[0] = value;

                if (!int.TryParse(dexBox.Text, out value) ||
                    !int.TryParse(conBox.Text, out value) ||
                    !int.TryParse(intBox.Text, out value) ||
                    !int.TryParse(wisBox.Text, out value) ||
                    !int.TryParse(chaBox.Text, out value))
                {
                    abilityWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                abilityWarning.Visibility = Visibility.Visible;
            }
        }

        private void dexBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(dexBox.Text, out value))
            {
                abilityWarning.Visibility = Visibility.Collapsed;
                character.charAS[1] = value;

                if (!int.TryParse(strBox.Text, out value) ||
                    !int.TryParse(conBox.Text, out value) ||
                    !int.TryParse(intBox.Text, out value) ||
                    !int.TryParse(wisBox.Text, out value) ||
                    !int.TryParse(chaBox.Text, out value))
                {
                    abilityWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                abilityWarning.Visibility = Visibility.Visible;
            }
        }

        private void conBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(conBox.Text, out value))
            {
                abilityWarning.Visibility = Visibility.Collapsed;
                character.charAS[2] = value;

                if (!int.TryParse(strBox.Text, out value) ||
                    !int.TryParse(dexBox.Text, out value) ||
                    !int.TryParse(intBox.Text, out value) ||
                    !int.TryParse(wisBox.Text, out value) ||
                    !int.TryParse(chaBox.Text, out value))
                {
                    abilityWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                abilityWarning.Visibility = Visibility.Visible;
            }
        }

        private void intBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(conBox.Text, out value))
            {
                abilityWarning.Visibility = Visibility.Collapsed;
                character.charAS[3] = value;

                if (!int.TryParse(strBox.Text, out value) ||
                    !int.TryParse(dexBox.Text, out value) ||
                    !int.TryParse(conBox.Text, out value) ||
                    !int.TryParse(wisBox.Text, out value) ||
                    !int.TryParse(chaBox.Text, out value))
                {
                    abilityWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                abilityWarning.Visibility = Visibility.Visible;
            }
        }

        private void wisBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(conBox.Text, out value))
            {
                abilityWarning.Visibility = Visibility.Collapsed;
                character.charAS[4] = value;

                if (!int.TryParse(strBox.Text, out value) ||
                    !int.TryParse(dexBox.Text, out value) ||
                    !int.TryParse(conBox.Text, out value) ||
                    !int.TryParse(intBox.Text, out value) ||
                    !int.TryParse(chaBox.Text, out value))
                {
                    abilityWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                abilityWarning.Visibility = Visibility.Visible;
            }
        }

        private void chaBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (int.TryParse(conBox.Text, out value))
            {
                abilityWarning.Visibility = Visibility.Collapsed;
                character.charAS[5] = value;

                if (!int.TryParse(strBox.Text, out value) ||
                    !int.TryParse(dexBox.Text, out value) ||
                    !int.TryParse(conBox.Text, out value) ||
                    !int.TryParse(intBox.Text, out value) ||
                    !int.TryParse(wisBox.Text, out value))
                {
                    abilityWarning.Visibility = Visibility.Visible;
                }
            }
            else
            {
                abilityWarning.Visibility = Visibility.Visible;
            }
        }
    }
}
