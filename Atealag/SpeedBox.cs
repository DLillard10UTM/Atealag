using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atealag
{
    public class SpeedBox : INotifyPropertyChanged
    {

        private int _totalSpeed;
        public int totalSpeed
        {
            get
            {
                return _totalSpeed;
            }
            set
            {
                _totalSpeed = value;
                NotifyPropertyChanged("totalSpeed");
            }
        }
        private int _baseSpeed;
        public int baseSpeed
        {
            get
            {
                return _baseSpeed;
            }
            set
            {
                _baseSpeed = value;
                NotifyPropertyChanged("baseSpeed");
                totalSpeed = calculateTotalSpeed();
            }
        }
        private int _miscSpeed;
        public int miscSpeed
        {
            get
            {
                return _miscSpeed;
            }
            set
            {
                _miscSpeed = value;
                NotifyPropertyChanged("miscSpeed");
                totalSpeed = calculateTotalSpeed();
            }
        }

        public SpeedBox() 
        {
            baseSpeed = 0;
            miscSpeed = 0;
        }

        //for file and Make it easy.
        public SpeedBox(int bs, int sm)
        {
            baseSpeed = bs;
            miscSpeed = sm;
        }
        int calculateTotalSpeed()
        {
            return baseSpeed + miscSpeed;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
