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

            List<int> miscBonuses = new List<int>();
            //loading the saving throw miscs and bools into lists for sanity.
            for(int i = 31; i <= 36; i++)
            {
                miscBonuses.Add(Convert.ToInt32(text[i]));
            }
            List<bool> isChecked = new List<bool>();
            for(int i = 37; i <= 42; i++)
            {
                    isChecked.Add(Convert.ToBoolean(text[i]));
            }
            userMainTab = new MainTab(text[21], text[22], text[23], text[25], text[26], text[29], text[30], miscBonuses,
                                      isChecked, text[43]);
            //Begin with the userCharVals.
            userCharVals = new CharVals(this, text[0], text[1], text[2], text[3], text[4], text[5], text[6], text[7],
                                        text[8], text[9], text[10], text[11], text[12], text[13], text[14], text[15],
                                        text[16], text[17], text[18], text[19], text[20]);


            //Leave this for last.
            updateLevel();
            coupleObjects(Convert.ToInt32(text[24]), Convert.ToInt32(text[27]), Convert.ToInt32(text[28]));
        }
        public void saveSheet(string ourFileName)
        {
            using (StreamWriter writer = new StreamWriter(ourFileName))
            {
                writer.WriteLine(userCharVals.name);
                writer.WriteLine(userCharVals.race);
                writer.WriteLine(userCharVals.u_class);
                writer.WriteLine(userCharVals.sClass);
                writer.WriteLine(userCharVals.BG);
                writer.WriteLine(userCharVals.alig);
                writer.WriteLine(userCharVals.level);
                writer.WriteLine(userCharVals.userName);
                writer.WriteLine(userCharVals.strBase);
                writer.WriteLine(userCharVals.intelBase);
                writer.WriteLine(userCharVals.dexBase);
                writer.WriteLine(userCharVals.wisBase);
                writer.WriteLine(userCharVals.conBase);
                writer.WriteLine(userCharVals.chaBase);
                writer.WriteLine(userCharVals.strMisc);
                writer.WriteLine(userCharVals.intelMisc);
                writer.WriteLine(userCharVals.dexMisc);
                writer.WriteLine(userCharVals.wisMisc);
                writer.WriteLine(userCharVals.conMisc);
                writer.WriteLine(userCharVals.chaMisc);
                writer.WriteLine(userCharVals.skillProfs);
                writer.WriteLine(userMainTab.hpDisplay.currHealth);
                writer.WriteLine(userMainTab.hpDisplay.baseHealth);
                writer.WriteLine(userMainTab.hpDisplay.misc);
                writer.WriteLine(userMainTab.hpDisplay.HPCalcCouple.getScoreIndex());
                writer.WriteLine(userMainTab.acDisplay.ArmorBonus);
                writer.WriteLine(userMainTab.acDisplay.misc);
                writer.WriteLine(userMainTab.acDisplay.primarySubCouple.getScoreIndex());
                writer.WriteLine(userMainTab.acDisplay.secondarySubCouple.getScoreIndex());
                writer.WriteLine(userMainTab.speedDisplay.baseSpeed);
                writer.WriteLine(userMainTab.speedDisplay.miscSpeed);
                writer.WriteLine(userMainTab.savingThrowsDisplay.miscBonuses[0]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.miscBonuses[1]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.miscBonuses[2]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.miscBonuses[3]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.miscBonuses[4]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.miscBonuses[5]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.checkBoxes[0]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.checkBoxes[1]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.checkBoxes[2]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.checkBoxes[3]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.checkBoxes[4]);
                writer.WriteLine(userMainTab.savingThrowsDisplay.checkBoxes[5]);
                writer.WriteLine(userMainTab.initCalcDisplay.miscBonus);
            }
        }
        public void saveAsSheet(string ourFileName)
        {
            saveSheet(ourFileName);
        }

        //This function will update all things dependent on level when called, called when level is changed.
        public void updateLevel()
        {
            if (userCharVals != null)
            {
                userMainTab.hpDisplay.level = userCharVals.level;
                userMainTab.savingThrowsDisplay.level = userCharVals.level;
                userCharVals.calcProficientBonus();
            }
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

            userMainTab.savingThrowsDisplay.createSubs(userCharVals.abilityScoreBroker);

            //creating sub and subbing to dex.
            userMainTab.initCalcDisplay.initSub = new InitSub(userCharVals.abilityScoreBroker, userMainTab.initCalcDisplay);
            userCharVals.abilityScoreBroker.subscribe(userMainTab.initCalcDisplay.initSub, 2);
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

            userMainTab.savingThrowsDisplay.createSubs(userCharVals.abilityScoreBroker);

            //creating sub and subbing to dex.
            userMainTab.initCalcDisplay.initSub = new InitSub(userCharVals.abilityScoreBroker, userMainTab.initCalcDisplay);
            userCharVals.abilityScoreBroker.subscribe(userMainTab.initCalcDisplay.initSub, 2);
        }
    }
}