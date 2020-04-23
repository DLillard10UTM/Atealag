using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    class MainTab
    {
        public HealthBox hpDisplay;
        public ACBox acDisplay;
        public SpeedBox speedDisplay;
        public SavingThrowsCalc savingThrowsDisplay;
        public InitCalc initCalcDisplay;
        public MainTab()
        {
            hpDisplay = new HealthBox(0,0);
            acDisplay = new ACBox();
            speedDisplay = new SpeedBox();
            savingThrowsDisplay = new SavingThrowsCalc();
            initCalcDisplay = new InitCalc();
        }
    }

    class SensesAndLanguages
    {

    }

    class ShortRest
    {

    }
}