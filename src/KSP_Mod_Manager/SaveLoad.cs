﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace KSP_Mod_Manager
{
    class SaveLoad
    {
        public static void SaveFileXml<T>(T data, string file)
        {
            FileStream fs = File.Create(file);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(fs, data);
            fs.Close();
        }

        public static T LoadFileXml<T>(string file)
        {
            T returnValue;

            try
            {
                FileStream fs = File.Open(file, FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(T));

                returnValue = (T) xs.Deserialize(fs);
                fs.Close();
            }
            catch
            {
                returnValue = default(T);
            }

            return returnValue;
        }
    }
}
