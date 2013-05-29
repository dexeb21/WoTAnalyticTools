using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Hosting.Providers;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace CacheReaper
{
    public class BattleResult_v2
    {
        private Dictionary<string, dynamic> __personalResult;
        //private BigInteger __arenaUniqueID;
        private long __arenaUniqueID;
        private Dictionary<string, dynamic> __common;
        private Dictionary<int, Dictionary<string, dynamic>> __players;
        private Dictionary<int, Dictionary<string, dynamic>> __vehicles;
        private string[] _common_names   = new string[] { "arenaTypeID", "arenaCreateTime", "winnerTeam", "finishReason", "duration", "bonusType", "guiType", "vehLockMode" };
        private string[] _details_names = new string[] { "spotted", "killed", "hits", "he_hits", "pierced", "damageDealt", "damageAssisted", "crits", "fire" };
        private string[] _player_names   = new string[] { "name", "clanDBID", "clanAbbver", "prebattleID", "team"};
        private string[] _personal_names = new string[] { "health", "credits", "xp", "shots", "hits", "thits", "he_hits", "pierced", "damageDealt", "damageAssisted", "damageReceived", "shotsReceived", "spotted", "damaged", "kills", "tdamageDealt", "tkills", "isTeamKiller", "capturePoints", "droppedCapturePoints", "mileage", "lifeTime", "killerID", "achievements", "potentialDamageReceived", "repair", "freeXP", "details", "accountDBID", "team", "typeCompDescr", "gold", "xpPenalty", "creditsPenalty", "creditsContributionIn", "creditsContributionOut", "tmenXP", "eventCredits", "eventGold", "eventXP", "eventFreeXP", "eventTMenXP", "autoRepairCost", "autoLoadCost", "autoEquipCost", "isPremium", "premiumXPFactor10", "premiumCreditsFactor10", "dailyXPFactor10", "aogasFactor10", "markOfMastery", "dossierPopUps" };

        /////////////////////////////////////////////////////////////////////////////
        public Dictionary<int, Dictionary<string, dynamic>> Players
        {
            get { return __players; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public Dictionary<int, Dictionary<string, dynamic>> Vehicles
        {
            get { return __vehicles; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public Dictionary<string, dynamic> Common
        {
            get { return __common; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public Dictionary<string, dynamic> Personal
        {
            get { return __personalResult; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public long ArenaUniqueID
        {
            get { return __arenaUniqueID; }
        }
        /////////////////////////////////////////////////////////////////////////////
        private Dictionary<int, Dictionary<string, int>> p27_RawDetailsToDict(string raw)
        {
            int size = raw.Length;
            byte[] bytes = new byte[size];

            int j = 0;
            foreach (char ch in raw)
            {
                bytes[j] = Convert.ToByte(ch);
                j++;
            }

            // 22 = магическое число Count = x(4+2*9) 4 и 2 колво байт, 9 колво данных
            int chunk_count = size / 22;

            Dictionary<string, int> details;
            Dictionary<int, Dictionary<string, int>> Result = new Dictionary<int, Dictionary<string, int>>();

            for (int k = 0; k < chunk_count; k++)
            {
                details = new Dictionary<string, int>();

                for (int l = 0; l < 9; l++)
                {
                    details.Add(_details_names[l], BitConverter.ToInt16(bytes, 4 * chunk_count + 2 * l + 18 * k));
                }
                Result.Add(BitConverter.ToInt32(bytes, 4 * k), details);
            }
            return Result;
        }
        /////////////////////////////////////////////////////////////////////////////
        private Dictionary<string, dynamic> SetCommonData(dynamic value)
        {
            Dictionary<string, dynamic>  Result = new Dictionary<string, dynamic>();

            int key_name = 0;

            foreach (dynamic d in value)
            {
                Result.Add(_common_names[key_name], d);
                key_name++;
            }

            return Result;
        }
        /////////////////////////////////////////////////////////////////////////////
        private Dictionary<int, Dictionary<string, dynamic>> SetPlayersData(dynamic value)
        {
            Dictionary<int, Dictionary<string, dynamic>> Result = new Dictionary<int, Dictionary<string, dynamic>>();
            Dictionary<string, dynamic> _player;
            int key_name;
            foreach (KeyValuePair<object, object> kvp in (PythonDictionary)value)
            {
                key_name = 0;
                _player = new Dictionary<string, dynamic>();
                foreach (dynamic d in (IList<object>)kvp.Value)
                {
                    _player.Add(_player_names[key_name], d);
                    key_name++;
                }
                Result.Add((int)kvp.Key, _player);
            }
            return Result;
        }
        /////////////////////////////////////////////////////////////////////////////
        private Dictionary<int, Dictionary<string, dynamic>> SetVehiclesData(dynamic value)
        {
            Dictionary<int, Dictionary<string, dynamic>> Result = new Dictionary<int, Dictionary<string, dynamic>>();

            foreach (KeyValuePair<object, object> kvp in (PythonDictionary)value)
            {
                Result.Add((int)kvp.Key, SetPersonalData(kvp.Value));
            }

            return Result;
        }
        /////////////////////////////////////////////////////////////////////////////
        private Dictionary<string, dynamic> SetPersonalData(dynamic value)
        {
            Dictionary<string, dynamic> Result = new Dictionary<string, dynamic>();

            int key_name = 0;

            foreach (dynamic d in value)
            {
                if (key_name != 27)
                {
                    if (d is IronPython.Runtime.List)
                    {
                        Result.Add(_personal_names[key_name], ((IronPython.Runtime.List)d).Cast<object>().ToList<object>());
                    }
                    else
                    {
                        Result.Add(_personal_names[key_name], d);
                    }
                }
                else
                {
                    Result.Add(_personal_names[key_name], p27_RawDetailsToDict(d));
                }
                key_name++;
            }
            return Result;
        }
        /////////////////////////////////////////////////////////////////////////////
        public BattleResult_v2(string path)
        {
            var paths = new List<string>();
            paths.Add(@"C:\Program Files (x86)\IronPython 2.7\Lib");
            
            ScriptEngine m_engine = Python.CreateEngine();
            ScriptScope m_scope = null;

            m_scope = m_engine.CreateScope();
            m_engine.SetSearchPaths(paths);

            ObjectOperations ops = m_engine.Operations;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string name = "CacheReaper.Scripts.Serialize.py";
            Stream stream = assembly.GetManifestResourceStream(name);
            StreamReader textStreamReader = new StreamReader(stream);
            string code = textStreamReader.ReadToEnd();

            ScriptSource source = m_engine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            
            source.Execute(m_scope);
            
            dynamic Serialize = ops.Invoke(m_scope.GetVariable("Serialize"));

            dynamic result = Serialize.unpickle((object)path);

            __arenaUniqueID = (long)((BigInteger)result[1][0]);
            __personalResult = SetPersonalData(result[1][1]);
            __common = SetCommonData(result[1][2][0]);
            __players = SetPlayersData(result[1][2][1]);
            __vehicles = SetVehiclesData(result[1][2][2]);
        }
    }
}