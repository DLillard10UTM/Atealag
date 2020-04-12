using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Atealag
{
    /*  Author: Danny Lillard of Atealag Dev Team
     *  Date: 4/11/2020
     *  Description: this file holds the mainWindow backend. Allowing funtionality for:
     *                                                          HP Tracking
     *                                                          Initiative Tracking
     *                                                          Creation of new files.
     */
    class AssetManager
    {
        public HPTracker userHPTrack = new HPTracker();
        public InitTracker userInitTrack = new InitTracker();
    }

    class HPTracker
    {
        public ObservableCollection<HPBar> hpBars { get; set; }

        public HPTracker()
        {
            hpBars = new ObservableCollection<HPBar>();
        }

        public void AddFromButton()
        {
            hpBars.Add(new HPBar(0, 0, "UNNAMED" + hpBars.Count));
        }
        public void RemoveFromButton(int toBeDeleted)
        {
            hpBars.RemoveAt(toBeDeleted);
        }
    }
    class HPBar : INotifyPropertyChanged
    {
        private int _currHP;
        public int currHP
        {
            get{ return _currHP; }    
            set
            {
                if (_currHP != value)
                {
                    _currHP = value;
                    NotifyPropertyChanged("currHP");
                    }
                }
            
        }
        private int _maxHP;
        public int maxHP
        {
            get { return _maxHP; }
            set
            {
                if (_maxHP != value)
                {
                    _maxHP = value;
                    NotifyPropertyChanged("maxHP");
                }
            }

        }

        private string _entityName;
        public string entityName
        {
            get { return _entityName; }
            set
            {
                if (_entityName != value)
                {
                    _entityName = value;
                    NotifyPropertyChanged("entityName");
                }
            }

        }

        public HPBar(int u_currHP, int u_maxHP, string u_entityName)
        {
            currHP = u_currHP;
            maxHP = u_maxHP;
            entityName = u_entityName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class InitTracker
    {
        public ObservableCollection<InitBubble> initBubbles { get; set; }

        public InitTracker()
        {
            initBubbles = new ObservableCollection<InitBubble>();
        }

        public void AddFromButton()
        {
            initBubbles.Add(new InitBubble(0, "UNNAMED" + initBubbles.Count));
        }
        public void RemoveFromButton(int toBeDeleted)
        {
            initBubbles.RemoveAt(toBeDeleted);
        }
    }
    class InitBubble : INotifyPropertyChanged
    {
        private int _init;
        public int init
        {
            get { return _init; }
            set
            {
                if (_init != value)
                {
                    _init = value;
                    NotifyPropertyChanged("init");
                }
            }

        }

        private string _entityName;
        public string entityName
        {
            get { return _entityName; }
            set
            {
                if (_entityName != value)
                {
                    _entityName = value;
                    NotifyPropertyChanged("entityName");
                }
            }

        }

        public InitBubble(int init, string entityName)
        {
            _init = init;
            _entityName = entityName;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
