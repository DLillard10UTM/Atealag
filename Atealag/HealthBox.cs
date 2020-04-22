using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    /*  Author: Danny Lillard of Aetalag dev team.
     *  Date: 4/20/2020
     *  Description: A class to calculate max hp and set current hp.
     */
    public class HealthBox : INotifyPropertyChanged
    {
        private int _currHealth;
        public int currHealth
        {
            get { return _currHealth; }
            set
            {
                _currHealth = value;
            }
        }
        private int _maxHealth;
        public int maxHealth
        {
            get { return _maxHealth; }
            set
            {
                _maxHealth = value;
                NotifyPropertyChanged("maxHealth");
            }
        }


        //From here on out the values are for HP calc.
        private int _baseHealth;
        public int baseHealth
        {
            get { return _baseHealth; }
            set
            {
                _baseHealth = value;
                NotifyPropertyChanged("baseHealth");
                maxHealth = calculateHP();
            }
        }

        private int _abilityBonus; 
        public int abilityBonus
        {
            get
            {
                return _abilityBonus;
            }
            set
            {
                _abilityBonus = value;
                NotifyPropertyChanged("abilityBonus");
            }
        }
        //Ability bonus is calculated by:
        //this int is effected by the pubsub, it is (ability score - 10) / 2.
        private int _abilityMod;
        public int abilityMod
        {
            get { return _abilityMod; }
            set
            {
                _abilityMod = value;
                maxHealth = calculateHP();
            }
        }

        private int _level;
        public int level
        {
            get { return _level; }
            set
            {
                _level = value;
                maxHealth = calculateHP();
            }
        }
        private int _misc;
        public int misc
        {
            get { return _misc; }
            set
            {
                _misc = value;
                NotifyPropertyChanged("misc");
                maxHealth = calculateHP();
            }
        }

        public HPSub HPCalcCouple;

        public HealthBox(int currHP, int maxHP)
        {
            currHealth = currHP;
            maxHealth = maxHP;
        }
        public void CalculateAbilityMod()
        {
            abilityMod = (HPCalcCouple.getScore() - 10) / 2;
        }
        //used to calculate ability bonus and maxhealth, called anytime basehealth, level, or misc is changed.
        int calculateHP()
        {
            abilityBonus = abilityMod * level;
            return baseHealth + (abilityMod * level) + misc;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //We need this class so ability mod can be dynamically updated.
    public class HPSub : Sub
    {
        Broker ourBroker;
        HealthBox ourHealthBox;
        public HPSub(Broker broker, HealthBox hpBoxToCoupleTo) : base(broker)
        {
            ourBroker = broker;
            ourHealthBox = hpBoxToCoupleTo;
        }
        public override void setScore(int newScore)
        {
            score = newScore;
            ourHealthBox.CalculateAbilityMod();
        }

        public override void changeAbScore(int currIndex, int newIndex)
        {
            ourBroker.unSubscribe(this, currIndex);
            ourBroker.subscribe(this, newIndex);
            pubIndex = newIndex;
            ourHealthBox.CalculateAbilityMod();
        }
    }
}
