using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ReplayReaper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using WoTTools.CacheReaper;

namespace ReplayViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Reaper RR = new Reaper();
            Reaper RR2 = new Reaper();

            richTextBox.Clear();

            WoTReplay Replay = RR.Parse(@"D:\MyClouds\YandexDisk\wot\REP_Cache\086\086_20130531_2238_uk-GB51_Excelsior_07_lakeville (1).wotreplay");
            WoTReplay Replay2 = RR2.Parse(@"D:\MyClouds\YandexDisk\wot\REP_Cache\67690030279124597.wotreplay");
            /*
            ReplayInfo RI = new ReplayInfo();
            RI.ReadDataFromFile(@"D:\MyClouds\YandexDisk\wot\REP_Cache\086\086_20130531_2238_uk-GB51_Excelsior_07_lakeville (1).wotreplay");

            ReplayInfo RI2 = new ReplayInfo();
            RI2.ReadDataFromFile(@"D:\MyClouds\YandexDisk\wot\REP_Cache\67690030279124597.wotreplay");
            */


           // BattleResult_v2 BR = new BattleResult_v2(@"D:\MyClouds\YandexDisk\wot\REP_Cache\!!!\___KV1_321235285635519637.dat");
            //BattleResult_v2 BRv2 = new BattleResult_v2(@"D:\MyClouds\YandexDisk\wot\REP_Cache\!!!\82243763184823766.dat");

            //KeyValuePair<string, dynamic>
            /*
            foreach (int key in BRv2.Vehicles.Keys)
            {
                richTextBox.AppendText(BRv2.Players[BRv2.Vehicles[key]["accountDBID"]]["name"]);
                if (BRv2.Players[BRv2.Vehicles[key]["accountDBID"]]["clanAbbver"] != "")
                {
                    richTextBox.AppendText("[" + BRv2.Players[BRv2.Vehicles[key]["accountDBID"]]["clanAbbver"] + "]");
                }
                richTextBox.AppendText("\n");
                richTextBox.AppendText("---Damage: "+BRv2.Vehicles[key]["damageDealt"]+"\n");
                richTextBox.AppendText("---Team  : " + BRv2.Vehicles[key]["team"] + "\n");
            }
*/

            JsonTextReader reader = new JsonTextReader(new StringReader(RR.RawData2));

            //////////////////////////////////////////////////////////////////
            /// UnPicle
            //////////////////////////////////////////////////////////////////

            ScriptEngine m_engine = Python.CreateEngine();
            ScriptScope m_scope = null;

            m_scope = m_engine.CreateScope();

            var paths = new List<string>();
            paths.Add(@"C:\Program Files (x86)\IronPython 2.7\Lib");

            m_engine.SetSearchPaths(paths);

            string code = @"
import pickle
class Serialize(object):
    def unpickle(self,value):
        return pickle.loads(value)
    def unpickledat(self,value):
        f = open(value,'rb')
        f.seek(261)
        bb = f.read()
        return pickle.loads(bb)
";

            ObjectOperations ops = m_engine.Operations;
            ScriptSource source;
            source = m_engine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
            source.Execute(m_scope);
            object klass = m_scope.GetVariable("Serialize");
            object instance = ops.Invoke(klass);
            object method = ops.GetMember(instance, "unpickle");
            
            object result1 = ops.Invoke(method, (object)RR.RawData3);
            object result2 = ops.Invoke(method, (object)RR2.RawData3);

            

            IronPython.Runtime.PythonDictionary OBJ = (IronPython.Runtime.PythonDictionary)result1;

            //Dictionary<string, dynamic> D = OBJ.ToDictionary<string, dynamic>(;

            //double O2 = (double)(((IronPython.Runtime.PythonDictionary)OBJ["common"])["duration"]);
            
            //269093955030980.dat




            //BattleResult BR2 = new BattleResult("D:\\322055057453144847.dat");

            string[] LAYOUT = new string[]{"_version",
  "lastBattleTime",
  "battleLifeTime",
  "maxFrags",
  "xp",
  "maxXP",
  "battlesCount",
  "wins",
  "losses",
  "survivedBattles",
  "winAndSurvived",
  "frags",
  "frags8p",
  "fragsBeast",
  "shots",
  "hits",
  "spotted",
  "damageDealt",
  "damageReceived",
  "treesCut",
  "capturePoints",
  "droppedCapturePoints",
  "sniperSeries",
  "maxSniperSeries",
  "invincibleSeries",
  "maxInvincibleSeries",
  "diehardSeries",
  "maxDiehardSeries",
  "killingSeries",
  "maxKillingSeries",
  "piercingSeries",
  "maxPiercingSeries",
  "battleHeroes",
  "fragsSinai",
  "warrior",
  "invader",
  "sniper",
  "defender",
  "steelwall",
  "supporter",
  "scout",
  "evileye",
  "medalKay",
  "medalCarius",
  "medalKnispel",
  "medalPoppel",
  "medalAbrams",
  "medalLeClerc",
  "medalLavrinenko",
  "medalEkins",
  "medalWittmann",
  "medalOrlik",
  "medalOskin",
  "medalHalonen",
  "medalBurda",
  "medalBillotte",
  "medalKolobanov",
  "medalFadin",
  "medalRadleyWalters",
  "medalBrunoPietro",
  "medalTarczay",
  "medalPascucci",
  "medalDumitru",
  "medalLehvaslaiho",
  "medalNikolas",
  "medalLafayettePool",
  "sinai",
  "heroesOfRassenay",
  "beasthunter",
  "mousebane",
  "tankExpertStrg",
  "titleSniper",
  "invincible",
  "diehard",
  "raider",
  "handOfDeath",
  "armorPiercer",
  "kamikaze",
  "lumberjack",
  "markOfMastery",
  "company/xp",
  "company/battlesCount",
  "company/wins",
  "company/losses",
  "company/survivedBattles",
  "company/frags",
  "company/shots",
  "company/hits",
  "company/spotted",
  "company/damageDealt",
  "company/damageReceived",
  "company/capturePoints",
  "company/droppedCapturePoints",
  "clan/xp",
  "clan/battlesCount",
  "clan/wins",
  "clan/losses",
  "clan/survivedBattles",
  "clan/frags",
  "clan/shots",
  "clan/hits",
  "clan/spotted",
  "clan/damageDealt",
  "clan/damageReceived",
  "clan/capturePoints",
  "clan/droppedCapturePoints",
  "tankExpert",
  "tankExpert0",
  "tankExpert1",
  "tankExpert2",
  "tankExpert3",
  "tankExpert4",
  "tankExpert5",
  "tankExpert6",
  "tankExpert7",
  "tankExpert8",
  "tankExpert9",
  "tankExpert10",
  "tankExpert11",
  "tankExpert12",
  "tankExpert13",
  "tankExpert14",
  "medalBrothersInArms",
  "medalCrucialContribution",
  "medalDeLanglade",
  "medalTamadaYoshio",
  "bombardier",
  "huntsman",
  "alaric",
  "sturdy",
  "ironMan",
  "luckyDevil",
  "pattonValley",
  "fragsPatton",
  "_dynRecPos_vehicle"};
            

        }
    }
}
