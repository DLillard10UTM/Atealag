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
    }

    class HealthBox
    {

    }
    class HPCalc
    {
        
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