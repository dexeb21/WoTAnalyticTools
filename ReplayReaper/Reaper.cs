using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ReplayReaper
{
    public class Reaper
    {
        private char[] _rawdata1;
        private char[] _rawdata2;
        private char[] _rawdata3;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        public Reaper()
        {
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private void ReadDataFromFile(string path)
        {
            byte[] ID = new byte[8];
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            ID = br.ReadBytes(8);
            UInt32 size = br.ReadUInt32();
            _rawdata1 = new char[size];
            br.Read(_rawdata1, 0, (int)size);

            size = br.ReadUInt32();
            _rawdata2 = new char[size];
            br.Read(_rawdata2, 0, (int)size);

            size = br.ReadUInt32();
            _rawdata3 = new char[size];
            br.Read(_rawdata3, 0, (int)size);
            br.Close();
            fs.Close();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public WoTReplay Parse(string path)
        {
            //"mapDisplayName":\s"((\\u[0-9abcdef-]+)+)"
            //"dateTime":\s"([0-9.: ]+)"

            /*
            "gameplayID": "ctf",
            "playerName": "Alkatras1998",
            "mapName": "29_el_hallouf",
            "mapDisplayName": "\u042d\u043b\u044c-\u0425\u0430\u043b\u043b\u0443\u0444",
            "playerID": 3246635,
            "dateTime": "23.03.2013 15:06:03"
            */

            ReadDataFromFile(path);

            WoTReplay replay = new WoTReplay();

            Match match = Regex.Match(RawData1, "\"vehicles\": {((.+)})}");
            if (match.Success)
            {
                foreach (Match match_pl in Regex.Matches(match.Groups[1].Value, "\"([0-9]+)\":\\s?{(.+?}.+?)}"))
                {
                    Player PL = ParsePlayers(UInt64.Parse(match_pl.Groups[1].Value), match_pl.Groups[2].Value);
                    replay.Players.AddPlayer(ref PL);
                }
            }
            return replay;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        private Player ParsePlayers(UInt64 ID,string RawData)
        {
            Player PL = new Player();
            PL.ID = ID;
            foreach (Match match in Regex.Matches(RawData, "\"(\\w+)\":\\s?\"?([-_:a-zA-Z0-9]*)\"?,?"))
            {
                switch (match.Groups[1].Value)
                {
                    case "vehicleType":
                        PL.VehicleType = match.Groups[2].Value;
                        break;
                    case "name":
                        PL.Name = match.Groups[2].Value;
                        break;
                    case "clanAbbrev":
                        PL.ClanAbbrev = match.Groups[2].Value;
                        break;
                    case "events":
                        PL.Events = match.Groups[2].Value;
                        break;
                    case "isAlive":
                        PL.isAlive = bool.Parse(match.Groups[2].Value);
                        break;
                    case "isTeamKiller":
                        PL.isTeamKiller = bool.Parse(match.Groups[2].Value);
                        break;
                    case "team":
                        PL.Team = int.Parse(match.Groups[2].Value);
                        break;
                }
            }
            return PL;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string RawData1
        {
            get
            {
                return new String(_rawdata1);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string RawData2
        {
            get
            {
                return new String(_rawdata2);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        public string RawData3
        {
            get
            {
                return new String(_rawdata3);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
    }
}