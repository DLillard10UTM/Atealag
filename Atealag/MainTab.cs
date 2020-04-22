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
        public MainTab()
        {
            hpDisplay = new HealthBox(0,0);
            acDisplay = new ACBox();
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