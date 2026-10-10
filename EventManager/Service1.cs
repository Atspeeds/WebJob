using Behin;
using BehinSignalR;
using EventManager.Classes;
using GeneralDataAccess;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace EventManager
{
    partial class Service1 : ServiceBase
    {
        string StrCon1 = "";
        public Service1()
        {
            InitializeComponent();
            Behin.Configuration config = new Behin.Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
            LogUrl = Application.StartupPath + "\\Log\\Log.txt";
        }
        bool IsSeviceStop = false;
        private string LogUrl =  "";
        Thread ThrEvent;
        Thread ThrMessage;
        Thread ThrSemakService;


        private Thread thrServerDialog;
        clsEventManager objEventManager = new clsEventManager();
        clsServerDialog objServerDg = new clsServerDialog();
        clsServerDialog.strcServerDialog myRowServerDg = new clsServerDialog.strcServerDialog();
        clsEntityActionSelf objEntAtion = new clsEntityActionSelf();
        clsEventType objEventType = new clsEventType();

        //BehinSignalR.clsSignalRStruct.strEvent myRowEvent = new clsSignalRStruct.strEvent();

        private void SetDefualt()
        {
            // initSignalRConnection();
            ThrEvent = new Thread(new ThreadStart(EventManager));
            thrServerDialog = new Thread(new ThreadStart(ReadServerDialog));
            ThrMessage = new Thread(new ThreadStart(ReadMessages));
            ThrSemakService = new Thread(new ThreadStart(SemakService));
        }

        public async void SemakService()
        {
            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + "semak service starting");
            string result = "";
            while (!IsSeviceStop)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("serviceurl"));
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = await client.PostAsync("company/sync", null);
                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                            // handle success
                        }
                        else
                        {
                            // handle error
                            result = await response.Content.ReadAsStringAsync();
                        }

                    }
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + "semak service result : " +  result);
                    Thread.Sleep(TimeSpan.FromHours(24));

                }
                catch (Exception er)
                {
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + er.ToString());
                    Thread.Sleep(TimeSpan.FromMinutes(20));
                }
            }
            
        }

        public void EventManager()
        {
            clsEvent objEvent = new clsEvent();
            clsEvent.strcEvent myRowEvent = new clsEvent.strcEvent();
            DMSService.EventManagerWebService objEventManagerWebService = new DMSService.EventManagerWebService();
            while (!IsSeviceStop)
            {
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
                            string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), dt1.Rows[i]["date"].ToString(),int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine +strconclusion);
                            if (strconclusion != "")
                            {
                                bool res = objEventManagerWebService.OccurDiagramEvent(new DMSService.strcStepRelationEvent()
                                {
                                   
                                    NoDiagram = int.Parse(strconclusion.Split('&')[0]),
                                    NoRun = int.Parse(strconclusion.Split('&')[1]),
                                    NoStepRelation = int.Parse(strconclusion.Split('&')[2]),
                                    NoEntity = int.Parse(strconclusion.Split('&')[3]) ,
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

                                Thread.Sleep(1000);
                            }
                        }
                        else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.Waiting)
                        {
                            string strconclusion = objBRMS.DMSMotor(int.Parse(dt1.Rows[i]["No"].ToString()), Convert.ToDateTime(dt1.Rows[i]["date"]).ToString("yyyy-MM-dd HH:mm"),int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()), DateTime.Now.ToString("HH:mm"));
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

                                Thread.Sleep(1000);
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
                                //clsServerDialog objServerDg = new clsServerDialog();
                                //clsServerDialog.strcServerDialog myRowServerDg = new clsServerDialog.strcServerDialog();
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

                                Thread.Sleep(1000);
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

                                Thread.Sleep(1000);
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
                                    Message = strconclusion,
                                    lstReiceivers = new List<int> { int.Parse(dt1.Rows[i]["General"].ToString()) },
                                    MessageType = ENUMS.MessageType.OnlineMessage,
                                    NoEntity = int.Parse(dt1.Rows[i]["NoSrc"].ToString()),
                                    NoSender = -1,
                                    NoSrcTable = -1,
                                    Parameter = new clsSignalRStruct.OnlineMessage(),

                                };
                                //clsServerDialog objServerDg = new clsServerDialog();
                                //clsServerDialog.strcServerDialog myRowServerDg = new clsServerDialog.strcServerDialog();
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

                                Thread.Sleep(1000);
                            }
                        }
                        else if (int.Parse(dt1.Rows[i]["IdEventEffect"].ToString()) == (int)ENUM.EffectType.SystemProcedure)
                        {

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
                                //clsServerDialog objServerDg1 = new clsServerDialog();
                                //clsServerDialog.strcServerDialog myRowServerDg1 = new clsServerDialog.strcServerDialog();
                                objServerDg.GetNewRow();
                                myRowServerDg.IDCommandMessageType = (int)BehinSignalR.ENUMS.CommandMessageType.OnlineMessage;
                                myRowServerDg.IDServerType = (int)BehinSignalR.ENUMS.ServerType.EventManager;
                                myRowServerDg.NoOprationEnvironment = 0;
                                myRowServerDg.received = false;
                                myRowServerDg.JsonValue = JsonConvert.SerializeObject(extMessage);
                                objServerDg.Insert(myRowServerDg);
                            }
                            int inttimer = 0;
                            int count = objEntAtion.SelectMagnifierFolderEntity(int.Parse(dt1.Rows[i]["General"].ToString()), int.Parse(dt1.Rows[i]["NoSrc"].ToString()));
                            if (count > -1)
                            {
                                List<clsEventType.strcEventType> lstEventType = objEventType.Select("No", "2"); /////ذره بین
                                if (lstEventType.Count > 0)
                                    inttimer = lstEventType[0].Timer;
                                clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcLenz> myevent = new clsSignalRStruct.strcEvent<clsSignalRStruct.strcLenz>
                                {
                                    DateTo = Convert.ToDateTime(dt1.Rows[i]["DateTo"].ToString()).AddMinutes(inttimer),
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

                                Thread.Sleep(1000);
                        }
                    }
                    if (IsSeviceStop)
                        ThrEvent.Abort();
                    Thread.Sleep(TimeSpan.FromMinutes(60));
                }
                catch (Exception er)
                {
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + er.ToString());
                    Thread.Sleep(TimeSpan.FromMinutes(20));
                }
            }
        }

        public void ReadServerDialog()
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            clsServerDialog objServerDialog = new clsServerDialog();
            clsServerDialog.strcServerDialog myrowServerDialog = new clsServerDialog.strcServerDialog();
            string Query = "";
            while (!IsSeviceStop)
            {
                try
                {

                    List<clsServerDialog.strcServerDialog> lstServerDialogs = objServerDialog.Select("received", "0",
                        " IDServerType=712 and IDCommandMessageType not in (856,859)");

                    foreach (clsServerDialog.strcServerDialog myRow in lstServerDialogs)
                    {

                        System.IO.File.AppendAllText(LogUrl, Environment.NewLine + myRow.JsonValue + " ****" + myRow.IDCommandMessageType );

                        if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.CreateEvent)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent>>(myRow.JsonValue);
                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + "CreateEvent");
                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType,myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.Wating)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<int> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<int>>(myRow.JsonValue);

                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.Delay)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<int> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<int>>(myRow.JsonValue);

                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.Trace)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.srtcTrace> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.srtcTrace>>(myRow.JsonValue);
                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.Lenz)
                        {
                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcLenz> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcLenz>>(myRow.JsonValue);
                            objEventManager.FillEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.RemoveEvent)
                        {

                            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + myRow.JsonValue + " ****" + myRow.IDCommandMessageType);

                            BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent> myRowEvent =
                                            Newtonsoft.Json.JsonConvert
                                                .DeserializeObject
                                                <BehinSignalR.clsSignalRStruct.strcEvent<BehinSignalR.clsSignalRStruct.strcStepRelationEvent>>(myRow.JsonValue);

                            objEventManager.RemoveEventDiagram(myRowEvent.DateTo, (int)myRowEvent.EffectType,
                                (int)myRowEvent.EventType, myRowEvent.NoEntity, myRowEvent.NoTable, (int)myRowEvent.ReminderType, myRowEvent.Parameter);
                        }
                        else if (myRow.IDCommandMessageType == (int)ENUMS.CommandMessageType.Session)
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
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(TimeSpan.FromMinutes(10));

                }
                catch (Exception ex)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(10));
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + ex.ToString());

                }

            }
        }

        public void ReadMessages()
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            clsServerDialog objServerDialog = new clsServerDialog();
            clsServerDialog.strcServerDialog myrowServerDialog = new clsServerDialog.strcServerDialog();
            while (!IsSeviceStop)
            {
                try
                {

                    List<clsServerDialog.strcServerDialog> lstServerDialogs = objServerDialog.Select("received", "0",
                        " IDServerType=712 and IDCommandMessageType in (856,859)");
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
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(TimeSpan.FromMinutes(2));

                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + ex.ToString());
                    Thread.Sleep(TimeSpan.FromMinutes(2));
                   

                }

            }
        }
        //private void initSignalRConnection()
        //{
        //    try
        //    {


        //        clsSignalRStruct.strcServer myServer = new clsSignalRStruct.strcServer();
        //        myServer.ConnectionId = "";

        //        myServer.ServerType = ENUMS.ServerType.EventManager;
        //        BehinSignalR.SignalRHelper.ServerURI = Configuration.loadServerURIFromFile();
        //        BehinSignalR.SignalRHelper.HubName = Configuration.loadHubNameFromFile();

        //        BehinSignalR.SignalRHelper.NeedLog = true;



        //        BehinSignalR.SignalRHelper.QueryString = string.Format("ClientType={1}&Server={0}",
        //            Newtonsoft.Json.JsonConvert.SerializeObject(myServer),
        //            (int)BehinSignalR.ENUMS.SignalRClientType.Server);
        //        bool hi = BehinSignalR.SignalRHelper.Connect();


        //        BehinSignalR.SignalRHelper.HubProxy.On<ENUMS.CommandMessageType, string>(
        //            "ClientMessageFromServer", ClientMessageFromServer);
        //    }
        //    catch (Exception e)
        //    {
        //        //System.IO.File.AppendAllText("C:\\EventManager\\1.txt", Environment.NewLine + e.ToString());
        //    }
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   در صورت دریافت پیام از سیگنال ار این متد فراخوانی می شود
        ///             و با توجه به نوع پیام عملیات مرتبط رو انجام می دهد </summary>
        ///
        /// <remarks>   Bahrami, 9/26/2017. </remarks>
        ///
        /// <param name="MessageType">  Type of the message. </param>
        /// <param name="JsonString">   The JSON string. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        //private void ClientMessageFromServer(ENUMS.CommandMessageType MessageType, string JsonString)
        //{
        //    System.IO.File.AppendAllText(LogUrl, Environment.NewLine + JsonString);
        //    switch (MessageType)
        //    {
        //        case ENUMS.CommandMessageType.CreateEvent:
        //            BehinSignalR.clsSignalRStruct.strEvent myRowEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<BehinSignalR.clsSignalRStruct.strEvent>(JsonString);
        //            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + "add Event");
        //            objEventManager.FillEventDiagram(myRowEvent.NoEntity, myRowEvent.NoDiagram, myRowEvent.NoStepRelation, myRowEvent.DateFrom, myRowEvent.DateTo, myRowEvent.NoRun);
        //            break;
        //        case ENUMS.CommandMessageType.RemoveEvent:
        //            myRowEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<BehinSignalR.clsSignalRStruct.strEvent>(JsonString);
        //            System.IO.File.AppendAllText(LogUrl, Environment.NewLine + "Remove Event");
        //            objEventManager.RemoveEventDiagram(myRowEvent.NoEntity, myRowEvent.NoDiagram, myRowEvent.NoStepRelation, myRowEvent.DateFrom, myRowEvent.DateTo, myRowEvent.NoRun);
        //            break;
        //    }
        //}

        protected override void OnStart(string[] args)
        {
            SetDefualt();
            ThrEvent.Start();
            thrServerDialog.Start();
            ThrMessage.Start();
            ThrSemakService.Start();
        }

        protected override void OnStop()
        {
            try
            {
                //BehinSignalR.SignalRHelper.Disconnect();
                IsSeviceStop = true;
                ThrEvent.Abort();
                thrServerDialog.Abort();
                ThrMessage.Abort();
                ThrSemakService.Abort();
            }
            catch (Exception er)
            {
                // LibEvent.clsLogginSelf.Write2Log(er.ToString(), "EvantManager", "");
            }
        }
    }
}
