using CurtLog;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace CoDUO_FoV_Changer
{
    class DatabaseFile
    {
        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static bool Write<T>(T objectToWrite, string fileName, bool append = false) where T : new()
        {
            try
            {
                var serializer = new DataContractSerializer(typeof(T));
                XmlDocument xmlDocument = new XmlDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, objectToWrite);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (Log.IsInitialized) Log.WriteLine(" * [" + ex.TargetSite.Name + "] " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T Read<T>(string filePath) where T : new()
        {
            T objectOut = default(T);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    DataContractSerializer serializer = new DataContractSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.ReadObject(reader);
                        reader.Close();
                    }
                    read.Close();
                }
                return objectOut;
                //T result;
                //var serializer = new DataContractSerializer(typeof(T));
                //using (FileStream reader = new FileStream(filePath, FileMode.Open))
                //{
                //    result = (T)serializer.ReadObject(reader);
                //}
                //return result;
            }
            catch (Exception ex)
            {

                //     Console.WriteLine(" * [" + ex.TargetSite.Name + "] " + ex.Message);
                if (Log.IsInitialized) Log.WriteLine(" * [" + ex.TargetSite.Name + "] " + ex.Message);
                return new T();
            }
        }
    }
}
