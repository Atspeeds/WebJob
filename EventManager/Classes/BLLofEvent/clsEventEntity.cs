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
    public partial class clsEventEntity
    {
        string StrCon1;
        string StrDb;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        [Serializable]
        public struct strcEventEntity
        {
            public int NoEntity;
            public int NoEvent;
            public short IDEventEntityRelationType;
            public int NoOprationEnvironment;

        } //End Struct

        public clsEventEntity()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcEventEntity GetNewRow()
        {
            strcEventEntity objNewStruct =  new strcEventEntity() ;
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcEventEntity myStruct)
        {
                Event.Event DAdlt = new Event.Event(StrCon1);
                Event.Event_DS.DS_EventEntityRow dtRow = new Event.Event_DS.DS_EventEntityDataTable().NewDS_EventEntityRow();

            dtRow.NoEntity = myStruct.NoEntity;
            dtRow.NoEvent = myStruct.NoEvent;
            dtRow.IDEventEntityRelationType = myStruct.IDEventEntityRelationType;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

                DAdlt.DS_EventEntityDataTable_DeleteCommand_SP(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"DELETE From [EventEntity] where " + FieldName + " = {0}" , Value);
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

        public int Insert(strcEventEntity myStruct)
        {
            Event.Event DAinsrt = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventEntityRow dtRow = new Event.Event_DS.DS_EventEntityDataTable().NewDS_EventEntityRow();

            dtRow.NoEntity = myStruct.NoEntity;
            dtRow.NoEvent = myStruct.NoEvent;
            dtRow.IDEventEntityRelationType = myStruct.IDEventEntityRelationType;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_EventEntityDataTable_InsertCommand_SP(dtRow);

        }  //End Insert Function


        public int Update(strcEventEntity myStruct)
        {
            Event.Event DAupdt = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventEntityRow dtRow = new Event.Event_DS.DS_EventEntityDataTable().NewDS_EventEntityRow();

            dtRow.NoEntity = myStruct.NoEntity;
            dtRow.NoEvent = myStruct.NoEvent;
            dtRow.IDEventEntityRelationType = myStruct.IDEventEntityRelationType;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            DAupdt.DS_EventEntityDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry = string.Format(@"Update [EventEntity] SET " + FieldsSetString + " WHERE NoEntity = {0} " , KeyValue);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEventEntity> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEventEntity> myStrcList = new List<strcEventEntity>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"SELECT * From [EventEntity] Where " + FieldName + " = {0}" , Value);
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEventEntity myStruct = new strcEventEntity();
                            myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
            myStruct.NoEvent = (int)dt.Rows[i]["NoEvent"];
            myStruct.IDEventEntityRelationType = (short)dt.Rows[i]["IDEventEntityRelationType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcEventEntity> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcEventEntity> myStrcList = new List<strcEventEntity>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EventEntity] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEventEntity myStruct = new strcEventEntity();
                            myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
            myStruct.NoEvent = (int)dt.Rows[i]["NoEvent"];
            myStruct.IDEventEntityRelationType = (short)dt.Rows[i]["IDEventEntityRelationType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"SELECT * From [EventEntity] Where " + FieldName + " = {0}" , Value);
            String FiledPersianName = "شناسه رويداد,شناسه EventEntityRelationType,شناسه سامانه شرکت";
            String FiledEnglishName = "Event.Name,EventEntity.IDEventEntityRelationType,EventEntity.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEventEntity> Select()
        {

            DataTable dt;
            List<strcEventEntity> myStrcList = new List<strcEventEntity>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EventEntity]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEventEntity myStruct = new strcEventEntity();
                            myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
            myStruct.NoEvent = (int)dt.Rows[i]["NoEvent"];
            myStruct.IDEventEntityRelationType = (short)dt.Rows[i]["IDEventEntityRelationType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEventEntity SelectByNo(strcEventEntity myStruct)
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            //Event.Event_DS.DS_EventEntityRow dtRow = new Event.Event_DS.DS_EventEntityDataTable().NewDS_EventEntityRow();

            Event.Event_DS.DS_EventEntityDataTable dt = new Event.Event_DS.DS_EventEntityDataTable();

            dt = DAslct.DS_EventEntityDataTable_SelectCommand(" WHERE NoEntity = " + myStruct.NoEntity + " AND NoEvent = " + myStruct.NoEvent);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoEntity = 0;
                myStruct.NoEvent = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.NoEvent = (int)dt.Rows[0]["NoEvent"];
            myStruct.IDEventEntityRelationType = (short)dt.Rows[0]["IDEventEntityRelationType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        }
        public strcEventEntity ClearByNo(strcEventEntity myStruct)
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventEntityDataTable dt = new Event.Event_DS.DS_EventEntityDataTable();

            dt = DAslct.DS_EventEntityDataTable_SelectCommand(" WHERE NoEntity = " + myStruct.NoEntity + " AND NoEvent = " + myStruct.NoEvent);

                                myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
                myStruct.NoEvent = (int)dt.Rows[0]["NoEvent"];
                myStruct.IDEventEntityRelationType = -1;
                myStruct.NoOprationEnvironment = -1;

                return myStruct ;
        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NoEntity] as 'PKNoEntity', [NoEvent] as 'PKNoEvent',[NoEvent] as 'FKNoEvent', 'شناسه رويداد' = (SELECT [Name] FROM [Event] WHERE TEventEntity.[NoEvent] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDEventEntityRelationType]) as 'شناسه EventEntityRelationType' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EventEntity] AS TEventEntity";
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
                string Qry1 = "SELECT  [NoEntity] as 'PKNoEntity', [NoEvent] as 'PKNoEvent',[NoEvent] as 'FKNoEvent', 'شناسه رويداد' = (SELECT [Name] FROM [Event] WHERE TEventEntity.[NoEvent] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDEventEntityRelationType]) as 'شناسه EventEntityRelationType' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EventEntity] AS TEventEntity";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill EventEntity Table Into DataGrid By NoEvent Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEvent.strcEvent myStruct )
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            return DAslct.DS_EventEntityDataTable_SelectCommand(" WHERE NoEvent = " + myStruct.No);

        }

  //End FillTable Function


        public DataTable GetDataTableNoEventDescriptive()
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Event]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEventDescriptive(string strName)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Event] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEventDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Event] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


        public DataTable GetDataTableIDEventEntityRelationTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 263 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDEventEntityRelationTypeDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 263 ORDER BY " + OrderName + " " + OrderSort;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001863 + ")";

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



        public string GetAtt( int NoEntity , int NoEvent , int NoFieldget)
        {

            string[] FieldName = GetFieldName(NoFieldget);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [EventEntity]." + FieldName[0] + " From EventEntity Where [NoEntity] = " + NoEntity + " And [NoEvent] = " + NoEvent;
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

        public void SetAtt( int NoEntity , int NoEvent , int NoFieldset, string value)
        {

            string[] FieldName = GetFieldName(NoFieldset);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [EventEntity] SET [" + FieldName[0] + "] = '" + value + "' where [NoEntity] = " + NoEntity + " And [NoEvent] = " + NoEvent;
            else if (FieldName[1] == "6")
                Str = "Update [EventEntity] SET [" + FieldName[0] + "] = " + value + " where [NoEntity] = " + NoEntity + " And [NoEvent] = " + NoEvent;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoEntity] as 'PKNoEntity', [NoEvent] as 'PKNoEvent',[NoEvent] as 'FKNoEvent', 'شناسه رويداد' = (SELECT [Name] FROM [Event] WHERE TEventEntity.[NoEvent] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDEventEntityRelationType]) as 'شناسه EventEntityRelationType' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EventEntity] AS TEventEntity Where TEventEntity." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public string FindSearchName(string Name)
        {
            string SearchName = "";

            switch (Name)
            {
              case "NoEntity":
                SearchName = "NoEntity";
                 break; 
            case "NoEvent":
                SearchName = "(SELECT [Name] FROM [Event] WHERE TEventEntity.[NoEvent] = [No])";
                break;
             case "IDEventEntityRelationType":
                SearchName = "(Select Name from [LookUp] Where [LookUp].No = [IDEventEntityRelationType])";
                 break; 
             case "NoOprationEnvironment":
                SearchName = "NoOprationEnvironment";
                 break; 

            }
            return SearchName;
        }
        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            FieldName = FindSearchName(FieldName);
            string Str = "Select  [NoEntity] as 'PKNoEntity', [NoEvent] as 'PKNoEvent',[NoEvent] as 'FKNoEvent', 'شناسه رويداد' = (SELECT [Name] FROM [Event] WHERE TEventEntity.[NoEvent] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDEventEntityRelationType]) as 'شناسه EventEntityRelationType' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EventEntity] AS TEventEntity Where " + FieldName  + " like N'%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value,string strwhere)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            FieldName = FindSearchName(FieldName);
            string Str = "Select  [NoEntity] as 'PKNoEntity', [NoEvent] as 'PKNoEvent',[NoEvent] as 'FKNoEvent', 'شناسه رويداد' = (SELECT [Name] FROM [Event] WHERE TEventEntity.[NoEvent] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDEventEntityRelationType]) as 'شناسه EventEntityRelationType' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EventEntity] AS TEventEntity Where " + FieldName + " like 'N%" +  Value  + "%' and " + strwhere;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001863) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001863) " + 
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


        public strcEventEntity GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEventEntity.strcEventEntity myStruct = new clsEventEntity.strcEventEntity();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [EventEntity] Where [EventEntity]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.NoEvent = (int)dt.Rows[0]["NoEvent"];
            myStruct.IDEventEntityRelationType = (short)dt.Rows[0]["IDEventEntityRelationType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public ArrayList FillArrayNumComplete(ArrayList lstcmp, strcEventEntity myRow)
        {

             ArrayList lstcomplete = new ArrayList();
            for (int cnt = 0; cnt < lstcmp.Count; cnt++)
            {
                switch (lstcmp[cnt].ToString())
                {
                    case "NoEntity":
                        lstcomplete.Add(myRow.NoEntity.ToString());
                        break;
                    case "NoEvent":
                        lstcomplete.Add(myRow.NoEvent.ToString());
                        break;
                    case "IDEventEntityRelationType":
                        lstcomplete.Add(myRow.IDEventEntityRelationType.ToString());
                        break;
                    case "NoOprationEnvironment":
                        lstcomplete.Add(myRow.NoOprationEnvironment.ToString());
                        break;

                }
            }
            return lstcomplete;
        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [EventEntity] where " + FieldName + " = '" + FieldValue + "'";
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