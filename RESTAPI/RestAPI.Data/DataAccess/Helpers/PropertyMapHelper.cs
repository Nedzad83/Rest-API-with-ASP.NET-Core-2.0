using System;
using System.Data;
using System.Reflection;

namespace RestAPI.Data.Helpers
{
    public class PropertyMapHelper
    {
        public static void Map(DataRow row, PropertyInfo prop, object entity)
        {
            if (!String.IsNullOrWhiteSpace(prop.Name) && row.Table.Columns.Contains(prop.Name))
            {
                var propertyValue = row[prop.Name];
                if (propertyValue != DBNull.Value)
                {
                    ParsePrimitive(prop, entity, row[prop.Name]);
                }
            }
        }

        private static void ParsePrimitive(PropertyInfo prop, object entity, object value)
        {
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(entity, value.ToString().Trim(), null);
            }
            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, int.Parse(value.ToString()), null);
                }
            }
        }
    }
}
