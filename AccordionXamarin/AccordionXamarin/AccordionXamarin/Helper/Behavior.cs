using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.XForms.Accordion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace AccordionXamarin.Helper
{
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfAccordion Accordion;
        SearchBar SearchBar;
        #endregion

        #region Overrides

        protected override void OnAttachedTo(ContentPage bindable)
        {
            Accordion = bindable.FindByName<SfAccordion>("OuterAccordion");
            SearchBar = bindable.FindByName<SearchBar>("searchBar");
            SearchBar.TextChanged += SearchBar_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            SearchBar.TextChanged -= SearchBar_TextChanged;
            Accordion = null;
            SearchBar = null;

            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region CallBack

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var viewModel = new ItemInfoRepository(); 
            var accordionItems = viewModel.Info;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                BindableLayout.SetItemsSource(Accordion, accordionItems);
                return;
            }

            var filteredSource = accordionItems.Where(x => (x.Name.ToLower()).StartsWith(e.NewTextValue.ToLower())).ToList<ItemInfo>();
            if(filteredSource.Count == 0)
            {
                List<Variety> listfilteredSource = null;
                for (int i = 0; i < accordionItems.Count; i++)
                {
                    var item = accordionItems[i];
                    listfilteredSource = item.Varieties.Where(x => (x.Name.ToLower()).StartsWith(e.NewTextValue.ToLower())).ToList<Variety>();
                    if (listfilteredSource.Count > 0)
                    {
                        var list = accordionItems.Where(x => x.Name.Contains(item.Name.ToString()));
                        item.Varieties = new System.Collections.ObjectModel.ObservableCollection<Variety>(listfilteredSource);
                        BindableLayout.SetItemsSource(Accordion, list);
                        if (!Accordion.Items[0].IsExpanded)
                            Accordion.Items[0].IsExpanded = true;
                        return;
                    }
                }
            }
            BindableLayout.SetItemsSource(Accordion, filteredSource);
        }
        #endregion
    }
}
