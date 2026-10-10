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
    public class clsTables
    {
        string StrCon1;
        string StrDb;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcTables
        {
            public int NO;
            public string Name;
            public string PersianName;
            public int dataBaseNo;
            public bool IsVirtual;
            public int NoOprationEnvironment;
            public bool IsGeneral;
            public bool HasTriggerLog;
            public bool EnableCache;
            public string CacheQuery;

        } //End Struct

        public clsTables()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
            string[] arrypath = StrCon1.Split(';');
            foreach (string section in arrypath)
            {
                string[] arrypathsec = section.Split('=');
                if (arrypathsec[0].ToUpper().Trim() == "DATABASE")
                    StrDb = arrypathsec[1];
                else if (arrypathsec[0].ToUpper().Trim() == "INITIAL CATALOG")
                    StrDb = arrypathsec[1];
            }
        }  //End Behin Function

        public strcTables GetNewRow()
        {
            strcTables objNewStruct =  new strcTables() ;
            objNewStruct.Name = ""; 
            objNewStruct.PersianName = ""; 
            objNewStruct.CacheQuery = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcTables myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_TablesRow dtRow = new Entity.Entity_DS.DS_TablesDataTable().NewDS_TablesRow();

                dtRow.NO = myStruct.NO;
                dtRow.Name = myStruct.Name;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.dataBaseNo = myStruct.dataBaseNo;
                dtRow.IsVirtual = myStruct.IsVirtual;
            if (myStruct.NoOprationEnvironment >= 1)
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
            else
            {
                SqlCommand cmdop = new SqlCommand();
                SqlConnection sqlconnop = new SqlConnection();
                SqlDataAdapter sqldaop = new SqlDataAdapter();
                DataTable dtop = new DataTable();
                string qryop = "select op.no from [database] as db,OprationEnvironment as op where op.name = db.persianname and db.name = '" + StrDb + "'";
                cmdop.CommandText = qryop;
                sqlconnop.ConnectionString = StrCon1;
                cmdop.Connection = sqlconnop;
                sqldaop.SelectCommand = cmdop;
                sqldaop.Fill(dtop);
                if (dtop.Rows.Count > 0)
                    dtRow.NoOprationEnvironment = int.Parse(dtop.Rows[0]["no"].ToString());
            }
                dtRow.IsGeneral = myStruct.IsGeneral;
                dtRow.HasTriggerLog = myStruct.HasTriggerLog;
                dtRow.EnableCache = myStruct.EnableCache;
                dtRow.CacheQuery = myStruct.CacheQuery;

                DAdlt.DS_TablesDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [Tables] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcTables myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_TablesRow dtRow = new Entity.Entity_DS.DS_TablesDataTable().NewDS_TablesRow();

            string Qry ="SELECT MAX(No) FROM [Tables]";
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

            dtRow.NO = MaxNO ;
            dtRow.Name = myStruct.Name;
            dtRow.PersianName = myStruct.PersianName;
            dtRow.dataBaseNo = myStruct.dataBaseNo;
            dtRow.IsVirtual = myStruct.IsVirtual;
            if (myStruct.NoOprationEnvironment >= 1)
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
            else
            {
                SqlCommand cmdop = new SqlCommand();
                SqlConnection sqlconnop = new SqlConnection();
                SqlDataAdapter sqldaop = new SqlDataAdapter();
                DataTable dtop = new DataTable();
                string qryop = "select op.no from [database] as db,OprationEnvironment as op where op.name = db.persianname and db.name = '" + StrDb + "'";
                cmdop.CommandText = qryop;
                sqlconnop.ConnectionString = StrCon1;
                cmdop.Connection = sqlconnop;
                sqldaop.SelectCommand = cmdop;
                sqldaop.Fill(dtop);
                if (dtop.Rows.Count > 0)
                    dtRow.NoOprationEnvironment = int.Parse(dtop.Rows[0]["no"].ToString());
            }
            dtRow.IsGeneral = myStruct.IsGeneral;
            dtRow.HasTriggerLog = myStruct.HasTriggerLog;
            dtRow.EnableCache = myStruct.EnableCache;
            dtRow.CacheQuery = myStruct.CacheQuery;

            return DAinsrt.DS_TablesDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcTables myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_TablesRow dtRow = new Entity.Entity_DS.DS_TablesDataTable().NewDS_TablesRow();

                dtRow.NO = myStruct.NO;
                dtRow.Name = myStruct.Name;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.dataBaseNo = myStruct.dataBaseNo;
                dtRow.IsVirtual = myStruct.IsVirtual;
            if (myStruct.NoOprationEnvironment >= 1)
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
            else
            {
                SqlCommand cmdop = new SqlCommand();
                SqlConnection sqlconnop = new SqlConnection();
                SqlDataAdapter sqldaop = new SqlDataAdapter();
                DataTable dtop = new DataTable();
                string qryop = "select op.no from [database] as db,OprationEnvironment as op where op.name = db.persianname and db.name = '" + StrDb + "'";
                cmdop.CommandText = qryop;
                sqlconnop.ConnectionString = StrCon1;
                cmdop.Connection = sqlconnop;
                sqldaop.SelectCommand = cmdop;
                sqldaop.Fill(dtop);
                if (dtop.Rows.Count > 0)
                    dtRow.NoOprationEnvironment = int.Parse(dtop.Rows[0]["no"].ToString());
            }
                dtRow.IsGeneral = myStruct.IsGeneral;
                dtRow.HasTriggerLog = myStruct.HasTriggerLog;
                dtRow.EnableCache = myStruct.EnableCache;
                dtRow.CacheQuery = myStruct.CacheQuery;

            DAupdt.DS_TablesDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Tables] SET " + FieldsSetString + " WHERE NO = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcTables> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcTables> myStrcList = new List<strcTables>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Tables] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcTables myStruct = new strcTables();
                                myStruct.NO = (int)dt.Rows[i]["NO"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.dataBaseNo = (int)dt.Rows[i]["dataBaseNo"];
                myStruct.IsVirtual = (bool)dt.Rows[i]["IsVirtual"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.IsGeneral = (bool)dt.Rows[i]["IsGeneral"];
                myStruct.HasTriggerLog = (bool)dt.Rows[i]["HasTriggerLog"];
                myStruct.EnableCache = (bool)dt.Rows[i]["EnableCache"];
                myStruct.CacheQuery = (string)dt.Rows[i]["CacheQuery"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcTables> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcTables> myStrcList = new List<strcTables>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Tables] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcTables myStruct = new strcTables();
                                myStruct.NO = (int)dt.Rows[i]["NO"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.dataBaseNo = (int)dt.Rows[i]["dataBaseNo"];
                myStruct.IsVirtual = (bool)dt.Rows[i]["IsVirtual"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.IsGeneral = (bool)dt.Rows[i]["IsGeneral"];
                myStruct.HasTriggerLog = (bool)dt.Rows[i]["HasTriggerLog"];
                myStruct.EnableCache = (bool)dt.Rows[i]["EnableCache"];
                myStruct.CacheQuery = (string)dt.Rows[i]["CacheQuery"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Tables] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نام جدول,نام فارسي,پايگاه داده,مجازي است,شناسه سامانه شرکت,اصلي است,داراي تريگر,فعال کردن کش,کوئري کش";
            String FiledEnglishName = "Tables.Name,Tables.PersianName,DataBase.Name,Tables.IsVirtual,Tables.NoOprationEnvironment,Tables.IsGeneral,Tables.HasTriggerLog,Tables.EnableCache,Tables.CacheQuery";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcTables> Select()
        {

            DataTable dt;
            List<strcTables> myStrcList = new List<strcTables>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Tables]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcTables myStruct = new strcTables();
                                myStruct.NO = (int)dt.Rows[i]["NO"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.dataBaseNo = (int)dt.Rows[i]["dataBaseNo"];
                myStruct.IsVirtual = (bool)dt.Rows[i]["IsVirtual"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.IsGeneral = (bool)dt.Rows[i]["IsGeneral"];
                myStruct.HasTriggerLog = (bool)dt.Rows[i]["HasTriggerLog"];
                myStruct.EnableCache = (bool)dt.Rows[i]["EnableCache"];
                myStruct.CacheQuery = (string)dt.Rows[i]["CacheQuery"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcTables SelectByNo(strcTables myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_TablesRow dtRow = new Entity.Entity_DS.DS_TablesDataTable().NewDS_TablesRow();

            Entity.Entity_DS.DS_TablesDataTable dt = new Entity.Entity_DS.DS_TablesDataTable();

            dt = DAslct.DS_TablesDataTable_SelectCommand(" WHERE NO = " + myStruct.NO);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NO = 0;
                myStruct.Name = "0";
                myStruct.PersianName = "0";
                myStruct.dataBaseNo = 0;
                myStruct.NoOprationEnvironment = 0;
                myStruct.CacheQuery = "0";

                return myStruct ;
            }
            myStruct.NO = (int)dt.Rows[0]["NO"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.dataBaseNo = (int)dt.Rows[0]["dataBaseNo"];
            myStruct.IsVirtual = (bool)dt.Rows[0]["IsVirtual"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.IsGeneral = (bool)dt.Rows[0]["IsGeneral"];
            myStruct.HasTriggerLog = (bool)dt.Rows[0]["HasTriggerLog"];
            myStruct.EnableCache = (bool)dt.Rows[0]["EnableCache"];
            myStruct.CacheQuery = (string)dt.Rows[0]["CacheQuery"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NO] as 'PKNO', [Name] as 'نام جدول' , [PersianName] as 'نام فارسي' ,[dataBaseNo] as 'FKdataBaseNo', 'پايگاه داده' = (SELECT [Name] FROM [DataBase] WHERE TTables.[dataBaseNo] = [NO]), [IsVirtual] as 'مجازي است' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [IsGeneral] as 'اصلي است' , [HasTriggerLog] as 'داراي تريگر' , [EnableCache] as 'فعال کردن کش' , [CacheQuery] as 'کوئري کش'  FROM [Tables] AS TTables";
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
                string Qry1 = "SELECT  [NO] as 'PKNO', [Name] as 'نام جدول' , [PersianName] as 'نام فارسي' ,[dataBaseNo] as 'FKdataBaseNo', 'پايگاه داده' = (SELECT [Name] FROM [DataBase] WHERE TTables.[dataBaseNo] = [NO]), [IsVirtual] as 'مجازي است' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [IsGeneral] as 'اصلي است' , [HasTriggerLog] as 'داراي تريگر' , [EnableCache] as 'فعال کردن کش' , [CacheQuery] as 'کوئري کش'  FROM [Tables] AS TTables";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill Tables Table Into DataGrid By dataBaseNo Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsDataBase.strcDataBase myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_TablesDataTable_SelectCommand(" WHERE dataBaseNo = " + myStruct.NO);

        }

  //End FillTable Function


        public DataTable GetDataTableDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTabledataBaseNoDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [DataBase]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTabledataBaseNoDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [DataBase] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTabledataBaseNoDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [DataBase] where Name like '" + strName + "%' and " + strwhere;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001115 + ")";

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



        public string GetAtt( int NO , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [Tables]." + FieldName[0] + " From Tables Where [NO] = " + NO;
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

        public void SetAtt( int NO , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [Tables] SET [" + FieldName[0] + "] = '" + value + "' where [NO] = " + NO;
            else if (FieldName[1] == "6")
                Str = "Update [Tables] SET [" + FieldName[0] + "] = " + value + " where [NO] = " + NO;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NO] as 'PKNO', [Name] as 'نام جدول' , [PersianName] as 'نام فارسي' ,[dataBaseNo] as 'FKdataBaseNo', 'پايگاه داده' = (SELECT [Name] FROM [DataBase] WHERE TTables.[dataBaseNo] = [NO]), [IsVirtual] as 'مجازي است' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [IsGeneral] as 'اصلي است' , [HasTriggerLog] as 'داراي تريگر' , [EnableCache] as 'فعال کردن کش' , [CacheQuery] as 'کوئري کش'  From [Tables] AS TTables Where TTables." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NO] as 'PKNO', [Name] as 'نام جدول' , [PersianName] as 'نام فارسي' ,[dataBaseNo] as 'FKdataBaseNo', 'پايگاه داده' = (SELECT [Name] FROM [DataBase] WHERE TTables.[dataBaseNo] = [NO]), [IsVirtual] as 'مجازي است' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [IsGeneral] as 'اصلي است' , [HasTriggerLog] as 'داراي تريگر' , [EnableCache] as 'فعال کردن کش' , [CacheQuery] as 'کوئري کش'  From [Tables] AS TTables Where TTables." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001115) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001115) " + 
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


        public strcTables GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsTables.strcTables myStruct = new clsTables.strcTables();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Tables] Where [Tables]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NO = (int)dt.Rows[0]["NO"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.dataBaseNo = (int)dt.Rows[0]["dataBaseNo"];
            myStruct.IsVirtual = (bool)dt.Rows[0]["IsVirtual"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.IsGeneral = (bool)dt.Rows[0]["IsGeneral"];
            myStruct.HasTriggerLog = (bool)dt.Rows[0]["HasTriggerLog"];
            myStruct.EnableCache = (bool)dt.Rows[0]["EnableCache"];
            myStruct.CacheQuery = (string)dt.Rows[0]["CacheQuery"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [Tables] where " + FieldName + " = '" + FieldValue + "'";
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