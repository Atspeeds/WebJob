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
    public class clsOnTime
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcOnTime
        {
            public int NoRuleCondition;
            public System.DateTime DateTimeFrom;
            public System.DateTime DateTimeTo;
            public int NoOprationEnvironment;

        } //End Struct

        public clsOnTime()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcOnTime GetNewRow()
        {
            strcOnTime objNewStruct =  new strcOnTime() ;
            objNewStruct.DateTimeFrom = DateTime.Parse("1800-01-01"); 
            objNewStruct.DateTimeTo = DateTime.Parse("1800-01-01"); 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcOnTime myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_OnTimeRow dtRow = new Rule.Rule_DS.DS_OnTimeDataTable().NewDS_OnTimeRow();

                dtRow.NoRuleCondition = myStruct.NoRuleCondition;
                dtRow.DateTimeFrom = myStruct.DateTimeFrom;
                dtRow.DateTimeTo = myStruct.DateTimeTo;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
                DAdlt.DS_OnTimeDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [OnTime] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcOnTime myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_OnTimeRow dtRow = new Rule.Rule_DS.DS_OnTimeDataTable().NewDS_OnTimeRow();

            dtRow.NoRuleCondition = myStruct.NoRuleCondition;
            dtRow.DateTimeFrom = myStruct.DateTimeFrom;
            dtRow.DateTimeTo = myStruct.DateTimeTo;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_OnTimeDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcOnTime myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_OnTimeRow dtRow = new Rule.Rule_DS.DS_OnTimeDataTable().NewDS_OnTimeRow();

                dtRow.NoRuleCondition = myStruct.NoRuleCondition;
                dtRow.DateTimeFrom = myStruct.DateTimeFrom;
                dtRow.DateTimeTo = myStruct.DateTimeTo;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            DAupdt.DS_OnTimeDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [OnTime] SET " + FieldsSetString + " WHERE NoRuleCondition = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcOnTime> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcOnTime> myStrcList = new List<strcOnTime>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [OnTime] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcOnTime myStruct = new strcOnTime();
                                myStruct.NoRuleCondition = (int)dt.Rows[i]["NoRuleCondition"];
                myStruct.DateTimeFrom = (System.DateTime)dt.Rows[i]["DateTimeFrom"];
                myStruct.DateTimeTo = (System.DateTime)dt.Rows[i]["DateTimeTo"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcOnTime> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcOnTime> myStrcList = new List<strcOnTime>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [OnTime] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcOnTime myStruct = new strcOnTime();
                                myStruct.NoRuleCondition = (int)dt.Rows[i]["NoRuleCondition"];
                myStruct.DateTimeFrom = (System.DateTime)dt.Rows[i]["DateTimeFrom"];
                myStruct.DateTimeTo = (System.DateTime)dt.Rows[i]["DateTimeTo"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [OnTime] Where " + FieldName + " = " + Value;
            String FiledPersianName = "تاريخ شروع,تاريخ پايان,شناسه سامانه شرکت";
            String FiledEnglishName = "OnTime.DateTimeFrom,OnTime.DateTimeTo,OnTime.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcOnTime> Select()
        {

            DataTable dt;
            List<strcOnTime> myStrcList = new List<strcOnTime>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [OnTime]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcOnTime myStruct = new strcOnTime();
                                myStruct.NoRuleCondition = (int)dt.Rows[i]["NoRuleCondition"];
                myStruct.DateTimeFrom = (System.DateTime)dt.Rows[i]["DateTimeFrom"];
                myStruct.DateTimeTo = (System.DateTime)dt.Rows[i]["DateTimeTo"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcOnTime SelectByNo(strcOnTime myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_OnTimeRow dtRow = new Rule.Rule_DS.DS_OnTimeDataTable().NewDS_OnTimeRow();

            Rule.Rule_DS.DS_OnTimeDataTable dt = new Rule.Rule_DS.DS_OnTimeDataTable();

            dt = DAslct.DS_OnTimeDataTable_SelectCommand(" WHERE NoRuleCondition = " + myStruct.NoRuleCondition);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoRuleCondition = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.NoRuleCondition = (int)dt.Rows[0]["NoRuleCondition"];
            myStruct.DateTimeFrom = (System.DateTime)dt.Rows[0]["DateTimeFrom"];
            myStruct.DateTimeTo = (System.DateTime)dt.Rows[0]["DateTimeTo"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NoRuleCondition] as 'PKNoRuleCondition',case when [DateTimeFrom] = '1800/01/01' then '' else dbo.mtosh( [DateTimeFrom] ) end as 'تاريخ شروع',case when [DateTimeTo] = '1800/01/01' then '' else dbo.mtosh( [DateTimeTo] ) end as 'تاريخ پايان', [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [OnTime] AS TOnTime";
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
                string Qry1 = "SELECT  [NoRuleCondition] as 'PKNoRuleCondition',case when [DateTimeFrom] = '1800/01/01' then '' else dbo.mtosh( [DateTimeFrom] ) end as 'تاريخ شروع',case when [DateTimeTo] = '1800/01/01' then '' else dbo.mtosh( [DateTimeTo] ) end as 'تاريخ پايان', [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [OnTime] AS TOnTime";

            }
            return 0 ;

        }


  //End FillTable Function


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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002143 + ")";

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



        public string GetAtt( int NoRuleCondition , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [OnTime]." + FieldName[0] + " From OnTime Where [NoRuleCondition] = " + NoRuleCondition;
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

        public void SetAtt( int NoRuleCondition , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [OnTime] SET [" + FieldName[0] + "] = '" + value + "' where [NoRuleCondition] = " + NoRuleCondition;
            else if (FieldName[1] == "6")
                Str = "Update [OnTime] SET [" + FieldName[0] + "] = " + value + " where [NoRuleCondition] = " + NoRuleCondition;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoRuleCondition] as 'PKNoRuleCondition',case when [DateTimeFrom] = '1800/01/01' then '' else dbo.mtosh( [DateTimeFrom] ) end as 'تاريخ شروع',case when [DateTimeTo] = '1800/01/01' then '' else dbo.mtosh( [DateTimeTo] ) end as 'تاريخ پايان', [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [OnTime] AS TOnTime Where TOnTime." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoRuleCondition] as 'PKNoRuleCondition',case when [DateTimeFrom] = '1800/01/01' then '' else dbo.mtosh( [DateTimeFrom] ) end as 'تاريخ شروع',case when [DateTimeTo] = '1800/01/01' then '' else dbo.mtosh( [DateTimeTo] ) end as 'تاريخ پايان', [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [OnTime] AS TOnTime Where TOnTime." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002143) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002143) " + 
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


        public strcOnTime GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsOnTime.strcOnTime myStruct = new clsOnTime.strcOnTime();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [OnTime] Where [OnTime]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoRuleCondition = (int)dt.Rows[0]["NoRuleCondition"];
            myStruct.DateTimeFrom = (System.DateTime)dt.Rows[0]["DateTimeFrom"];
            myStruct.DateTimeTo = (System.DateTime)dt.Rows[0]["DateTimeTo"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [OnTime] where " + FieldName + " = '" + FieldValue + "'";
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