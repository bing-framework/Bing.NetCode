﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

// ReSharper disable once CheckNamespace
namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 对象(<see cref="object"/>) 扩展
    /// </summary>
    public static class ObjectExtensions
    {
        #region DeepClone(对象深拷贝)

        /// <summary>
        /// 对象深度拷贝，复制相同数据，但指向内存位置不一样的数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">值</param>
        /// <returns></returns>
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
            {
                return default(T);
            }

            if (!typeof(T).HasAttribute<SerializableAttribute>(true))
            {
                throw new NotSupportedException($"当前对象未标记特性“{typeof(SerializableAttribute)}”，无法进行DeepClone操作");
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(ms);
            }
        }

        #endregion

        #region PropertyClone 属性克隆

        /// <summary>
        /// 从源对象赋值到当前对象
        /// </summary>
        /// <param name="destination">当前对象</param>
        /// <param name="source">源对象</param>
        /// <returns>成功复制的值个数</returns>
        public static int ClonePropertyFrom(this object destination, object source)
        {
            return ClonePropertyFrom(destination, source, null);
        }

        /// <summary>
        /// 从源对象赋值到当前对象
        /// </summary>
        /// <param name="destination">当前对象</param>
        /// <param name="source">源对象</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int ClonePropertyFrom(this object destination, object source, IEnumerable<string> excludeName)
        {
            if (destination == null || source == null)
            {
                return 0;
            }
            return destination.ClonePropertyFrom(source, source.GetType(), excludeName);
        }

        /// <summary>
        /// 复制属性值
        /// </summary>
        /// <param name="this">当前对象</param>
        /// <param name="source">属性值来源对象</param>
        /// <param name="type">复制的属性字段模板</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int ClonePropertyFrom(this object @this, object source, Type type, IEnumerable<string> excludeName)
        {
            if (@this == null || source == null)
            {
                return 0;
            }

            if (excludeName == null)
            {
                excludeName = new List<string>();
            }

            int i = 0;
            var desType = @this.GetType();
            foreach (var mi in type.GetFields())
            {
                if (excludeName.Contains(mi.Name))
                {
                    continue;
                }
                try
                {
                    var des = desType.GetField(mi.Name);
                    if (des != null && des.FieldType == mi.FieldType)
                    {
                        des.SetValue(@this, mi.GetValue(source));
                        i++;
                    }
                }
                catch
                {
                }
            }

            foreach (var pi in type.GetProperties())
            {
                if (excludeName.Contains(pi.Name))
                {
                    continue;
                }
                try
                {
                    var des = desType.GetProperty(pi.Name);
                    if (des != null && des.PropertyType == pi.PropertyType && des.CanWrite && pi.CanRead)
                    {
                        des.SetValue(@this, pi.GetValue(source, null), null);
                        i++;
                    }
                }
                catch
                {
                }
            }
            return i;
        }

        /// <summary>
        /// 从当前对象赋值到目标对象
        /// </summary>
        /// <param name="source">当前对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>成功复制的值个数</returns>
        public static int ClonePropertyTo(this object source, object destination)
        {
            return ClonePropertyTo(destination, source, null);
        }

        /// <summary>
        /// 从当前对象赋值到目标对象
        /// </summary>
        /// <param name="source">当前对象</param>
        /// <param name="destination">目标对象</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int ClonePropertyTo(this object source, object destination, IEnumerable<string> excludeName)
        {
            if (destination == null || source == null)
            {
                return 0;
            }
            return destination.ClonePropertyFrom(source, source.GetType(), excludeName);
        }

        #endregion

        #region ToDynamic(将对象转换为dynamic)

        ///// <summary>
        ///// 将对象[主要是匿名对象]转换为dynamic
        ///// </summary>
        ///// <param name="value">值</param>
        ///// <returns></returns>
        //public static dynamic ToDynamic(this object value)
        //{
        //    IDictionary<string,object> expando=new ExpandoObject();
        //    Type type = value.GetType();
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
        //    foreach (PropertyDescriptor property in properties)
        //    {
        //        var val = property.GetValue(value);
        //        if (property.PropertyType.FullName != null &&
        //            property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
        //        {
        //            dynamic dval = val.ToDynamic();
        //            expando.Add(property.Name,dval);
        //        }
        //        else
        //        {
        //            expando.Add(property.Name, val);
        //        }
        //    }

        //    return (ExpandoObject) expando;
        //}

        #endregion

        #region ToNullable(将指定值转换为对应的可空类型)

        /// <summary>
        /// 将指定值转换为对应的可空类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static T? ToNullable<T>(this T value) where T : struct
        {
            return value.IsNull() ? null : (T?)value;
        }

        #endregion
    }
}
