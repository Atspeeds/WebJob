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
    public class clsVariantValue
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcVariantValue
        {
            public int No;
            public int Value;
            public int NoRuleSimple;
            public int NoOprationEnvironment;

        } //End Struct

        public clsVariantValue()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcVariantValue GetNewRow()
        {
            strcVariantValue objNewStruct =  new strcVariantValue() ;
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcVariantValue myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_VariantValueRow dtRow = new Rule.Rule_DS.DS_VariantValueDataTable().NewDS_VariantValueRow();

                dtRow.No = myStruct.No;
                dtRow.Value = myStruct.Value;
                dtRow.NoRuleSimple = myStruct.NoRuleSimple;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
                DAdlt.DS_VariantValueDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [VariantValue] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcVariantValue myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_VariantValueRow dtRow = new Rule.Rule_DS.DS_VariantValueDataTable().NewDS_VariantValueRow();

            dtRow.No = myStruct.No;
            dtRow.Value = myStruct.Value;
            dtRow.NoRuleSimple = myStruct.NoRuleSimple;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_VariantValueDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcVariantValue myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_VariantValueRow dtRow = new Rule.Rule_DS.DS_VariantValueDataTable().NewDS_VariantValueRow();

                dtRow.No = myStruct.No;
                dtRow.Value = myStruct.Value;
                dtRow.NoRuleSimple = myStruct.NoRuleSimple;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            DAupdt.DS_VariantValueDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [VariantValue] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcVariantValue> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcVariantValue> myStrcList = new List<strcVariantValue>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [VariantValue] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcVariantValue myStruct = new strcVariantValue();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Value = (int)dt.Rows[i]["Value"];
                myStruct.NoRuleSimple = (int)dt.Rows[i]["NoRuleSimple"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcVariantValue> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcVariantValue> myStrcList = new List<strcVariantValue>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [VariantValue] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcVariantValue myStruct = new strcVariantValue();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Value = (int)dt.Rows[i]["Value"];
                myStruct.NoRuleSimple = (int)dt.Rows[i]["NoRuleSimple"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [VariantValue] Where " + FieldName + " = " + Value;
            String FiledPersianName = "مقدار,شناسه شرايط ساده قاعده,شناسه سامانه شرکت";
            String FiledEnglishName = "VariantValue.Value,VariantValue.NoRuleSimple,VariantValue.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcVariantValue> Select()
        {

            DataTable dt;
            List<strcVariantValue> myStrcList = new List<strcVariantValue>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [VariantValue]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcVariantValue myStruct = new strcVariantValue();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Value = (int)dt.Rows[i]["Value"];
                myStruct.NoRuleSimple = (int)dt.Rows[i]["NoRuleSimple"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcVariantValue SelectByNo(strcVariantValue myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_VariantValueRow dtRow = new Rule.Rule_DS.DS_VariantValueDataTable().NewDS_VariantValueRow();

            Rule.Rule_DS.DS_VariantValueDataTable dt = new Rule.Rule_DS.DS_VariantValueDataTable();

            dt = DAslct.DS_VariantValueDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.Value = 0;
                myStruct.NoRuleSimple = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Value = (int)dt.Rows[0]["Value"];
            myStruct.NoRuleSimple = (int)dt.Rows[0]["NoRuleSimple"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [Value] as 'مقدار' , [NoRuleSimple] as 'FKNoRuleSimple' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [VariantValue] AS TVariantValue";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [Value] as 'مقدار' , [NoRuleSimple] as 'FKNoRuleSimple' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [VariantValue] AS TVariantValue";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill VariantValue Table Into DataGrid By NoRuleSimple Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRuleSimple.strcRuleSimple myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_VariantValueDataTable_SelectCommand(" WHERE NoRuleSimple = " + myStruct.NoRuleCondition);

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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002133 + ")";

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

            string Str = "Select [VariantValue]." + FieldName[0] + " From VariantValue Where [No] = " + No;
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
                Str = "Update [VariantValue] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [VariantValue] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Value] as 'مقدار' , [NoRuleSimple] as 'FKNoRuleSimple' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [VariantValue] AS TVariantValue Where TVariantValue." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Value] as 'مقدار' , [NoRuleSimple] as 'FKNoRuleSimple' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [VariantValue] AS TVariantValue Where TVariantValue." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002133) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002133) " + 
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


        public strcVariantValue GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsVariantValue.strcVariantValue myStruct = new clsVariantValue.strcVariantValue();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [VariantValue] Where [VariantValue]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Value = (int)dt.Rows[0]["Value"];
            myStruct.NoRuleSimple = (int)dt.Rows[0]["NoRuleSimple"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [VariantValue] where " + FieldName + " = '" + FieldValue + "'";
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