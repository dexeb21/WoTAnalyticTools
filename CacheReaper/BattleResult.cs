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


    public class BattleResult
    {
        private PersonalData _personalResult;
        private BigInteger _arenaUniqueID;
        private CommonData _common;
        private Dictionary<int, PlayerData> _players;
        private Dictionary<int, PersonalData> _vehicles;

        /////////////////////////////////////////////////////////////////////////////
        public Dictionary<int, PlayerData> Players
        {
            get { return _players; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public Dictionary<int, PersonalData> Vehicles
        {
            get { return _vehicles; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public CommonData Common
        {
            get { return _common; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public PersonalData Personal
        {
            get { return _personalResult; }
        }
        /////////////////////////////////////////////////////////////////////////////
        public BigInteger ArenaUniqueID
        {
            get { return _arenaUniqueID; }
        }
        /////////////////////////////////////////////////////////////////////////////

        public BattleResult(string path)
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



            //IList<object> Temp_1 = result[1];
            //IList<object> Temp_1_2 = result[1][2];

            _arenaUniqueID = result[1][0];
            _personalResult = new PersonalData(result[1][1]);
            _common = new CommonData(result[1][2][0]);

            _players = new Dictionary<int, PlayerData>();
            PlayerData _player;
            foreach (KeyValuePair<object, object> kvp in (PythonDictionary)result[1][2][1])
            {
                _player = new PlayerData((IList<object>)kvp.Value);
                _players.Add((int)kvp.Key, _player);
            }
                        
            PersonalData _vehicle;
            _vehicles = new Dictionary<int, PersonalData>();
            foreach (KeyValuePair<object, object> kvp in (PythonDictionary)result[1][2][2])
            {
                _vehicle = new PersonalData((IList<object>)kvp.Value);
                _vehicles.Add((int)kvp.Key, _vehicle);
            }

            int x = 0;
        }
    }
}
