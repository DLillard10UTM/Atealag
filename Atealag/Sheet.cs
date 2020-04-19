using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{

    /*  Author: Danny Lillard of Atealag dev team.
     *  Date: 4/12/20
     *  Description: This file will be responsible for the holding of the tabs that run the manager window 
     *               for the program. This can also like a sheet with the HP and Initiative trackers.
     *               It also holds any information that transends the tabs.
     * 
     */
    class Sheet 
    {
        public string fileName;

        //Global variable that all tabs must have to populate ability score dropdown menus.
        public ReadOnlyCollection<string> abilityScores { get; } = new ReadOnlyCollection<string>(
        new string[] { "Strength", "Intelligence", "Dexterity", "Wisdom", "Constitution", "Charisma" });

        public CharVals userCharVals;
        public MainTab userMainTab;
        
        //Non-paramertized constructor, for "New Sheet" button on Main Window.
        public Sheet()
        {
            userCharVals = new CharVals();
            //I like the name traveler for rpg characters, so that is the name here.
            userMainTab = new MainTab();
            
        }

        //Paramertized constructor, for "Open Sheet" & "Make it Easy" button on Main Window.
        public Sheet(string file)
        {
            fileName = file;
            List<string> text = new List<string>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    text.Add(line);
                }
            }
            //CharacterSheet newCharSheet = new CharacterSheet(fileName);
            //newCharSheet.Show();
            //20
            userCharVals = new CharVals(text[0], text[1], text[2], text[3], text[4], text[5], text[6], text[7],
                                        text[8], text[9], text[10], text[11], text[12], text[13], text[14], text[15],
                                        text[16], text[17], text[18], text[19], text[20]);
        }
        public void saveSheet()
        {

        }
        public void saveAsSheet()
        {

        }

        public void addToHPTracker()
        {

        }
        public void addToInitTracker()
        {

        }
    }
}