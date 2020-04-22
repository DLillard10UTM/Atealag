using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*  Author: Danny Lillard of Atealag dev team
 *  Date: 4/14/2020
 *  Description: Basis for the pub and sub design pattern.
 */
namespace Atealag
{
    class Pub
    {
        private int score;
        private Broker pubBroker;
        private int pubIndex;
        public Pub(Broker broker, int index)
        {
            score = 0;
            pubBroker = broker;
            pubIndex = index;
        }


        public void scoreChange(int newScore)
        {
            score = newScore;
            pubBroker.update(pubIndex);
        }
        public int getScore()
        {
            return score;
            
        }
    }

    //Works between the publishers and the subscribers.
    public class Broker
    {
        private List<Pub> abilityScoreList = new List<Pub>();
        private List<List<Sub>> subList = new List<List<Sub>>();

        public Broker()
        {
            for(int i = 0; i < 6; i++)
            {
                abilityScoreList.Add(new Pub(this, i));
                subList.Add(new List<Sub>());
            }
        }

        public void subscribe(Sub subToAdd, int index)
        {
            subList[index].Add(subToAdd);
            subToAdd.setScoreIndex(index);
            subToAdd.setScore(abilityScoreList[index].getScore());
        }

        public void unSubscribe(Sub subToRemove, int index)
        {
            subList[index].Remove(subToRemove);
        }

        //Function goes through all subscribers of a publisher and updates their values.
        public void update(int index)
        {
            int pubScore = abilityScoreList[index].getScore();
            for (int i = 0; i < subList[index].Count(); i++)
            {
                subList[index][i].setScore(pubScore);
            }
        }
        //To update the values for the Pubs
        public void updatePub(int index, int score)
        {
            abilityScoreList[index].scoreChange(score);
        }
    }

    public class Sub
    {
        //the score, eg DEX = 12, this holds 12.
        protected int score;
        //The index they are subscribed to, eg subbed to str = 0.
        protected int pubIndex;
        //The broker they communicate to, should be same for all subs and pubs, we only have one broker.
        private Broker subBroker;

        public Sub(Broker broker)
        {
            subBroker = broker;
        }
        //this function will be called when the drop down menu Ability scores is changed.
        public virtual void changeAbScore(int currIndex, int newIndex)
        {
            subBroker.unSubscribe(this, currIndex);
            subBroker.subscribe(this, newIndex);
            pubIndex = newIndex;
        }
        public virtual void setScore(int newScore)
        {
            score = newScore;
        }
        public int getScore()
        {
            return score;
        }
        //we use this function to set up the comboboxes.
        public int getScoreIndex()
        {
            return pubIndex;
        }
        //For the start of setting drop down menus and subs.
        public void setScoreIndex(int index)
        {
            pubIndex = index;
        }
    }
}
