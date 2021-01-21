using IndependentReserve.DotNetClientApi.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
