using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CacheReaper;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;

namespace BattleResultCollector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            BattleResult_v2 BRv2 = new BattleResult_v2(@"D:\MyClouds\YandexDisk\wot\REP_Cache\!!!\82243763184823766.dat");
            BattleResult_v2 BRv2_2 = new BattleResult_v2(@"D:\MyClouds\YandexDisk\wot\REP_Cache\325096852076416703.dat");
            BattleResult_v2 BRv2_3 = new BattleResult_v2(@"D:\MyClouds\YandexDisk\wot\REP_Cache\67690030279124597.dat");

            string connectionString =
            "User=SYSDBA;" +
            "Password=masterkey;" +
            @"Database=D:\Project\WoTAnalyticTools\Database\COLLECTORDB.FDB;" +
            @"client library=fbembed.dll;" +
            "Dialect=3;" +
            "Charset=NONE;" +
            "Connection lifetime=15;" +
            "ServerType=Embedded";


            FbConnection myConnection = new FbConnection(connectionString);
            myConnection.Open();

            FbTransaction myTransaction = myConnection.BeginTransaction();

            FbCommand myCommand = new FbCommand();
            CultureInfo cultureUS;
            cultureUS = CultureInfo.CreateSpecificCulture("en-US");


            myCommand.CommandText = "execute procedure ADD_COMMON (";
            myCommand.CommandText += BRv2.ArenaUniqueID.ToString()+",";

            myCommand.CommandText += BRv2.Common["arenaTypeID"].ToString() + ",";
            myCommand.CommandText += BRv2.Common["arenaCreateTime"].ToString() + ",";
            myCommand.CommandText += BRv2.Common["winnerTeam"].ToString() + ",";
            myCommand.CommandText += BRv2.Common["finishReason"].ToString() + ",";
            myCommand.CommandText += BRv2.Common["duration"].ToString(cultureUS) + ",";
            myCommand.CommandText += BRv2.Common["bonusType"].ToString() + ",";
            myCommand.CommandText += BRv2.Common["guiType"].ToString() + ",";
            myCommand.CommandText += BRv2.Common["vehLockMode"].ToString() + ")";

            myCommand.Connection = myConnection;
            myCommand.Transaction = myTransaction;

            Int32 BATTLE_ID = (Int32)myCommand.ExecuteScalar();

            myCommand.Dispose();
            
            foreach (KeyValuePair<int, Dictionary<string,dynamic>> Val in BRv2.Players)
            {
                myCommand = new FbCommand();
                myCommand.Connection = myConnection;
                myCommand.Transaction = myTransaction;
                myCommand.CommandText = "execute procedure ADD_PLAYER (";
                myCommand.CommandText += BATTLE_ID.ToString()+",";
                myCommand.CommandText += Val.Key.ToString() + ",";
                myCommand.CommandText += "'"+Val.Value["name"] + "',";
                myCommand.CommandText += Val.Value["clanDBID"].ToString() + ",";
                myCommand.CommandText += "'" + Val.Value["clanAbbver"] + "',";
                myCommand.CommandText += Val.Value["prebattleID"].ToString() + ",";
                myCommand.CommandText += Val.Value["team"].ToString() + ")";
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }

            myTransaction.Commit();
            myConnection.Close();            
        }
    }
}
