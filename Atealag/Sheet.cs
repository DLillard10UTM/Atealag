using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        new string[] { "Strength", "Intelligence", "Dexterity", "Wisdom", "Constitution", "Charisma" });

        public CharVals userCharVals;
        public MainTab userMainTab;
        Broker abilityScoreBroker;
        //Non-paramertized constructor, for "New Sheet" button on Main Window.
        public Sheet()
        {
            userCharVals = new CharVals();
            //I like the name traveler for rpg characters, so that is the name here.
            userCharVals.name = "Traveler";
            userCharVals.race = "[RACE]";
            userCharVals.u_class = "[CLASS]";
            userCharVals.sClass = "[SUBCLASS]";
            userCharVals.BG = "[BACKGROUND]";
            userCharVals.alig = "[ALIGNMENT]";
            userCharVals.userName = "[USERNAME]";
            abilityScoreBroker = new Broker();
            userMainTab = new MainTab();
            
        }

        //Paramertized constructor, for "Open Sheet" & "Make it Easy" button on Main Window.
        Sheet(CharVals fedVals)
        {
            userCharVals = new CharVals();
            userCharVals = fedVals;
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