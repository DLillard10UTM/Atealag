using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    public class InitCalc : INotifyPropertyChanged
    {
        private int _totalInit;
        public int totalInit
        {
            get { return _totalInit; }
            set
            {
                _totalInit = value;
                NotifyPropertyChanged("totalInit");
            }
        }

        private int _miscBonus;
        public int miscBonus
        {
            get { return _miscBonus; }
            set
            {
                _miscBonus = value;
                calculateTotalInit();
                NotifyPropertyChanged("miscBonus");
            }
        }

        private int _abilityBonus;
        public int abilityBonus
        {
            get { return _abilityBonus; }
            set
            {
                _abilityBonus = value;
                calculateTotalInit();
                NotifyPropertyChanged("abilityBonus");
            }
        }

        public InitSub initSub;

        public InitCalc()
        {
            miscBonus = 0;
        }
        //for files.
        public InitCalc(int m)
        {
            miscBonus = m;
        }
        public void calculateAbilityBonus()
        {
            abilityBonus = (initSub.getScore() - 10) / 2;
        }
        public void calculateTotalInit()
        {
            totalInit = abilityBonus + miscBonus;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InitSub : Sub
    {
        public Broker ourBroker;
        public InitCalc ourCalc;
        public InitSub(Broker broker, InitCalc init) : base(broker)
        {
            ourBroker = broker;
            ourCalc = init;
        }

        public override void setScore(int newScore)
        {
            score = newScore;
            ourCalc.calculateAbilityBonus();
        }
    }
}
