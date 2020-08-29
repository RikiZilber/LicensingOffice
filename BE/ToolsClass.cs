using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BE
{
    public static class ToolsClass
    {
        // static generic function wich show all the properties for each class
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if ((item.Name != "ImageSource") && (item.Name != "AvailabilityTester") && (item.Name != "Criteria")&& (item.Name !=  "matrix_availability"))
                {
                    str += insertSpaces(item.Name) + ": " + item.GetValue(t, null) + "\n";
                }
            }
            return str;
        }

        // insert spaces into properties names
        public static string insertSpaces(string str)
        {
            for (int i = 1; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    str = str.Substring(0, i) + ' ' + str.Substring(i++);
                }
            }
            return str;
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static T GetCopy<T>(this T source) where T : new()
        {
            T result = new T();
            foreach (PropertyInfo item in source.GetType().GetProperties())
            {
                try
                {
                    if (item.CanWrite && item.CanRead) // (item.PropertyType.IsValueType)
                    {
                        object srcValue = item.GetValue(source, null);
                        item.SetValue(result, srcValue);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($" --->> err copy property {item.Name} from {source.GetType().Name} \n {e.Message}");
                }
            }
            return result;
        }

    }
}
