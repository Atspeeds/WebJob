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
    public class clsEntityState
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcEntityState
        {
            public int No;
            public int NoEntityType;
            public string Name;
            public int NoParent;
            public bool IsFinal;

        } //End Struct

        public clsEntityState()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcEntityState GetNewRow()
        {
            strcEntityState objNewStruct =  new strcEntityState() ;
            objNewStruct.Name = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcEntityState myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_EntityStateRow dtRow = new Entity.Entity_DS.DS_EntityStateDataTable().NewDS_EntityStateRow();

                dtRow.No = myStruct.No;
                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.Name = myStruct.Name;
                dtRow.NoParent = myStruct.NoParent;
				//dtRow.IsFinal = myStruct.IsFinal;

                DAdlt.DS_EntityStateDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [EntityState] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcEntityState myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityStateRow dtRow = new Entity.Entity_DS.DS_EntityStateDataTable().NewDS_EntityStateRow();

            dtRow.No = myStruct.No;
            dtRow.NoEntityType = myStruct.NoEntityType;
            dtRow.Name = myStruct.Name;
            dtRow.NoParent = myStruct.NoParent;
			//dtRow.IsFinal = myStruct.IsFinal;

            return DAinsrt.DS_EntityStateDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcEntityState myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityStateRow dtRow = new Entity.Entity_DS.DS_EntityStateDataTable().NewDS_EntityStateRow();

                dtRow.No = myStruct.No;
                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.Name = myStruct.Name;
                dtRow.NoParent = myStruct.NoParent;
				//dtRow.IsFinal = myStruct.IsFinal;

            DAupdt.DS_EntityStateDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [EntityState] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEntityState> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEntityState> myStrcList = new List<strcEntityState>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityState] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityState myStruct = new strcEntityState();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.IsFinal = (bool)dt.Rows[i]["IsFinal"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityState] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نوع موجوديت,نام وضعيت موجوديت,وضعيت موجوديت,اتمام يافته";
            String FiledEnglishName = "EntityType.Name,EntityState.Name,EntityState.Name,EntityState.IsFinal";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEntityState> Select()
        {

            DataTable dt;
            List<strcEntityState> myStrcList = new List<strcEntityState>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityState]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityState myStruct = new strcEntityState();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoParent = (int)dt.Rows[i]["NoParent"];
                myStruct.IsFinal = (bool)dt.Rows[i]["IsFinal"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEntityState SelectByNo(strcEntityState myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_EntityStateRow dtRow = new Entity.Entity_DS.DS_EntityStateDataTable().NewDS_EntityStateRow();

            Entity.Entity_DS.DS_EntityStateDataTable dt = new Entity.Entity_DS.DS_EntityStateDataTable();

            dt = DAslct.DS_EntityStateDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.NoEntityType = 0;
                myStruct.Name = "0";
                myStruct.NoParent = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.IsFinal = (bool)dt.Rows[0]["IsFinal"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityState.[NoEntityType] = [No]), [Name] as 'نام وضعيت موجوديت' ,[NoParent] as 'FKNoParent', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TEntityState.[NoParent] = [No]), [IsFinal] as 'اتمام يافته'  FROM [EntityState] AS TEntityState";
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
                string Qry1 = "SELECT  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityState.[NoEntityType] = [No]), [Name] as 'نام وضعيت موجوديت' ,[NoParent] as 'FKNoParent', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TEntityState.[NoParent] = [No]), [IsFinal] as 'اتمام يافته'  FROM [EntityState] AS TEntityState";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill EntityState Table Into DataGrid By NoEntityType Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityType.strcEntityType myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityStateDataTable_SelectCommand(" WHERE NoEntityType = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill EntityState Table Into DataGrid By NoParent Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityState.strcEntityState myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityStateDataTable_SelectCommand(" WHERE NoParent = " + myStruct.No);

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
        public DataTable GetDataTableDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityState]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoParentDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityState]";
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
                    "as (SELECT [EntityState].No as No,[EntityState].Name as Name,[EntityState].NoParent as NoParent  , '' as 'Icon' FROM EntityState " + 
                    "where [EntityState].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [EntityState].No as No,[EntityState].Name,[EntityState].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [EntityState] ON tree.No = [EntityState].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [EntityState].No as No,[EntityState].Name,[EntityState].NoParent  , '' as 'Icon' FROM [EntityState] where [EntityState].No = " + NoParent + " order by No " ;

            System.Data.SqlClient.SqlCommand objcmd = new System.Data.SqlClient.SqlCommand(objQuery); 
            return DAC.GeneralSelectCommand(objcmd); 
        }
        public DataTable NoParent_SetTree(int NoParent,string FieldName)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string objQuery = "WITH Tree " + 
                    "as (SELECT [EntityState].No as No,[EntityState].Name as Name,[EntityState].NoParent as NoParent  , '' as 'Icon' FROM [EntityState] " + 
                    "where [EntityState].NoParent = " + NoParent + 
                    " UNION ALL " + 
                    "SELECT [EntityState].No as No,[EntityState].Name,[EntityState].NoParent  , '' as 'Icon' FROM Tree AS tree JOIN [EntityState] ON tree.No = [EntityState].NoParent ) " + 
                    "SELECT * FROM Tree UNION ALL SELECT [EntityState].No as No,[EntityState].Name,[EntityState].NoParent  , '' as 'Icon' FROM [EntityState] where [EntityState]." + FieldName +" = " + NoParent + " order by No " ;

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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001048 + ")";

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

            string Str = "Select [EntityState]." + FieldName[0] + " From EntityState Where [No] = " + No;
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
                Str = "Update [EntityState] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [EntityState] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo',[NoEntityType] as 'FKNoEntityType', 'نوع موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TEntityState.[NoEntityType] = [No]), [Name] as 'نام وضعيت موجوديت' ,[NoParent] as 'FKNoParent', 'وضعيت موجوديت' = (SELECT [Name] FROM [EntityState] WHERE TEntityState.[NoParent] = [No]), [IsFinal] as 'اتمام يافته'  From [EntityState] AS TEntityState Where TEntityState." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001048) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001048) " + 
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


        public strcEntityState GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEntityState.strcEntityState myStruct = new clsEntityState.strcEntityState();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [EntityState] Where [EntityState]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            myStruct.IsFinal = (bool)dt.Rows[0]["IsFinal"];


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