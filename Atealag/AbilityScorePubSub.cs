using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    class Pub
    {
        private int score;

        public Pub()
        {
            score = 0;
        }


        public void scoreChange(int newScore)
        {
            score = newScore;
        }
        public int getScore()
        {
            return score;
        }
    }

    class Broker
    {
        private List<Pub> abilityScoreList;
        private List<List<Sub>> subList = new List<List<Sub>>();

        Broker()
        {
            for(int i = 0; i < 6; i++)
            {
                abilityScoreList.Add(new Pub());
                subList.Add(new List<Sub>());
            }
        }

        public void subscribe(Sub subToAdd, int index)
        {
            subList[index].Add(subToAdd);
        }

        public void unSubscribe(Sub subToRemove, int index)
        {
            subList[index].Remove(subToRemove);
        }

        public void update()
        {

        }
    }

    class Sub
    {
        private int score;
        private void changeAbScore(int currIndex, int newIndex)
        {

        }
        public void setScore(int newScore)
        {
            score = newScore;
        }

    }
}
