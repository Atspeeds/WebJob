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
    public class clsRun
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcRun
        {
            public int No;
            public int NoDiagram;
            public int NoEntityClass;
            public string Name;
            public int NoParent;
            public int IDDiagramState;
            public int NoOprationEnvironment;

        } //End Struct

        public clsRun()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcRun GetNewRow()
        {
            strcRun objNewStruct =  new strcRun() ;
            objNewStruct.Name = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcRun myStruct)
        {
                Diagram.Diagram DAdlt = new Diagram.Diagram(StrCon1);
                Diagram.Diagram_DS.DS_RunRow dtRow = new Diagram.Diagram_DS.DS_RunDataTable().NewDS_RunRow();

                dtRow.No = myStruct.No;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.NoEntityClass = myStruct.NoEntityClass;
                dtRow.Name = myStruct.Name;
                dtRow.NoParent = myStruct.NoParent;
                dtRow.IDDiagramState = myStruct.IDDiagramState;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

                DAdlt.DS_RunDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [Run] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcRun myStruct)
        {
            Diagram.Diagram DAinsrt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_RunRow dtRow = new Diagram.Diagram_DS.DS_RunDataTable().NewDS_RunRow();

            dtRow.No = myStruct.No;
            dtRow.NoDiagram = myStruct.NoDiagram;
            dtRow.NoEntityClass = myStruct.NoEntityClass;
            dtRow.Name = myStruct.Name;
            dtRow.NoParent = myStruct.NoParent;
            dtRow.IDDiagramState = myStruct.IDDiagramState;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

            return DAinsrt.DS_RunDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcRun myStruct)
        {
            Diagram.Diagram DAupdt = new Diagram.Diagram(StrCon1);
            Diagram.Diagram_DS.DS_RunRow dtRow = new Diagram.Diagram_DS.DS_RunDataTable().NewDS_RunRow();

                dtRow.No = myStruct.No;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.NoEntityClass = myStruct.NoEntityClass;
                dtRow.Name = myStruct.Name;
                dtRow.NoParent = myStruct.NoParent;
                dtRow.IDDiagramState = myStruct.IDDiagramState;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

            DAupdt.DS_RunDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Run] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcRun> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcRun> myStrcList = new List<strcRun>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Run] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRun myStruct = new strcRun();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.IDDiagramState = (int)dt.Rows[i]["IDDiagramState"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcRun> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcRun> myStrcList = new List<strcRun>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Run] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRun myStruct = new strcRun();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.IDDiagramState = (int)dt.Rows[i]["IDDiagramState"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Run] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نمودار وضعيت,کلاس هاي جداول,عنوان,اجراي نمودار,شناسه وضعيت چرخه,شناسه سامانه شرکت";
            String FiledEnglishName = "Diagram.PersianName,EntityClass.Name,Run.Name,Run.Name,Run.IDDiagramState,Run.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcRun> Select()
        {

            DataTable dt;
            List<strcRun> myStrcList = new List<strcRun>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Run]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRun myStruct = new strcRun();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.IDDiagramState = (int)dt.Rows[i]["IDDiagramState"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcRun SelectByNo(strcRun myStruct)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            //Diagram.Diagram_DS.DS_RunRow dtRow = new Diagram.Diagram_DS.DS_RunDataTable().NewDS_RunRow();

            Diagram.Diagram_DS.DS_RunDataTable dt = new Diagram.Diagram_DS.DS_RunDataTable();

            dt = DAslct.DS_RunDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoDiagram = 0;
                myStruct.NoEntityClass = 0;
                myStruct.Name = "0";
                myStruct.NoParent = 0;
                myStruct.IDDiagramState = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.NoEntityClass = (int)dt.Rows[0]["NoEntityClass"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.IDDiagramState = (int)dt.Rows[0]["IDDiagramState"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TRun.[NoDiagram] = [NoEntity]),[NoEntityClass] as 'FKNoEntityClass', 'کلاس هاي جداول' = (SELECT [Name] FROM [EntityClass] WHERE TRun.[NoEntityClass] = [No]), [Name] as 'عنوان' ,[NoParent] as 'FKNoParent', 'اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TRun.[NoParent] = [No]), [IDDiagramState] as 'شناسه وضعيت چرخه' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [Run] AS TRun";
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
                string Qry1 = "SELECT  [No] as 'PKNo',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TRun.[NoDiagram] = [NoEntity]),[NoEntityClass] as 'FKNoEntityClass', 'کلاس هاي جداول' = (SELECT [Name] FROM [EntityClass] WHERE TRun.[NoEntityClass] = [No]), [Name] as 'عنوان' ,[NoParent] as 'FKNoParent', 'اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TRun.[NoParent] = [No]), [IDDiagramState] as 'شناسه وضعيت چرخه' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [Run] AS TRun";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill Run Table Into DataGrid By NoDiagram Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsDiagram.strcDiagram myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_RunDataTable_SelectCommand(" WHERE NoDiagram = " + myStruct.NoEntity);

        }

        /// <summary>
        /// This Function Will Fill Run Table Into DataGrid By NoEntityClass Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityClass.strcEntityClass myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_RunDataTable_SelectCommand(" WHERE NoEntityClass = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill Run Table Into DataGrid By NoParent Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRun.strcRun myStruct )
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            return DAslct.DS_RunDataTable_SelectCommand(" WHERE NoParent = " + myStruct.No);

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
        public DataTable GetDataTableNoDiagramDescriptive(string strName)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoDiagramDescriptive(string strName,string strwhere)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityClassDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityClass]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityClassDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityClass] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityClassDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityClass] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive(string strName)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive(string strName,string strwhere)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [Run] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


  //End FillComboBYLookUp



        public DataTable NoParent_SetTree(int NoParent)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = "WITH Tree " + 
                    "as (SELECT [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  , '' as 'Icon' FROM Run " + 
                    "where [Run].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [Run].No as No,[Run].Name,[Run].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [Run] ON tree.No = [Run].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [Run].No as No,[Run].Name,[Run].NoParent  , '' as 'Icon' FROM [Run] where [Run].No = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable NoParent_SetTree(int NoParent,string FieldName)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = "WITH Tree " + 
                    "as (SELECT [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  , '' as 'Icon' FROM [Run] " + 
                    "where [Run].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [Run].No as No,[Run].Name,[Run].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [Run] ON tree.No = [Run].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [Run].No as No,[Run].Name,[Run].NoParent  , '' as 'Icon' FROM [Run] where [Run]." + FieldName +" = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable GetTreeOneLevel(int NoParent,bool blroot)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery ="";
            if (blroot == true)
                objQuery = string.Format(" Select * From (Select [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  FROM [Run] where [Run].NoParent =-1  union all " + 
                " Select [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  FROM [Run] where [Run].NoParent ={0} )as  t1 order by  No", NoParent);
            else
                objQuery = string.Format(" select -1 as no,'ریشه' as name,-1 as noparent union Select * From (Select [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  FROM [Run] where [Run].NoParent =-1  union all " + 
                " Select [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  FROM [Run] where [Run].NoParent ={0} )as  t1 order by  No", NoParent);
            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery);
            return DAC.GeneralSelectCommand(objcmd);
        }
        public DataTable GetChildLevel(int NoParent)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = string.Format(" Select [Run].No as No,[Run].Name as Name,[Run].NoParent as NoParent  , ChildCount=( Select COUNT(*) from [Run] OU1 Where OU1.NoParent = [Run].No) FROM [Run] where [Run].NoParent ={0} order by  No", NoParent);
            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery);
            return DAC.GeneralSelectCommand(objcmd);
        }



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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001081 + ")";

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

            string Str = "Select [Run]." + FieldName[0] + " From Run Where [No] = " + No;
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
                Str = "Update [Run] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [Run] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TRun.[NoDiagram] = [NoEntity]),[NoEntityClass] as 'FKNoEntityClass', 'کلاس هاي جداول' = (SELECT [Name] FROM [EntityClass] WHERE TRun.[NoEntityClass] = [No]), [Name] as 'عنوان' ,[NoParent] as 'FKNoParent', 'اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TRun.[NoParent] = [No]), [IDDiagramState] as 'شناسه وضعيت چرخه' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [Run] AS TRun Where TRun." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TRun.[NoDiagram] = [NoEntity]),[NoEntityClass] as 'FKNoEntityClass', 'کلاس هاي جداول' = (SELECT [Name] FROM [EntityClass] WHERE TRun.[NoEntityClass] = [No]), [Name] as 'عنوان' ,[NoParent] as 'FKNoParent', 'اجراي نمودار' = (SELECT [Name] FROM [Run] WHERE TRun.[NoParent] = [No]), [IDDiagramState] as 'شناسه وضعيت چرخه' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [Run] AS TRun Where TRun." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001081) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001081) " + 
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


        public strcRun GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsRun.strcRun myStruct = new clsRun.strcRun();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Run] Where [Run]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.NoEntityClass = (int)dt.Rows[0]["NoEntityClass"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.IDDiagramState = (int)dt.Rows[0]["IDDiagramState"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [Run] where " + FieldName + " = '" + FieldValue + "'";
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