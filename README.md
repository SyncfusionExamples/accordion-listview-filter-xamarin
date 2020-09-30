# How to search both Accordion (SfAccordion) and ListView (SflistView) in Xamarin.Forms

You can search for both [SfAccordion](https://help.syncfusion.com/xamarin/accordion/getting-started) and [SfListView](https://help.syncfusion.com/xamarin/listview/overview) loaded inside [AccordionItem.Content](https://help.syncfusion.com/cr/xamarin/Syncfusion.XForms.Accordion.AccordionItem.html#Syncfusion_XForms_Accordion_AccordionItem_Content) in Xamarin.Forms. To load the **SfListView** inside **Accordion** , refer to the article from [here](https://www.syncfusion.com/kb/11448/how-to-work-with-accordion-with-sflistview-in-xamarin-forms-sfaccordion).

You can also refer the following article.

https://www.syncfusion.com/kb/11954/how-to-search-both-accordion-sfaccordion-and-listview-sflistview-in-xamarin-forms

**XAML**

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:AccordionXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.XForms.Accordion;assembly=Syncfusion.Expander.XForms"
             xmlns:sfListView="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             xmlns:converter="clr-namespace:AccordionXamarin.Converter"
             xmlns:helper="clr-namespace:AccordionXamarin.Helper"
             x:Class="AccordionXamarin.MainPage" Padding="{OnPlatform iOS='0,40,0,0'}">
...
    <ContentPage.Content>
        <StackLayout VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand"> 
            <SearchBar x:Name="searchBar" HeightRequest="50"/>
            <syncfusion:SfAccordion x:Name="OuterAccordion" ExpandMode="SingleOrNone" Margin="5" BindableLayout.ItemsSource="{Binding Info}">
                <BindableLayout.ItemTemplate>
                    <DataTemplate>
                        <syncfusion:AccordionItem x:Name="AccordionItem">
                            <syncfusion:AccordionItem.Header>
                                <Grid HeightRequest="50" RowSpacing="0">
                                    <Label Text="{Binding Name}" VerticalOptions="Center" HorizontalOptions="StartAndExpand"/>
                                </Grid>
                            </syncfusion:AccordionItem.Header>
                            <syncfusion:AccordionItem.Content>
                                <sfListView:SfListView x:Name="listView" SelectedItem="{Binding SelectedItem}" HeightRequest="{Binding Varieties, Converter={StaticResource HeightConverter}, ConverterParameter={x:Reference listView}}" ItemSize="50" ItemsSource="{Binding Varieties}" ItemSpacing="1">
                                    <sfListView:SfListView.ItemTemplate>
                                        <DataTemplate>
                                            ...
                                        </DataTemplate>
                                    </sfListView:SfListView.ItemTemplate>
                                </sfListView:SfListView>
                            </syncfusion:AccordionItem.Content>
                        </syncfusion:AccordionItem>
                    </DataTemplate>
                </BindableLayout.ItemTemplate>
            </syncfusion:SfAccordion>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```

**C#**

In the [SearchBar.TextChanged](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.inputview.textchanged?view=xamarin-forms) event, filter items based on the [NewTextValue](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.textchangedeventargs.newtextvalue?view=xamarin-forms#Xamarin_Forms_TextChangedEventArgs_NewTextValue) property of [TextChangedEventArgs](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.textchangedeventargs?view=xamarin-forms). You can filter items based on the collection bound to both the Accordion and the ListView. Filtered items set to **Accordion** using the [BindableLayout.SetItemsSource](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.bindablelayout.setitemssource?view=xamarin-forms) method. You can expand the [AccordionItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.XForms.Accordion.AccordionItem.html) to show the searched [ListViewItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.ListViewItem.html) by setting the [IsExpanded](https://help.syncfusion.com/cr/xamarin/Syncfusion.XForms.Accordion.AccordionItem.html#Syncfusion_XForms_Accordion_AccordionItem_IsExpanded) property to **true**.

``` c#
namespace AccordionXamarin.Helper
{
    public class Behavior : Behavior<ContentPage>
    {
        SfAccordion Accordion;
        SearchBar SearchBar;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            Accordion = bindable.FindByName<SfAccordion>("OuterAccordion");
            SearchBar = bindable.FindByName<SearchBar>("searchBar");
            SearchBar.TextChanged += SearchBar_TextChanged;
            
            base.OnAttachedTo(bindable);
        }

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
                        BindableLayout.SetItemsSource(Accordion, list);
                        if (!Accordion.Items[0].IsExpanded)
                            Accordion.Items[0].IsExpanded = true;
                        return;
                    }
                }
            }
            BindableLayout.SetItemsSource(Accordion, filteredSource);
        }
    }
}
```

**Output**

![AccordionListViewFilter](https://github.com/SyncfusionExamples/accordion-listview-filter-xamarin/blob/master/ScreenShot/AccordionListViewFilter.gif)
