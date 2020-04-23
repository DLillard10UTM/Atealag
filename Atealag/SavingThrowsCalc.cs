using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Atealag
{
    public class SavingThrowsCalc
    {
        public ObservableCollection<int> miscBonuses { get; set; }
        public ObservableCollection<int> savingThrowTotals { get; set; }
        public List<SavingThrowsSub> subs { get; set; }

        public List<int> subsScores { get; set; }

        public List<bool> checkBoxes { get; set; }

        private int profBonus;

        private int _level;
        public int level
        {
            get { return _level; }
            set
            {
                _level = value;
                calculateProfBonus();
            }
        }

        public SavingThrowsCalc()
        {
            miscBonuses = new ObservableCollection<int>();
            subsScores = new List<int>();
            savingThrowTotals = new ObservableCollection<int>();
            checkBoxes = new List<bool>();
            for (int i = 0; i < 6; i++)
            {
                miscBonuses.Add(0);
                subsScores.Add(0);
                savingThrowTotals.Add(0);
                checkBoxes.Add(false);
            }
        }

        //for saved files and make it easy
        public SavingThrowsCalc(List<int> miscBonus, List<bool> isChecked)
        {
            miscBonuses = new ObservableCollection<int>();
            subsScores = new List<int>();
            savingThrowTotals = new ObservableCollection<int>();
            checkBoxes = new List<bool>();
            for (int i = 0; i < 6; i++)
            {
                miscBonuses.Add(miscBonus[i]);
                subsScores.Add(0);
                savingThrowTotals.Add(0);
                checkBoxes.Add(isChecked[i]);
            }
        }
        public void createSubs(Broker broker)
        {
            subs = new List<SavingThrowsSub>();
            for (int i = 0; i < 6; i++)
            {
                subs.Add(new SavingThrowsSub(broker, this));
                broker.subscribe(subs[i], i);
                updateSubScores(i);
            }
        }

        public void updateSubScores(int index)
        {
            subsScores[index] = (subs[index].getScore() - 10) / 2;
            calculateTotalScore(index);
        }

        public void calculateProfBonus()
        {
            profBonus = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(level) / 4) + 1);
        }

        public void calculateTotalScore(int index)
        {
            if (checkBoxes[index])
            {
                savingThrowTotals[index] = subsScores[index] + profBonus + miscBonuses[index];
            }
            else
                savingThrowTotals[index] = subsScores[index] + miscBonuses[index];
        }
    }
    public class SavingThrowsSub : Sub
    {
        Broker ourBroker;
        SavingThrowsCalc ourstc;
        public SavingThrowsSub(Broker broker, SavingThrowsCalc stc) : base(broker)
        {
            ourBroker = broker;
            ourstc = stc;
        }

        public override void setScore(int newScore)
        {
            score = newScore;
            ourstc.updateSubScores(this.pubIndex);
        }
    }
}
