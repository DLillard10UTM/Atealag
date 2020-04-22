using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    /*  Author: Danny Lillard of aetalag dev team
     *  Date: 4/20/2020
     *  Description: this class calculates, and is the logic that displays the AC on the main tab.
     */

    //Holds the AC box and all calculations needed to make it run.
    public class ACBox : INotifyPropertyChanged
    {
        const int baseAC = 10;
        public primaryACSub primarySubCouple;
        public secondaryACSub secondarySubCouple;
        private int _ACTotal;
        public int ACTotal
        {
            get
            {
                return _ACTotal;
            }
            set
            {
                _ACTotal = value;
                NotifyPropertyChanged("ACTotal");
            }
        }

        //Mod is calculated, (bonus - 10) / 2.

        private int _primaryAbilityMod;
        public int primaryAbilityMod
        {
            get { return _primaryAbilityMod; }
            set
            {
                _primaryAbilityMod = value;
                NotifyPropertyChanged("primaryAbilityMod");
                ACTotal = calculateAC();
            }
        }

        private int _secondaryAbilityMod;
        public int secondaryAbilityMod
        {
            get { return _secondaryAbilityMod; }
            set
            {
                _secondaryAbilityMod = value;
                NotifyPropertyChanged("secondaryAbilityMod");
                ACTotal = calculateAC();
            }
        }

        private int _armorBonus;
        public int ArmorBonus
        {
            get { return _armorBonus; }
            set
            {
                if (_armorBonus != value)
                {
                    _armorBonus = value;
                    ACTotal = calculateAC();
                }
            }
        }

        private int _misc;
        public int misc
        {
            get { return _misc; }
            set
            {
                if (_misc != value)
                {
                    _misc = value;
                    ACTotal = calculateAC();
                }
            }
        }

        public void calculatePrimaryAbilityMod()
        {
            primaryAbilityMod = (primarySubCouple.getScore() - 10) / 2;
        }
        public void calculateSecondaryAbilityMod()
        {
            secondaryAbilityMod = (secondarySubCouple.getScore() - 10) / 2;
        }
        int calculateAC()
        {
            return primaryAbilityMod + secondaryAbilityMod + baseAC + ArmorBonus + misc;
        }

        //Updates gui on changes.
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //Holds the sub for the primary ability score. 
    public class primaryACSub : Sub
    {
        Broker ourBroker;
        ACBox ourACBox;
        public primaryACSub(Broker broker, ACBox acBoxToCoupleTo) : base(broker)
        {
            ourBroker = broker;
            ourACBox = acBoxToCoupleTo;
        }
        public override void setScore(int newScore)
        {
            score = newScore;
            ourACBox.calculatePrimaryAbilityMod();
        }

        public override void changeAbScore(int currIndex, int newIndex)
        {
            ourBroker.unSubscribe(this, currIndex);
            ourBroker.subscribe(this, newIndex);
            pubIndex = newIndex;
            ourACBox.calculatePrimaryAbilityMod();
        }
    }

    //Holds the sub for the secondary ability score, only for some classes so the GUI will need a N/A. 
    public class secondaryACSub : Sub
    {
        public Broker ourBroker;
        ACBox ourACBox; 
        public secondaryACSub(Broker broker, ACBox acBoxToCoupleTo) : base(broker)
        {
            ourBroker = broker;
            ourACBox = acBoxToCoupleTo;
            pubIndex = 6;
        }
        public override void setScore(int newScore)
        {
            score = newScore;
            ourACBox.calculateSecondaryAbilityMod();
        }

        public override void changeAbScore(int currIndex, int newIndex)
        {
            //If N/A is currently selected.
            if (currIndex == 6 && newIndex != 6)
            {
                //There is nothing to un sub from.
                ourBroker.subscribe(this, newIndex);
                pubIndex = newIndex;
                ourACBox.calculateSecondaryAbilityMod();
                return;
            }
            //If moving into N/A
            if (newIndex == 6 && currIndex != 6)
            {
                ourBroker.unSubscribe(this, currIndex);
                //Nothing to sub to
                pubIndex = newIndex;
                setScore(0);
                ourACBox.secondaryAbilityMod = 0;
                return;
            }
            if (newIndex == 6 && currIndex == 6)
            { /*Do NOTHING */ return;  }
            ourBroker.unSubscribe(this, currIndex);
            ourBroker.subscribe(this, newIndex);
            pubIndex = newIndex;
            ourACBox.calculatePrimaryAbilityMod();
        }
    }
}
