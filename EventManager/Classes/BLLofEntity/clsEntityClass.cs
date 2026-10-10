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
    public class clsEntityClass
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcEntityClass
        {
            public int No;
            public int NoEntityType;
            public int NoTable;
            public string Name;
            public bool IsMain;
            public int NoDiagram;
            public int NoOprationEnvironment;

        } //End Struct

        public clsEntityClass()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcEntityClass GetNewRow()
        {
            strcEntityClass objNewStruct =  new strcEntityClass() ;
            objNewStruct.Name = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcEntityClass myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_EntityClassRow dtRow = new Entity.Entity_DS.DS_EntityClassDataTable().NewDS_EntityClassRow();

                dtRow.No = myStruct.No;
                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.NoTable = myStruct.NoTable;
                dtRow.Name = myStruct.Name;
                dtRow.IsMain = myStruct.IsMain;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

                DAdlt.DS_EntityClassDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [EntityClass] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcEntityClass myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityClassRow dtRow = new Entity.Entity_DS.DS_EntityClassDataTable().NewDS_EntityClassRow();

            dtRow.No = myStruct.No;
            dtRow.NoEntityType = myStruct.NoEntityType;
            dtRow.NoTable = myStruct.NoTable;
            dtRow.Name = myStruct.Name;
            dtRow.IsMain = myStruct.IsMain;
            dtRow.NoDiagram = myStruct.NoDiagram;
            dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

            return DAinsrt.DS_EntityClassDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcEntityClass myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityClassRow dtRow = new Entity.Entity_DS.DS_EntityClassDataTable().NewDS_EntityClassRow();

                dtRow.No = myStruct.No;
                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.NoTable = myStruct.NoTable;
                dtRow.Name = myStruct.Name;
                dtRow.IsMain = myStruct.IsMain;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

            DAupdt.DS_EntityClassDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [EntityClass] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEntityClass> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEntityClass> myStrcList = new List<strcEntityClass>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClass] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityClass myStruct = new strcEntityClass();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.NoTable = (int)dt.Rows[i]["NoTable"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsMain = (bool)dt.Rows[i]["IsMain"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcEntityClass> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcEntityClass> myStrcList = new List<strcEntityClass>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClass] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityClass myStruct = new strcEntityClass();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.NoTable = (int)dt.Rows[i]["NoTable"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsMain = (bool)dt.Rows[i]["IsMain"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClass] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نوع موجوديت,جداول,نام کلاس,اصلي است,شناسه نمودار وضعيت,شناسه سامانه شرکت";
            String FiledEnglishName = "EntityType.Name,Tables.Name,EntityClass.Name,EntityClass.IsMain,EntityClass.NoDiagram,EntityClass.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEntityClass> Select()
        {

            DataTable dt;
            List<strcEntityClass> myStrcList = new List<strcEntityClass>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClass]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityClass myStruct = new strcEntityClass();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.NoTable = (int)dt.Rows[i]["NoTable"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsMain = (bool)dt.Rows[i]["IsMain"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEntityClass SelectByNo(strcEntityClass myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_EntityClassRow dtRow = new Entity.Entity_DS.DS_EntityClassDataTable().NewDS_EntityClassRow();

            Entity.Entity_DS.DS_EntityClassDataTable dt = new Entity.Entity_DS.DS_EntityClassDataTable();

            dt = DAslct.DS_EntityClassDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoEntityType = 0;
                myStruct.NoTable = 0;
                myStruct.Name = "0";
                myStruct.NoDiagram = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.NoTable = (int)dt.Rows[0]["NoTable"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.IsMain = (bool)dt.Rows[0]["IsMain"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityClass.[NoEntityType] = [No]),[NoTable] as 'FKNoTable', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TEntityClass.[NoTable] = [NO]), [Name] as 'نام کلاس' , [IsMain] as 'اصلي است' , [NoDiagram] as 'FKNoDiagram' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EntityClass] AS TEntityClass";
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
                string Qry1 = "SELECT  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityClass.[NoEntityType] = [No]),[NoTable] as 'FKNoTable', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TEntityClass.[NoTable] = [NO]), [Name] as 'نام کلاس' , [IsMain] as 'اصلي است' , [NoDiagram] as 'FKNoDiagram' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EntityClass] AS TEntityClass";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill EntityClass Table Into DataGrid By NoEntityType Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityType.strcEntityType myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityClassDataTable_SelectCommand(" WHERE NoEntityType = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill EntityClass Table Into DataGrid By NoTable Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsTables.strcTables myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityClassDataTable_SelectCommand(" WHERE NoTable = " + myStruct.NO);

        }

  //End FillTable Function


        public DataTable GetDataTableNoEntityTypeDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityTypeDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityTypeDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTableDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTableDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoTableDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityClass]";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001113 + ")";

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

            string Str = "Select [EntityClass]." + FieldName[0] + " From EntityClass Where [No] = " + No;
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
                Str = "Update [EntityClass] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [EntityClass] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityClass.[NoEntityType] = [No]),[NoTable] as 'FKNoTable', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TEntityClass.[NoTable] = [NO]), [Name] as 'نام کلاس' , [IsMain] as 'اصلي است' , [NoDiagram] as 'FKNoDiagram' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EntityClass] AS TEntityClass Where TEntityClass." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityClass.[NoEntityType] = [No]),[NoTable] as 'FKNoTable', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TEntityClass.[NoTable] = [NO]), [Name] as 'نام کلاس' , [IsMain] as 'اصلي است' , [NoDiagram] as 'FKNoDiagram' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EntityClass] AS TEntityClass Where TEntityClass." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001113) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001113) " + 
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


        public strcEntityClass GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEntityClass.strcEntityClass myStruct = new clsEntityClass.strcEntityClass();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [EntityClass] Where [EntityClass]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.NoTable = (int)dt.Rows[0]["NoTable"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.IsMain = (bool)dt.Rows[0]["IsMain"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [EntityClass] where " + FieldName + " = '" + FieldValue + "'";
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