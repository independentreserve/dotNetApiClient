using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using IndependentReserve.DotNetClientApi.Data;

namespace SampleApplication
{
    public class TimeInForceComboBox : ComboBox
    {
        public TimeInForceComboBox()
        {
            var data = Enum.GetValues(typeof(TimeInForce))
                .Cast<TimeInForce>()
                .Where(t => t != TimeInForce.None)
                .ToDictionary(t => t, GetDescription);

            var source = new ObservableCollection<KeyValuePair<TimeInForce, string>>(data);
            ItemsSource = source;
            SelectedValuePath = "Key";
            DisplayMemberPath = "Value";
        }

        private string GetDescription(TimeInForce value)
        {
            var attr = typeof(TimeInForce).GetField(value.ToString()).GetCustomAttribute(typeof(DescriptionAttribute));
            if (attr is DescriptionAttribute description)
            {
                return description.Description;
            }

            return value.ToString();
        }
    }
}