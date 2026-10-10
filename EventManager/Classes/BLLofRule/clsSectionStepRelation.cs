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
    public class clsSectionStepRelation
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcSectionStepRelation
        {
            public int No;
            public bool IsStart;
            public int NoRuleCondition;
            public int NoStepRelation;
            public int NoOprationEnvironment;

        } //End Struct

        public clsSectionStepRelation()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcSectionStepRelation GetNewRow()
        {
            strcSectionStepRelation objNewStruct =  new strcSectionStepRelation() ;
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcSectionStepRelation myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_SectionStepRelationRow dtRow = new Rule.Rule_DS.DS_SectionStepRelationDataTable().NewDS_SectionStepRelationRow();

                dtRow.No = myStruct.No;
                dtRow.IsStart = myStruct.IsStart;
                dtRow.NoRuleCondition = myStruct.NoRuleCondition;
                dtRow.NoStepRelation = myStruct.NoStepRelation;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

                DAdlt.DS_SectionStepRelationDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [SectionStepRelation] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcSectionStepRelation myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_SectionStepRelationRow dtRow = new Rule.Rule_DS.DS_SectionStepRelationDataTable().NewDS_SectionStepRelationRow();

            dtRow.No = myStruct.No;
            dtRow.IsStart = myStruct.IsStart;
            dtRow.NoRuleCondition = myStruct.NoRuleCondition;
            dtRow.NoStepRelation = myStruct.NoStepRelation;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            return DAinsrt.DS_SectionStepRelationDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcSectionStepRelation myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_SectionStepRelationRow dtRow = new Rule.Rule_DS.DS_SectionStepRelationDataTable().NewDS_SectionStepRelationRow();

                dtRow.No = myStruct.No;
                dtRow.IsStart = myStruct.IsStart;
                dtRow.NoRuleCondition = myStruct.NoRuleCondition;
                dtRow.NoStepRelation = myStruct.NoStepRelation;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            DAupdt.DS_SectionStepRelationDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [SectionStepRelation] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcSectionStepRelation> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcSectionStepRelation> myStrcList = new List<strcSectionStepRelation>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [SectionStepRelation] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcSectionStepRelation myStruct = new strcSectionStepRelation();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.IsStart = (bool)dt.Rows[i]["IsStart"];
                myStruct.NoRuleCondition = (int)dt.Rows[i]["NoRuleCondition"];
                myStruct.NoStepRelation = (int)dt.Rows[i]["NoStepRelation"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcSectionStepRelation> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcSectionStepRelation> myStrcList = new List<strcSectionStepRelation>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [SectionStepRelation] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcSectionStepRelation myStruct = new strcSectionStepRelation();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.IsStart = (bool)dt.Rows[i]["IsStart"];
                myStruct.NoRuleCondition = (int)dt.Rows[i]["NoRuleCondition"];
                myStruct.NoStepRelation = (int)dt.Rows[i]["NoStepRelation"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [SectionStepRelation] Where " + FieldName + " = " + Value;
            String FiledPersianName = "شروع,شناسه شرايط قاعده,,شناسه سامانه شرکت";
            String FiledEnglishName = "SectionStepRelation.IsStart,SectionStepRelation.NoRuleCondition,,SectionStepRelation.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcSectionStepRelation> Select()
        {

            DataTable dt;
            List<strcSectionStepRelation> myStrcList = new List<strcSectionStepRelation>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [SectionStepRelation]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcSectionStepRelation myStruct = new strcSectionStepRelation();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.IsStart = (bool)dt.Rows[i]["IsStart"];
                myStruct.NoRuleCondition = (int)dt.Rows[i]["NoRuleCondition"];
                myStruct.NoStepRelation = (int)dt.Rows[i]["NoStepRelation"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcSectionStepRelation SelectByNo(strcSectionStepRelation myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_SectionStepRelationRow dtRow = new Rule.Rule_DS.DS_SectionStepRelationDataTable().NewDS_SectionStepRelationRow();

            Rule.Rule_DS.DS_SectionStepRelationDataTable dt = new Rule.Rule_DS.DS_SectionStepRelationDataTable();

            dt = DAslct.DS_SectionStepRelationDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoRuleCondition = 0;
                myStruct.NoStepRelation = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.IsStart = (bool)dt.Rows[0]["IsStart"];
            myStruct.NoRuleCondition = (int)dt.Rows[0]["NoRuleCondition"];
            myStruct.NoStepRelation = (int)dt.Rows[0]["NoStepRelation"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [IsStart] as 'شروع' , [NoRuleCondition] as 'FKNoRuleCondition' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [SectionStepRelation] AS TSectionStepRelation";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [IsStart] as 'شروع' , [NoRuleCondition] as 'FKNoRuleCondition' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [SectionStepRelation] AS TSectionStepRelation";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill SectionStepRelation Table Into DataGrid By NoRuleCondition Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRuleCondition.strcRuleCondition myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_SectionStepRelationDataTable_SelectCommand(" WHERE NoRuleCondition = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill SectionStepRelation Table Into DataGrid By NoStepRelation Field
        /// </summary>
        /// <param name="tblName"></param>"
		//public DataTable FillThisDataGridView(clsStepRelation.strcStepRelation myStruct )
		//{
		//    Rule.Rule DAslct = new Rule.Rule(StrCon1);
		//    return DAslct.DS_SectionStepRelationDataTable_SelectCommand(" WHERE NoStepRelation = " + myStruct.No);

		//}

  //End FillTable Function


        public DataTable GetDataTableNoStepRelationDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [StepRelation]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepRelationDescriptive(string strName)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [StepRelation] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepRelationDescriptive(string strName,string strwhere)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [StepRelation] where Name like '" + strName + "%' and " + strwhere;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002146 + ")";

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

            string Str = "Select [SectionStepRelation]." + FieldName[0] + " From SectionStepRelation Where [No] = " + No;
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
                Str = "Update [SectionStepRelation] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [SectionStepRelation] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [IsStart] as 'شروع' , [NoRuleCondition] as 'FKNoRuleCondition' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [SectionStepRelation] AS TSectionStepRelation Where TSectionStepRelation." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [IsStart] as 'شروع' , [NoRuleCondition] as 'FKNoRuleCondition' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [SectionStepRelation] AS TSectionStepRelation Where TSectionStepRelation." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002146) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002146) " + 
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


        public strcSectionStepRelation GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsSectionStepRelation.strcSectionStepRelation myStruct = new clsSectionStepRelation.strcSectionStepRelation();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [SectionStepRelation] Where [SectionStepRelation]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.IsStart = (bool)dt.Rows[0]["IsStart"];
            myStruct.NoRuleCondition = (int)dt.Rows[0]["NoRuleCondition"];
            myStruct.NoStepRelation = (int)dt.Rows[0]["NoStepRelation"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [SectionStepRelation] where " + FieldName + " = '" + FieldValue + "'";
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