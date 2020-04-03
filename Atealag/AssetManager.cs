using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    class AssetManager
    {
    }

    class HPTracker 
    {
        Dictionary<string, HPBar> hpBars = new Dictionary<string, HPBar>();

        void Add()
        {
            hpBars.Add("UNNAMED" + hpBars.Count, new HPBar());
        }
    }
    class HPBar
    {
        int currHP;
        int maxHP;
        string entityName;

        HPBar(int currHP, int maxHP, string entityName)
        {

        }
        public HPBar()
        {
            currHP = 0;
            maxHP = 0;
            entityName = "[ENTER NAME]";
        }
    }

    class InitativeTracker
    {

    }
    class InitBubble
    {

    }
}
