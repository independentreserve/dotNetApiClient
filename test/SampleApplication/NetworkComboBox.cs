using IndependentReserve.DotNetClientApi.Data;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;
using System.Reflection;

namespace SampleApplication
{
    public class NetworkComboBox : ComboBox
    {
        public NetworkComboBox()
        {
            var networks = typeof(BlockchainNetwork)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .Select(f => f.GetRawConstantValue())
                .OfType<string>()
                .OrderBy(n => n)
                .ToList();

            var list = new ObservableCollection<string>();
            networks.ForEach(c => list.Add(c));

            ItemsSource = list;
        }
    }
}
