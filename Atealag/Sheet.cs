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

        public CharVals userCharVals;
        public MainTab userMainTab;
        
        //Non-paramertized constructor, for "New Sheet" button on Main Window.
        public Sheet()
        {
            userCharVals = new CharVals();
            userCharVals.sheet = this;
            userMainTab = new MainTab();

            //Leave this for last.
            coupleObjects();
        }

        //Paramertized constructor, for "Open Sheet" & "Make it Easy" button on Main Window.
        //Each object will need a paramertized constructor to be able to follow this format.
        public Sheet(string file)
        {
            //We read in the entire file this way.
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
            
            //Begin with the userCharVals.
            userCharVals = new CharVals(this, text[0], text[1], text[2], text[3], text[4], text[5], text[6], text[7],
                                        text[8], text[9], text[10], text[11], text[12], text[13], text[14], text[15],
                                        text[16], text[17], text[18], text[19], text[20]);

            //Leave this for last.
            coupleObjects();
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

        //This function will update all things dependent on level when called, called when level is changed.
        public void updateLevel()
        {
            userMainTab.hpDisplay.level = userCharVals.level;
        }

        //Couple the ability score pub subs, for new sheet.
        void coupleObjects()
        {
            userMainTab.hpDisplay.HPCalcCouple = new HPSub(userCharVals.abilityScoreBroker, userMainTab.hpDisplay);
            //setting up the default to con.
            userCharVals.abilityScoreBroker.subscribe(userMainTab.hpDisplay.HPCalcCouple, 4);

            //Coupling AC objects, need to couple two subs here, primary and secondary.
            userMainTab.acDisplay.primarySubCouple = new primaryACSub(userCharVals.abilityScoreBroker, userMainTab.acDisplay);
            userCharVals.abilityScoreBroker.subscribe(userMainTab.acDisplay.primarySubCouple, 2);

            userMainTab.acDisplay.secondarySubCouple = new secondaryACSub(userCharVals.abilityScoreBroker, userMainTab.acDisplay);
        }

        //Couple the ability score pub subs, for saved sheet. index is the ability score they are subbed to.
        void coupleObjects(int hpindex, int acPrimIndex, int acSecIndex)
        {
            //Coupling HP objects.
            userMainTab.hpDisplay.HPCalcCouple = new HPSub(userCharVals.abilityScoreBroker, userMainTab.hpDisplay);
            //setting up the default to con.
            userCharVals.abilityScoreBroker.subscribe(userMainTab.hpDisplay.HPCalcCouple, hpindex);

            //Coupling AC objects, need to couple two subs here, primary and secondary.
            userMainTab.acDisplay.primarySubCouple = new primaryACSub(userCharVals.abilityScoreBroker, userMainTab.acDisplay);
            userCharVals.abilityScoreBroker.subscribe(userMainTab.acDisplay.primarySubCouple, acPrimIndex);

            userMainTab.acDisplay.secondarySubCouple = new secondaryACSub(userCharVals.abilityScoreBroker, userMainTab.acDisplay);
            userCharVals.abilityScoreBroker.subscribe(userMainTab.acDisplay.secondarySubCouple, acSecIndex);
        }
    }
}