using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zzb.Utility
{
    public class ConvertHelper
    {
        
        public static List<T2> ConvertToList<T1, T2>(List<T1> source)
        {
            List<T2> t2List = new List<T2>();
            if (source == null || source.Count == 0)
                return t2List;

            T2 model = default(T2);
            PropertyInfo[] pi = typeof(T2).GetProperties();
            PropertyInfo[] pi1 = typeof(T1).GetProperties();
            foreach (T1 t1Model in source)
            {
                model = Activator.CreateInstance<T2>();
                foreach (var p in pi)
                {
                    foreach (var p1 in pi1)
                    {
                        if (p.Name == p1.Name)
                        {
                            p.SetValue(model, p1.GetValue(t1Model, null), null);
                            break;
                        }
                    }
                }
                t2List.Add(model);
            }
            return t2List;
        }

        public static T2 ConvertToModel<T1, T2>(T1 source)
        {
            T2 model = default(T2);
            PropertyInfo[] pi = typeof(T2).GetProperties();
            PropertyInfo[] pi1 = typeof(T1).GetProperties();
            model = Activator.CreateInstance<T2>();
            foreach (var p in pi)
            {
                foreach (var p1 in pi1)
                {
                    if (p.Name == p1.Name)
                    {
                        p.SetValue(model, p1.GetValue(source, null), null);
                        break;
                    }
                }
            }
            return model;
        }
    }
}
