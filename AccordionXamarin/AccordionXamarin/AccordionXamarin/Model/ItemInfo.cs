using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AccordionXamarin
{
    public class ItemInfo : INotifyPropertyChanged
    {
        #region Fields
        private string _name;
        private string _description;
        #endregion

        #region Properties
        public ObservableCollection<Variety> Varieties { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }
        #endregion
    }
}
