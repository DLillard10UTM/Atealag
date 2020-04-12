using System;
using System.Collections.Generic;
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
        struct charVals
        {
            public string name;
            public string race; 
            public string u_class;
            public string sClass;
            public string BG;
            //Size 9 array with format: Health, AC, Speed, STR, INT, DEX, WIS, CON, CHA
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
