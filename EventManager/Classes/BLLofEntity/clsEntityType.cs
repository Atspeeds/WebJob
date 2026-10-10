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
    public class clsEntityType
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcEntityType
        {
            public int No;
            public int NoViewDefault;
            public int NoParent;
            public int LevelNum;
            public int NoClause;
            public int NoOutPutDefault;
            public bool HasSelfAction;
            public int NoDiagramMainDefault;
            public int NoFileIconDefault;
            public string Name;
            public string PersianName;
            public int NoOprationEnvironment;
            public int NoTables;

        } //End Struct

        public clsEntityType()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcEntityType GetNewRow()
        {
            strcEntityType objNewStruct =  new strcEntityType() ;
            objNewStruct.Name = ""; 
            objNewStruct.PersianName = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcEntityType myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_EntityTypeRow dtRow = new Entity.Entity_DS.DS_EntityTypeDataTable().NewDS_EntityTypeRow();

                dtRow.No = myStruct.No;
                dtRow.NoViewDefault = myStruct.NoViewDefault;
                dtRow.NoParent = myStruct.NoParent;
                dtRow.LevelNum = myStruct.LevelNum;
                dtRow.NoClause = myStruct.NoClause;
                dtRow.NoOutPutDefault = myStruct.NoOutPutDefault;
                dtRow.HasSelfAction = myStruct.HasSelfAction;
                dtRow.NoDiagramMainDefault = myStruct.NoDiagramMainDefault;
                dtRow.NoFileIconDefault = myStruct.NoFileIconDefault;
                dtRow.Name = myStruct.Name;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
                dtRow.NoTables = myStruct.NoTables;

                DAdlt.DS_EntityTypeDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [EntityType] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcEntityType myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityTypeRow dtRow = new Entity.Entity_DS.DS_EntityTypeDataTable().NewDS_EntityTypeRow();

            string Qry ="SELECT MAX(No) FROM [EntityType]";
            object value = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DataTable dt = DAC.GeneralSelectCommand(objCommand);

            foreach (DataRow dr in dt.Rows)
            {
                value = dr[0];
            }

            int MaxNO  = Convert.ToInt32(value);
            MaxNO++;

            dtRow.No = MaxNO ;
            dtRow.NoViewDefault = myStruct.NoViewDefault;
            dtRow.NoParent = myStruct.NoParent;
            dtRow.LevelNum = myStruct.LevelNum;
            dtRow.NoClause = myStruct.NoClause;
            dtRow.NoOutPutDefault = myStruct.NoOutPutDefault;
            dtRow.HasSelfAction = myStruct.HasSelfAction;
            dtRow.NoDiagramMainDefault = myStruct.NoDiagramMainDefault;
            dtRow.NoFileIconDefault = myStruct.NoFileIconDefault;
            dtRow.Name = myStruct.Name;
            dtRow.PersianName = myStruct.PersianName;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
            dtRow.NoTables = myStruct.NoTables;

            return DAinsrt.DS_EntityTypeDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcEntityType myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityTypeRow dtRow = new Entity.Entity_DS.DS_EntityTypeDataTable().NewDS_EntityTypeRow();

                dtRow.No = myStruct.No;
                dtRow.NoViewDefault = myStruct.NoViewDefault;
                dtRow.NoParent = myStruct.NoParent;
                dtRow.LevelNum = myStruct.LevelNum;
                dtRow.NoClause = myStruct.NoClause;
                dtRow.NoOutPutDefault = myStruct.NoOutPutDefault;
                dtRow.HasSelfAction = myStruct.HasSelfAction;
                dtRow.NoDiagramMainDefault = myStruct.NoDiagramMainDefault;
                dtRow.NoFileIconDefault = myStruct.NoFileIconDefault;
                dtRow.Name = myStruct.Name;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
                dtRow.NoTables = myStruct.NoTables;

            DAupdt.DS_EntityTypeDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [EntityType] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEntityType> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEntityType> myStrcList = new List<strcEntityType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityType] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityType myStruct = new strcEntityType();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoViewDefault = (int)dt.Rows[i]["NoViewDefault"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.LevelNum = (int)dt.Rows[i]["LevelNum"];
                myStruct.NoClause = (int)dt.Rows[i]["NoClause"];
                myStruct.NoOutPutDefault = (int)dt.Rows[i]["NoOutPutDefault"];
                myStruct.HasSelfAction = (bool)dt.Rows[i]["HasSelfAction"];
                myStruct.NoDiagramMainDefault = (int)dt.Rows[i]["NoDiagramMainDefault"];
                myStruct.NoFileIconDefault = (int)dt.Rows[i]["NoFileIconDefault"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.NoTables = (int)dt.Rows[i]["NoTables"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcEntityType> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcEntityType> myStrcList = new List<strcEntityType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityType] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityType myStruct = new strcEntityType();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoViewDefault = (int)dt.Rows[i]["NoViewDefault"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.LevelNum = (int)dt.Rows[i]["LevelNum"];
                myStruct.NoClause = (int)dt.Rows[i]["NoClause"];
                myStruct.NoOutPutDefault = (int)dt.Rows[i]["NoOutPutDefault"];
                myStruct.HasSelfAction = (bool)dt.Rows[i]["HasSelfAction"];
                myStruct.NoDiagramMainDefault = (int)dt.Rows[i]["NoDiagramMainDefault"];
                myStruct.NoFileIconDefault = (int)dt.Rows[i]["NoFileIconDefault"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.NoTables = (int)dt.Rows[i]["NoTables"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityType] Where " + FieldName + " = " + Value;
            String FiledPersianName = "ديد,نوع موجوديت,شماره سطح,شناسه عبارت,,داراي کلاس کنش مي باشد,,,نام موجوديت,نام فارسي,شناسه سامانه شرکت,";
            String FiledEnglishName = "View.dllName,EntityType.Name,EntityType.LevelNum,EntityType.NoClause,,EntityType.HasSelfAction,,,EntityType.Name,EntityType.PersianName,EntityType.NoOprationEnvironment,";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEntityType> Select()
        {

            DataTable dt;
            List<strcEntityType> myStrcList = new List<strcEntityType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityType]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityType myStruct = new strcEntityType();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoViewDefault = (int)dt.Rows[i]["NoViewDefault"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.LevelNum = (int)dt.Rows[i]["LevelNum"];
                myStruct.NoClause = (int)dt.Rows[i]["NoClause"];
                myStruct.NoOutPutDefault = (int)dt.Rows[i]["NoOutPutDefault"];
                myStruct.HasSelfAction = (bool)dt.Rows[i]["HasSelfAction"];
                myStruct.NoDiagramMainDefault = (int)dt.Rows[i]["NoDiagramMainDefault"];
                myStruct.NoFileIconDefault = (int)dt.Rows[i]["NoFileIconDefault"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.NoTables = (int)dt.Rows[i]["NoTables"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEntityType SelectByNo(strcEntityType myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_EntityTypeRow dtRow = new Entity.Entity_DS.DS_EntityTypeDataTable().NewDS_EntityTypeRow();

            Entity.Entity_DS.DS_EntityTypeDataTable dt = new Entity.Entity_DS.DS_EntityTypeDataTable();

            dt = DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoViewDefault = 0;
                myStruct.NoParent = 0;
                myStruct.LevelNum = 0;
                myStruct.NoClause = 0;
                myStruct.NoOutPutDefault = 0;
                myStruct.NoDiagramMainDefault = 0;
                myStruct.NoFileIconDefault = 0;
                myStruct.Name = "0";
                myStruct.PersianName = "0";
                myStruct.NoOprationEnvironment = 0;
                myStruct.NoTables = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoViewDefault = (int)dt.Rows[0]["NoViewDefault"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.LevelNum = (int)dt.Rows[0]["LevelNum"];
            myStruct.NoClause = (int)dt.Rows[0]["NoClause"];
            myStruct.NoOutPutDefault = (int)dt.Rows[0]["NoOutPutDefault"];
            myStruct.HasSelfAction = (bool)dt.Rows[0]["HasSelfAction"];
            myStruct.NoDiagramMainDefault = (int)dt.Rows[0]["NoDiagramMainDefault"];
            myStruct.NoFileIconDefault = (int)dt.Rows[0]["NoFileIconDefault"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.NoTables = (int)dt.Rows[0]["NoTables"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo',[NoViewDefault] as 'FKNoViewDefault', 'ديد' = (SELECT [dllName] FROM [View] WHERE TEntityType.[NoViewDefault] = [NoComponent]),[NoParent] as 'FKNoParent', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityType.[NoParent] = [No]), [LevelNum] as 'شماره سطح' , [NoClause] as 'FKNoClause' , [HasSelfAction] as 'داراي کلاس کنش مي باشد' , [Name] as 'نام موجوديت' , [PersianName] as 'نام فارسي' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EntityType] AS TEntityType";
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
                string Qry1 = "SELECT  [No] as 'PKNo',[NoViewDefault] as 'FKNoViewDefault', 'ديد' = (SELECT [dllName] FROM [View] WHERE TEntityType.[NoViewDefault] = [NoComponent]),[NoParent] as 'FKNoParent', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityType.[NoParent] = [No]), [LevelNum] as 'شماره سطح' , [NoClause] as 'FKNoClause' , [HasSelfAction] as 'داراي کلاس کنش مي باشد' , [Name] as 'نام موجوديت' , [PersianName] as 'نام فارسي' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EntityType] AS TEntityType";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoViewDefault Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsView.strcView myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoViewDefault = " + myStruct.NoComponent);

        }

        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoParent Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityType.strcEntityType myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoParent = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoClause Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsClause.strcClause myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoClause = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoOutPutDefault Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsOutput.strcOutput myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoOutPutDefault = " + myStruct.NoEntity);

        }

        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoDiagramMainDefault Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsDiagram.strcDiagram myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoDiagramMainDefault = " + myStruct.NoEntity);

        }

        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoFileIconDefault Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsFile.strcFile myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoFileIconDefault = " + myStruct.NoEntity);

        }

        /// <summary>
        /// This Function Will Fill EntityType Table Into DataGrid By NoTables Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsTables.strcTables myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityTypeDataTable_SelectCommand(" WHERE NoTables = " + myStruct.NO);

        }

  //End FillTable Function


        public DataTable GetDataTableNoViewDefaultDescriptive()
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            string Qry = "SELECT NoComponent as 'No',dllName as 'Name' FROM [View]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoViewDefaultDescriptive(string strName)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            string Qry = "SELECT NoComponent as 'No',dllName as 'Name' FROM [View] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoViewDefaultDescriptive(string strName,string strwhere)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            string Qry = "SELECT NoComponent as 'No',dllName as 'Name' FROM [View] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoOutPutDefaultDescriptive()
        {
            Output.Output DAslct = new Output.Output(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Output]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoOutPutDefaultDescriptive(string strName)
        {
            Output.Output DAslct = new Output.Output(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Output] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoOutPutDefaultDescriptive(string strName,string strwhere)
        {
            Output.Output DAslct = new Output.Output(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Output] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoDiagramMainDefaultDescriptive()
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoDiagramMainDefaultDescriptive(string strName)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoDiagramMainDefaultDescriptive(string strName,string strwhere)
        {
            Diagram.Diagram DAslct = new Diagram.Diagram(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Diagram] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoFileIconDefaultDescriptive()
        {
            File.File DAslct = new File.File(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [File]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoFileIconDefaultDescriptive(string strName)
        {
            File.File DAslct = new File.File(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [File] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoFileIconDefaultDescriptive(string strName,string strwhere)
        {
            File.File DAslct = new File.File(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [File] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTablesDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%' and " + strwhere;
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
                    "as (SELECT [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  , '' as 'Icon' FROM EntityType " + 
                    "where [EntityType].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [EntityType].No as No,[EntityType].Name,[EntityType].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [EntityType] ON tree.No = [EntityType].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [EntityType].No as No,[EntityType].Name,[EntityType].NoParent  , '' as 'Icon' FROM [EntityType] where [EntityType].No = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable NoParent_SetTree(int NoParent,string FieldName)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = "WITH Tree " + 
                    "as (SELECT [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  , '' as 'Icon' FROM [EntityType] " + 
                    "where [EntityType].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [EntityType].No as No,[EntityType].Name,[EntityType].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [EntityType] ON tree.No = [EntityType].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [EntityType].No as No,[EntityType].Name,[EntityType].NoParent  , '' as 'Icon' FROM [EntityType] where [EntityType]." + FieldName +" = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable GetTreeOneLevel(int NoParent,bool blroot)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery ="";
            if (blroot == true)
                objQuery = string.Format(" Select * From (Select [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  FROM [EntityType] where [EntityType].NoParent =-1  union all " + 
                " Select [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  FROM [EntityType] where [EntityType].NoParent ={0} )as  t1 order by  No", NoParent);
            else
                objQuery = string.Format(" select -1 as no,'ریشه' as name,-1 as noparent union Select * From (Select [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  FROM [EntityType] where [EntityType].NoParent =-1  union all " + 
                " Select [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  FROM [EntityType] where [EntityType].NoParent ={0} )as  t1 order by  No", NoParent);
            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery);
            return DAC.GeneralSelectCommand(objcmd);
        }
        public DataTable GetChildLevel(int NoParent)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = string.Format(" Select [EntityType].No as No,[EntityType].Name as Name,[EntityType].NoParent as NoParent  , ChildCount=( Select COUNT(*) from [EntityType] OU1 Where OU1.NoParent = [EntityType].No) FROM [EntityType] where [EntityType].NoParent ={0} order by  No", NoParent);
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001049 + ")";

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

            string Str = "Select [EntityType]." + FieldName[0] + " From EntityType Where [No] = " + No;
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
                Str = "Update [EntityType] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [EntityType] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoViewDefault] as 'FKNoViewDefault', 'ديد' = (SELECT [dllName] FROM [View] WHERE TEntityType.[NoViewDefault] = [NoComponent]),[NoParent] as 'FKNoParent', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityType.[NoParent] = [No]), [LevelNum] as 'شماره سطح' , [NoClause] as 'FKNoClause' , [HasSelfAction] as 'داراي کلاس کنش مي باشد' , [Name] as 'نام موجوديت' , [PersianName] as 'نام فارسي' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EntityType] AS TEntityType Where TEntityType." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoViewDefault] as 'FKNoViewDefault', 'ديد' = (SELECT [dllName] FROM [View] WHERE TEntityType.[NoViewDefault] = [NoComponent]),[NoParent] as 'FKNoParent', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityType.[NoParent] = [No]), [LevelNum] as 'شماره سطح' , [NoClause] as 'FKNoClause' , [HasSelfAction] as 'داراي کلاس کنش مي باشد' , [Name] as 'نام موجوديت' , [PersianName] as 'نام فارسي' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EntityType] AS TEntityType Where TEntityType." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001049) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001049) " + 
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


        public strcEntityType GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEntityType.strcEntityType myStruct = new clsEntityType.strcEntityType();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [EntityType] Where [EntityType]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoViewDefault = (int)dt.Rows[0]["NoViewDefault"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.LevelNum = (int)dt.Rows[0]["LevelNum"];
            myStruct.NoClause = (int)dt.Rows[0]["NoClause"];
            myStruct.NoOutPutDefault = (int)dt.Rows[0]["NoOutPutDefault"];
            myStruct.HasSelfAction = (bool)dt.Rows[0]["HasSelfAction"];
            myStruct.NoDiagramMainDefault = (int)dt.Rows[0]["NoDiagramMainDefault"];
            myStruct.NoFileIconDefault = (int)dt.Rows[0]["NoFileIconDefault"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.NoTables = (int)dt.Rows[0]["NoTables"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [EntityType] where " + FieldName + " = '" + FieldValue + "'";
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