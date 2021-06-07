using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Zzb.Utility
{
    public class ConvertHelper
    {

        public static IList<T2> ConvertToList<T1, T2>(IList<T1> source)
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
                    if (!p.PropertyType.IsGenericType)
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
                if (!p.PropertyType.IsGenericType)
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
            }
            return model;
        }

        public static T2 ConvertToModel<T1, T2>(T1 sourceEntity, IList<string> targetFieldList)
        {
            T2 targetEntity = default(T2);
            IList<PropertyInfo> targetPIList = typeof(T2).GetProperties();
            IList<PropertyInfo> sourcePIList = typeof(T1).GetProperties();
            targetEntity = Activator.CreateInstance<T2>();
            for (int i = 0; i < targetFieldList.Count; i++)
            {
                PropertyInfo targetPI = targetPIList.FirstOrDefault(p => string.Equals(p.Name, targetFieldList[i], StringComparison.OrdinalIgnoreCase));
                PropertyInfo sourcePI = sourcePIList.FirstOrDefault(p => string.Equals(p.Name, targetFieldList[i], StringComparison.OrdinalIgnoreCase));
                if (targetPI != null && sourcePI != null)
                {
                    if (!sourcePI.PropertyType.IsGenericType&& !targetPI.PropertyType.IsGenericType)
                    {
                        targetFieldList[i] = targetPI.Name;
                        targetPI.SetValue(targetEntity, sourcePI.GetValue(sourceEntity, null), null);
                    }
                }
                else
                {
                    targetFieldList.RemoveAt(i--);
                }
            }
            return targetEntity;
        }

        public static void ModelCopyValue<T1, T2>(IList<string> targetFieldList,T1 sourceEntity,ref T2 targetEntity)
        {

            IList<PropertyInfo> targetPIList = typeof(T2).GetProperties();
            IList<PropertyInfo> sourcePIList = typeof(T1).GetProperties();
            targetEntity = Activator.CreateInstance<T2>();
            for (int i = 0; i < targetFieldList.Count; i++)
            {
                PropertyInfo targetPI = targetPIList.FirstOrDefault(p => string.Equals(p.Name, targetFieldList[i], StringComparison.OrdinalIgnoreCase));
                PropertyInfo sourcePI = sourcePIList.FirstOrDefault(p => string.Equals(p.Name, targetFieldList[i], StringComparison.OrdinalIgnoreCase));
                if (targetPI != null && sourcePI != null)
                {
                    if (!sourcePI.PropertyType.IsGenericType && !targetPI.PropertyType.IsGenericType)
                    {
                        targetFieldList[i] = targetPI.Name;
                        targetPI.SetValue(targetEntity, sourcePI.GetValue(sourceEntity, null), null);
                    }
                }
                else
                {
                    targetFieldList.RemoveAt(i--);
                }
            }
        }
    }
}
