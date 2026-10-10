using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Behin
{
    public partial class clsEvent
    {
        string StrCon1;
        string StrDb;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        [Serializable]
        public struct strcEvent
        {
            public int No;
            public string Name;
            public string General;
            public short IdEvent;
            public bool IsOccured;
            public int NoEventSource;
            public int NoStepRelation;
            public int NoEventSourceType;
            public System.DateTime Date;
            public string General2;
            public int NoRun;
            public string Message;
            public int NoOprationEnvironment;
            public int NoRuleNode;
            public bool HasNeedDetail;
            public int NoTables;
            public int NoSrc;
            public int IDEventEffect;
            public int IDReminderType;
            public int NoEventType;

        } //End Struct

        public clsEvent()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcEvent GetNewRow()
        {
            strcEvent objNewStruct =  new strcEvent() ;
            objNewStruct.Name = ""; 
            objNewStruct.General = ""; 
            objNewStruct.Date = DateTime.Parse("1800-01-01"); 
            objNewStruct.General2 = ""; 
            objNewStruct.Message = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance DELETE : Event --> AgentBased --> 
        /// </summary>
        public int Delete(strcEvent myStruct)
        {
            if(CheckDelete(myStruct.No))
            {

                Event.Event DAdlt = new Event.Event(StrCon1);
                Event.Event_DS.DS_EventRow dtRow = new Event.Event_DS.DS_EventDataTable().NewDS_EventRow();

            dtRow.No = myStruct.No;
            if (myStruct.Name.Contains('ك') || myStruct.Name.Contains('ي'))
            {
                myStruct.Name = Utility.SafeArabicStr(myStruct.Name); 
            }
            dtRow.Name = myStruct.Name;
            if (myStruct.General.Contains('ك') || myStruct.General.Contains('ي'))
            {
                myStruct.General = Utility.SafeArabicStr(myStruct.General); 
            }
            dtRow.General = myStruct.General;
            dtRow.IdEvent = myStruct.IdEvent;
            dtRow.IsOccured = myStruct.IsOccured;
            dtRow.NoEventSource = myStruct.NoEventSource;
            dtRow.NoStepRelation = myStruct.NoStepRelation;
            dtRow.NoEventSourceType = myStruct.NoEventSourceType;
            dtRow.Date = myStruct.Date;
            if (myStruct.General2.Contains('ك') || myStruct.General2.Contains('ي'))
            {
                myStruct.General2 = Utility.SafeArabicStr(myStruct.General2); 
            }
            dtRow.General2 = myStruct.General2;
            dtRow.NoRun = myStruct.NoRun;
            if (myStruct.Message.Contains('ك') || myStruct.Message.Contains('ي'))
            {
                myStruct.Message = Utility.SafeArabicStr(myStruct.Message); 
            }
            dtRow.Message = myStruct.Message;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            dtRow.NoRuleNode = myStruct.NoRuleNode;
            dtRow.HasNeedDetail = myStruct.HasNeedDetail;
            dtRow.NoTables = myStruct.NoTables;
            dtRow.NoSrc = myStruct.NoSrc;
            dtRow.IDEventEffect = myStruct.IDEventEffect;
            dtRow.IDReminderType = myStruct.IDReminderType;
            dtRow.NoEventType = myStruct.NoEventType;

                DAdlt.DS_EventDataTable_DeleteCommand_SP(dtRow);

            }

            return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"DELETE From [Event] where " + FieldName + " = {0}" , Value);
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }
        public string Delete(string FieldName,string Value,int TblNo)
        {
            string strdelete = "";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Strrel = "select * from Relations where SrcTableNo = dbo.FuncEncryptTableNo(" + TblNo + ") and IDDeleteActionType = -1";
            DataTable dtrel = DAC.GeneralSelectCommand(new SqlCommand(Strrel));
            if (dtrel.Rows.Count > 0)
            {
                Delete(FieldName, Value);
                strdelete = "";
            }
            else
            {
                SqlConnection conn = null;
                SqlDataReader rdr = null;
                int intDelete = 0;
                conn = new SqlConnection(StrCon1);
                conn.Open();
                SqlCommand cmd = new SqlCommand("AllowDelete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TblNo", TblNo));
                cmd.Parameters.Add(new SqlParameter("@No", int.Parse(Value)));
                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteScalar();
                intDelete = (int)returnParameter.Value;
                if (intDelete > 0)
                {
                    string Str = "select * From tables where no = dbo.FuncDecryptTableNo( " + intDelete + ")";
                    DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(Str));
                    if (dt.Rows.Count > 0)
                        strdelete = "امكان حذف در جدول  " + dt.Rows[0]["name"].ToString() + " وجود ندارد .";
                }
                else
                {
                    cmd = new SqlCommand("DeleteRecord", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TblSrcNo", DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@TblDestNo", TblNo));
                    cmd.Parameters.Add(new SqlParameter("@No", Value));
                    cmd.Parameters.Add(new SqlParameter("@IdDeleteActionType", 767));
                    cmd.Parameters.Add(new SqlParameter("@NoParent", -1));
                    rdr = cmd.ExecuteReader();
                }
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
            return strdelete;
        } 
  //End Delete Function

        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance INSERT: Event --> 
        /// </summary>
        public int Insert( clsEvent.strcEvent myStructEvent )
        {
            Dictionary<string, object> Dic = new Dictionary<string, object>();

            Event.Event DAinsrt = new Event.Event(StrCon1);

            Dic.Add( "Event_No" , myStructEvent.No);
            if (myStructEvent.Name.Contains('ك') || myStructEvent.Name.Contains('ي'))
            {
                myStructEvent.Name = Utility.SafeArabicStr(myStructEvent.Name); 
            }
            Dic.Add( "Event_Name" , myStructEvent.Name);
            if (myStructEvent.General.Contains('ك') || myStructEvent.General.Contains('ي'))
            {
                myStructEvent.General = Utility.SafeArabicStr(myStructEvent.General); 
            }
            Dic.Add( "Event_General" , myStructEvent.General);
            Dic.Add( "Event_IdEvent" , myStructEvent.IdEvent);
            Dic.Add( "Event_IsOccured" , myStructEvent.IsOccured);
            Dic.Add( "Event_NoEventSource" , myStructEvent.NoEventSource);
            Dic.Add( "Event_NoStepRelation" , myStructEvent.NoStepRelation);
            Dic.Add( "Event_NoEventSourceType" , myStructEvent.NoEventSourceType);
            Dic.Add( "Event_Date" , myStructEvent.Date);
            if (myStructEvent.General2.Contains('ك') || myStructEvent.General2.Contains('ي'))
            {
                myStructEvent.General2 = Utility.SafeArabicStr(myStructEvent.General2); 
            }
            Dic.Add( "Event_General2" , myStructEvent.General2);
            Dic.Add( "Event_NoRun" , myStructEvent.NoRun);
            if (myStructEvent.Message.Contains('ك') || myStructEvent.Message.Contains('ي'))
            {
                myStructEvent.Message = Utility.SafeArabicStr(myStructEvent.Message); 
            }
            Dic.Add( "Event_Message" , myStructEvent.Message);
            Dic.Add( "Event_NoOprationEnvironment" , myStructEvent.NoOprationEnvironment);
            Dic.Add( "Event_NoRuleNode" , myStructEvent.NoRuleNode);
            Dic.Add( "Event_HasNeedDetail" , myStructEvent.HasNeedDetail);
            Dic.Add( "Event_NoTables" , myStructEvent.NoTables);
            Dic.Add( "Event_NoSrc" , myStructEvent.NoSrc);
            Dic.Add( "Event_IDEventEffect" , myStructEvent.IDEventEffect);
            Dic.Add( "Event_IDReminderType" , myStructEvent.IDReminderType);
            Dic.Add( "Event_NoEventType" , myStructEvent.NoEventType);


            return DAinsrt.DS_EventDataTable_InsertCommand_SP(Dic);



        }  //End Insert Function


        public int Update(strcEvent myStruct)
        {
            Event.Event DAupdt = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventRow dtRow = new Event.Event_DS.DS_EventDataTable().NewDS_EventRow();

            dtRow.No = myStruct.No;
            if (myStruct.Name.Contains('ك') || myStruct.Name.Contains('ي'))
            {
                myStruct.Name = Utility.SafeArabicStr(myStruct.Name); 
            }
            dtRow.Name = myStruct.Name;
            if (myStruct.General.Contains('ك') || myStruct.General.Contains('ي'))
            {
                myStruct.General = Utility.SafeArabicStr(myStruct.General); 
            }
            dtRow.General = myStruct.General;
            dtRow.IdEvent = myStruct.IdEvent;
            dtRow.IsOccured = myStruct.IsOccured;
            dtRow.NoEventSource = myStruct.NoEventSource;
            dtRow.NoStepRelation = myStruct.NoStepRelation;
            dtRow.NoEventSourceType = myStruct.NoEventSourceType;
            dtRow.Date = myStruct.Date;
            if (myStruct.General2.Contains('ك') || myStruct.General2.Contains('ي'))
            {
                myStruct.General2 = Utility.SafeArabicStr(myStruct.General2); 
            }
            dtRow.General2 = myStruct.General2;
            dtRow.NoRun = myStruct.NoRun;
            if (myStruct.Message.Contains('ك') || myStruct.Message.Contains('ي'))
            {
                myStruct.Message = Utility.SafeArabicStr(myStruct.Message); 
            }
            dtRow.Message = myStruct.Message;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            dtRow.NoRuleNode = myStruct.NoRuleNode;
            dtRow.HasNeedDetail = myStruct.HasNeedDetail;
            dtRow.NoTables = myStruct.NoTables;
            dtRow.NoSrc = myStruct.NoSrc;
            dtRow.IDEventEffect = myStruct.IDEventEffect;
            dtRow.IDReminderType = myStruct.IDReminderType;
            dtRow.NoEventType = myStruct.NoEventType;

            DAupdt.DS_EventDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry = string.Format(@"Update [Event] SET " + FieldsSetString + " WHERE No = {0} " , KeyValue);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEvent> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEvent> myStrcList = new List<strcEvent>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"SELECT * From [Event] Where " + FieldName + " = {0}" , Value);
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEvent myStruct = new strcEvent();
                            myStruct.No = (int)dt.Rows[i]["No"];
            string myrowName = (string)dt.Rows[i]["Name"];
            if (myrowName.Contains('ك') || myrowName.Contains('ي'))
            {
                myrowName = Utility.SafeArabicStr(myrowName); 
        }
            myStruct.Name = myrowName;
            string myrowGeneral = (string)dt.Rows[i]["General"];
            if (myrowGeneral.Contains('ك') || myrowGeneral.Contains('ي'))
            {
                myrowGeneral = Utility.SafeArabicStr(myrowGeneral); 
        }
            myStruct.General = myrowGeneral;
            myStruct.IdEvent = (short)dt.Rows[i]["IdEvent"];
            myStruct.IsOccured = (bool)dt.Rows[i]["IsOccured"];
            myStruct.NoEventSource = (int)dt.Rows[i]["NoEventSource"];
            myStruct.NoStepRelation = (int)dt.Rows[i]["NoStepRelation"];
            myStruct.NoEventSourceType = (int)dt.Rows[i]["NoEventSourceType"];
            myStruct.Date = (System.DateTime)dt.Rows[i]["Date"];
            string myrowGeneral2 = (string)dt.Rows[i]["General2"];
            if (myrowGeneral2.Contains('ك') || myrowGeneral2.Contains('ي'))
            {
                myrowGeneral2 = Utility.SafeArabicStr(myrowGeneral2); 
        }
            myStruct.General2 = myrowGeneral2;
            myStruct.NoRun = (int)dt.Rows[i]["NoRun"];
            string myrowMessage = (string)dt.Rows[i]["Message"];
            if (myrowMessage.Contains('ك') || myrowMessage.Contains('ي'))
            {
                myrowMessage = Utility.SafeArabicStr(myrowMessage); 
        }
            myStruct.Message = myrowMessage;
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
            myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
            myStruct.HasNeedDetail = (bool)dt.Rows[i]["HasNeedDetail"];
            myStruct.NoTables = (int)dt.Rows[i]["NoTables"];
            myStruct.NoSrc = (int)dt.Rows[i]["NoSrc"];
            myStruct.IDEventEffect = (int)dt.Rows[i]["IDEventEffect"];
            myStruct.IDReminderType = (int)dt.Rows[i]["IDReminderType"];
            myStruct.NoEventType = (int)dt.Rows[i]["NoEventType"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcEvent> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcEvent> myStrcList = new List<strcEvent>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Event] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEvent myStruct = new strcEvent();
                            myStruct.No = (int)dt.Rows[i]["No"];
            string myrowName = (string)dt.Rows[i]["Name"];
            if (myrowName.Contains('ك') || myrowName.Contains('ي'))
            {
                myrowName = Utility.SafeArabicStr(myrowName); 
        }
            myStruct.Name = myrowName;
            string myrowGeneral = (string)dt.Rows[i]["General"];
            if (myrowGeneral.Contains('ك') || myrowGeneral.Contains('ي'))
            {
                myrowGeneral = Utility.SafeArabicStr(myrowGeneral); 
        }
            myStruct.General = myrowGeneral;
            myStruct.IdEvent = (short)dt.Rows[i]["IdEvent"];
            myStruct.IsOccured = (bool)dt.Rows[i]["IsOccured"];
            myStruct.NoEventSource = (int)dt.Rows[i]["NoEventSource"];
            myStruct.NoStepRelation = (int)dt.Rows[i]["NoStepRelation"];
            myStruct.NoEventSourceType = (int)dt.Rows[i]["NoEventSourceType"];
            myStruct.Date = (System.DateTime)dt.Rows[i]["Date"];
            string myrowGeneral2 = (string)dt.Rows[i]["General2"];
            if (myrowGeneral2.Contains('ك') || myrowGeneral2.Contains('ي'))
            {
                myrowGeneral2 = Utility.SafeArabicStr(myrowGeneral2); 
        }
            myStruct.General2 = myrowGeneral2;
            myStruct.NoRun = (int)dt.Rows[i]["NoRun"];
            string myrowMessage = (string)dt.Rows[i]["Message"];
            if (myrowMessage.Contains('ك') || myrowMessage.Contains('ي'))
            {
                myrowMessage = Utility.SafeArabicStr(myrowMessage); 
        }
            myStruct.Message = myrowMessage;
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
            myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
            myStruct.HasNeedDetail = (bool)dt.Rows[i]["HasNeedDetail"];
            myStruct.NoTables = (int)dt.Rows[i]["NoTables"];
            myStruct.NoSrc = (int)dt.Rows[i]["NoSrc"];
            myStruct.IDEventEffect = (int)dt.Rows[i]["IDEventEffect"];
            myStruct.IDReminderType = (int)dt.Rows[i]["IDReminderType"];
            myStruct.NoEventType = (int)dt.Rows[i]["NoEventType"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"SELECT * From [Event] Where " + FieldName + " = {0}" , Value);
            String FiledPersianName = "نام رويداد,عمومي,نوع رويداد,رخ داده است؟,شناسه مبدا رويداد,شناسه روابط مراحل,نوع مبدا رويداد,تاريخ,عمومي 2,شناسه اجراي نمودار,پيام,شناسه سامانه شرکت,شناسه نودهاي قاعده,نياز به جزييات دارد,شناسه جداول,شماره جدولي,اثرات رويداد,نوع يادآوري رويداد,انواع رويداد";
            String FiledEnglishName = "Event.Name,Event.General,Event.IdEvent,Event.IsOccured,Event.NoEventSource,StepRelation.PersianName,Event.NoEventSourceType,Event.Date,Event.General2,Run.Name,Event.Message,Event.NoOprationEnvironment,RuleNode.AbstractCode,Event.HasNeedDetail,Tables.Name,Event.NoSrc,Event.IDEventEffect,Event.IDReminderType,EventType.Name";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEvent> Select()
        {

            DataTable dt;
            List<strcEvent> myStrcList = new List<strcEvent>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Event]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEvent myStruct = new strcEvent();
                            myStruct.No = (int)dt.Rows[i]["No"];
            string myrowName = (string)dt.Rows[i]["Name"];
            if (myrowName.Contains('ك') || myrowName.Contains('ي'))
            {
                myrowName = Utility.SafeArabicStr(myrowName); 
        }
            myStruct.Name = myrowName;
            string myrowGeneral = (string)dt.Rows[i]["General"];
            if (myrowGeneral.Contains('ك') || myrowGeneral.Contains('ي'))
            {
                myrowGeneral = Utility.SafeArabicStr(myrowGeneral); 
        }
            myStruct.General = myrowGeneral;
            myStruct.IdEvent = (short)dt.Rows[i]["IdEvent"];
            myStruct.IsOccured = (bool)dt.Rows[i]["IsOccured"];
            myStruct.NoEventSource = (int)dt.Rows[i]["NoEventSource"];
            myStruct.NoStepRelation = (int)dt.Rows[i]["NoStepRelation"];
            myStruct.NoEventSourceType = (int)dt.Rows[i]["NoEventSourceType"];
            myStruct.Date = (System.DateTime)dt.Rows[i]["Date"];
            string myrowGeneral2 = (string)dt.Rows[i]["General2"];
            if (myrowGeneral2.Contains('ك') || myrowGeneral2.Contains('ي'))
            {
                myrowGeneral2 = Utility.SafeArabicStr(myrowGeneral2); 
        }
            myStruct.General2 = myrowGeneral2;
            myStruct.NoRun = (int)dt.Rows[i]["NoRun"];
            string myrowMessage = (string)dt.Rows[i]["Message"];
            if (myrowMessage.Contains('ك') || myrowMessage.Contains('ي'))
            {
                myrowMessage = Utility.SafeArabicStr(myrowMessage); 
        }
            myStruct.Message = myrowMessage;
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
            myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
            myStruct.HasNeedDetail = (bool)dt.Rows[i]["HasNeedDetail"];
            myStruct.NoTables = (int)dt.Rows[i]["NoTables"];
            myStruct.NoSrc = (int)dt.Rows[i]["NoSrc"];
            myStruct.IDEventEffect = (int)dt.Rows[i]["IDEventEffect"];
            myStruct.IDReminderType = (int)dt.Rows[i]["IDReminderType"];
            myStruct.NoEventType = (int)dt.Rows[i]["NoEventType"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEvent SelectByNo(strcEvent myStruct)
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            //Event.Event_DS.DS_EventRow dtRow = new Event.Event_DS.DS_EventDataTable().NewDS_EventRow();

            Event.Event_DS.DS_EventDataTable dt = new Event.Event_DS.DS_EventDataTable();

            dt = DAslct.DS_EventDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.Name = "";
                myStruct.General = "";
                myStruct.NoEventSource = 0;
                myStruct.NoStepRelation = 0;
                myStruct.NoEventSourceType = 0;
                myStruct.General2 = "";
                myStruct.NoRun = 0;
                myStruct.Message = "";
                myStruct.NoOprationEnvironment = 0;
                myStruct.NoRuleNode = 0;
                myStruct.NoTables = 0;
                myStruct.NoSrc = 0;
                myStruct.IDEventEffect = 0;
                myStruct.IDReminderType = 0;
                myStruct.NoEventType = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            string myrowName = (string)dt.Rows[0]["Name"];
            if (myrowName.Contains('ك') || myrowName.Contains('ي'))
            {
                myrowName = Utility.SafeArabicStr(myrowName); 
            }
            myStruct.Name = myrowName;
            string myrowGeneral = (string)dt.Rows[0]["General"];
            if (myrowGeneral.Contains('ك') || myrowGeneral.Contains('ي'))
            {
                myrowGeneral = Utility.SafeArabicStr(myrowGeneral); 
            }
            myStruct.General = myrowGeneral;
            myStruct.IdEvent = (short)dt.Rows[0]["IdEvent"];
            myStruct.IsOccured = (bool)dt.Rows[0]["IsOccured"];
            myStruct.NoEventSource = (int)dt.Rows[0]["NoEventSource"];
            myStruct.NoStepRelation = (int)dt.Rows[0]["NoStepRelation"];
            myStruct.NoEventSourceType = (int)dt.Rows[0]["NoEventSourceType"];
            myStruct.Date = (System.DateTime)dt.Rows[0]["Date"];
            string myrowGeneral2 = (string)dt.Rows[0]["General2"];
            if (myrowGeneral2.Contains('ك') || myrowGeneral2.Contains('ي'))
            {
                myrowGeneral2 = Utility.SafeArabicStr(myrowGeneral2); 
            }
            myStruct.General2 = myrowGeneral2;
            myStruct.NoRun = (int)dt.Rows[0]["NoRun"];
            string myrowMessage = (string)dt.Rows[0]["Message"];
            if (myrowMessage.Contains('ك') || myrowMessage.Contains('ي'))
            {
                myrowMessage = Utility.SafeArabicStr(myrowMessage); 
            }
            myStruct.Message = myrowMessage;
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.HasNeedDetail = (bool)dt.Rows[0]["HasNeedDetail"];
            myStruct.NoTables = (int)dt.Rows[0]["NoTables"];
            myStruct.NoSrc = (int)dt.Rows[0]["NoSrc"];
            myStruct.IDEventEffect = (int)dt.Rows[0]["IDEventEffect"];
            myStruct.IDReminderType = (int)dt.Rows[0]["IDReminderType"];
            myStruct.NoEventType = (int)dt.Rows[0]["NoEventType"];

            return myStruct ;

        }
        public strcEvent ClearByNo(strcEvent myStruct)
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventDataTable dt = new Event.Event_DS.DS_EventDataTable();

            dt = DAslct.DS_EventDataTable_SelectCommand(" WHERE No = " + myStruct.No);

                                myStruct.No = (int)dt.Rows[0]["No"];
                myStruct.Name = "";
                myStruct.General = "";
                myStruct.IdEvent = -1;
                myStruct.IsOccured = false;
                myStruct.NoEventSource = -1;
                myStruct.NoStepRelation = -1;
                myStruct.NoEventSourceType = -1;
                myStruct.Date = Convert.ToDateTime("1800-01-01");
                myStruct.General2 = "";
                myStruct.NoRun = -1;
                myStruct.Message = "";
                myStruct.NoOprationEnvironment = -1;
                myStruct.NoRuleNode = -1;
                myStruct.HasNeedDetail = false;
                myStruct.NoTables = -1;
                myStruct.NoSrc = -1;
                myStruct.IDEventEffect = -1;
                myStruct.IDReminderType = -1;
                myStruct.NoEventType = -1;

                return myStruct ;
        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , case when [IsOccured] = 1 then 'بلی' else 'خیر' end as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE TEvent.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TEvent.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TEvent.[NoRuleNode] = [No]), case when [HasNeedDetail] = 1 then 'بلی' else 'خیر' end as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE TEvent.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE TEvent.[NoEventType] = [No]) FROM [Event] AS TEvent";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand  scmd_Class = new SqlCommand(Qry1);
            return DAC.GeneralSelectCommand(scmd_Class);

        }

        public DataTable GetDataTableForGridEdit()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [Name] , [General] , IdEvent, [IsOccured] , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', [NoEventSourceType] , [General2] ,[NoRun] as 'FKNoRun', [Message] , [NoOprationEnvironment] ,[NoRuleNode] as 'FKNoRuleNode', [HasNeedDetail] ,[NoTables] as 'FKNoTables', [NoSrc] , IDEventEffect, IDReminderType,[NoEventType] as 'FKNoEventType' FROM [Event] AS TEvent";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand  scmd_Class = new SqlCommand(Qry1);
            return DAC.GeneralSelectCommand(scmd_Class);

        }  //End FillTable Function


        public int FillThisDataGridView(int NoQuery , int NoGrid , int NoAgent)
        {
            DataTable dt = CheckHasDivision(NoGrid ,NoAgent);
            if (dt.Rows.Count != 0)
            {
                return 0 ;

            }
            dt = CheckHasDisplay(NoQuery , NoGrid);
            if (dt.Rows.Count != 0)
            {
                return 0;

            }
            else
            {
                string Qry1 = "SELECT  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , case when [IsOccured] = 1 then 'بلی' else 'خیر' end as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE TEvent.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TEvent.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TEvent.[NoRuleNode] = [No]), case when [HasNeedDetail] = 1 then 'بلی' else 'خیر' end as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE TEvent.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE TEvent.[NoEventType] = [No]) FROM [Event] AS TEvent";

            }
            return 0 ;

        }


        


        public DataTable GetDataTableDescriptive()
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Event]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptiveParent()
        {
            string Qry = "SELECT No as 'No',Name as 'Name',NoParent FROM [Event]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptiveParent(string strWhere)
        {
            string Qry = "SELECT No as 'No',Name as 'Name',NoParent FROM [Event] where " + strWhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepRelationDescriptive()
        {
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [StepRelation]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepRelationDescriptive(string strName)
        {
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [StepRelation] where PersianName like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepRelationDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [StepRelation] where PersianName like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepRelationDescriptiveParent()
        {
            string Qry = "SELECT No as 'No',PersianName as 'Name',NoParent FROM [StepRelation]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRunDescriptive()
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRunDescriptive(string strName)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRunDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRunDescriptiveParent()
        {
            string Qry = "SELECT No as 'No',Name as 'Name',NoParent FROM [Run]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive()
        {
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive(string strName)
        {
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where AbstractCode like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where AbstractCode like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptiveParent()
        {
            string Qry = "SELECT No as 'No',AbstractCode as 'Name',NoParent FROM [RuleNode]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptive()
        {
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptive(string strName)
        {
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptiveParent()
        {
            string Qry = "SELECT NO as 'No',Name as 'Name',NoParent FROM [Tables]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEventTypeDescriptive()
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EventType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEventTypeDescriptive(string strName)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EventType] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEventTypeDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EventType] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEventTypeDescriptiveParent()
        {
            string Qry = "SELECT No as 'No',Name as 'Name',NoParent FROM [EventType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


        public DataTable GetDataTableIdEventDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 0 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdEventDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 0 ORDER BY " + OrderName + " " + OrderSort;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDEventEffectDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 779 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDEventEffectDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 779 ORDER BY " + OrderName + " " + OrderSort;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDReminderTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 780 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDReminderTypeDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 780 ORDER BY " + OrderName + " " + OrderSort;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYLookUp






        public int GetNo()
        {
            string Query = "INSERT Entity(Name) VALUES ('') SELECT No from Entity where No = SCOPE_IDENTITY()";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Query);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = DAC.GeneralSelectCommand(scmd_Class);
            if(dt.Rows.Count != 0)
            {
                int No = int.Parse(dt.Rows[0]["No"].ToString());
                Query = "Delete From Entity Where No not in(" + No + ")";
                scmd_Class = new SqlCommand(Query);
                DAC.GeneralDeleteCommand(scmd_Class);
                return No;
            }
            return -1;
        }



        public Dictionary<int,bool> VerifyInsert()
        {

            Dictionary<int,bool> FieldsListFill =  new Dictionary<int,bool>();

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001069 + ")";

            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Query);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = DAC.GeneralSelectCommand(scmd_Class);

            foreach(DataRow dr in dt.Rows)
            {
                FieldsListFill.Add(int.Parse(dr["NO"].ToString()), bool.Parse(dr["Fill"].ToString()));
            }

            return FieldsListFill;

        }



        public string GetAtt( int No , int NoFieldget)
        {

            string[] FieldName = GetFieldName(NoFieldget);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [Event]." + FieldName[0] + " From Event Where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd).Rows[0][0].ToString();

        }


        private string[] GetFieldName(int NoField)
        {

            string[] Field = new string[2];
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT Fields.Name , Fields.FieldDataTypeNo FROM Fields WHERE (Fields.No = @noField)";
            SqlCommand sqlCmnd = new SqlCommand(Str);
            sqlCmnd.Parameters.Add("@noField",DbType.Int32);
            sqlCmnd.Parameters[0].Value = NoField ;

            Field[0] = DAC.GeneralSelectCommand(sqlCmnd).Rows[0][0].ToString();//name
            Field[1] = DAC.GeneralSelectCommand(sqlCmnd).Rows[0][1].ToString();//type

            return Field;        }

        public void SetAtt( int No , int NoFieldset, string value)
        {

            string[] FieldName = GetFieldName(NoFieldset);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [Event] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [Event] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , case when [IsOccured] = 1 then 'بلی' else 'خیر' end as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE TEvent.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TEvent.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TEvent.[NoRuleNode] = [No]), case when [HasNeedDetail] = 1 then 'بلی' else 'خیر' end as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE TEvent.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE TEvent.[NoEventType] = [No]) From [Event] AS TEvent Where TEvent." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }
        public DataTable GetDataTableForGrid(string FieldName , string Value , string strwhere)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , [IsOccured] as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE Event.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE Event.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE Event.[NoRuleNode] = [No]), [HasNeedDetail] as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE Event.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE Event.[NoEventType] = [No]) From [Event] AS Event Where Event." + FieldName + " = " +  Value + " and " + strwhere ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }
        public DataTable GetDataTableForGrid(string strwhere)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , [IsOccured] as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE Event.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE Event.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE Event.[NoRuleNode] = [No]), [HasNeedDetail] as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE Event.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE Event.[NoEventType] = [No]) From [Event] AS Event Where " + strwhere;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }
        public DataTable GetDataTableForGridEdit(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] , [General] , IdEvent, [IsOccured] , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', [NoEventSourceType] , [General2] ,[NoRun] as 'FKNoRun', [Message] , [NoOprationEnvironment] ,[NoRuleNode] as 'FKNoRuleNode', [HasNeedDetail] ,[NoTables] as 'FKNoTables', [NoSrc] , IDEventEffect, IDReminderType,[NoEventType] as 'FKNoEventType' From [Event] AS TEvent Where TEvent." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }
        public string FindSearchName(string Name)
        {
            string SearchName = "";

            switch (Name)
            {
              case "No":
                SearchName = "No";
                 break; 
             case "Name":
                SearchName = "Name";
                 break; 
             case "General":
                SearchName = "General";
                 break; 
             case "IdEvent":
                SearchName = "(Select Name from [LookUp] Where [LookUp].No = [IdEvent])";
                 break; 
             case "IsOccured":
                SearchName = "IsOccured";
                 break; 
             case "NoEventSource":
                SearchName = "NoEventSource";
                 break; 
            case "NoStepRelation":
                SearchName = "(SELECT [PersianName] FROM [StepRelation] WHERE TEvent.[NoStepRelation] = [No])";
                break;
             case "NoEventSourceType":
                SearchName = "NoEventSourceType";
                 break; 
            case "Date":
                SearchName = "dbo.mtosh([Date])";
                break;
             case "General2":
                SearchName = "General2";
                 break; 
            case "NoRun":
                SearchName = "(SELECT [Name] FROM [Run] WHERE TEvent.[NoRun] = [No])";
                break;
             case "Message":
                SearchName = "Message";
                 break; 
             case "NoOprationEnvironment":
                SearchName = "NoOprationEnvironment";
                 break; 
            case "NoRuleNode":
                SearchName = "(SELECT [AbstractCode] FROM [RuleNode] WHERE TEvent.[NoRuleNode] = [No])";
                break;
             case "HasNeedDetail":
                SearchName = "HasNeedDetail";
                 break; 
            case "NoTables":
                SearchName = "(SELECT [Name] FROM [Tables] WHERE TEvent.[NoTables] = [NO])";
                break;
             case "NoSrc":
                SearchName = "NoSrc";
                 break; 
             case "IDEventEffect":
                SearchName = "(Select Name from [LookUp] Where [LookUp].No = [IDEventEffect])";
                 break; 
             case "IDReminderType":
                SearchName = "(Select Name from [LookUp] Where [LookUp].No = [IDReminderType])";
                 break; 
            case "NoEventType":
                SearchName = "(SELECT [Name] FROM [EventType] WHERE TEvent.[NoEventType] = [No])";
                break;

            }
            return SearchName;
        }
        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

        if (Value.Contains('ك') || Value.Contains('ي'))
        {
            Value = Utility.SafeArabicStr(Value);
        }
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            FieldName = FindSearchName(FieldName);
            string Str = "Select  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , case when [IsOccured] = 1 then 'بلی' else 'خیر' end as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE TEvent.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TEvent.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TEvent.[NoRuleNode] = [No]), case when [HasNeedDetail] = 1 then 'بلی' else 'خیر' end as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE TEvent.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE TEvent.[NoEventType] = [No]) From [Event] AS TEvent Where " + FieldName  + " like N'%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value,string strwhere)
        {

        if (Value.Contains('ك') || Value.Contains('ي'))
        {
            Value = Utility.SafeArabicStr(Value);
        }
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            FieldName = FindSearchName(FieldName);
            string Str = "Select  [No] as 'PKNo', [Name] as 'نام رويداد' , [General] as 'عمومي'  , (Select Name from [LookUp] Where [LookUp].No = [IdEvent]) as 'نوع رويداد' , case when [IsOccured] = 1 then 'بلی' else 'خیر' end as 'رخ داده است؟' , [NoEventSource] as 'FKNoEventSource' ,[NoStepRelation] as 'FKNoStepRelation', 'شناسه روابط مراحل' = (SELECT [PersianName] FROM [StepRelation] WHERE TEvent.[NoStepRelation] = [No]), [NoEventSourceType] as 'نوع مبدا رويداد' ,case when [Date] = '1800/01/01' then '' else dbo.mtosh( [Date] ) end as 'تاريخ', [General2] as 'عمومي 2' ,[NoRun] as 'FKNoRun', 'شناسه اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TEvent.[NoRun] = [No]), [Message] as 'پيام' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' ,[NoRuleNode] as 'FKNoRuleNode', 'شناسه نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TEvent.[NoRuleNode] = [No]), case when [HasNeedDetail] = 1 then 'بلی' else 'خیر' end as 'نياز به جزييات دارد' ,[NoTables] as 'FKNoTables', 'شناسه جداول' = (SELECT [Name] FROM [Tables] WHERE TEvent.[NoTables] = [NO]), [NoSrc] as 'شماره جدولي'  , (Select Name from [LookUp] Where [LookUp].No = [IDEventEffect]) as 'اثرات رويداد'  , (Select Name from [LookUp] Where [LookUp].No = [IDReminderType]) as 'نوع يادآوري رويداد' ,[NoEventType] as 'FKNoEventType', 'انواع رويداد' = (SELECT [Name] FROM [EventType] WHERE TEvent.[NoEventType] = [No]) From [Event] AS TEvent Where " + FieldName + " like 'N%" +  Value  + "%' and " + strwhere;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001069) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001069) " + 
                "and RelTypeNo = 2 and Tables.IsVirtual = 'False' " ;
            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand cmd = new SqlCommand(str);
            dt = DAC.GeneralSelectCommand(cmd);

            DataTable dtCheck = new DataTable();
            foreach (DataRow dr in dt.Rows)
            {
                string StrSelect = "Select [" + dr["FieldName"].ToString() + "] From [" + 
                    dr["TableName"].ToString() + "] Where [" + dr["FieldName"].ToString() + "] = " + No ;
                dtCheck.Rows.Clear();
                DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                cmd = new System.Data.SqlClient.SqlCommand(StrSelect);
                dtCheck = DAC.GeneralSelectCommand(cmd);
                if (dtCheck.Rows.Count != 0)
                {
                    return false;
                }
            }

            return true;

        }


        public strcEvent GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEvent.strcEvent myStruct = new clsEvent.strcEvent();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Event] Where [Event]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            string myrowName = (string)dt.Rows[0]["Name"];
            if (myrowName.Contains('ك') || myrowName.Contains('ي'))
            {
                myrowName = Utility.SafeArabicStr(myrowName); 
            }
            myStruct.Name = myrowName;
            string myrowGeneral = (string)dt.Rows[0]["General"];
            if (myrowGeneral.Contains('ك') || myrowGeneral.Contains('ي'))
            {
                myrowGeneral = Utility.SafeArabicStr(myrowGeneral); 
            }
            myStruct.General = myrowGeneral;
            myStruct.IdEvent = (short)dt.Rows[0]["IdEvent"];
            myStruct.IsOccured = (bool)dt.Rows[0]["IsOccured"];
            myStruct.NoEventSource = (int)dt.Rows[0]["NoEventSource"];
            myStruct.NoStepRelation = (int)dt.Rows[0]["NoStepRelation"];
            myStruct.NoEventSourceType = (int)dt.Rows[0]["NoEventSourceType"];
            myStruct.Date = (System.DateTime)dt.Rows[0]["Date"];
            string myrowGeneral2 = (string)dt.Rows[0]["General2"];
            if (myrowGeneral2.Contains('ك') || myrowGeneral2.Contains('ي'))
            {
                myrowGeneral2 = Utility.SafeArabicStr(myrowGeneral2); 
            }
            myStruct.General2 = myrowGeneral2;
            myStruct.NoRun = (int)dt.Rows[0]["NoRun"];
            string myrowMessage = (string)dt.Rows[0]["Message"];
            if (myrowMessage.Contains('ك') || myrowMessage.Contains('ي'))
            {
                myrowMessage = Utility.SafeArabicStr(myrowMessage); 
            }
            myStruct.Message = myrowMessage;
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.HasNeedDetail = (bool)dt.Rows[0]["HasNeedDetail"];
            myStruct.NoTables = (int)dt.Rows[0]["NoTables"];
            myStruct.NoSrc = (int)dt.Rows[0]["NoSrc"];
            myStruct.IDEventEffect = (int)dt.Rows[0]["IDEventEffect"];
            myStruct.IDReminderType = (int)dt.Rows[0]["IDReminderType"];
            myStruct.NoEventType = (int)dt.Rows[0]["NoEventType"];


            return myStruct;

        }




        public ArrayList FillArrayNumComplete(ArrayList lstcmp, strcEvent myRow)
        {

             ArrayList lstcomplete = new ArrayList();
            for (int cnt = 0; cnt < lstcmp.Count; cnt++)
            {
                switch (lstcmp[cnt].ToString())
                {
                    case "No":
                        lstcomplete.Add(myRow.No.ToString());
                        break;
                    case "Name":
                        lstcomplete.Add(myRow.Name.ToString());
                        break;
                    case "General":
                        lstcomplete.Add(myRow.General.ToString());
                        break;
                    case "IdEvent":
                        lstcomplete.Add(myRow.IdEvent.ToString());
                        break;
                    case "IsOccured":
                        lstcomplete.Add(myRow.IsOccured.ToString());
                        break;
                    case "NoEventSource":
                        lstcomplete.Add(myRow.NoEventSource.ToString());
                        break;
                    case "NoStepRelation":
                        lstcomplete.Add(myRow.NoStepRelation.ToString());
                        break;
                    case "NoEventSourceType":
                        lstcomplete.Add(myRow.NoEventSourceType.ToString());
                        break;
                    case "Date":
                        lstcomplete.Add(myRow.Date.ToString());
                        break;
                    case "General2":
                        lstcomplete.Add(myRow.General2.ToString());
                        break;
                    case "NoRun":
                        lstcomplete.Add(myRow.NoRun.ToString());
                        break;
                    case "Message":
                        lstcomplete.Add(myRow.Message.ToString());
                        break;
                    case "NoOprationEnvironment":
                        lstcomplete.Add(myRow.NoOprationEnvironment.ToString());
                        break;
                    case "NoRuleNode":
                        lstcomplete.Add(myRow.NoRuleNode.ToString());
                        break;
                    case "HasNeedDetail":
                        lstcomplete.Add(myRow.HasNeedDetail.ToString());
                        break;
                    case "NoTables":
                        lstcomplete.Add(myRow.NoTables.ToString());
                        break;
                    case "NoSrc":
                        lstcomplete.Add(myRow.NoSrc.ToString());
                        break;
                    case "IDEventEffect":
                        lstcomplete.Add(myRow.IDEventEffect.ToString());
                        break;
                    case "IDReminderType":
                        lstcomplete.Add(myRow.IDReminderType.ToString());
                        break;
                    case "NoEventType":
                        lstcomplete.Add(myRow.NoEventType.ToString());
                        break;

                }
            }
            return lstcomplete;
        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [Event] where " + FieldName + " = '" + FieldValue + "'";
                GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                SqlCommand scmd_Class = new SqlCommand(Qry1);
                DataTable dt = DAC.GeneralSelectCommand(scmd_Class);
                if (dt.Rows.Count > 0)
                    	return true;
                else
                    	return false;
            }
            catch (Exception er)
            {
                return true;
            }
        }




        private DataTable CheckHasDivision(int NoGrid ,int NoAgent)
        {

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select SelectClause From GridDetail where NoAgent = " +  NoAgent  + " And NoGrid = " + NoGrid + " And Color = '-1'";
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            return dt;

        }




        private DataTable CheckHasDisplay(int NoQuery , int NoGrid)
        {

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select NoQuery From Display where NoEntityClass = " + NoGrid + " And NoQuery = " + NoQuery + " And IsDefault = 1";
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            return dt;

        }




        public List<string> GetColorSelect(int NoGrid ,int NoAgent)
        {

            List<string> ColorList = new List<string>();
            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select SelectClause From GridDetail where NoAgent = " +  NoAgent  + " And NoGrid = " + NoGrid + " And Color != '-1'";
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            foreach(DataRow dr in dt.Rows)
            {
                string SelectClause = dr["SelectClause"].ToString();
                string Color = dr["Color"].ToString();
                ColorList.Add(SelectClause + ";" + Color);
            }
            return ColorList;

        }




//Methods


    }  //End Class

}  //End namespace