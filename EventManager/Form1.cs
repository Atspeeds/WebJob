using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EventManager.Classes;
using Behin;
using System.Data.SqlClient;
using BehinSignalR;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace EventManager
{
    public partial class Form1 : Form
    {
        string StrCon1 = "";
        private string LogUrl = "";
        clsEventManager objEventManager = new clsEventManager();
        public Form1()
        {
            InitializeComponent();
            Behin.Configuration config = new Behin.Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
            LogUrl = Application.StartupPath + "\\Log\\Log.txt";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //EventManager();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //BehinSignalR.clsSignalRStruct.strEvent myRowEvent = new clsSignalRStruct.strEvent();
            //myRowEvent.DateFrom =DateTime.Now;
            //myRowEvent.NoDiagram = -54;
            //myRowEvent.NoRun = 6458;
            //myRowEvent.NoEntity = 24006093;
            //myRowEvent.NoStepRelation = 461;
            //myRowEvent.DateTo = DateTime.Now;

            //BehinSignalR.SignalRHelper.ServerMessageToServer(ENUMS.CommandMessageType.OccurDiagramEvent,
            //   ENUMS.ServerType.WebApplication, Newtonsoft.Json.JsonConvert.SerializeObject(myRowEvent));

            //clsEventManager objEventManager = new clsEventManager();
            //objEventManager.FillEventDiagram(myRowEvent.NoEntity, myRowEvent.NoDiagram, myRowEvent.NoStepRelation, myRowEvent.DateFrom, myRowEvent.DateTo, myRowEvent.NoRun);
        }

        public void EventManager()
        {
            clsEvent objEvent = new clsEvent();
            clsEvent.strcEvent myRowEvent = new clsEvent.strcEvent();
            DMSService.EventManagerWebService objEventManagerWebService = new DMSService.EventManagerWebService();
            //while (!IsSeviceStop)
            //{
            try
            {

                clsBRMS objBRMS = new clsBRMS();
                string Query = " Select E.* From Event E  " +
                               " Where Date <= getdate() and IsOccured = 0";
                SqlCommand scmd_Class = null;
                GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                scmd_Class = new SqlCommand(Query);
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1 = GClass.GeneralSelectCommand(scmd_Class);

                System.IO.File.AppendAllText(LogUrl, Environment.NewLine + dt1.Rows.Count.ToString());

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    CDate lDate = new CDate();


                    if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.EffectOnDiagram)
                    {
                        string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), dt1.Rows[i]["date"].ToString(), int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + strconclusion);
                        if (strconclusion != "")
                        {
                            bool res = objEventManagerWebService.OccurDiagramEvent(new DMSService.strcStepRelationEvent()
                            {

                                NoDiagram = int.Parse(strconclusion.Split('&')[0]),
                                NoRun = int.Parse(strconclusion.Split('&')[1]),
                                NoStepRelation = int.Parse(strconclusion.Split('&')[2]),
                                NoEntity = int.Parse(strconclusion.Split('&')[3]),
                                No = int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()),
                                Date = DateTime.Parse(dt1.Rows[i]["Date"].ToString())


                            });
                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + res);
                            if (res)
                            {

                                string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                                    dt1.Rows[i]["No"]);
                                GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));
                            }

                        }
                    }
                    else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.Waiting)
                    {
                        string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), Convert.ToDateTime(dt1.Rows[i]["date"]).ToString("yyyy-MM-dd HH:mm"), int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + strconclusion);
                        if (strconclusion != "")
                        {
                            bool res = objEventManagerWebService.OccurWating(int.Parse(strconclusion));
                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + res);
                            if (res)
                            {
                                string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                                    dt1.Rows[i]["No"]);
                                GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));
                            }

                        }
                    }
                    else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.Session)
                    {
                        string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), Convert.ToDateTime(dt1.Rows[i]["date"]).ToString("yyyy-MM-dd HH:mm"), int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + strconclusion);
                        if (strconclusion != "")
                        {
                            clsSignalRStruct.ExtMessageStrc<clsSignalRStruct.OnlineMessage> extMessage = new clsSignalRStruct.
                            ExtMessageStrc<clsSignalRStruct.OnlineMessage>
                            {
                                Date = DateTime.Now,
                                Message = strconclusion,
                                lstReiceivers = new List<int> { int.Parse(dt1.Rows[i]["General"].ToString()) },
                                MessageType = ENUMS.MessageType.OnlineMessage,
                                NoEntity = int.Parse(dt1.Rows[i]["NoSrc"].ToString()),
                                NoSender = -1,
                                NoSrcTable = -1,
                                Parameter = new clsSignalRStruct.OnlineMessage(),

                            };
                            clsServerDialog objServerDg = new clsServerDialog();
                            clsServerDialog.strcServerDialog myRowServerDg = new clsServerDialog.strcServerDialog();
                            objServerDg.GetNewRow();
                            myRowServerDg.IDCommandMessageType = (int)BehinSignalR.ENUMS.CommandMessageType.OnlineMessage;
                            myRowServerDg.IDServerType = (int)BehinSignalR.ENUMS.ServerType.EventManager;
                            myRowServerDg.NoOprationEnvironment = 0;
                            myRowServerDg.received = false;
                            myRowServerDg.JsonValue = JsonConvert.SerializeObject(extMessage);
                            objServerDg.Insert(myRowServerDg);

                            string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                                dt1.Rows[i]["No"]);
                            GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));

                            //bool res = objEventManagerWebService.OccurWating(int.Parse(strconclusion));
                            //System.IO.File.AppendAllText(LogUrl, Environment.NewLine + res);
                            //if (res)
                            //{
                            //    string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                            //        dt1.Rows[i]["No"]);
                            //    GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));
                            //}

                        }
                    }
                    else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.TaskExpire)
                    {
                        string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), Convert.ToDateTime(dt1.Rows[i]["date"]).ToString("yyyy-MM-dd HH:mm"), int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + strconclusion);
                        if (strconclusion != "")
                        {
                            bool res = objEventManagerWebService.OccurDelay(int.Parse(strconclusion));
                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + res);
                            if (res)
                            {
                                string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                                    dt1.Rows[i]["No"]);
                                GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));
                            }

                        }
                    }
                    else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.Trace)
                    {
                        string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), Convert.ToDateTime(dt1.Rows[i]["date"]).ToString("yyyy-MM-dd HH:mm"), int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + strconclusion);
                        if (strconclusion != "")
                        {
                            clsSignalRStruct.ExtMessageStrc<clsSignalRStruct.OnlineMessage> extMessage = new clsSignalRStruct.
                            ExtMessageStrc<clsSignalRStruct.OnlineMessage>
                            {
                                Date = DateTime.Now,
                                Message = dt1.Rows[i]["General2"].ToString(),
                                lstReiceivers = new List<int> { int.Parse(dt1.Rows[i]["General"].ToString()) },
                                MessageType = ENUMS.MessageType.OnlineMessage,
                                NoEntity = int.Parse(dt1.Rows[i]["NoSrc"].ToString()),
                                NoSender = -1,
                                NoSrcTable = -1,
                                Parameter = new clsSignalRStruct.OnlineMessage(),

                            };
                            clsServerDialog objServerDg = new clsServerDialog();
                            clsServerDialog.strcServerDialog myRowServerDg = new clsServerDialog.strcServerDialog();
                            objServerDg.GetNewRow();
                            myRowServerDg.IDCommandMessageType = (int)BehinSignalR.ENUMS.CommandMessageType.OnlineMessage;
                            myRowServerDg.IDServerType = (int)BehinSignalR.ENUMS.ServerType.EventManager;
                            myRowServerDg.NoOprationEnvironment = 0;
                            myRowServerDg.received = false;
                            myRowServerDg.JsonValue = JsonConvert.SerializeObject(extMessage);
                            objServerDg.Insert(myRowServerDg);

                            string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                                dt1.Rows[i]["No"]);
                            GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));

                        }
                    }
                    else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.Lenz)
                    {
                        string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), Convert.ToDateTime(dt1.Rows[i]["date"]).ToString("yyyy-MM-dd HH:mm"), int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + strconclusion);
                        if (strconclusion != "")
                        {
                            clsSignalRStruct.ExtMessageStrc<clsSignalRStruct.OnlineMessage> extMessage = new clsSignalRStruct.
                            ExtMessageStrc<clsSignalRStruct.OnlineMessage>
                            {
                                Date = DateTime.Now,
                                Message = strconclusion,
                                lstReiceivers = new List<int> { int.Parse(dt1.Rows[i]["General"].ToString()) },
                                MessageType = ENUMS.MessageType.OnlineMessage,
                                NoEntity = int.Parse(dt1.Rows[i]["NoSrc"].ToString()),
                                NoSender = -1,
                                NoSrcTable = -1,
                                Parameter = new clsSignalRStruct.OnlineMessage(),

                            };
                            clsServerDialog objServerDg1 = new clsServerDialog();
                            clsServerDialog.strcServerDialog myRowServerDg1 = new clsServerDialog.strcServerDialog();
                            objServerDg1.GetNewRow();
                            myRowServerDg1.IDCommandMessageType = (int)BehinSignalR.ENUMS.CommandMessageType.OnlineMessage;
                            myRowServerDg1.IDServerType = (int)BehinSignalR.ENUMS.ServerType.EventManager;
                            myRowServerDg1.NoOprationEnvironment = 0;
                            myRowServerDg1.received = false;
                            myRowServerDg1.JsonValue = JsonConvert.SerializeObject(extMessage);
                            objServerDg1.Insert(myRowServerDg1);
                        }
                        clsServerDialog objServerDg = new clsServerDialog();
                        clsServerDialog.strcServerDialog myRowServerDg = new clsServerDialog.strcServerDialog();
                        clsEntityActionSelf objEntAtion = new clsEntityActionSelf();
                        int count = objEntAtion.SelectMagnifierFolderEntity(int.Parse(dt1.Rows[i]["General"].ToString()), int.Parse(dt1.Rows[i]["NoSrc"].ToString())) ;
                        if (count > -1)
                        {
                            clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcLenz> myevent = new clsSignalRStruct.strcEvent<clsSignalRStruct.strcLenz>
                            {
                                DateTo = Convert.ToDateTime(dt1.Rows[i]["Date"].ToString()).AddHours(1),
                                NoEntity = int.Parse(dt1.Rows[i]["NoSrc"].ToString()),
                                NoTable = int.Parse(dt1.Rows[i]["NoTables"].ToString()),
                                CommandMessageType = ENUMS.CommandMessageType.Lenz,
                                EffectType = ENUMS.EffectType.Lenz,
                                EventType = ENUMS.EventType.Lenz,
                                ReminderType = ENUMS.ReminderType.System,
                                Parameter = new clsSignalRStruct.strcLenz()
                                {
                                    NoAgent = int.Parse(dt1.Rows[i]["General"].ToString()),
                                    NoEntity = int.Parse(dt1.Rows[i]["NoSrc"].ToString())
                                }
                            };
                            objServerDg.GetNewRow();
                            myRowServerDg.IDCommandMessageType = (int)BehinSignalR.ENUMS.CommandMessageType.Lenz;
                            myRowServerDg.IDServerType = (int)BehinSignalR.ENUMS.ServerType.EventManager;
                            myRowServerDg.NoOprationEnvironment = 0;
                            myRowServerDg.received = false;
                            myRowServerDg.JsonValue = JsonConvert.SerializeObject(myevent);
                            objServerDg.Insert(myRowServerDg);
                        }
                        string UpdateQuery = string.Format("Update [Event] Set IsOccured =1 where No={0}",
                            dt1.Rows[i]["No"]);
                        GClass.GeneralUpdateCommand(new SqlCommand(UpdateQuery));

                    }
                }
            }
            catch (Exception er)
            {
                System.IO.File.AppendAllText(LogUrl, Environment.NewLine + er.ToString());
            }
            //}
        }

        public void ReadServerDialog()
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            clsServerDialog objServerDialog = new clsServerDialog();
            clsServerDialog.strcServerDialog myrowServerDialog = new clsServerDialog.strcServerDialog();
            string Query = "";
            //while (!IsSeviceStop)
            //{
                try
                {

                    List<clsServerDialog.strcServerDialog> lstServerDialogs = objServerDialog.Select("received", "0",
                        " IDServerType=712");

                    foreach (clsServerDialog.strcServerDialog myRow in lstServerDialogs)
                    {

                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + myRow.JsonValue + " ==>>>" + myrowServerDialog.IDCommandMessageType);

                        if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.CreateEvent)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent>>(myRow.JsonValue);

                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.Wating)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<int> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<int>>(myRow.JsonValue);

                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.Delay)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<int> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<int>>(myRow.JsonValue);

                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.Trace)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.srtcTrace> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.srtcTrace>>(myRow.JsonValue);
                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.Lenz)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcLenz> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcLenz>>(myRow.JsonValue);
                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.RemoveEvent)
                        {

                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + myRow.JsonValue + " ****" + myrowServerDialog.IDCommandMessageType);

                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent>>(myRow.JsonValue);

                            objEventManager.RemoveEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.Session)
                        {
                            BehinSignalR.clsSignalRStruct.SessionStrc myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.SessionStrc>(myRow.JsonValue);

                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        myrowServerDialog = myRow;
                        myrowServerDialog.received = true;
                        objServerDialog.Update(myrowServerDialog);
                    }

                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + ex.ToString());

                }

            //}
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //      clsSignalRStruct.strcServer myServer = new clsSignalRStruct.strcServer();
            //      myServer.ConnectionId = "";
            //      myServer.ServerType= ENUMS.ServerType.EventManager;
            //      BehinSignalR.SignalRHelper.ServerURI = "http://192.168.1.46:5520/signalr";
            //      BehinSignalR.SignalRHelper.HubName = "BehinSignalrServer";
            //      BehinSignalR.SignalRHelper.QueryString = string.Format("ClientType={1}&Server={0}", Newtonsoft.Json.JsonConvert.SerializeObject(myServer),(int) BehinSignalR.ENUMS.SignalRClientType.Server);
            //      bool hi = BehinSignalR.SignalRHelper.Connect();

            //BehinSignalR.SignalRHelper.HubProxy.On<ENUMS.CommandMessageType, string>(
            //          "ClientMessageFromServer", ClientMessageFromServer);

        }
        public void ReadMessages()
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            clsServerDialog objServerDialog = new clsServerDialog();
            clsServerDialog.strcServerDialog myrowServerDialog = new clsServerDialog.strcServerDialog();
            //while (!IsSeviceStop)
            //{
                try
                {

                    List<clsServerDialog.strcServerDialog> lstServerDialogs = objServerDialog.Select("received", "0",
                        " IDServerType=712 ");
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + lstServerDialogs.Count.ToString());
                    foreach (clsServerDialog.strcServerDialog myRow in lstServerDialogs)
                    {

                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + myRow.JsonValue + " ==>>>" + myrowServerDialog.IDCommandMessageType);

                        //if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.Email)
                        //{
                        //    BehinMessage.Messenger objmessage = new BehinMessage.Messenger();
                        //    objmessage.SendMesssage(myRow.JsonValue, myRow.IDCommandMessageType);
                        //}
                        //else if (myRow.IDCommandMessageType == (short)ENUMS.CommandMessageType.OnlineMessage)
                        //{
                            BehinMessage.Messenger objmessage = new BehinMessage.Messenger();
                            objmessage.SendMesssage(myRow.JsonValue, myRow.IDCommandMessageType);
                        //}

                        myrowServerDialog = myRow;
                        myrowServerDialog.received = true;
                        objServerDialog.Update(myrowServerDialog);
                    }

                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + ex.ToString());


                }

            //}
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BehinSignalR.SignalRHelper.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventManager();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadServerDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ReadMessages();
        }
    }
}
