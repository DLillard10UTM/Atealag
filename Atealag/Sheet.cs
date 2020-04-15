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

        public charVals userCharVals;
        public MainTab userMainTab;
        Broker abilityScoreBroker;
        //Non-paramertized constructor, for "New Sheet" button on Main Window.
        public Sheet()
        {
            userCharVals = new charVals();
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
        Sheet(charVals fedVals)
        {
            userCharVals = new charVals();
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

    //Class to hold the user values.
    class charVals : INotifyPropertyChanged
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("name");
                }
            }
        }
        private string _race;
        public string race
        {
            get { return _race; }
            set
            {
                if (_race != value)
                {
                    _race = value;
                    NotifyPropertyChanged("race");
                }
            }
        }
        private string _u_class;
        public string u_class
        {
            get { return _u_class; }
            set
            {
                if (_u_class != value)
                {
                    _u_class = value;
                    NotifyPropertyChanged("u_class");
                }
            }
        }
        private string _sClass;
        public string sClass
        {
            get { return _sClass; }
            set
            {
                if (_sClass != value)
                {
                    _sClass = value;
                    NotifyPropertyChanged("sClass");
                }
            }
        }
        private string _BG;
        public string BG
        {
            get { return _BG; }
            set
            {
                if (_BG != value)
                {
                    _BG = value;
                    NotifyPropertyChanged("BG");
                }
            }
        }
        private string _alig;
        public string alig
        {
            get { return _alig; }
            set
            {
                if (_alig != value)
                {
                    _alig = value;
                    NotifyPropertyChanged("alig");
                }
            }
        }
        private int _level;
        public int level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    NotifyPropertyChanged("level");
                }
            }
        }
        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyPropertyChanged("userName");
                }
            }
        }

        //Size 6 array with format: STR, INT, DEX, WIS, CON, CHA
        public int[] modScoreVals;
        public int[] baseScoreVals;
        public int[] miscScoreVals;
        public List<string> skillProfs;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}