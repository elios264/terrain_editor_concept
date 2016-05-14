﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TerrainEditor.Utilities
{
    public class SelectManyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (Type)parameter;
            var objects = new List<object>();

            foreach (object value in values.Where(value => value != null))
            {
                if (value.GetType() == type)
                {
                    objects.Add(value);
                }
                else
                {
                    var valueList = value as IEnumerable;

                    if (valueList != null)
                        objects.AddRange(valueList.Cast<object>().Where(o => o != null));
                }
            }

            return objects;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToBooleanConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value == false ? null : Activator.CreateInstance(targetType);
        }
    }
}