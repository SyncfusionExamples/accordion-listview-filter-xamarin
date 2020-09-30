using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AccordionXamarin
{
    public class ItemInfoRepository
    {
        #region Properties
        public ObservableCollection<ItemInfo> Info { get; set; }
        #endregion

        #region Constructor
        public ItemInfoRepository()
        {
            Info = new ObservableCollection<ItemInfo>();
            Info.Add(new ItemInfo() { Name = "Veg Extravaganza", Varieties = new ObservableCollection<Variety>() { new Variety() { Name = "GodFather", Price = 300 }, new Variety() { Name = "Supreme", Price = 260 }, new Variety() { Name = "Ciao-ciao", Price = 500 }, new Variety() { Name = "Frutti di mare", Price = 600 } } });
            Info.Add(new ItemInfo() { Name = "Deluxe Veggie", Varieties = new ObservableCollection<Variety>() { new Variety() { Name = "Kebabpizza", Price = 320 }, new Variety() { Name = "Napolitana", Price = 310 }, new Variety() { Name = "Apricot Chicken", Price = 480 } }});
            Info.Add(new ItemInfo() { Name = "Peppy Panner", Varieties = new ObservableCollection<Variety>() { new Variety() { Name = "Lamb Tzatziki", Price = 280 }, new Variety() { Name = "Mr Wedge", Price = 270 }} });
            Info.Add(new ItemInfo() { Name = "Mexican Green Wave", Varieties = new ObservableCollection<Variety>() { new Variety() { Name = "Margherita", Price = 330 }, new Variety() { Name = "Funghi", Price = 340 }, new Variety() { Name = "Capriciosa", Price = 550 } } });
        }
        #endregion
    }
}