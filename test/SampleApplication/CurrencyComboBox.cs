using IndependentReserve.DotNetClientApi.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace SampleApplication
{
    public class CurrencyComboBox : ComboBox
    {
        public CurrencyComboBox()
        {
            var currencyValues = ((CurrencyCode[])Enum.GetValues(typeof(CurrencyCode))).ToList();
            currencyValues.Remove(CurrencyCode.Unknown);
            currencyValues.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

            var list = new ObservableCollection<string>();
            currencyValues.ForEach(c => list.Add(c.ToString()));

            ItemsSource = list;
        }
    }
}
