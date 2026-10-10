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
    public partial class clsEventType
    {
        string StrCon1;
        string StrDb;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        [Serializable]
        public struct strcEventType
        {
            public int No;
            public int NoLookupType;
            public int NoOprationEnvironment;
            public int Timer;
            public string Name;

        } //End Struct

        public clsEventType()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcEventType GetNewRow()
        {
            strcEventType objNewStruct =  new strcEventType() ;
            objNewStruct.Name = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcEventType myStruct)
        {
                Event.Event DAdlt = new Event.Event(StrCon1);
                Event.Event_DS.DS_EventTypeRow dtRow = new Event.Event_DS.DS_EventTypeDataTable().NewDS_EventTypeRow();

            dtRow.No = myStruct.No;
            dtRow.NoLookupType = myStruct.NoLookupType;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            dtRow.Timer = myStruct.Timer;
            dtRow.Name = myStruct.Name;

                DAdlt.DS_EventTypeDataTable_DeleteCommand_SP(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"DELETE From [EventType] where " + FieldName + " = {0}" , Value);
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

        public int Insert(strcEventType myStruct)
        {
            Event.Event DAinsrt = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventTypeRow dtRow = new Event.Event_DS.DS_EventTypeDataTable().NewDS_EventTypeRow();

            dtRow.No = myStruct.No;
            dtRow.NoLookupType = myStruct.NoLookupType;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            dtRow.Timer = myStruct.Timer;
            dtRow.Name = myStruct.Name;

            return DAinsrt.DS_EventTypeDataTable_InsertCommand_SP(dtRow);

        }  //End Insert Function


        public int Update(strcEventType myStruct)
        {
            Event.Event DAupdt = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventTypeRow dtRow = new Event.Event_DS.DS_EventTypeDataTable().NewDS_EventTypeRow();

            dtRow.No = myStruct.No;
            dtRow.NoLookupType = myStruct.NoLookupType;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            dtRow.Timer = myStruct.Timer;
            dtRow.Name = myStruct.Name;

            DAupdt.DS_EventTypeDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry = string.Format(@"Update [EventType] SET " + FieldsSetString + " WHERE No = {0} " , KeyValue);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEventType> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEventType> myStrcList = new List<strcEventType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"SELECT * From [EventType] Where " + FieldName + " = {0}" , Value);
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEventType myStruct = new strcEventType();
                            myStruct.No = (int)dt.Rows[i]["No"];
            myStruct.NoLookupType = (int)dt.Rows[i]["NoLookupType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
            myStruct.Timer = (int)dt.Rows[i]["Timer"];
            string myrowName = (string)dt.Rows[i]["Name"];
            myStruct.Name = myrowName;

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcEventType> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcEventType> myStrcList = new List<strcEventType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EventType] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEventType myStruct = new strcEventType();
                            myStruct.No = (int)dt.Rows[i]["No"];
            myStruct.NoLookupType = (int)dt.Rows[i]["NoLookupType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
            myStruct.Timer = (int)dt.Rows[i]["Timer"];
            string myrowName = (string)dt.Rows[i]["Name"];
            myStruct.Name = myrowName;

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = string.Format(@"SELECT * From [EventType] Where " + FieldName + " = {0}" , Value);
            String FiledPersianName = "نوع اطلاعات پايه,شناسه سامانه شرکت,نوع حساب دقيقه,عنوان";
            String FiledEnglishName = "LookUpType.Name,EventType.NoOprationEnvironment,EventType.Timer,EventType.Name";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEventType> Select()
        {

            DataTable dt;
            List<strcEventType> myStrcList = new List<strcEventType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EventType]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEventType myStruct = new strcEventType();
                            myStruct.No = (int)dt.Rows[i]["No"];
            myStruct.NoLookupType = (int)dt.Rows[i]["NoLookupType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
            myStruct.Timer = (int)dt.Rows[i]["Timer"];
            string myrowName = (string)dt.Rows[i]["Name"];
            myStruct.Name = myrowName;

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEventType SelectByNo(strcEventType myStruct)
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            //Event.Event_DS.DS_EventTypeRow dtRow = new Event.Event_DS.DS_EventTypeDataTable().NewDS_EventTypeRow();

            Event.Event_DS.DS_EventTypeDataTable dt = new Event.Event_DS.DS_EventTypeDataTable();

            dt = DAslct.DS_EventTypeDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoLookupType = 0;
                myStruct.NoOprationEnvironment = 0;
                myStruct.Timer = 0;
                myStruct.Name = "0";

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoLookupType = (int)dt.Rows[0]["NoLookupType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.Timer = (int)dt.Rows[0]["Timer"];
            string myrowName = (string)dt.Rows[0]["Name"];
            myStruct.Name = myrowName;

            return myStruct ;

        }
        public strcEventType ClearByNo(strcEventType myStruct)
        {
            Event.Event DAslct = new Event.Event(StrCon1);
            Event.Event_DS.DS_EventTypeDataTable dt = new Event.Event_DS.DS_EventTypeDataTable();

            dt = DAslct.DS_EventTypeDataTable_SelectCommand(" WHERE No = " + myStruct.No);

                                myStruct.No = (int)dt.Rows[0]["No"];
                myStruct.NoLookupType = -1;
                myStruct.NoOprationEnvironment = -1;
                myStruct.Timer = -1;
                myStruct.Name = "";

                return myStruct ;
        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo',[NoLookupType] as 'FKNoLookupType', 'نوع اطلاعات پايه' = (SELECT [Name] FROM [LookUpType] WHERE TEventType.[NoLookupType] = [No]), [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [Timer] as 'نوع حساب دقيقه' , [Name] as 'عنوان'  FROM [EventType] AS TEventType";
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
                string Qry1 = "SELECT  [No] as 'PKNo',[NoLookupType] as 'FKNoLookupType', 'نوع اطلاعات پايه' = (SELECT [Name] FROM [LookUpType] WHERE TEventType.[NoLookupType] = [No]), [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [Timer] as 'نوع حساب دقيقه' , [Name] as 'عنوان'  FROM [EventType] AS TEventType";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill EventType Table Into DataGrid By NoLookupType Field
        /// </summary>
        /// <param name="tblName"></param>"
        //public DataTable FillThisDataGridView(clsLookUpType.strcLookUpType myStruct )
        //{
        //    Event.Event DAslct = new Event.Event(StrCon1);
        //    return DAslct.DS_EventTypeDataTable_SelectCommand(" WHERE NoLookupType = " + myStruct.No);

        //}

  //End FillTable Function


        public DataTable GetDataTableNoLookupTypeDescriptive()
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [LookUpType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoLookupTypeDescriptive(string strName)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [LookUpType] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoLookupTypeDescriptive(string strName,string strwhere)
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [LookUpType] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EventType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptiveParent()
        {
            string Qry = "SELECT No as 'No',Name as 'Name',NoParent FROM [EventType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002868 + ")";

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

            string Str = "Select [EventType]." + FieldName[0] + " From EventType Where [No] = " + No;
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
                Str = "Update [EventType] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [EventType] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoLookupType] as 'FKNoLookupType', 'نوع اطلاعات پايه' = (SELECT [Name] FROM [LookUpType] WHERE TEventType.[NoLookupType] = [No]), [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [Timer] as 'نوع حساب دقيقه' , [Name] as 'عنوان'  From [EventType] AS TEventType Where TEventType." + FieldName + " = " +  Value ;
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
            case "NoLookupType":
                SearchName = "(SELECT [Name] FROM [LookUpType] WHERE TEventType.[NoLookupType] = [No])";
                break;
             case "NoOprationEnvironment":
                SearchName = "NoOprationEnvironment";
                 break; 
             case "Timer":
                SearchName = "Timer";
                 break; 
             case "Name":
                SearchName = "Name";
                 break; 

            }
            return SearchName;
        }
        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            FieldName = FindSearchName(FieldName);
            string Str = "Select  [No] as 'PKNo',[NoLookupType] as 'FKNoLookupType', 'نوع اطلاعات پايه' = (SELECT [Name] FROM [LookUpType] WHERE TEventType.[NoLookupType] = [No]), [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [Timer] as 'نوع حساب دقيقه' , [Name] as 'عنوان'  From [EventType] AS TEventType Where " + FieldName  + " like N'%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value,string strwhere)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            FieldName = FindSearchName(FieldName);
            string Str = "Select  [No] as 'PKNo',[NoLookupType] as 'FKNoLookupType', 'نوع اطلاعات پايه' = (SELECT [Name] FROM [LookUpType] WHERE TEventType.[NoLookupType] = [No]), [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [Timer] as 'نوع حساب دقيقه' , [Name] as 'عنوان'  From [EventType] AS TEventType Where " + FieldName + " like 'N%" +  Value  + "%' and " + strwhere;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002868) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002868) " + 
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


        public strcEventType GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEventType.strcEventType myStruct = new clsEventType.strcEventType();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [EventType] Where [EventType]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoLookupType = (int)dt.Rows[0]["NoLookupType"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.Timer = (int)dt.Rows[0]["Timer"];
            string myrowName = (string)dt.Rows[0]["Name"];
            myStruct.Name = myrowName;


            return myStruct;

        }




        public ArrayList FillArrayNumComplete(ArrayList lstcmp, strcEventType myRow)
        {

             ArrayList lstcomplete = new ArrayList();
            for (int cnt = 0; cnt < lstcmp.Count; cnt++)
            {
                switch (lstcmp[cnt].ToString())
                {
                    case "No":
                        lstcomplete.Add(myRow.No.ToString());
                        break;
                    case "NoLookupType":
                        lstcomplete.Add(myRow.NoLookupType.ToString());
                        break;
                    case "NoOprationEnvironment":
                        lstcomplete.Add(myRow.NoOprationEnvironment.ToString());
                        break;
                    case "Timer":
                        lstcomplete.Add(myRow.Timer.ToString());
                        break;
                    case "Name":
                        lstcomplete.Add(myRow.Name.ToString());
                        break;

                }
            }
            return lstcomplete;
        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [EventType] where " + FieldName + " = '" + FieldValue + "'";
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