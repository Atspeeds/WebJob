using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace Behin
{
    public class clsQuery
    {
        string StrCon1;

        public struct strcQuery
        {
            public int No;
            public short IdType;

        } //End Struct

        public clsQuery()
        {

            Configuration config = new Configuration("Web.config");
            StrCon1 = config.GetConnectionString("SiteSqlServer");

        }  //End Behin Function

        public strcQuery GetNewRow()
        {
            return new strcQuery() ;

        }//End GetNewRow Function


        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance DELETE : Query --> Report --> 
        /// </summary>
        public int Delete(strcQuery myStruct)
        {
            if(CheckDelete(myStruct.No))
            {

                DEQ.DEQ DAdlt = new DEQ.DEQ(StrCon1);
                DEQ.DEQ_DS.DS_QueryRow dtRow = new DEQ.DEQ_DS.DS_QueryDataTable().NewDS_QueryRow();

                if(myStruct.No != null)
                    dtRow.NoEntity = myStruct.No;
                if(myStruct.IdType != null)
                    dtRow.IdType = myStruct.IdType;

                DAdlt.DS_QueryDataTable_DeleteCommand_SP(dtRow);

            }

            return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From Query where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance INSERT: Query --> 
        /// </summary>
        public int Insert( clsQuery.strcQuery myStructQuery )
        {
            Dictionary<string, object> Dic = new Dictionary<string, object>();

            DEQ.DEQ DAinsrt = new DEQ.DEQ(StrCon1);

            string Qry ="SELECT MAX(No) FROM Query";
            int MaxNO = 0;
            object value = null;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DataTable dt = GClass.GeneralSelectCommand(objCommand);

            if(dt.Rows.Count > 0 && dt.Rows[0][0].ToString().Trim().Length > 0)
            {
                MaxNO  = int.Parse(dt.Rows[0][0].ToString());
            }

            MaxNO++;

            Dic.Add( "Query_No" , MaxNO );
            Dic.Add( "Query_IdType" , myStructQuery.IdType);


            return DAinsrt.DS_QueryDataTable_InsertCommand_SP(Dic);



        }  //End Insert Function


        public int Update(strcQuery myStruct)
        {
            DEQ.DEQ DAupdt = new DEQ.DEQ(StrCon1);
            DEQ.DEQ_DS.DS_QueryRow dtRow = new DEQ.DEQ_DS.DS_QueryDataTable().NewDS_QueryRow();

                if(myStruct.No != null)
                    dtRow.NoEntity = myStruct.No;
                if(myStruct.IdType != null)
                    dtRow.IdType = myStruct.IdType;

            DAupdt.DS_QueryDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Query] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            GClass.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public DataTable Select(string FieldName, string Value)
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From Query Where " + FieldName + " = " + Value;
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public DataTable Select()
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From Query";
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public strcQuery SelectByNo(strcQuery myStruct)
        {
            DEQ.DEQ DAslct = new DEQ.DEQ(StrCon1);
            //DEQ.DEQ_DS.DS_QueryRow dtRow = new DEQ.DEQ_DS.DS_QueryDataTable().NewDS_QueryRow();

            DEQ.DEQ_DS.DS_QueryDataTable dt = new DEQ.DEQ_DS.DS_QueryDataTable();

            dt = DAslct.DS_QueryDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.No = 0;

                return myStruct ;
            }
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.IdType = (short)dt.Rows[0]["IdType"];

            return myStruct ;

        } // End SelectByNo


        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid)
        {
            string Qry1 = "SELECT  [No] , [IdType] as 'Query.IdType'  FROM Query AS TQuery";

            String FiledPersianName = "نوع کوئري";
            String FiledEnglishName = "Query.IdType";

            String[] PFieldsName = FiledPersianName.Split(',');
            String[] EFieldsName = FiledEnglishName.Split(',');

            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            SqlCommand  scmd_Class = new SqlCommand(Qry1);

            myGrid.DataSource = GClass.GeneralSelectCommand(scmd_Class);
            

            for(int i = 0 ; i < myGrid.Columns.Count-1 ; i++)
            {
                myGrid.Columns[EFieldsName[i]].HeaderText = PFieldsName[i];
            }

            return 0 ;

        }  //End FillTable Function


        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid , int NoQuery , int NoGrid , int NoAgent)
        {
            DataTable dt = CheckHasDivision(NoGrid ,NoAgent);
            if (dt.Rows.Count != 0)
            {
                myGrid.DataSource = dt;
                
                return 0 ;

            }
            dt = CheckHasDisplay(NoQuery , NoGrid);
            if (dt.Rows.Count != 0)
            {
                myGrid.DataSource = dt;
                
                return 0;

            }
            else
            {
                string Qry1 = "SELECT  [No] , [IdType] as 'Query.IdType'  FROM Query AS TQuery";

                String FiledPersianName = "نوع کوئري";
                String FiledEnglishName = "Query.IdType";

                String[] PFieldsName = FiledPersianName.Split(',');
                String[] EFieldsName = FiledEnglishName.Split(',');

                GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

                SqlCommand  scmd_Class = new SqlCommand(Qry1);

                myGrid.DataSource = GClass.GeneralSelectCommand(scmd_Class);
                

                for(int i = 0 ; i < myGrid.Columns.Count ; i++)
                {
                    myGrid.Columns[EFieldsName[i]].HeaderText = PFieldsName[i];
                }
            }

            return 0 ;

        }


  //End FillTable Function


  //End FillComboBYTable


        public int FillIdTypeComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 124 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            myComboBox.DataSource = GClass.GeneralSelectCommand(scmd_Class);
            myComboBox.DisplayMember = "Name";
            myComboBox.ValueMember = "No";
            

            return 0 ;

        }  //End FillComboBYLookUp






        public int GetNo()
        {
            string Query = "INSERT Entity(Name) VALUES ('') SELECT No from Entity where No = SCOPE_IDENTITY()";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Query);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = GClass.GeneralSelectCommand(scmd_Class);
            if(dt.Rows.Count != 0)
            {
                int No = int.Parse(dt.Rows[0]["No"].ToString());
                Query = "Delete From Entity Where No not in(" + No + ")";
                scmd_Class = new SqlCommand(Query);
                GClass.GeneralDeleteCommand(scmd_Class);
                return No;
            }
            return -1;
        }



        public Dictionary<int,bool> VerifyInsert()
        {

            Dictionary<int,bool> FieldsListFill =  new Dictionary<int,bool>();

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001058 + ")";

            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Query);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = GClass.GeneralSelectCommand(scmd_Class);

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

            string Str = "Select [Query]." + FieldName[0] + " From Query Where [No] = " + No;
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
                Str = "Update [Query] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [Query] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public int FillThisDataGridView(System.Windows.Forms.DataGridView myGrid ,string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] , [IdType] as 'Query.IdType'  From Query Where [Query]." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            myGrid.DataSource = DAC.GeneralSelectCommand(cmd);
            

            return 0 ;

        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001058) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001058) " + 
                "and RelTypeNo = 2 and Tables.IsVirtual = 'False' " ;
            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass GDA = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand cmd = new SqlCommand(str);
            dt = GDA.GeneralSelectCommand(cmd);

            DataTable dtCheck = new DataTable();
            foreach (DataRow dr in dt.Rows)
            {
                string StrSelect = "Select [" + dr["FieldName"].ToString() + "] From [" + 
                    dr["TableName"].ToString() + "] Where [" + dr["FieldName"].ToString() + "] = " + No ;
                dtCheck.Rows.Clear();
                GDA = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                cmd = new System.Data.SqlClient.SqlCommand(StrSelect);
                dtCheck = GDA.GeneralSelectCommand(cmd);
                if (dtCheck.Rows.Count != 0)
                {
                    return false;
                }
            }

            return true;

        }


        public strcQuery GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsQuery.strcQuery myStruct = new clsQuery.strcQuery();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From Query Where [Query]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (int)dt.Rows[0]["No"];
            myStruct.IdType = (short)dt.Rows[0]["IdType"];


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