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
    public class clsRunStep
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcRunStep
        {
            public int NoRun;
            public int NoStep;
            public System.DateTime DateBegin;
            public System.DateTime DateEnd;
            public int No;

        } //End Struct

        public clsRunStep()
        {

			//StrCon1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
        }  //End Behin Function

        public strcRunStep GetNewRow()
        {
            return new strcRunStep() ;

        }//End GetNewRow Function


        public int Delete(strcRunStep myStruct)
        {
                Diagram.Diagram DAdlt = new Diagram.Diagram(StrCon1);
                Diagram.Diagram_DS.DS_RunStepRow dtRow = new Diagram.Diagram_DS.DS_RunStepDataTable().NewDS_RunStepRow();

                dtRow.NoRun = myStruct.NoRun;
                dtRow.NoStep = myStruct.NoStep;
                dtRow.DateBegin = myStruct.DateBegin;
                dtRow.DateEnd = myStruct.DateEnd;
                dtRow.No = myStruct.No;

                DAdlt.DS_RunStepDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [RunStep] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcRunStep myStruct)
        {
            Diagram.Diagram DAinsrt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_RunStepRow dtRow = new Diagram.Diagram_DS.DS_RunStepDataTable().NewDS_RunStepRow();

            dtRow.NoRun = myStruct.NoRun;
            dtRow.NoStep = myStruct.NoStep;
            dtRow.DateBegin = myStruct.DateBegin;
            dtRow.DateEnd = myStruct.DateEnd;
            dtRow.No = myStruct.No;

            return DAinsrt.DS_RunStepDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcRunStep myStruct)
        {
            Diagram.Diagram DAupdt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_RunStepRow dtRow = new Diagram.Diagram_DS.DS_RunStepDataTable().NewDS_RunStepRow();

                dtRow.NoRun = myStruct.NoRun;
                dtRow.NoStep = myStruct.NoStep;
                dtRow.DateBegin = myStruct.DateBegin;
                dtRow.DateEnd = myStruct.DateEnd;
                dtRow.No = myStruct.No;

            DAupdt.DS_RunStepDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [RunStep] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcRunStep> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcRunStep> myStrcList = new List<strcRunStep>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RunStep] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRunStep myStruct = new strcRunStep();
                                myStruct.NoRun = (int)dt.Rows[i]["NoRun"];
                myStruct.NoStep = (int)dt.Rows[i]["NoStep"];
                myStruct.DateBegin = (System.DateTime)dt.Rows[i]["DateBegin"];
                myStruct.DateEnd = (System.DateTime)dt.Rows[i]["DateEnd"];
                myStruct.No = (int)dt.Rows[i]["No"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RunStep] Where " + FieldName + " = " + Value;
            String FiledPersianName = "اجراي نمودار,گام هاي نمودار,تاريخ شروع,تاريخ پايان,";
            String FiledEnglishName = "Run.Name,Step.PersianName,RunStep.DateBegin,RunStep.DateEnd,";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcRunStep> Select()
        {

            DataTable dt;
            List<strcRunStep> myStrcList = new List<strcRunStep>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RunStep]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRunStep myStruct = new strcRunStep();
                                myStruct.NoRun = (int)dt.Rows[i]["NoRun"];
                myStruct.NoStep = (int)dt.Rows[i]["NoStep"];
                myStruct.DateBegin = (System.DateTime)dt.Rows[i]["DateBegin"];
                myStruct.DateEnd = (System.DateTime)dt.Rows[i]["DateEnd"];
                myStruct.No = (int)dt.Rows[i]["No"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcRunStep SelectByNo(strcRunStep myStruct)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            //Diagram.Diagram_DS.DS_RunStepRow dtRow = new Diagram.Diagram_DS.DS_RunStepDataTable().NewDS_RunStepRow();

            Diagram.Diagram_DS.DS_RunStepDataTable dt = new Diagram.Diagram_DS.DS_RunStepDataTable();

            dt = DAslct.DS_RunStepDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoRun = 0;
                myStruct.NoStep = 0;
                myStruct.No = 0;

                return myStruct ;
            }
            myStruct.NoRun = (int)dt.Rows[0]["NoRun"];
            myStruct.NoStep = (int)dt.Rows[0]["NoStep"];
            myStruct.DateBegin = (System.DateTime)dt.Rows[0]["DateBegin"];
            myStruct.DateEnd = (System.DateTime)dt.Rows[0]["DateEnd"];
            myStruct.No = (int)dt.Rows[0]["No"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  'اجراي نمودار' = (SELECT Name FROM Run"+
                " WHERE TRunStep.[NoRun] = No),[NoStep] as 'FKNoStep', 'گام هاي نمودار' = (SELECT [PersianName] FROM [Step] WHERE TRunStep.[NoStep] = [No]), 'تاريخ شروع' = dbo.mtosh( [DateBegin] ) , 'تاريخ پايان' = dbo.mtosh( [DateEnd] ) , [No] as 'PKNo' FROM [RunStep] AS TRunStep";
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
                string Qry1 = "SELECT  'اجراي نمودار' = (SELECT Name FROM Run"+
                " WHERE TRunStep.[NoRun] = No),[NoStep] as 'FKNoStep', 'گام هاي نمودار' = (SELECT [PersianName] FROM [Step] WHERE TRunStep.[NoStep] = [No]), 'تاريخ شروع' = dbo.mtosh( [DateBegin] ) , 'تاريخ پايان' = dbo.mtosh( [DateEnd] ) , [No] as 'PKNo' FROM [RunStep] AS TRunStep";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill RunStep Table Into DataGrid By NoRun Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRun.strcRun myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_RunStepDataTable_SelectCommand(" WHERE NoRun = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill RunStep Table Into DataGrid By NoStep Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsStep.strcStep myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_RunStepDataTable_SelectCommand(" WHERE NoStep = " + myStruct.No);

        }

  //End FillTable Function


        public DataTable GetDataTableNoRunDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoStepDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [Step]";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001082 + ")";

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

            string Str = "Select [RunStep]." + FieldName[0] + " From RunStep Where [No] = " + No;
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
                Str = "Update [RunStep] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [RunStep] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  'اجراي نمودار' = (SELECT Name FROM Run"+
                " WHERE TRunStep.[NoRun] = No),[NoStep] as 'FKNoStep', 'گام هاي نمودار' = (SELECT [PersianName] FROM [Step] WHERE TRunStep.[NoStep] = [No]), 'تاريخ شروع' = dbo.mtosh( [DateBegin] ) , 'تاريخ پايان' = dbo.mtosh( [DateEnd] ) , [No] as 'PKNo' From [RunStep] AS TRunStep Where TRunStep." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001082) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001082) " + 
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


        public strcRunStep GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsRunStep.strcRunStep myStruct = new clsRunStep.strcRunStep();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [RunStep] Where [RunStep]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoRun = (int)dt.Rows[0]["NoRun"];
            myStruct.NoStep = (int)dt.Rows[0]["NoStep"];
            myStruct.DateBegin = (System.DateTime)dt.Rows[0]["DateBegin"];
            myStruct.DateEnd = (System.DateTime)dt.Rows[0]["DateEnd"];
            myStruct.No = (int)dt.Rows[0]["No"];


            return myStruct;

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