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
    public class clsDiagram
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcDiagram
        {
            public int NoEntity;
            public string Name;
            public int NoDiagramType;
            public string PersianName;

        } //End Struct

        public clsDiagram()
        {
			Configuration config = new Configuration("app.conf");
			StrCon1 = config.GetConnectionString("BehinCnn");

			//StrCon1 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
        }  //End Behin Function

        public strcDiagram GetNewRow()
        {
            return new strcDiagram() ;

        }//End GetNewRow Function


        public int Delete(strcDiagram myStruct)
        {
                Diagram.Diagram DAdlt = new Diagram.Diagram(StrCon1);
                Diagram.Diagram_DS.DS_DiagramRow dtRow = new Diagram.Diagram_DS.DS_DiagramDataTable().NewDS_DiagramRow();

                dtRow.NoEntity = myStruct.NoEntity;
                dtRow.Name = myStruct.Name;
                dtRow.NoDiagramType = myStruct.NoDiagramType;
                dtRow.PersianName = myStruct.PersianName;

                DAdlt.DS_DiagramDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [Diagram] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcDiagram myStruct)
        {
            Diagram.Diagram DAinsrt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_DiagramRow dtRow = new Diagram.Diagram_DS.DS_DiagramDataTable().NewDS_DiagramRow();

            dtRow.NoEntity = myStruct.NoEntity;
            dtRow.Name = myStruct.Name;
            dtRow.NoDiagramType = myStruct.NoDiagramType;
            dtRow.PersianName = myStruct.PersianName;

            return DAinsrt.DS_DiagramDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcDiagram myStruct)
        {
            Diagram.Diagram DAupdt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_DiagramRow dtRow = new Diagram.Diagram_DS.DS_DiagramDataTable().NewDS_DiagramRow();

                dtRow.NoEntity = myStruct.NoEntity;
                dtRow.Name = myStruct.Name;
                dtRow.NoDiagramType = myStruct.NoDiagramType;
                dtRow.PersianName = myStruct.PersianName;

            DAupdt.DS_DiagramDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Diagram] SET " + FieldsSetString + " WHERE NoEntity = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcDiagram> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcDiagram> myStrcList = new List<strcDiagram>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Diagram] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcDiagram myStruct = new strcDiagram();
                                myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoDiagramType = (int)dt.Rows[i]["NoDiagramType"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Diagram] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نام نمودار وضعيت,نوع نمودار,نام فارسي";
            String FiledEnglishName = "Diagram.Name,DiagramType.Name,Diagram.PersianName";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcDiagram> Select()
        {

            DataTable dt;
            List<strcDiagram> myStrcList = new List<strcDiagram>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Diagram]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcDiagram myStruct = new strcDiagram();
                                myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoDiagramType = (int)dt.Rows[i]["NoDiagramType"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcDiagram SelectByNo(strcDiagram myStruct)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            //Diagram.Diagram_DS.DS_DiagramRow dtRow = new Diagram.Diagram_DS.DS_DiagramDataTable().NewDS_DiagramRow();

            Diagram.Diagram_DS.DS_DiagramDataTable dt = new Diagram.Diagram_DS.DS_DiagramDataTable();

            dt = DAslct.DS_DiagramDataTable_SelectCommand(" WHERE NoEntity = " + myStruct.NoEntity);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoEntity = 0;
                myStruct.Name = "0";
                myStruct.NoDiagramType = 0;
                myStruct.PersianName = "0";

                return myStruct ;
            }
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoDiagramType = (int)dt.Rows[0]["NoDiagramType"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NoEntity] as 'PKNoEntity', [Name] as 'نام نمودار وضعيت' ,[NoDiagramType] as 'FKNoDiagramType', 'نوع نمودار' = (SELECT [Name] FROM [DiagramType] WHERE TDiagram.[NoDiagramType] = [No]), [PersianName] as 'نام فارسي'  FROM [Diagram] AS TDiagram";
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
                string Qry1 = "SELECT  [NoEntity] as 'PKNoEntity', [Name] as 'نام نمودار وضعيت' ,[NoDiagramType] as 'FKNoDiagramType', 'نوع نمودار' = (SELECT [Name] FROM [DiagramType] WHERE TDiagram.[NoDiagramType] = [No]), [PersianName] as 'نام فارسي'  FROM [Diagram] AS TDiagram";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill Diagram Table Into DataGrid By NoDiagramType Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsDiagramType.strcDiagramType myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_DiagramDataTable_SelectCommand(" WHERE NoDiagramType = " + myStruct.No);

        }

  //End FillTable Function


        public DataTable GetDataTableNoDiagramTypeDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [DiagramType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram]";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001051 + ")";

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



        public string GetAtt( int NoEntity , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [Diagram]." + FieldName[0] + " From Diagram Where [NoEntity] = " + NoEntity;
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

        public void SetAtt( int NoEntity , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [Diagram] SET [" + FieldName[0] + "] = '" + value + "' where [NoEntity] = " + NoEntity;
            else if (FieldName[1] == "6")
                Str = "Update [Diagram] SET [" + FieldName[0] + "] = " + value + " where [NoEntity] = " + NoEntity;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoEntity] as 'PKNoEntity', [Name] as 'نام نمودار وضعيت' ,[NoDiagramType] as 'FKNoDiagramType', 'نوع نمودار' = (SELECT [Name] FROM [DiagramType] WHERE TDiagram.[NoDiagramType] = [No]), [PersianName] as 'نام فارسي'  From [Diagram] AS TDiagram Where TDiagram." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001051) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001051) " + 
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


        public strcDiagram GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsDiagram.strcDiagram myStruct = new clsDiagram.strcDiagram();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Diagram] Where [Diagram]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoDiagramType = (int)dt.Rows[0]["NoDiagramType"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];


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