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
        //for make it easy and load.
        public MainTab(string currhp, string basehp, string mischp, string arAc, string miscac, string bspd, string mspd,
                        List<int> miscSaves, List<bool> isCheckSaves, string initm)
        {
            hpDisplay = new HealthBox(Convert.ToInt32(currhp), Convert.ToInt32(basehp), Convert.ToInt32(mischp));
            acDisplay = new ACBox(Convert.ToInt32(arAc), Convert.ToInt32(miscac));
            speedDisplay = new SpeedBox(Convert.ToInt32(bspd), Convert.ToInt32(mspd));
            savingThrowsDisplay = new SavingThrowsCalc(miscSaves, isCheckSaves);
            initCalcDisplay = new InitCalc(Convert.ToInt32(initm));
        }
    }
}