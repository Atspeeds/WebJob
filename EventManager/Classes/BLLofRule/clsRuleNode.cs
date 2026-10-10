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
    public class clsRuleNode
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcRuleNode
        {
            public int No;
            public string Name;
            public bool IsCondition;
            public bool IsYes;
            public int NoParent;
            public string AbstractCode;
            public double TopNode;
            public double LeftNode;
            public double HeightNode;
            public double WidthNode;
            public int NoOprationEnvironment;

        } //End Struct

        public clsRuleNode()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcRuleNode GetNewRow()
        {
            strcRuleNode objNewStruct =  new strcRuleNode() ;
            objNewStruct.Name = ""; 
            objNewStruct.AbstractCode = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcRuleNode myStruct)
        {
                Rule.Rule DAdlt = new Rule.Rule(StrCon1);
                Rule.Rule_DS.DS_RuleNodeRow dtRow = new Rule.Rule_DS.DS_RuleNodeDataTable().NewDS_RuleNodeRow();

                dtRow.No = myStruct.No;
                dtRow.Name = myStruct.Name;
                dtRow.IsCondition = myStruct.IsCondition;
                dtRow.IsYes = myStruct.IsYes;
                dtRow.NoParent = myStruct.NoParent;
                dtRow.AbstractCode = myStruct.AbstractCode;
                dtRow.TopNode = myStruct.TopNode;
                dtRow.LeftNode = myStruct.LeftNode;
                dtRow.HeightNode = myStruct.HeightNode;
                dtRow.WidthNode = myStruct.WidthNode;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

                DAdlt.DS_RuleNodeDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [RuleNode] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcRuleNode myStruct)
        {
            Rule.Rule DAinsrt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleNodeRow dtRow = new Rule.Rule_DS.DS_RuleNodeDataTable().NewDS_RuleNodeRow();

         //   dtRow.No = myStruct.No;
            dtRow.Name = myStruct.Name;
            dtRow.IsCondition = myStruct.IsCondition;
            dtRow.IsYes = myStruct.IsYes;
            dtRow.NoParent = myStruct.NoParent;
            dtRow.AbstractCode = myStruct.AbstractCode;
            dtRow.TopNode = myStruct.TopNode;
            dtRow.LeftNode = myStruct.LeftNode;
            dtRow.HeightNode = myStruct.HeightNode;
            dtRow.WidthNode = myStruct.WidthNode;
                dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            return DAinsrt.DS_RuleNodeDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcRuleNode myStruct)
        {
            Rule.Rule DAupdt = new Rule.Rule(StrCon1);
            Rule.Rule_DS.DS_RuleNodeRow dtRow = new Rule.Rule_DS.DS_RuleNodeDataTable().NewDS_RuleNodeRow();

                dtRow.No = myStruct.No;
                dtRow.Name = myStruct.Name;
                dtRow.IsCondition = myStruct.IsCondition;
                dtRow.IsYes = myStruct.IsYes;
                dtRow.NoParent = myStruct.NoParent;
                dtRow.AbstractCode = myStruct.AbstractCode;
                dtRow.TopNode = myStruct.TopNode;
                dtRow.LeftNode = myStruct.LeftNode;
                dtRow.HeightNode = myStruct.HeightNode;
                dtRow.WidthNode = myStruct.WidthNode;
                    dtRow.NoOprationEnvironment = myStruct.NoOprationEnvironment;

            DAupdt.DS_RuleNodeDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [RuleNode] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcRuleNode> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcRuleNode> myStrcList = new List<strcRuleNode>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleNode] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleNode myStruct = new strcRuleNode();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsCondition = (bool)dt.Rows[i]["IsCondition"];
                myStruct.IsYes = (bool)dt.Rows[i]["IsYes"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.AbstractCode = (string)dt.Rows[i]["AbstractCode"];
                myStruct.TopNode = (double)dt.Rows[i]["TopNode"];
                myStruct.LeftNode = (double)dt.Rows[i]["LeftNode"];
                myStruct.HeightNode = (double)dt.Rows[i]["HeightNode"];
                myStruct.WidthNode = (double)dt.Rows[i]["WidthNode"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcRuleNode> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcRuleNode> myStrcList = new List<strcRuleNode>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleNode] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleNode myStruct = new strcRuleNode();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsCondition = (bool)dt.Rows[i]["IsCondition"];
                myStruct.IsYes = (bool)dt.Rows[i]["IsYes"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.AbstractCode = (string)dt.Rows[i]["AbstractCode"];
                myStruct.TopNode = (double)dt.Rows[i]["TopNode"];
                myStruct.LeftNode = (double)dt.Rows[i]["LeftNode"];
                myStruct.HeightNode = (double)dt.Rows[i]["HeightNode"];
                myStruct.WidthNode = (double)dt.Rows[i]["WidthNode"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleNode] Where " + FieldName + " = " + Value;
            String FiledPersianName = "عنوان,شرط است,صحيح است,نودهاي قاعده,کد انتزاعي,فاصله از بالاي صفحه,فاصله از چپ صفحه,عرض,طول,شناسه سامانه شرکت";
            String FiledEnglishName = "RuleNode.Name,RuleNode.IsCondition,RuleNode.IsYes,RuleNode.AbstractCode,RuleNode.AbstractCode,RuleNode.TopNode,RuleNode.LeftNode,RuleNode.HeightNode,RuleNode.WidthNode,RuleNode.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcRuleNode> Select()
        {

            DataTable dt;
            List<strcRuleNode> myStrcList = new List<strcRuleNode>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [RuleNode]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcRuleNode myStruct = new strcRuleNode();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.IsCondition = (bool)dt.Rows[i]["IsCondition"];
                myStruct.IsYes = (bool)dt.Rows[i]["IsYes"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.AbstractCode = (string)dt.Rows[i]["AbstractCode"];
                myStruct.TopNode = (double)dt.Rows[i]["TopNode"];
                myStruct.LeftNode = (double)dt.Rows[i]["LeftNode"];
                myStruct.HeightNode = (double)dt.Rows[i]["HeightNode"];
                myStruct.WidthNode = (double)dt.Rows[i]["WidthNode"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcRuleNode SelectByNo(strcRuleNode myStruct)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            //Rule.Rule_DS.DS_RuleNodeRow dtRow = new Rule.Rule_DS.DS_RuleNodeDataTable().NewDS_RuleNodeRow();

            Rule.Rule_DS.DS_RuleNodeDataTable dt = new Rule.Rule_DS.DS_RuleNodeDataTable();

            dt = DAslct.DS_RuleNodeDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.Name = "0";
                myStruct.NoParent = 0;
                myStruct.AbstractCode = "0";
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.IsCondition = (bool)dt.Rows[0]["IsCondition"];
            myStruct.IsYes = (bool)dt.Rows[0]["IsYes"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.AbstractCode = (string)dt.Rows[0]["AbstractCode"];
            myStruct.TopNode = (double)dt.Rows[0]["TopNode"];
            myStruct.LeftNode = (double)dt.Rows[0]["LeftNode"];
            myStruct.HeightNode = (double)dt.Rows[0]["HeightNode"];
            myStruct.WidthNode = (double)dt.Rows[0]["WidthNode"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [Name] as 'عنوان' , [IsCondition] as 'شرط است' , [IsYes] as 'صحيح است' ,[NoParent] as 'FKNoParent', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleNode.[NoParent] = [No]), [AbstractCode] as 'کد انتزاعي' , [TopNode] as 'فاصله از بالاي صفحه' , [LeftNode] as 'فاصله از چپ صفحه' , [HeightNode] as 'عرض' , [WidthNode] as 'طول' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [RuleNode] AS TRuleNode";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [Name] as 'عنوان' , [IsCondition] as 'شرط است' , [IsYes] as 'صحيح است' ,[NoParent] as 'FKNoParent', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleNode.[NoParent] = [No]), [AbstractCode] as 'کد انتزاعي' , [TopNode] as 'فاصله از بالاي صفحه' , [LeftNode] as 'فاصله از چپ صفحه' , [HeightNode] as 'عرض' , [WidthNode] as 'طول' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [RuleNode] AS TRuleNode";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill RuleNode Table Into DataGrid By NoParent Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsRuleNode.strcRuleNode myStruct )
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            return DAslct.DS_RuleNodeDataTable_SelectCommand(" WHERE NoParent = " + myStruct.No);

        }

  //End FillTable Function


        public DataTable GetDataTableNoParentDescriptive()
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive(string strName)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive(string strName,string strwhere)
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode] where Name like '" + strName + "%' and " + strwhere;
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Rule.Rule DAslct = new Rule.Rule(StrCon1);
            string Qry = "SELECT No as 'No',AbstractCode as 'Name' FROM [RuleNode]";
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
                    "as (SELECT [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  , '' as 'Icon' FROM RuleNode " + 
                    "where [RuleNode].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [RuleNode].No as No,[RuleNode].AbstractCode,[RuleNode].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [RuleNode] ON tree.No = [RuleNode].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [RuleNode].No as No,[RuleNode].AbstractCode,[RuleNode].NoParent  , '' as 'Icon' FROM [RuleNode] where [RuleNode].No = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable NoParent_SetTree(int NoParent,string FieldName)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = "WITH Tree " + 
                    "as (SELECT [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  , '' as 'Icon' FROM [RuleNode] " + 
                    "where [RuleNode].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [RuleNode].No as No,[RuleNode].AbstractCode,[RuleNode].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [RuleNode] ON tree.No = [RuleNode].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [RuleNode].No as No,[RuleNode].AbstractCode,[RuleNode].NoParent  , '' as 'Icon' FROM [RuleNode] where [RuleNode]." + FieldName +" = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable GetTreeOneLevel(int NoParent,bool blroot)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery ="";
            if (blroot == true)
                objQuery = string.Format(" Select * From (Select [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  FROM [RuleNode] where [RuleNode].NoParent =-1  union all " + 
                " Select [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  FROM [RuleNode] where [RuleNode].NoParent ={0} )as  t1 order by  No", NoParent);
            else
                objQuery = string.Format(" select -1 as no,'ریشه' as name,-1 as noparent union Select * From (Select [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  FROM [RuleNode] where [RuleNode].NoParent =-1  union all " + 
                " Select [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  FROM [RuleNode] where [RuleNode].NoParent ={0} )as  t1 order by  No", NoParent);
            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery);
            return DAC.GeneralSelectCommand(objcmd);
        }
        public DataTable GetChildLevel(int NoParent)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = string.Format(" Select [RuleNode].No as No,[RuleNode].AbstractCode as Name,[RuleNode].NoParent as NoParent  , ChildCount=( Select COUNT(*) from [RuleNode] OU1 Where OU1.NoParent = [RuleNode].No) FROM [RuleNode] where [RuleNode].NoParent ={0} order by  No", NoParent);
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1002129 + ")";

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

            string Str = "Select [RuleNode]." + FieldName[0] + " From RuleNode Where [No] = " + No;
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
                Str = "Update [RuleNode] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [RuleNode] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'عنوان' , [IsCondition] as 'شرط است' , [IsYes] as 'صحيح است' ,[NoParent] as 'FKNoParent', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleNode.[NoParent] = [No]), [AbstractCode] as 'کد انتزاعي' , [TopNode] as 'فاصله از بالاي صفحه' , [LeftNode] as 'فاصله از چپ صفحه' , [HeightNode] as 'عرض' , [WidthNode] as 'طول' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [RuleNode] AS TRuleNode Where TRuleNode." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as 'عنوان' , [IsCondition] as 'شرط است' , [IsYes] as 'صحيح است' ,[NoParent] as 'FKNoParent', 'نودهاي قاعده' = (SELECT [AbstractCode] FROM [RuleNode] WHERE TRuleNode.[NoParent] = [No]), [AbstractCode] as 'کد انتزاعي' , [TopNode] as 'فاصله از بالاي صفحه' , [LeftNode] as 'فاصله از چپ صفحه' , [HeightNode] as 'عرض' , [WidthNode] as 'طول' , [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [RuleNode] AS TRuleNode Where TRuleNode." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1002129) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1002129) " + 
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


        public strcRuleNode GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsRuleNode.strcRuleNode myStruct = new clsRuleNode.strcRuleNode();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [RuleNode] Where [RuleNode]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.IsCondition = (bool)dt.Rows[0]["IsCondition"];
            myStruct.IsYes = (bool)dt.Rows[0]["IsYes"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.AbstractCode = (string)dt.Rows[0]["AbstractCode"];
            myStruct.TopNode = (double)dt.Rows[0]["TopNode"];
            myStruct.LeftNode = (double)dt.Rows[0]["LeftNode"];
            myStruct.HeightNode = (double)dt.Rows[0]["HeightNode"];
            myStruct.WidthNode = (double)dt.Rows[0]["WidthNode"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [RuleNode] where " + FieldName + " = '" + FieldValue + "'";
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