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
    public class clsRule
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcRule
        {
            public int NoEntityType;
            public int NoEntityState;
            public string Name;
            public short Version;
            public bool IsActive;
            public string General;
            public int NoRuleNode;
            public int NoEntity;
            public int IDRuleMode;
            public int NoOprationEnvironment;

        } //End Struct

        public clsRule()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcRule GetNewRow()
        {
            strcRule objNewStruct =  new strcRule() ;
            objNewStruct.Name = ""; 
            objNewStruct.General = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcRule myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_RuleRow dtRow = new Rule.Rule_DS.DS_RuleDataTable().NewDS_RuleRow();

                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.NoEntityState = myStruct.NoEntityState;
                dtRow.Name = myStruct.Name;
                dtRow.Version = myStruct.Version;
                dtRow.IsActive = myStruct.IsActive;
                dtRow.General = myStruct.General;
                dtRow.NoRuleNode = myStruct.NoRuleNode;
                dtRow.NoEntity = myStruct.NoEntity;
                dtRow.IDRuleMode = myStruct.IDRuleMode;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

                DAdlt.DS_RuleDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [Rule] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcRule myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleRow dtRow = new Rule.Rule_DS.DS_RuleDataTable().NewDS_RuleRow();

            dtRow.NoEntityType = myStruct.NoEntityType;
            dtRow.NoEntityState = myStruct.NoEntityState;
            dtRow.Name = myStruct.Name;
            dtRow.Version = myStruct.Version;
            dtRow.IsActive = myStruct.IsActive;
            dtRow.General = myStruct.General;
            dtRow.NoRuleNode = myStruct.NoRuleNode;
            dtRow.NoEntity = myStruct.NoEntity;
            dtRow.IDRuleMode = myStruct.IDRuleMode;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_RuleDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcRule myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleRow dtRow = new Rule.Rule_DS.DS_RuleDataTable().NewDS_RuleRow();

                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.NoEntityState = myStruct.NoEntityState;
                dtRow.Name = myStruct.Name;
                dtRow.Version = myStruct.Version;
                dtRow.IsActive = myStruct.IsActive;
                dtRow.General = myStruct.General;
                dtRow.NoRuleNode = myStruct.NoRuleNode;
                dtRow.NoEntity = myStruct.NoEntity;
                dtRow.IDRuleMode = myStruct.IDRuleMode;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;
            DAupdt.DS_RuleDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Rule] SET " + FieldsSetString + " WHERE NoEntity = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcRule> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcRule> myStrcList = new List<strcRule>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Rule] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRule myStruct = new strcRule();
                                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.NoEntityState = (int)dt.Rows[i]["NoEntityState"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.Version = (short)dt.Rows[i]["Version"];
                myStruct.IsActive = (bool)dt.Rows[i]["IsActive"];
                myStruct.General = (string)dt.Rows[i]["General"];
                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
                myStruct.IDRuleMode = (int)dt.Rows[i]["IDRuleMode"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcRule> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcRule> myStrcList = new List<strcRule>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Rule] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRule myStruct = new strcRule();
                                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.NoEntityState = (int)dt.Rows[i]["NoEntityState"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.Version = (short)dt.Rows[i]["Version"];
                myStruct.IsActive = (bool)dt.Rows[i]["IsActive"];
                myStruct.General = (string)dt.Rows[i]["General"];
                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
                myStruct.IDRuleMode = (int)dt.Rows[i]["IDRuleMode"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Rule] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نوع موجوديت,وضعيت موجوديت,عنوان,نسخه,فعال است,عمومي,نودهاي قاعده,شناسه نوع قانون,شناسه سامانه شرکت";
            String FiledEnglishName = "EntityType.Name,EntityState.Name,Rule.Name,Rule.Version,Rule.IsActive,Rule.General,RuleNode.AbstractCode,Rule.IDRuleMode,Rule.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcRule> Select()
        {

            DataTable dt;
            List<strcRule> myStrcList = new List<strcRule>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Rule]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRule myStruct = new strcRule();
                                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.NoEntityState = (int)dt.Rows[i]["NoEntityState"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.Version = (short)dt.Rows[i]["Version"];
                myStruct.IsActive = (bool)dt.Rows[i]["IsActive"];
                myStruct.General = (string)dt.Rows[i]["General"];
                myStruct.NoRuleNode = (int)dt.Rows[i]["NoRuleNode"];
                myStruct.NoEntity = (int)dt.Rows[i]["NoEntity"];
                myStruct.IDRuleMode = (int)dt.Rows[i]["IDRuleMode"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcRule SelectByNo(strcRule myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_RuleRow dtRow = new Rule.Rule_DS.DS_RuleDataTable().NewDS_RuleRow();

            Rule.Rule_DS.DS_RuleDataTable dt = new Rule.Rule_DS.DS_RuleDataTable();

            dt = DAslct.DS_RuleDataTable_SelectCommand(" WHERE NoEntity = " + myStruct.NoEntity);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoEntityType = 0;
                myStruct.NoEntityState = 0;
                myStruct.Name = "0";
                myStruct.General = "0";
                myStruct.NoRuleNode = 0;
                myStruct.NoEntity = 0;
                myStruct.IDRuleMode = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.NoEntityState = (int)dt.Rows[0]["NoEntityState"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.Version = (short)dt.Rows[0]["Version"];
            myStruct.IsActive = (bool)dt.Rows[0]["IsActive"];
            myStruct.General = (string)dt.Rows[0]["General"];
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.IDRuleMode = (int)dt.Rows[0]["IDRuleMode"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  'نوع موجوديت' = (SELECT Name FROM EntityType"+
                " WHERE TRule.[NoEntityType] = No),[NoEntityState] as 'FKNoEntityState', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TRule.[NoEntityState] = [No]), [Name] as 'عنوان' , [Version] as 'نسخه' , [IsActive] as 'فعال است' , [General] as 'عمومي' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRule.[NoRuleNode] = [No]), [NoEntity] as 'PKNoEntity' , (Select Name from [LookUp] Where [LookUp].No = [IDRuleMode]) as 'شناسه نوع قانون' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [Rule] AS TRule";
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
                string Qry1 = "SELECT  'نوع موجوديت' = (SELECT Name FROM EntityType"+
                " WHERE TRule.[NoEntityType] = No),[NoEntityState] as 'FKNoEntityState', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TRule.[NoEntityState] = [No]), [Name] as 'عنوان' , [Version] as 'نسخه' , [IsActive] as 'فعال است' , [General] as 'عمومي' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRule.[NoRuleNode] = [No]), [NoEntity] as 'PKNoEntity' , (Select Name from [LookUp] Where [LookUp].No = [IDRuleMode]) as 'شناسه نوع قانون' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [Rule] AS TRule";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill Rule Table Into DataGrid By NoEntityType Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityType.strcEntityType myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_RuleDataTable_SelectCommand(" WHERE NoEntityType = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill Rule Table Into DataGrid By NoEntityState Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityState.strcEntityState myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_RuleDataTable_SelectCommand(" WHERE NoEntityState = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill Rule Table Into DataGrid By NoRuleNode Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRuleNode.strcRuleNode myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_RuleDataTable_SelectCommand(" WHERE NoRuleNode = " + myStruct.No);

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
        public DataTable GetDataTableNoEntityStateDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityState]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityStateDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityState] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityStateDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityState] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT NoEntity as 'No',Name as 'Name' FROM [Rule]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive()
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive(string strName)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoRuleNodeDescriptive(string strName,string strwhere)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


        public DataTable GetDataTableIDRuleModeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 358 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIDRuleModeDescriptive(string OrderName,string OrderSort)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 358 ORDER BY " + OrderName + " " + OrderSort;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002128 + ")";

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

            string Str = "Select [Rule]." + FieldName[0] + " From Rule Where [NoEntity] = " + NoEntity;
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
                Str = "Update [Rule] SET [" + FieldName[0] + "] = '" + value + "' where [NoEntity] = " + NoEntity;
            else if (FieldName[1] == "6")
                Str = "Update [Rule] SET [" + FieldName[0] + "] = " + value + " where [NoEntity] = " + NoEntity;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  'نوع موجوديت' = (SELECT Name FROM EntityType"+
                " WHERE TRule.[NoEntityType] = No),[NoEntityState] as 'FKNoEntityState', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TRule.[NoEntityState] = [No]), [Name] as 'عنوان' , [Version] as 'نسخه' , [IsActive] as 'فعال است' , [General] as 'عمومي' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRule.[NoRuleNode] = [No]), [NoEntity] as 'PKNoEntity' , (Select Name from [LookUp] Where [LookUp].No = [IDRuleMode]) as 'شناسه نوع قانون' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [Rule] AS TRule Where TRule." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  'نوع موجوديت' = (SELECT Name FROM EntityType"+
                " WHERE TRule.[NoEntityType] = No),[NoEntityState] as 'FKNoEntityState', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TRule.[NoEntityState] = [No]), [Name] as 'عنوان' , [Version] as 'نسخه' , [IsActive] as 'فعال است' , [General] as 'عمومي' ,[NoRuleNode] as 'FKNoRuleNode', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRule.[NoRuleNode] = [No]), [NoEntity] as 'PKNoEntity' , (Select Name from [LookUp] Where [LookUp].No = [IDRuleMode]) as 'شناسه نوع قانون' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [Rule] AS TRule Where TRule." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002128) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002128) " + 
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


        public strcRule GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsRule.strcRule myStruct = new clsRule.strcRule();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Rule] Where [Rule]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.NoEntityState = (int)dt.Rows[0]["NoEntityState"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.Version = (short)dt.Rows[0]["Version"];
            myStruct.IsActive = (bool)dt.Rows[0]["IsActive"];
            myStruct.General = (string)dt.Rows[0]["General"];
            myStruct.NoRuleNode = (int)dt.Rows[0]["NoRuleNode"];
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.IDRuleMode = (int)dt.Rows[0]["IDRuleMode"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [Rule] where " + FieldName + " = '" + FieldValue + "'";
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