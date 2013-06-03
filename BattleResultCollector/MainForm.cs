using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WoTTools.CacheReaper;
using FirebirdSql.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;
using System.IO;

namespace BattleResultCollector
{
    public partial class MainForm : Form
    {
        private FbConnection myConnection;
        private CollectorDB CDB;
        private List<string> CacheFilesProcessed;
        /////////////////////////////////////////////////////////////////////////////////////////
        public MainForm()
        {
            InitializeComponent();
            CacheFilesProcessed = new List<string>();
            string connectionString =
                    "User=SYSDBA;" +
                    "Password=masterkey;" +
                    @"Database=D:\Project\WoTAnalyticTools\Database\COLLECTORDB.FDB;" +
                    @"client library=fbembed.dll;" +
                    "Dialect=3;" +
                    "Charset=NONE;" +
                    "Connection lifetime=15;" +
                    "ServerType=Embedded";

            myConnection = new FbConnection(connectionString);            
            myConnection.Open();
            CDB = new CollectorDB(ref myConnection);
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void buttonTest_Click(object sender, EventArgs e)
        {
            openWoTCahceFileDialog.ShowDialog();          
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void openWoTCahceFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            BattleResult_v2 BRv2 = new BattleResult_v2(openWoTCahceFileDialog.FileName);

            FbTransaction myTransaction = myConnection.BeginTransaction();
            try
            {
                CDB.AppendBattle(ref BRv2, ref myTransaction);
                myTransaction.Commit();
            }
            catch
            {
                myTransaction.Rollback();
                throw new Exception("Ooops! ---> openWoTCahceFileDialog_FileOk");
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void CacheLoadTimer_Tick(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\MyClouds\YandexDisk\wot\REP_Cache");
            FileSystemInfo[] infos = dir.GetFiles("*.dat");
            BattleResult_v2 BRv2;
            
            FbTransaction myTransaction;
            AppendResult Res;
            foreach (FileSystemInfo file in infos)
            {
                if (!CacheFilesProcessed.Contains(file.Name))
                {
                    myTransaction = myConnection.BeginTransaction();
                    try
                    {
                        BRv2 = new BattleResult_v2(file.FullName.ToString());
                        Res = CDB.AppendBattle(ref BRv2, ref  myTransaction);
                        myTransaction.Commit();
                        CacheFilesProcessed.Add(file.Name);
                        if (Res == AppendResult.Exist)
                        {
                            LogRichTextBox.AppendText(file.Name + " - добавлен ранее" + "\n");
                        }
                        else
                        {
                            LogRichTextBox.AppendText(file.Name + " - добавлен" + "\n");
                        }
                    }
                    catch (Exception expept)
                    {
                        myTransaction.Rollback();
                        throw new Exception("Ooops! ---> CacheLoadTimer_Tick: " + expept.Message);
                    }
                }
                else
                {
                    LogRichTextBox.AppendText(file.Name + " - обработан ранее" + "\n");
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void buttonTest2_Click(object sender, EventArgs e)
        {
            CacheLoadTimer.Enabled = !CacheLoadTimer.Enabled;
            LogRichTextBox.AppendText("Timer Enabled - " + CacheLoadTimer.Enabled.ToString()+"\n");
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            myConnection.Close();
        }

        private void buttonTest3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
