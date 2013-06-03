using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace BattleResultCollector
{
    public class CollectorConfig
    {
        public string Database { get; set; }
        public string CachePath { get; set; }
        public int ScanInterval { get; set; }
        
        public string ConfigFileName { get; set; }
        ///////////////////////////////////////////////////////////////////////////
        public CollectorConfig()
        {
            Database = "";
            CachePath = "";
            ScanInterval = 1000;
        }
        ///////////////////////////////////////////////////////////////////////////
        public static CollectorConfig LoadFromFile(string file)
        {
            CollectorConfig Temp;
            StreamReader sr;

            try
            {
                sr = new StreamReader(file);
                string json = sr.ReadToEnd();
                Temp = JsonConvert.DeserializeObject<CollectorConfig>(json);
                Temp.ConfigFileName = file;
                sr.Close();
            }
            catch (Exception e)
            {                
                Temp = new CollectorConfig();
                Temp.Database = "";
                Temp.CachePath = "";
                Temp.ScanInterval = 5000;
            }

            return Temp;
        }
        ///////////////////////////////////////////////////////////////////////////
        public void Save()
        {
            StreamWriter sw = new StreamWriter(ConfigFileName);
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            sw.Write(json);
            sw.Close();
        }
    }
}
