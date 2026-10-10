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
    public partial class clsServerDialog
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        [Serializable]
        public struct strcServerDialog
        {
            public int No;
            public int NoOprationEnvironment;
            public string JsonValue;
            public int IDServerType;
            public int IDCommandMessageType;
            public bool received;

        } //End Struct

        public clsServerDialog()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcServerDialog GetNewRow()
        {
            strcServerDialog objNewStruct =  new strcServerDialog() ;
            objNewStruct.JsonValue = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcServerDialog myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_ServerDialogRow dtRow = new Entity.Entity_DS.DS_ServerDialogDataTable().NewDS_ServerDialogRow();

                dtRow.No = myStruct.No;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
                dtRow.JsonValue = myStruct.JsonValue;
                dtRow.IDServerType = myStruct.IDServerType;
                dtRow.IDCommandMessageType = myStruct.IDCommandMessageType;
                dtRow.received = myStruct.received;

                DAdlt.DS_ServerDialogDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [ServerDialog] where " + FieldName + " = " + Value ;
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

        public int Insert(strcServerDialog myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_ServerDialogRow dtRow = new Entity.Entity_DS.DS_ServerDialogDataTable().NewDS_ServerDialogRow();

            dtRow.No = myStruct.No;
            dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            dtRow.JsonValue = myStruct.JsonValue;
            dtRow.IDServerType = myStruct.IDServerType;
            dtRow.IDCommandMessageType = myStruct.IDCommandMessageType;
            dtRow.received = myStruct.received;

            return DAinsrt.DS_ServerDialogDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcServerDialog myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_ServerDialogRow dtRow = new Entity.Entity_DS.DS_ServerDialogDataTable().NewDS_ServerDialogRow();

                dtRow.No = myStruct.No;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
                dtRow.JsonValue = myStruct.JsonValue;
                dtRow.IDServerType = myStruct.IDServerType;
                dtRow.IDCommandMessageType = myStruct.IDCommandMessageType;
                dtRow.received = myStruct.received;

            DAupdt.DS_ServerDialogDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [ServerDialog] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcServerDialog> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcServerDialog> myStrcList = new List<strcServerDialog>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [ServerDialog] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcServerDialog myStruct = new strcServerDialog();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.JsonValue = (string)dt.Rows[i]["JsonValue"];
                myStruct.IDServerType = (int)dt.Rows[i]["IDServerType"];
                myStruct.IDCommandMessageType = (int)dt.Rows[i]["IDCommandMessageType"];
                myStruct.received = (bool)dt.Rows[i]["received"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcServerDialog> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcServerDialog> myStrcList = new List<strcServerDialog>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [ServerDialog] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcServerDialog myStruct = new strcServerDialog();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.JsonValue = (string)dt.Rows[i]["JsonValue"];
                myStruct.IDServerType = (int)dt.Rows[i]["IDServerType"];
                myStruct.IDCommandMessageType = (int)dt.Rows[i]["IDCommandMessageType"];
                myStruct.received = (bool)dt.Rows[i]["received"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [ServerDialog] Where " + FieldName + " = " + Value;
            String FiledPersianName = "شناسه سامانه شرکت,مقدار جي سون,ServerType,CommandMessageType,دريافت شده";
            String FiledEnglishName = "ServerDialog.NoOprationEnvironment,ServerDialog.JsonValue,ServerDialog.IDServerType,ServerDialog.IDCommandMessageType,ServerDialog.received";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcServerDialog> Select()
        {

            DataTable dt;
            List<strcServerDialog> myStrcList = new List<strcServerDialog>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [ServerDialog]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcServerDialog myStruct = new strcServerDialog();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.JsonValue = (string)dt.Rows[i]["JsonValue"];
                myStruct.IDServerType = (int)dt.Rows[i]["IDServerType"];
                myStruct.IDCommandMessageType = (int)dt.Rows[i]["IDCommandMessageType"];
                myStruct.received = (bool)dt.Rows[i]["received"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcServerDialog SelectByNo(strcServerDialog myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_ServerDialogRow dtRow = new Entity.Entity_DS.DS_ServerDialogDataTable().NewDS_ServerDialogRow();

            Entity.Entity_DS.DS_ServerDialogDataTable dt = new Entity.Entity_DS.DS_ServerDialogDataTable();

            dt = DAslct.DS_ServerDialogDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoOprationEnvironment = 0;
                myStruct.JsonValue = "0";
                myStruct.IDServerType = 0;
                myStruct.IDCommandMessageType = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.JsonValue = (string)dt.Rows[0]["JsonValue"];
            myStruct.IDServerType = (int)dt.Rows[0]["IDServerType"];
            myStruct.IDCommandMessageType = (int)dt.Rows[0]["IDCommandMessageType"];
            myStruct.received = (bool)dt.Rows[0]["received"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [JsonValue] as 'مقدار جي سون'  , (Select Name from [LookUp] Where [LookUp].No = [IDServerType]) as 'ServerType'  , (Select Name from [LookUp] Where [LookUp].No = [IDCommandMessageType]) as 'CommandMessageType' , [received] as 'دريافت شده'  FROM [ServerDialog] AS TServerDialog";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [JsonValue] as 'مقدار جي سون'  , (Select Name from [LookUp] Where [LookUp].No = [IDServerType]) as 'ServerType'  , (Select Name from [LookUp] Where [LookUp].No = [IDCommandMessageType]) as 'CommandMessageType' , [received] as 'دريافت شده'  FROM [ServerDialog] AS TServerDialog";

            }
            return 0 ;

        }


  //End FillTable Function


  //End FillComboBYTable


        public DataTable GetDataTableIDServerTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 643 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDServerTypeDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 643 ORDER BY " + OrderName + " " + OrderSort;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDCommandMessageTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 642 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDCommandMessageTypeDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 642 ORDER BY " + OrderName + " " + OrderSort;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002417 + ")";

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



        public string GetAtt( int No , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [ServerDialog]." + FieldName[0] + " From ServerDialog Where [No] = " + No;
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

        public void SetAtt( int No , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [ServerDialog] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [ServerDialog] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [JsonValue] as 'مقدار جي سون'  , (Select Name from [LookUp] Where [LookUp].No = [IDServerType]) as 'ServerType'  , (Select Name from [LookUp] Where [LookUp].No = [IDCommandMessageType]) as 'CommandMessageType' , [received] as 'دريافت شده'  From [ServerDialog] AS TServerDialog Where TServerDialog." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [JsonValue] as 'مقدار جي سون'  , (Select Name from [LookUp] Where [LookUp].No = [IDServerType]) as 'ServerType'  , (Select Name from [LookUp] Where [LookUp].No = [IDCommandMessageType]) as 'CommandMessageType' , [received] as 'دريافت شده'  From [ServerDialog] AS TServerDialog Where TServerDialog." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value,string strwhere)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [NoOprationEnvironment] as 'FKNoOprationEnvironment' , [JsonValue] as 'مقدار جي سون'  , (Select Name from [LookUp] Where [LookUp].No = [IDServerType]) as 'ServerType'  , (Select Name from [LookUp] Where [LookUp].No = [IDCommandMessageType]) as 'CommandMessageType' , [received] as 'دريافت شده'  From [ServerDialog] AS TServerDialog Where TServerDialog." + FieldName + " like '%" +  Value  + "%' and " + strwhere;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002417) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002417) " + 
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


        public strcServerDialog GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsServerDialog.strcServerDialog myStruct = new clsServerDialog.strcServerDialog();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [ServerDialog] Where [ServerDialog]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.JsonValue = (string)dt.Rows[0]["JsonValue"];
            myStruct.IDServerType = (int)dt.Rows[0]["IDServerType"];
            myStruct.IDCommandMessageType = (int)dt.Rows[0]["IDCommandMessageType"];
            myStruct.received = (bool)dt.Rows[0]["received"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [ServerDialog] where " + FieldName + " = '" + FieldValue + "'";
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