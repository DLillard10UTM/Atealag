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
    class AssetManager
    {
        public HPTracker userHPTrack = new HPTracker();
    }

    class HPTracker
    {
        private static readonly object ItemsLock = new object();
        public ObservableCollection<HPBar> hpBars { get; set; }
        private ObservableCollection<HPBar> hpBars1 { get; set; }

        public HPTracker()
        {
            hpBars = new ObservableCollection<HPBar>();
            hpBars.Add(new HPBar(0,0,"test"));

            hpBars1 = new ObservableCollection<HPBar>();
            hpBars1.Add(new HPBar(0, 0, "bruh"));
            hpBars1.Add(new HPBar(0, 0, "bruh1"));

            BindingOperations.EnableCollectionSynchronization(hpBars, ItemsLock);
        }

        public void AddFromButton()
        {
            LoadHPList(hpBars1);
        }

        private void LoadHPList(IEnumerable<HPBar> newhpBars)
        {
            hpBars.Clear();
            foreach (var HPBar in newhpBars)
            {
                hpBars.Add(HPBar);
            }
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    class InitativeTracker
    {

    }
    class InitBubble
    {

    }
}
