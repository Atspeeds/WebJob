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
    public class clsRuleConclusion
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcRuleConclusion
        {
            public int NoRuleNode;
            public string Name;
            public string Comment;
            public int NoRule;
            public int NoOprationEnvironment;

        } //End Struct

        public clsRuleConclusion()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcRuleConclusion GetNewRow()
        {
            strcRuleConclusion objNewStruct =  new strcRuleConclusion() ;
            objNewStruct.Name = ""; 
            objNewStruct.Comment = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcRuleConclusion myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_RuleConclusionRow dtRow = new Rule.Rule_DS.DS_RuleConclusionDataTable().NewDS_RuleConclusionRow();

                dtRow.NoRuleNode = myStruct.NoRuleNode;
                dtRow.Name = myStruct.Name;
                dtRow.Comment = myStruct.Comment;
                dtRow.NoRule = myStruct.NoRule;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

                DAdlt.DS_RuleConclusionDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [RuleConclusion] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcRuleConclusion myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleConclusionRow dtRow = new Rule.Rule_DS.DS_RuleConclusionDataTable().NewDS_RuleConclusionRow();

            dtRow.NoRuleNode = myStruct.NoRuleNode;
            dtRow.Name = myStruct.Name;
            dtRow.Comment = myStruct.Comment;
            dtRow.NoRule = myStruct.NoRule;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_RuleConclusionDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcRuleConclusion myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleConclusionRow dtRow = new Rule.Rule_DS.DS_RuleConclusionDataTable().NewDS_RuleConclusionRow();

                dtRow.NoRuleNode = myStruct.NoRuleNode;
                dtRow.Name = myStruct.Name;
                dtRow.Comment = myStruct.Comment;
                dtRow.NoRule = myStruct.NoRule;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            DAupdt.DS_RuleConclusionDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [RuleConclusion] SET " + FieldsSetString + " WHERE NoRuleNode = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcRuleConclusion> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcRuleConclusion> myStrcList = new List<strcRuleConclusion>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleConclusion] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleConclusion myStruct = new strcRuleConclusion();
                                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.Comment = (string)dt.Rows[i]["Comment"];
                myStruct.NoRule = (int)dt.Rows[i]["NoRule"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcRuleConclusion> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcRuleConclusion> myStrcList = new List<strcRuleConclusion>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleConclusion] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleConclusion myStruct = new strcRuleConclusion();
                                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.Comment = (string)dt.Rows[i]["Comment"];
                myStruct.NoRule = (int)dt.Rows[i]["NoRule"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleConclusion] Where " + FieldName + " = " + Value;
            String FiledPersianName = "عنوان,توضيحات,قاعده,شناسه سامانه شرکت";
            String FiledEnglishName = "RuleConclusion.Name,RuleConclusion.Comment,Rule.Name,RuleConclusion.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcRuleConclusion> Select()
        {

            DataTable dt;
            List<strcRuleConclusion> myStrcList = new List<strcRuleConclusion>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleConclusion]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleConclusion myStruct = new strcRuleConclusion();
                                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.Comment = (string)dt.Rows[i]["Comment"];
                myStruct.NoRule = (int)dt.Rows[i]["NoRule"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcRuleConclusion SelectByNo(strcRuleConclusion myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_RuleConclusionRow dtRow = new Rule.Rule_DS.DS_RuleConclusionDataTable().NewDS_RuleConclusionRow();

            Rule.Rule_DS.DS_RuleConclusionDataTable dt = new Rule.Rule_DS.DS_RuleConclusionDataTable();

            dt = DAslct.DS_RuleConclusionDataTable_SelectCommand(" WHERE NoRuleNode = " + myStruct.NoRuleNode);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoRuleNode = 0;
                myStruct.Name = "0";
                myStruct.Comment = "0";
                myStruct.NoRule = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.Comment = (string)dt.Rows[0]["Comment"];
            myStruct.NoRule = (int)dt.Rows[0]["NoRule"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NoRuleNode] as 'PKNoRuleNode', [Name] as 'عنوان' , [Comment] as 'توضيحات' ,[NoRule] as 'FKNoRule', 'قاعده' = (SELECT [Name] FROM [Rule] WHERE TRuleConclusion.[NoRule] = []), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [RuleConclusion] AS TRuleConclusion";
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
                string Qry1 = "SELECT  [NoRuleNode] as 'PKNoRuleNode', [Name] as 'عنوان' , [Comment] as 'توضيحات' ,[NoRule] as 'FKNoRule', 'قاعده' = (SELECT [Name] FROM [Rule] WHERE TRuleConclusion.[NoRule] = []), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [RuleConclusion] AS TRuleConclusion";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill RuleConclusion Table Into DataGrid By NoRule Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRule.strcRule myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
			return DAslct.DS_RuleConclusionDataTable_SelectCommand(" WHERE NoRule = " + myStruct.NoEntity);

        }

  //End FillTable Function


        public DataTable GetDataTableNoRuleDescriptive()
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Rule]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleDescriptive(string strName)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Rule] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleDescriptive(string strName,string strwhere)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Rule] where Name like '" + strName + "%' and " + strwhere;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002130 + ")";

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



        public string GetAtt( int NoRuleNode , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [RuleConclusion]." + FieldName[0] + " From RuleConclusion Where [NoRuleNode] = " + NoRuleNode;
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

        public void SetAtt( int NoRuleNode , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [RuleConclusion] SET [" + FieldName[0] + "] = '" + value + "' where [NoRuleNode] = " + NoRuleNode;
            else if (FieldName[1] == "6")
                Str = "Update [RuleConclusion] SET [" + FieldName[0] + "] = " + value + " where [NoRuleNode] = " + NoRuleNode;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoRuleNode] as 'PKNoRuleNode', [Name] as 'عنوان' , [Comment] as 'توضيحات' ,[NoRule] as 'FKNoRule', 'قاعده' = (SELECT [Name] FROM [Rule] WHERE TRuleConclusion.[NoRule] = []), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [RuleConclusion] AS TRuleConclusion Where TRuleConclusion." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoRuleNode] as 'PKNoRuleNode', [Name] as 'عنوان' , [Comment] as 'توضيحات' ,[NoRule] as 'FKNoRule', 'قاعده' = (SELECT [Name] FROM [Rule] WHERE TRuleConclusion.[NoRule] = []), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [RuleConclusion] AS TRuleConclusion Where TRuleConclusion." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002130) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002130) " + 
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


        public strcRuleConclusion GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsRuleConclusion.strcRuleConclusion myStruct = new clsRuleConclusion.strcRuleConclusion();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [RuleConclusion] Where [RuleConclusion]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.Comment = (string)dt.Rows[0]["Comment"];
            myStruct.NoRule = (int)dt.Rows[0]["NoRule"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [RuleConclusion] where " + FieldName + " = '" + FieldValue + "'";
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