using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    class CharVals : INotifyPropertyChanged
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("name");
                }
            }
        }
        private string _race;
        public string race
        {
            get { return _race; }
            set
            {
                if (_race != value)
                {
                    _race = value;
                    NotifyPropertyChanged("race");
                }
            }
        }
        private string _u_class;
        public string u_class
        {
            get { return _u_class; }
            set
            {
                if (_u_class != value)
                {
                    _u_class = value;
                    NotifyPropertyChanged("u_class");
                }
            }
        }
        private string _sClass;
        public string sClass
        {
            get { return _sClass; }
            set
            {
                if (_sClass != value)
                {
                    _sClass = value;
                    NotifyPropertyChanged("sClass");
                }
            }
        }
        private string _BG;
        public string BG
        {
            get { return _BG; }
            set
            {
                if (_BG != value)
                {
                    _BG = value;
                    NotifyPropertyChanged("BG");
                }
            }
        }
        private string _alig;
        public string alig
        {
            get { return _alig; }
            set
            {
                if (_alig != value)
                {
                    _alig = value;
                    NotifyPropertyChanged("alig");
                }
            }
        }
        private int _level;
        public int level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    NotifyPropertyChanged("level");
                }
            }
        }
        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyPropertyChanged("userName");
                }
            }
        }

        //We cannot put these values inside of an array or any kind of struct for binding reasons.
        private int _strBase; private int _intelBase; private int _dexBase;
        private int _wisBase; private int _conBase; private int _chaBase;

        private int _strMisc; private int _intelMisc; private int _dexMisc;
        private int _wisMisc; private int _conMisc; private int _chaMisc;

        private int _str; private int _intel; private int _dex;
        private int _wis; private int _con; private int _cha;
        public int strBase
        {
            get
            {
                return _strBase;
            }
            set
            {
                _strBase = value;
                NotifyPropertyChanged("strBase");
                updateTotalStrScoreVal();
            }
        }
        public int intelBase
        {
            get
            {
                return _intelBase;
            }
            set
            {
                _intelBase = value;
                NotifyPropertyChanged("intelBase");
                updateTotalIntelScoreVal();
            }
        }
        public int dexBase
        {
            get
            {
                return _dexBase;
            }
            set
            {
                _dexBase = value;
                NotifyPropertyChanged("dexBase");
                updateTotalDexScoreVal();
            }
        }
        public int wisBase
        {
            get
            {
                return _wisBase;
            }
            set
            {
                _wisBase = value;
                NotifyPropertyChanged("wisBase");
                updateTotalWisScoreVal();
            }
        }
        public int conBase
        {
            get
            {
                return _conBase;
            }
            set
            {
                _conBase = value;
                NotifyPropertyChanged("conBase");
                updateTotalConScoreVal();
            }
        }
        public int chaBase
        {
            get
            {
                return _chaBase;
            }
            set
            {
                _chaBase = value;
                NotifyPropertyChanged("chaBase");
                updateTotalChaScoreVal();
            }
        }

        public int strMisc
        {
            get
            {
                return _strMisc;
            }
            set
            {
                _strMisc = value;
                NotifyPropertyChanged("strMisc");
                updateTotalStrScoreVal();
            }
        }
        public int intelMisc
        {
            get
            {
                return _intelMisc;
            }
            set
            {
                _intelMisc = value;
                NotifyPropertyChanged("intelMisc");
                updateTotalIntelScoreVal();
            }
        }
        public int dexMisc
        {
            get
            {
                return _dexMisc;
            }
            set
            {
                _dexMisc = value;
                NotifyPropertyChanged("dexMisc");
                updateTotalDexScoreVal();
            }
        }
        public int wisMisc
        {
            get
            {
                return _wisMisc;
            }
            set
            {
                _wisMisc = value;
                NotifyPropertyChanged("wisMisc");
                updateTotalWisScoreVal();
            }
        }
        public int conMisc
        {
            get
            {
                return _conMisc;
            }
            set
            {
                _conMisc = value;
                NotifyPropertyChanged("conMisc");
                updateTotalConScoreVal();
            }
        }
        public int chaMisc
        {
            get
            {
                return _chaMisc;
            }
            set
            {
                _chaMisc = value;
                NotifyPropertyChanged("chaMisc");
                updateTotalChaScoreVal();
            }
        }
        public List<string> skillProfs;

        private void updateTotalStrScoreVal()
        {
            _str = _strBase + _strMisc;
        }
        private void updateTotalIntelScoreVal()
        {
            _intel = _intelBase + _intelMisc;
        }
        private void updateTotalDexScoreVal()
        {
            _dex = _dexBase + _dexMisc;
        }
        private void updateTotalWisScoreVal()
        {
            _wis = _wisBase + _wisMisc;
        }
        private void updateTotalConScoreVal()
        {
            _con = _conBase + _conMisc;
        }
        private void updateTotalChaScoreVal()
        {
            _cha = _chaBase + _chaMisc;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
