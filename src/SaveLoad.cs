using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kerbal_Mod_Manager
{
    class SaveLoad
    {
        public static void SaveFileXml<T>(T data, string file)
        {
            bool succeed = false;

            while (!succeed)
            {
                try
                {
                    using (FileStream fs = File.Create(file))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(T));
                        xs.Serialize(fs, data);
                    }

                    succeed = true;
                }
                catch
                { }
            }
        }

        public static T LoadFileXml<T>(string file)
        {
            T returnValue;

            try
            {
                using (FileStream fs = File.Open(file, FileMode.Open))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    returnValue = (T)xs.Deserialize(fs);
                }
            }
            catch
            {
                returnValue = default(T);
            }

            return returnValue;
        }

        public static void SaveFileBf<T>(T data, string file)
        {
            bool succeed = false;

            while (!succeed)
            {
                try
                {
                    using (FileStream fs = File.Create(file))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, data);
                    }

                    succeed = true;
                }
                catch
                { }
            }
        }

        public static T LoadFileBf<T>(string file)
        {
            T returnValue;

            try
            {
                using (FileStream fs = File.Open(file, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    returnValue = (T)bf.Deserialize(fs);
                }
            }
            catch
            {
                returnValue = default(T);
            }

            return returnValue;
        }
    }
}
