using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace ReplayReaper
{
    public class ReplayInfo
    {        
        private char[] _rawdata3;

        private CommonInfo _BeforeBattle;
        private CommonInfo _AfterBattle;
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public ReplayInfo()
        {
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public CommonInfo BeforeBattle 
        {
            get
            {
                return _BeforeBattle;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public CommonInfo AfterBattle
        {
            get
            {
                return _AfterBattle;
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public void ReadDataFromFile(string path)
        {
            byte[] ID = new byte[8];
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            ID = br.ReadBytes(8);
            UInt32 size = br.ReadUInt32();
            char[] _rawdata1 = new char[size];
            br.Read(_rawdata1, 0, (int)size);
            _BeforeBattle = JsonConvert.DeserializeObject<CommonInfo>(new String(_rawdata1));

            size = br.ReadUInt32();
            char[]  _rawdata2 = new char[size];
            br.Read(_rawdata2, 0, (int)size);
            List<JObject> O = JsonConvert.DeserializeObject<List<JObject>>(new String(_rawdata2));
            
            PersonalInfo                   T  = O[0].ToObject<PersonalInfo>();
            Dictionary<string, PlayerInfo> T2 = O[1].ToObject<Dictionary<string, PlayerInfo>>();
            Dictionary<string, KillInfo>   T3 = O[2].ToObject<Dictionary<string, KillInfo>>();
            
            size = br.ReadUInt32();
            _rawdata3 = new char[size];
            br.Read(_rawdata3, 0, (int)size);
            br.Close();
            fs.Close();
        }
    }


    public class CommonInfo
    {
        public string mapName { get; set;}
        public string playerID { get; set;}
        public string gameplayID { get; set;}
        public string playerName { get; set;}
        public Dictionary<string, PlayerInfo> vehicles { get; set; }
        public string mapDisplayName { get; set; }
        public string dateTime { get; set;}
    }
    
    public class PlayerInfo
    {
        public string vehicleType { get; set;}
        public bool isAlive { get; set;}
        public string name { get; set;}
        public string clanAbbrev { get; set;}
        public int team { get; set;}
        //\"events\": {}, 
        public bool isTeamKiller { get; set; }
    }

    public class KillInfo
    {
        public int frags { get; set; }
    }


    public class PersonalInfo
    {
        //achieveIndices
        public int arenaCreateTime { get; set; }
        public int arenaTypeID { get; set; }
        public int capturePoints { get; set; }
        public int credits { get; set; }
        //damaged
        public int damageDealt { get; set; }
        public int damageReceived { get; set; }
        public int droppedCapturePoints { get; set; }
        //factors
        //heroVehicleIDs
        public int hits { get; set; }
        public int isWinner { get; set; }
        //killed
        public int killerID { get; set; }
        public int repair { get; set; }
        public int shots { get; set; }
        public int shotsReceived { get; set; }
        //public int spotted { get; set; }
        public int xp { get; set; }
    }



    public class CommonInfo3
    {
        public int vehLockMode { get; set; }
        public int finishReason { get; set; }
        public int guiType { get; set; }
        public int arenaTypeID { get; set; }
        public int bonusType { get; set; }
        public int winnerTeam { get; set; }
        public int arenaCreateTime { get; set; }
        public float duration { get; set; }
    }
}