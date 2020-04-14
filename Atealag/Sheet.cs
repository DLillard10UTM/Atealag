using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string fileName;

        //Global variable that all tabs must have to populate ability score dropdown menus.
        public ReadOnlyCollection<string> abilityScores { get; } = new ReadOnlyCollection<string>(
        new string[] { "Strength", "Intelligence", "Dexterity", "Wisdom", "constitution", "Charisma" });
        struct charVals
        {
            
            public string name;
            public string race; 
            public string u_class;
            public string sClass;
            public string BG;
            public string alig;
            //Size 6 array with format: STR, INT, DEX, WIS, CON, CHA
            public int[] scoreVals;
            public List<string> skillProfs;
        }
        charVals userCharVals;
        //Non-paramertized constructor, for "New Sheet" button on Main Window.
        Sheet()
        {
            userCharVals = new charVals();
            //I like the name traveler for rpg characters, so that is the name here.
            userCharVals.name = "Traveler";
        }

        //Paramertized constructor, for "Open Sheet" & "Make it Easy" button on Main Window.
        Sheet(charVals fedVals)
        {
            userCharVals = new charVals();
            userCharVals = fedVals;
            userCharVals.name = "Traveler";
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
