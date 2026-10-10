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
    public class clsEntityClassDiagram
    {
        string StrCon1;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcEntityClassDiagram
        {
            public int NoEntityClass;
            public int NoDiagram;
            public int NoOprationEnvironment;

        } //End Struct

        public clsEntityClassDiagram()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcEntityClassDiagram GetNewRow()
        {
            strcEntityClassDiagram objNewStruct =  new strcEntityClassDiagram() ;
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcEntityClassDiagram myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_EntityClassDiagramRow dtRow = new Entity.Entity_DS.DS_EntityClassDiagramDataTable().NewDS_EntityClassDiagramRow();

                dtRow.NoEntityClass = myStruct.NoEntityClass;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

                DAdlt.DS_EntityClassDiagramDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [EntityClassDiagram] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcEntityClassDiagram myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityClassDiagramRow dtRow = new Entity.Entity_DS.DS_EntityClassDiagramDataTable().NewDS_EntityClassDiagramRow();

            dtRow.NoEntityClass = myStruct.NoEntityClass;
            dtRow.NoDiagram = myStruct.NoDiagram;
            dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;

            return DAinsrt.DS_EntityClassDiagramDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcEntityClassDiagram myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_EntityClassDiagramRow dtRow = new Entity.Entity_DS.DS_EntityClassDiagramDataTable().NewDS_EntityClassDiagramRow();

                dtRow.NoEntityClass = myStruct.NoEntityClass;
                dtRow.NoDiagram = myStruct.NoDiagram;
                dtRow.NoOprationEnvironment= myStruct.NoOprationEnvironment;
            DAupdt.DS_EntityClassDiagramDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [EntityClassDiagram] SET " + FieldsSetString + " WHERE NoEntityClass = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcEntityClassDiagram> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcEntityClassDiagram> myStrcList = new List<strcEntityClassDiagram>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClassDiagram] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityClassDiagram myStruct = new strcEntityClassDiagram();
                                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcEntityClassDiagram> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcEntityClassDiagram> myStrcList = new List<strcEntityClassDiagram>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClassDiagram] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityClassDiagram myStruct = new strcEntityClassDiagram();
                                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClassDiagram] Where " + FieldName + " = " + Value;
            String FiledPersianName = "کلاس هاي جداول,نمودار وضعيت,شناسه سامانه شرکت";
            String FiledEnglishName = "EntityClass.Name,Diagram.PersianName,EntityClassDiagram.NoOprationEnvironment";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcEntityClassDiagram> Select()
        {

            DataTable dt;
            List<strcEntityClassDiagram> myStrcList = new List<strcEntityClassDiagram>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [EntityClassDiagram]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcEntityClassDiagram myStruct = new strcEntityClassDiagram();
                                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.NoDiagram = (int)dt.Rows[i]["NoDiagram"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcEntityClassDiagram SelectByNo(strcEntityClassDiagram myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_EntityClassDiagramRow dtRow = new Entity.Entity_DS.DS_EntityClassDiagramDataTable().NewDS_EntityClassDiagramRow();

            Entity.Entity_DS.DS_EntityClassDiagramDataTable dt = new Entity.Entity_DS.DS_EntityClassDiagramDataTable();

            dt = DAslct.DS_EntityClassDiagramDataTable_SelectCommand(" WHERE NoEntityClass = " + myStruct.NoEntityClass + " AND NoDiagram = " + myStruct.NoDiagram);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoEntityClass = 0;
                myStruct.NoDiagram = 0;
                myStruct.NoOprationEnvironment = 0;

                return myStruct ;
            }
            myStruct.NoEntityClass = (int)dt.Rows[0]["NoEntityClass"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NoEntityClass] as 'PKNoEntityClass' , 'کلاس هاي جداول' = (SELECT Name FROM EntityClass"+
                " WHERE TEntityClassDiagram.[NoEntityClass] = No), [NoDiagram] as 'PKNoDiagram',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TEntityClassDiagram.[NoDiagram] = [NoEntity]), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EntityClassDiagram] AS TEntityClassDiagram";
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
                string Qry1 = "SELECT  [NoEntityClass] as 'PKNoEntityClass' , 'کلاس هاي جداول' = (SELECT Name FROM EntityClass"+
                " WHERE TEntityClassDiagram.[NoEntityClass] = No), [NoDiagram] as 'PKNoDiagram',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TEntityClassDiagram.[NoDiagram] = [NoEntity]), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  FROM [EntityClassDiagram] AS TEntityClassDiagram";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill EntityClassDiagram Table Into DataGrid By NoEntityClass Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsEntityClass.strcEntityClass myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityClassDiagramDataTable_SelectCommand(" WHERE NoEntityClass = " + myStruct.No);

        }

        /// <summary>
        /// This Function Will Fill EntityClassDiagram Table Into DataGrid By NoDiagram Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsDiagram.strcDiagram myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_EntityClassDiagramDataTable_SelectCommand(" WHERE NoDiagram = " + myStruct.NoEntity);

        }

  //End FillTable Function


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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001248 + ")";

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



        public string GetAtt( int NoEntityClass , int NoDiagram , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [EntityClassDiagram]." + FieldName[0] + " From EntityClassDiagram Where [NoEntityClass] = " + NoEntityClass + " And [NoDiagram] = " + NoDiagram;
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

        public void SetAtt( int NoEntityClass , int NoDiagram , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [EntityClassDiagram] SET [" + FieldName[0] + "] = '" + value + "' where [NoEntityClass] = " + NoEntityClass + " And [NoDiagram] = " + NoDiagram;
            else if (FieldName[1] == "6")
                Str = "Update [EntityClassDiagram] SET [" + FieldName[0] + "] = " + value + " where [NoEntityClass] = " + NoEntityClass + " And [NoDiagram] = " + NoDiagram;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoEntityClass] as 'PKNoEntityClass' , 'کلاس هاي جداول' = (SELECT Name FROM EntityClass"+
                " WHERE TEntityClassDiagram.[NoEntityClass] = No), [NoDiagram] as 'PKNoDiagram',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TEntityClassDiagram.[NoDiagram] = [NoEntity]), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EntityClassDiagram] AS TEntityClassDiagram Where TEntityClassDiagram." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoEntityClass] as 'PKNoEntityClass' , 'کلاس هاي جداول' = (SELECT Name FROM EntityClass"+
                " WHERE TEntityClassDiagram.[NoEntityClass] = No), [NoDiagram] as 'PKNoDiagram',[NoDiagram] as 'FKNoDiagram', 'نمودار وضعيت' = (SELECT [PersianName] FROM [Diagram] WHERE TEntityClassDiagram.[NoDiagram] = [NoEntity]), [NoOprationEnvironment] as 'شناسه سامانه شرکت'  From [EntityClassDiagram] AS TEntityClassDiagram Where TEntityClassDiagram." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001248) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001248) " + 
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


        public strcEntityClassDiagram GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsEntityClassDiagram.strcEntityClassDiagram myStruct = new clsEntityClassDiagram.strcEntityClassDiagram();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [EntityClassDiagram] Where [EntityClassDiagram]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoEntityClass = (int)dt.Rows[0]["NoEntityClass"];
            myStruct.NoDiagram = (int)dt.Rows[0]["NoDiagram"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [EntityClassDiagram] where " + FieldName + " = '" + FieldValue + "'";
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