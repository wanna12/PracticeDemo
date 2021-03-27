using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Common.util
{
    public class ExtensionHelper
    {
        /**
         * 将sourceObj赋值给targetObj
         */
        public static void CopyPropertiesTo(object sourceObj, object targetObj, bool ignoreCase)
        {
           FieldInfo[] sourceFieldArray = sourceObj.GetType().GetFields();
           FieldInfo[] targetFieldArray = targetObj.GetType().GetFields();

           foreach (FieldInfo src in sourceFieldArray)
           {
               string srcName = src.Name;
               object srcValue = src.GetValue(sourceObj);
               foreach (FieldInfo tar in targetFieldArray)
               {
                   string tarName = tar.Name;
                   
                   if (ignoreCase)
                   {
                       srcName = srcName.ToLower();
                       tarName = tarName.ToLower();
                   }

                   if (srcName.Equals(tarName))
                   {
                       tar.SetValue(targetObj,srcValue);
                       break;
                   }
               }
           }
        }

        public static byte[] ObjectToByteArray<T>(T obj)
        {
            BinaryFormatter bf=new BinaryFormatter();
            MemoryStream stream=new MemoryStream();
            bf.Serialize(stream,obj);
            byte[] data = stream.ToArray();
            stream.Dispose();
            return data;
        }
    }
}
