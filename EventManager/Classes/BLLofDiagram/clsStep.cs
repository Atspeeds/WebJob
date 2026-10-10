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
    public class clsStep
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcStep
        {
            public int No;
            public string EnglishName;
            public int Normalduration;
            public int NoStateType;
            public int NoProgram;
            public int NoDiagram;
            public string PersianName;
            public double Top;
            public double Left;
            public double Height;
            public double Width;
            public short IdStepType;
            public string Color;
            public double Percents;

        } //End Struct

        public clsStep()
        {

			//StrCon1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
        }  //End Behin Function

        public strcStep GetNewRow()
        {
            return new strcStep() ;

        }//End GetNewRow Function


        public int Delete(strcStep myStruct)
        {
                Diagram.Diagram DAdlt = new Diagram.Diagram(StrCon1);
                Diagram.Diagram_DS.DS_StepRow dtRow = new Diagram.Diagram_DS.DS_StepDataTable().NewDS_StepRow();

                dtRow.No = myStruct.No;
                dtRow.EnglishName = myStruct.EnglishName;
                dtRow.Normalduration = myStruct.Normalduration;
                dtRow.NoStateType = myStruct.NoStateType;
                dtRow.NoProgram = myStruct.NoProgram;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.Top = myStruct.Top;
                dtRow.Left = myStruct.Left;
                dtRow.Height = myStruct.Height;
                dtRow.Width = myStruct.Width;
                dtRow.IdStepType = myStruct.IdStepType;
                dtRow.Color = myStruct.Color;
                dtRow.Percents = myStruct.Percents;

                DAdlt.DS_StepDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [Step] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcStep myStruct)
        {
            Diagram.Diagram DAinsrt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_StepRow dtRow = new Diagram.Diagram_DS.DS_StepDataTable().NewDS_StepRow();

            dtRow.No = myStruct.No;
            dtRow.EnglishName = myStruct.EnglishName;
            dtRow.Normalduration = myStruct.Normalduration;
            dtRow.NoStateType = myStruct.NoStateType;
            dtRow.NoProgram = myStruct.NoProgram;
            dtRow.NoDiagram = myStruct.NoDiagram;
            dtRow.PersianName = myStruct.PersianName;
            dtRow.Top = myStruct.Top;
            dtRow.Left = myStruct.Left;
            dtRow.Height = myStruct.Height;
            dtRow.Width = myStruct.Width;
            dtRow.IdStepType = myStruct.IdStepType;
            dtRow.Color = myStruct.Color;
            dtRow.Percents = myStruct.Percents;

            return DAinsrt.DS_StepDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcStep myStruct)
        {
            Diagram.Diagram DAupdt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_StepRow dtRow = new Diagram.Diagram_DS.DS_StepDataTable().NewDS_StepRow();

                dtRow.No = myStruct.No;
                dtRow.EnglishName = myStruct.EnglishName;
                dtRow.Normalduration = myStruct.Normalduration;
                dtRow.NoStateType = myStruct.NoStateType;
                dtRow.NoProgram = myStruct.NoProgram;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.Top = myStruct.Top;
                dtRow.Left = myStruct.Left;
                dtRow.Height = myStruct.Height;
                dtRow.Width = myStruct.Width;
                dtRow.IdStepType = myStruct.IdStepType;
                dtRow.Color = myStruct.Color;
                dtRow.Percents = myStruct.Percents;

            DAupdt.DS_StepDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Step] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcStep> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcStep> myStrcList = new List<strcStep>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Step] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcStep myStruct = new strcStep();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.EnglishName = (string)dt.Rows[i]["EnglishName"];
                myStruct.Normalduration = (int)dt.Rows[i]["Normalduration"];
                myStruct.NoStateType = (int)dt.Rows[i]["NoStateType"];
                myStruct.NoProgram = (int)dt.Rows[i]["NoProgram"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.Top = (double)dt.Rows[i]["Top"];
                myStruct.Left = (double)dt.Rows[i]["Left"];
                myStruct.Height = (double)dt.Rows[i]["Height"];
                myStruct.Width = (double)dt.Rows[i]["Width"];
                myStruct.IdStepType = (short)dt.Rows[i]["IdStepType"];
                myStruct.Color = (string)dt.Rows[i]["Color"];
                myStruct.Percents = (double)dt.Rows[i]["Percents"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Step] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نام گام,مدت و بقاء نرمال مرحله نمودار,شناسه نوع وضعيت,شناسه برنامه,نمودار وضعيت,نام فاسي,فاصله از بالاي صفحه,فاصله از چپ صفحه,عرض,طول,نوع گام,رنگ,درصد";
            String FiledEnglishName = "Step.EnglishName,Step.Normalduration,Step.NoStateType,Step.NoProgram,Diagram.PersianName,Step.PersianName,Step.Top,Step.Left,Step.Height,Step.Width,Step.IdStepType,Step.Color,Step.Percents";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcStep> Select()
        {

            DataTable dt;
            List<strcStep> myStrcList = new List<strcStep>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Step]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcStep myStruct = new strcStep();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.EnglishName = (string)dt.Rows[i]["EnglishName"];
                myStruct.Normalduration = (int)dt.Rows[i]["Normalduration"];
                myStruct.NoStateType = (int)dt.Rows[i]["NoStateType"];
                myStruct.NoProgram = (int)dt.Rows[i]["NoProgram"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.Top = (double)dt.Rows[i]["Top"];
                myStruct.Left = (double)dt.Rows[i]["Left"];
                myStruct.Height = (double)dt.Rows[i]["Height"];
                myStruct.Width = (double)dt.Rows[i]["Width"];
                myStruct.IdStepType = (short)dt.Rows[i]["IdStepType"];
                myStruct.Color = (string)dt.Rows[i]["Color"];
                myStruct.Percents = (double)dt.Rows[i]["Percents"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcStep SelectByNo(strcStep myStruct)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            //Diagram.Diagram_DS.DS_StepRow dtRow = new Diagram.Diagram_DS.DS_StepDataTable().NewDS_StepRow();

            Diagram.Diagram_DS.DS_StepDataTable dt = new Diagram.Diagram_DS.DS_StepDataTable();

            dt = DAslct.DS_StepDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.EnglishName = "0";
                myStruct.Normalduration = 0;
                myStruct.NoStateType = 0;
                myStruct.NoProgram = 0;
                myStruct.NoDiagram = 0;
                myStruct.PersianName = "0";
                myStruct.Color = "0";

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.EnglishName = (string)dt.Rows[0]["EnglishName"];
            myStruct.Normalduration = (int)dt.Rows[0]["Normalduration"];
            myStruct.NoStateType = (int)dt.Rows[0]["NoStateType"];
            myStruct.NoProgram = (int)dt.Rows[0]["NoProgram"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.Top = (double)dt.Rows[0]["Top"];
            myStruct.Left = (double)dt.Rows[0]["Left"];
            myStruct.Height = (double)dt.Rows[0]["Height"];
            myStruct.Width = (double)dt.Rows[0]["Width"];
            myStruct.IdStepType = (short)dt.Rows[0]["IdStepType"];
            myStruct.Color = (string)dt.Rows[0]["Color"];
            myStruct.Percents = (double)dt.Rows[0]["Percents"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [EnglishName] as 'نام گام' , [Normalduration] as 'مدت و بقاء نرمال مرحله نمودار' , [NoStateType] as 'شناسه نوع وضعيت' , [NoProgram] as 'FKNoProgram' ,[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TStep.[NoDiagram] = [NoEntity]), [PersianName] as 'نام فاسي' , [Top] as 'فاصله از بالاي صفحه' , [Left] as 'فاصله از چپ صفحه' , [Height] as 'عرض' , [Width] as 'طول' , [IdStepType] as 'نوع گام' , [Color] as 'رنگ' , [Percents] as 'درصد'  FROM [Step] AS TStep";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [EnglishName] as 'نام گام' , [Normalduration] as 'مدت و بقاء نرمال مرحله نمودار' , [NoStateType] as 'شناسه نوع وضعيت' , [NoProgram] as 'FKNoProgram' ,[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TStep.[NoDiagram] = [NoEntity]), [PersianName] as 'نام فاسي' , [Top] as 'فاصله از بالاي صفحه' , [Left] as 'فاصله از چپ صفحه' , [Height] as 'عرض' , [Width] as 'طول' , [IdStepType] as 'نوع گام' , [Color] as 'رنگ' , [Percents] as 'درصد'  FROM [Step] AS TStep";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill Step Table Into DataGrid By NoDiagram Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsDiagram.strcDiagram myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_StepDataTable_SelectCommand(" WHERE NoDiagram = " + myStruct.NoEntity);

        }

  //End FillTable Function


        public DataTable GetDataTableNoDiagramDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',PersianName as 'Name' FROM [Step]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


        public DataTable GetDataTableIdStepTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 2 ORDER BY Num";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001083 + ")";

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

            string Str = "Select [Step]." + FieldName[0] + " From Step Where [No] = " + No;
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
                Str = "Update [Step] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [Step] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [EnglishName] as 'نام گام' , [Normalduration] as 'مدت و بقاء نرمال مرحله نمودار' , [NoStateType] as 'شناسه نوع وضعيت' , [NoProgram] as 'FKNoProgram' ,[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TStep.[NoDiagram] = [NoEntity]), [PersianName] as 'نام فاسي' , [Top] as 'فاصله از بالاي صفحه' , [Left] as 'فاصله از چپ صفحه' , [Height] as 'عرض' , [Width] as 'طول' , [IdStepType] as 'نوع گام' , [Color] as 'رنگ' , [Percents] as 'درصد'  From [Step] AS TStep Where TStep." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001083) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001083) " + 
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


        public strcStep GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsStep.strcStep myStruct = new clsStep.strcStep();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Step] Where [Step]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.EnglishName = (string)dt.Rows[0]["EnglishName"];
            myStruct.Normalduration = (int)dt.Rows[0]["Normalduration"];
            myStruct.NoStateType = (int)dt.Rows[0]["NoStateType"];
            myStruct.NoProgram = (int)dt.Rows[0]["NoProgram"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.Top = (double)dt.Rows[0]["Top"];
            myStruct.Left = (double)dt.Rows[0]["Left"];
            myStruct.Height = (double)dt.Rows[0]["Height"];
            myStruct.Width = (double)dt.Rows[0]["Width"];
            myStruct.IdStepType = (short)dt.Rows[0]["IdStepType"];
            myStruct.Color = (string)dt.Rows[0]["Color"];
            myStruct.Percents = (double)dt.Rows[0]["Percents"];


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