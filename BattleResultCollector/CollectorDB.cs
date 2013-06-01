using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;
using WoTTools.CacheReaper;

namespace BattleResultCollector
{
    public enum AppendResult
    {
        Append,
        Exist
    }

    public class CollectorDB
    {
        private CultureInfo cultureUS;
        private FbConnection  myConnection;
        public CollectorDB(ref FbConnection Connection)
        {
            cultureUS = CultureInfo.CreateSpecificCulture("en-US");
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                myConnection = Connection;
            }
            else
            {
                throw new Exception("Нет соединения с БД");
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////
        private int exec_proc_ADD_COMMON(ref FbConnection Connection, ref FbTransaction Transaction, ref BattleResult_v2 BRv2)
        {
            FbCommand myCommand = new FbCommand();
          
            myCommand.CommandText = "execute procedure ADD_COMMON ("
            + BRv2.ArenaUniqueID.ToString() + ","
            + BRv2.Common["arenaTypeID"].ToString() + ","
            + BRv2.Common["arenaCreateTime"].ToString() + ","
            + BRv2.Common["winnerTeam"].ToString() + ","
            + BRv2.Common["finishReason"].ToString() + ","
            + BRv2.Common["duration"].ToString(cultureUS) + ","
            + BRv2.Common["bonusType"].ToString() + ","
            + BRv2.Common["guiType"].ToString() + ","
            + BRv2.Common["vehLockMode"].ToString() + ")";

            myCommand.Connection = Connection;
            myCommand.Transaction = Transaction;

            Int32 BATTLE_ID = (Int32)myCommand.ExecuteScalar();

            myCommand.Dispose();

            return BATTLE_ID;
        }
        //////////////////////////////////////////////////////////////////////////////////////
        private void exec_proc_ADD_PLAYER(ref FbConnection Connection, ref FbTransaction Transaction, ref BattleResult_v2 BRv2, int BATTLE_ID)
        {
            FbCommand myCommand;
            foreach (KeyValuePair<int, Dictionary<string, dynamic>> Val in BRv2.Players)
            {
                myCommand = new FbCommand();
                myCommand.Connection = Connection;
                myCommand.Transaction = Transaction;
                myCommand.CommandText = "execute procedure ADD_PLAYER ("
                + BATTLE_ID.ToString() + ","
                + Val.Key.ToString() + ","
                + "'" + Val.Value["name"] + "',"
                + Val.Value["clanDBID"].ToString() + ","
                + "'" + Val.Value["clanAbbver"] + "',"
                + Val.Value["prebattleID"].ToString() + ","
                + Val.Value["team"].ToString() + ")";
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void exec_proc_ADD_PERSONAL(ref FbConnection Connection, ref FbTransaction Transaction, ref BattleResult_v2 BRv2, int BATTLE_ID)
        {
            FbCommand myCommand = new FbCommand();
            myCommand.CommandText = "execute procedure ADD_Personal ("
            + BATTLE_ID.ToString() + ","

            + BRv2.Personal["health"].ToString() + ","
            + BRv2.Personal["credits"].ToString() + ","
            + BRv2.Personal["xp"].ToString() + ","
            + BRv2.Personal["shots"].ToString() + ","
            + BRv2.Personal["hits"].ToString() + ","
            + BRv2.Personal["thits"].ToString() + ","
            + BRv2.Personal["he_hits"].ToString() + ","
            + BRv2.Personal["pierced"].ToString() + ","
            + BRv2.Personal["damageDealt"].ToString() + ","
            + BRv2.Personal["damageAssisted"].ToString() + ","
            + BRv2.Personal["damageReceived"].ToString() + ","
            + BRv2.Personal["shotsReceived"].ToString() + ","
            + BRv2.Personal["damaged"].ToString() + ","
            + BRv2.Personal["spotted"].ToString() + ","
            + BRv2.Personal["kills"].ToString() + ","
            + BRv2.Personal["tdamageDealt"].ToString() + ","
            + BRv2.Personal["tkills"].ToString() + ","
            + (Convert.ToInt32(BRv2.Personal["isTeamKiller"])).ToString() + ","
            + BRv2.Personal["capturePoints"].ToString() + ","
            + BRv2.Personal["droppedCapturePoints"].ToString() + ","
            + BRv2.Personal["mileage"].ToString() + ","
            + BRv2.Personal["lifeTime"].ToString() + ","
            + BRv2.Personal["killerID"].ToString() + ","
            + BRv2.Personal["achievements"].Count.ToString() + ","
            + BRv2.Personal["potentialDamageReceived"].ToString() + ","
            + BRv2.Personal["repair"].ToString() + ","
            + BRv2.Personal["freeXP"].ToString() + ","
            + "0,"//BRv2.Personal["details"].ToString() + ","
            + BRv2.Personal["accountDBID"].ToString() + ","
            + BRv2.Personal["team"].ToString() + ","
            + BRv2.Personal["typeCompDescr"].ToString() + ","
            + BRv2.Personal["gold"].ToString() + ","
            + BRv2.Personal["xpPenalty"].ToString() + ","
            + BRv2.Personal["creditsPenalty"].ToString() + ","
            + BRv2.Personal["creditsContributionIn"].ToString() + ","
            + BRv2.Personal["creditsContributionOut"].ToString() + ","
            + BRv2.Personal["tmenXP"].ToString() + ","
            + BRv2.Personal["eventCredits"].ToString() + ","
            + BRv2.Personal["eventGold"].ToString() + ","
            + BRv2.Personal["eventXP"].ToString() + ","
            + BRv2.Personal["eventFreeXP"].ToString() + ","
            + BRv2.Personal["eventTMenXP"].ToString() + ","
            + BRv2.Personal["autoRepairCost"].ToString() + ","
            + BRv2.Personal["autoLoadCost"][1].ToString() + ","
            + BRv2.Personal["autoLoadCost"][0].ToString() + ","
            + BRv2.Personal["autoEquipCost"][1].ToString() + ","
            + BRv2.Personal["autoEquipCost"][0].ToString() + ","
            + (Convert.ToInt32(BRv2.Personal["isPremium"])).ToString() + ","
            + BRv2.Personal["premiumXPFactor10"].ToString() + ","
            + BRv2.Personal["premiumCreditsFactor10"].ToString() + ","
            + BRv2.Personal["dailyXPFactor10"].ToString() + ","
            + BRv2.Personal["aogasFactor10"].ToString() + ","
            + BRv2.Personal["markOfMastery"].ToString() + ")";

            myCommand.Connection = Connection;
            myCommand.Transaction = Transaction;
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void exec_proc_ADD_DOSSIERPOPUPS(ref FbConnection Connection, ref FbTransaction Transaction, ref BattleResult_v2 BRv2, int BATTLE_ID)
        {
            FbCommand myCommand;
            if (BRv2.Personal["dossierPopUps"].Count > 0)
            {
                foreach (IList<object> val in BRv2.Personal["dossierPopUps"])
                {
                    myCommand = new FbCommand();

                    myCommand.CommandText = "execute procedure ADD_DOSSIERPOPUPS(" +
                    BATTLE_ID.ToString() + "," +
                    ((int)val[0]).ToString() +","+
                    ((int)val[1]).ToString() + ")";

                    myCommand.Connection = Connection;
                    myCommand.Transaction = Transaction;
                    myCommand.ExecuteNonQuery();
                    myCommand.Dispose();
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private bool exec_proc_EXISTS_BATTLE(ref FbConnection Connection,ref FbTransaction Transaction, ref BattleResult_v2 BRv2)
        {
            FbCommand myCommand = new FbCommand();
            myCommand.CommandText = "execute procedure exists_battle(" + BRv2.ArenaUniqueID.ToString() + ")";
            
            myCommand.Connection = Connection;
            myCommand.Transaction = Transaction;
            int exists;
            try
            {
                exists = (int)myCommand.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw (e);
            }
            myCommand.Dispose();
            return (exists>0)?true:false;
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        public AppendResult AppendBattle(ref BattleResult_v2 BRv2, ref FbTransaction Transaction)
        {
            if (!exec_proc_EXISTS_BATTLE(ref myConnection, ref Transaction, ref BRv2))
            {
                Int32 BATTLE_ID = exec_proc_ADD_COMMON(ref myConnection, ref Transaction, ref BRv2);
                exec_proc_ADD_PLAYER(ref myConnection, ref Transaction, ref BRv2, BATTLE_ID);
                exec_proc_ADD_PERSONAL(ref myConnection, ref Transaction, ref BRv2, BATTLE_ID);
                exec_proc_ADD_DOSSIERPOPUPS(ref myConnection, ref Transaction, ref BRv2, BATTLE_ID);
                return AppendResult.Append;
            }
            else
            {
                return AppendResult.Exist;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
    }
}
