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
    public class clsComponentType
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcComponentType
        {
            public int No;
            public string Name;
            public int NoComponentGroup;

        } //End Struct

        public clsComponentType()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcComponentType GetNewRow()
        {
            return new strcComponentType() ;

        }//End GetNewRow Function


        public int Delete(strcComponentType myStruct)
        {
                Component.Component DAdlt = new Component.Component(StrCon1);
                Component.Component_DS.DS_ComponentTypeRow dtRow = new Component.Component_DS.DS_ComponentTypeDataTable().NewDS_ComponentTypeRow();

                dtRow.No = myStruct.No;
                dtRow.Name = myStruct.Name;
                dtRow.NoComponentGroup = myStruct.NoComponentGroup;

                DAdlt.DS_ComponentTypeDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From ComponentType where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcComponentType myStruct)
        {
            Component.Component DAinsrt = new Component.Component(StrCon1);
            Component.Component_DS.DS_ComponentTypeRow dtRow = new Component.Component_DS.DS_ComponentTypeDataTable().NewDS_ComponentTypeRow();

            dtRow.No = myStruct.No;
            dtRow.Name = myStruct.Name;
            dtRow.NoComponentGroup = myStruct.NoComponentGroup;

            return DAinsrt.DS_ComponentTypeDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcComponentType myStruct)
        {
            Component.Component DAupdt = new Component.Component(StrCon1);
            Component.Component_DS.DS_ComponentTypeRow dtRow = new Component.Component_DS.DS_ComponentTypeDataTable().NewDS_ComponentTypeRow();

                dtRow.No = myStruct.No;
                dtRow.Name = myStruct.Name;
                dtRow.NoComponentGroup = myStruct.NoComponentGroup;

            DAupdt.DS_ComponentTypeDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [ComponentType] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcComponentType> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcComponentType> myStrcList = new List<strcComponentType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From ComponentType Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcComponentType myStruct = new strcComponentType();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoComponentGroup = (int)dt.Rows[i]["NoComponentGroup"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From ComponentType Where " + FieldName + " = " + Value;
            String FiledPersianName = ",نام";
            String FiledEnglishName = "ComponentType.Name,ComponentGroup.Name";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcComponentType> Select()
        {

            DataTable dt;
            List<strcComponentType> myStrcList = new List<strcComponentType>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From ComponentType";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcComponentType myStruct = new strcComponentType();
                                myStruct.No = (int)dt.Rows[i]["No"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.NoComponentGroup = (int)dt.Rows[i]["NoComponentGroup"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcComponentType SelectByNo(strcComponentType myStruct)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            //Component.Component_DS.DS_ComponentTypeRow dtRow = new Component.Component_DS.DS_ComponentTypeDataTable().NewDS_ComponentTypeRow();

            Component.Component_DS.DS_ComponentTypeDataTable dt = new Component.Component_DS.DS_ComponentTypeDataTable();

            dt = DAslct.DS_ComponentTypeDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;
                myStruct.Name = "0";
                myStruct.NoComponentGroup = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoComponentGroup = (int)dt.Rows[0]["NoComponentGroup"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [No] as 'PKNo', [Name] as '' ,[NoComponentGroup] as 'FKNoComponentGroup', 'نام' = (SELECT [Name] FROM [ComponentGroup] WHERE TComponentType.[NoComponentGroup] = [No]) FROM ComponentType AS TComponentType";
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
                string Qry1 = "SELECT  [No] as 'PKNo', [Name] as '' ,[NoComponentGroup] as 'FKNoComponentGroup', 'نام' = (SELECT [Name] FROM [ComponentGroup] WHERE TComponentType.[NoComponentGroup] = [No]) FROM ComponentType AS TComponentType";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill ComponentType Table Into DataGrid By NoComponentGroup Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsComponentGroup.strcComponentGroup myStruct )
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            return DAslct.DS_ComponentTypeDataTable_SelectCommand(" WHERE NoComponentGroup = " + myStruct.No);

        }

  //End FillTable Function


        public DataTable GetDataTableNoComponentGroupDescriptive()
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            string Qry = "SELECT No,Name FROM Component";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001159 + ")";

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

            string Str = "Select [ComponentType]." + FieldName[0] + " From ComponentType Where [No] = " + No;
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
                Str = "Update [ComponentType] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [ComponentType] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] as 'PKNo', [Name] as '' ,[NoComponentGroup] as 'FKNoComponentGroup', 'نام' = (SELECT [Name] FROM [ComponentGroup] WHERE TComponentType.[NoComponentGroup] = [No]) From ComponentType Where [ComponentType]." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001159) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001159) " + 
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


        public strcComponentType GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsComponentType.strcComponentType myStruct = new clsComponentType.strcComponentType();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From ComponentType Where [ComponentType]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.NoComponentGroup = (int)dt.Rows[0]["NoComponentGroup"];


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