using System;
using System.Collections;
using Avalonia.Data.Converters;
namespace VolunteerHub.Models.Converters {
    public class ProjectIsNullConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value is Project) {
                return (value as Project) != null;
            }
            else return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}


