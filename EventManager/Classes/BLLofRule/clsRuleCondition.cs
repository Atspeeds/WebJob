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
    public class clsRuleCondition
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcRuleCondition
        {
            public int No;
            public string Name;
            public bool IsMain;
            public int NoRuleNode;
            public int IDRuleConditionType;
            public string General1;
            public string General2;
            public string General3;
            public string General4;
            public string General5;
            public int NoOprationEnvironment;

        } //End Struct

        public clsRuleCondition()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcRuleCondition GetNewRow()
        {
            strcRuleCondition objNewStruct =  new strcRuleCondition() ;
            objNewStruct.Name = ""; 
            objNewStruct.General1 = ""; 
            objNewStruct.General2 = ""; 
            objNewStruct.General3 = ""; 
            objNewStruct.General4 = ""; 
            objNewStruct.General5 = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcRuleCondition myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_RuleConditionRow dtRow = new Rule.Rule_DS.DS_RuleConditionDataTable().NewDS_RuleConditionRow();

                dtRow.No = myStruct.No;
                dtRow.Name = myStruct.Name;
                dtRow.IsMain = myStruct.IsMain;
                dtRow.NoRuleNode = myStruct.NoRuleNode;
                dtRow.IDRuleConditionType = myStruct.IDRuleConditionType;
                dtRow.General1 = myStruct.General1;
                dtRow.General2 = myStruct.General2;
                dtRow.General3 = myStruct.General3;
                dtRow.General4 = myStruct.General4;
                dtRow.General5 = myStruct.General5;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

                DAdlt.DS_RuleConditionDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [RuleCondition] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcRuleCondition myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleConditionRow dtRow = new Rule.Rule_DS.DS_RuleConditionDataTable().NewDS_RuleConditionRow();

            dtRow.No = myStruct.No;
            dtRow.Name = myStruct.Name;
            dtRow.IsMain = myStruct.IsMain;
            dtRow.NoRuleNode = myStruct.NoRuleNode;
            dtRow.IDRuleConditionType = myStruct.IDRuleConditionType;
            dtRow.General1 = myStruct.General1;
            dtRow.General2 = myStruct.General2;
            dtRow.General3 = myStruct.General3;
            dtRow.General4 = myStruct.General4;
            dtRow.General5 = myStruct.General5;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_RuleConditionDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcRuleCondition myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleConditionRow dtRow = new Rule.Rule_DS.DS_RuleConditionDataTable().NewDS_RuleConditionRow();

                dtRow.No = myStruct.No;
                dtRow.Name = myStruct.Name;
                dtRow.IsMain = myStruct.IsMain;
                dtRow.NoRuleNode = myStruct.NoRuleNode;
                dtRow.IDRuleConditionType = myStruct.IDRuleConditionType;
                dtRow.General1 = myStruct.General1;
                dtRow.General2 = myStruct.General2;
                dtRow.General3 = myStruct.General3;
                dtRow.General4 = myStruct.General4;
                dtRow.General5 = myStruct.General5;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            DAupdt.DS_RuleConditionDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [RuleCondition] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcRuleCondition> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcRuleCondition> myStrcList = new List<strcRuleCondition>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleCondition] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleCondition myStruct = new strcRuleCondition();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsMain = (bool)dt.Rows[i]["IsMain"];
                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.IDRuleConditionType = (int)dt.Rows[i]["IDRuleConditionType"];
                myStruct.General1 = (string)dt.Rows[i]["General1"];
                myStruct.General2 = (string)dt.Rows[i]["General2"];
                myStruct.General3 = (string)dt.Rows[i]["General3"];
                myStruct.General4 = (string)dt.Rows[i]["General4"];
                myStruct.General5 = (string)dt.Rows[i]["General5"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcRuleCondition> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcRuleCondition> myStrcList = new List<strcRuleCondition>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleCondition] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleCondition myStruct = new strcRuleCondition();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsMain = (bool)dt.Rows[i]["IsMain"];
                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.IDRuleConditionType = (int)dt.Rows[i]["IDRuleConditionType"];
                myStruct.General1 = (string)dt.Rows[i]["General1"];
                myStruct.General2 = (string)dt.Rows[i]["General2"];
                myStruct.General3 = (string)dt.Rows[i]["General3"];
                myStruct.General4 = (string)dt.Rows[i]["General4"];
                myStruct.General5 = (string)dt.Rows[i]["General5"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleCondition] Where " + FieldName + " = " + Value;
            String FiledPersianName = "عنوان,اصلي است,نودهاي قاعده,شناسه نوع شرط قانون,عمومي1,عمومي2,عمومي3,عمومي4,عمومي5,شناسه سامانه شرکت";
            String FiledEnglishName = "RuleCondition.Name,RuleCondition.IsMain,RuleNode.AbstractCode,RuleCondition.IDRuleConditionType,RuleCondition.General1,RuleCondition.General2,RuleCondition.General3,RuleCondition.General4,RuleCondition.General5,RuleCondition.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcRuleCondition> Select()
        {

            DataTable dt;
            List<strcRuleCondition> myStrcList = new List<strcRuleCondition>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleCondition]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleCondition myStruct = new strcRuleCondition();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsMain = (bool)dt.Rows[i]["IsMain"];
                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.IDRuleConditionType = (int)dt.Rows[i]["IDRuleConditionType"];
                myStruct.General1 = (string)dt.Rows[i]["General1"];
                myStruct.General2 = (string)dt.Rows[i]["General2"];
                myStruct.General3 = (string)dt.Rows[i]["General3"];
                myStruct.General4 = (string)dt.Rows[i]["General4"];
                myStruct.General5 = (string)dt.Rows[i]["General5"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcRuleCondition SelectByNo(strcRuleCondition myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_RuleConditionRow dtRow = new Rule.Rule_DS.DS_RuleConditionDataTable().NewDS_RuleConditionRow();

            Rule.Rule_DS.DS_RuleConditionDataTable dt = new Rule.Rule_DS.DS_RuleConditionDataTable();

            dt = DAslct.DS_RuleConditionDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.Name = "0";
                myStruct.NoRuleNode = 0;
                myStruct.IDRuleConditionType = 0;
                myStruct.General1 = "0";
                myStruct.General2 = "0";
                myStruct.General3 = "0";
                myStruct.General4 = "0";
                myStruct.General5 = "0";
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.IsMain = (bool)dt.Rows[0]["IsMain"];
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.IDRuleConditionType = (int)dt.Rows[0]["IDRuleConditionType"];
            myStruct.General1 = (string)dt.Rows[0]["General1"];
            myStruct.General2 = (string)dt.Rows[0]["General2"];
            myStruct.General3 = (string)dt.Rows[0]["General3"];
            myStruct.General4 = (string)dt.Rows[0]["General4"];
            myStruct.General5 = (string)dt.Rows[0]["General5"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [Name] as 'عنوان' , [IsMain] as 'اصلي است' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleCondition.[NoRuleNode] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDRuleConditionType]) as 'شناسه نوع شرط قانون' , [General1] as 'عمومي1' , [General2] as 'عمومي2' , [General3] as 'عمومي3' , [General4] as 'عمومي4' , [General5] as 'عمومي5' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [RuleCondition] AS TRuleCondition";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [Name] as 'عنوان' , [IsMain] as 'اصلي است' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleCondition.[NoRuleNode] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDRuleConditionType]) as 'شناسه نوع شرط قانون' , [General1] as 'عمومي1' , [General2] as 'عمومي2' , [General3] as 'عمومي3' , [General4] as 'عمومي4' , [General5] as 'عمومي5' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [RuleCondition] AS TRuleCondition";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill RuleCondition Table Into DataGrid By NoRuleNode Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRuleNode.strcRuleNode myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_RuleConditionDataTable_SelectCommand(" WHERE NoRuleNode = " + myStruct.No);

        }

  //End FillTable Function


        public DataTable GetDataTableNoRuleNodeDescriptive()
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive(string strName)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive(string strName,string strwhere)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


        public DataTable GetDataTableIDRuleConditionTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 357 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDRuleConditionTypeDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 357 ORDER BY " + OrderName + " " + OrderSort;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002136 + ")";

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

            string Str = "Select [RuleCondition]." + FieldName[0] + " From RuleCondition Where [No] = " + No;
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
                Str = "Update [RuleCondition] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [RuleCondition] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'عنوان' , [IsMain] as 'اصلي است' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleCondition.[NoRuleNode] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDRuleConditionType]) as 'شناسه نوع شرط قانون' , [General1] as 'عمومي1' , [General2] as 'عمومي2' , [General3] as 'عمومي3' , [General4] as 'عمومي4' , [General5] as 'عمومي5' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [RuleCondition] AS TRuleCondition Where TRuleCondition." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'عنوان' , [IsMain] as 'اصلي است' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleCondition.[NoRuleNode] = [No]) , (Select Name from [LookUp] Where [LookUp].No = [IDRuleConditionType]) as 'شناسه نوع شرط قانون' , [General1] as 'عمومي1' , [General2] as 'عمومي2' , [General3] as 'عمومي3' , [General4] as 'عمومي4' , [General5] as 'عمومي5' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [RuleCondition] AS TRuleCondition Where TRuleCondition." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002136) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002136) " + 
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


        public strcRuleCondition GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsRuleCondition.strcRuleCondition myStruct = new clsRuleCondition.strcRuleCondition();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [RuleCondition] Where [RuleCondition]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.IsMain = (bool)dt.Rows[0]["IsMain"];
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.IDRuleConditionType = (int)dt.Rows[0]["IDRuleConditionType"];
            myStruct.General1 = (string)dt.Rows[0]["General1"];
            myStruct.General2 = (string)dt.Rows[0]["General2"];
            myStruct.General3 = (string)dt.Rows[0]["General3"];
            myStruct.General4 = (string)dt.Rows[0]["General4"];
            myStruct.General5 = (string)dt.Rows[0]["General5"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [RuleCondition] where " + FieldName + " = '" + FieldValue + "'";
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