using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    class MainTab
    {
        AbilityScoresDisplay abScoreDisplay = new AbilityScoresDisplay();
    }

    class HealthBox
    {
        HealthBox(int currHP, int maxHP)
        {
            _currHealth = currHP;
            _maxHealth = maxHP;
        }
        private int _currHealth;
        public int currHealth
        {
            get{ return _currHealth; }
            set 
            { 
                _currHealth = value; 
            }
        }
        private int _maxHealth;
        public int maxHealth
        {
            get { return _maxHealth; }
            //We do not want a public set function for maxHealth to protect it.
            //Only the HP calc should be able to effect the _maxHealth.
        }
    }
    class HPCalculator
    {
        private int _baseHealth;
        public int baseHealth
        {
            get { return _baseHealth; }
            set
            {
                _baseHealth = value;
            }
        }

        private int _abilityBonus;
        public int abilityBonus
        {
            get { return _abilityBonus; }
            set
            {
                _abilityBonus = value;
            }
        }

        private int _misc;
        public int misc
        {
            get { return _misc; }
            set
            {
                _misc = value;
            }
        }
    }

    class ACBox
    {

    }
    class ACCalc
    {
        private int _armorBonus;
        public int ArmorBonus
        {
            get { return _armorBonus; }
            set
            {
                if (_armorBonus != value)
                {
                    _armorBonus = value;
                    updateACScore();
                }
            }
        }

        private int _misc;
        public int Misc
        {
            get { return _misc; }
            set
            {
                if (_misc != value)
                {
                    _misc = value;
                    updateACScore();
                }
            }
        }

        void updateACScore()
        {

        }
    }

    class SpeedBox
    {

    }
    class SpeedCalc
    {

    }

    class SensesAndLanguages
    {

    }

    class AbilityScoresDisplay
    {
        
    }

    class SavingThrows
    {

    }
    class SavingThrowsCalc
    {

    }

    class ShortRest
    {

    }
}