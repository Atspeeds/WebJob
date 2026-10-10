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
    public class clsClause
    {
        string StrCon1;

        public struct strcClause
        {
            public long No;
            public int NoQuery;
            public int NoClause;
            public string SelectCommand;
            public string FromCommand;
            public string WhereCommand;
            public short IdClauseType;
            public string GroupByCommand;
            public string OrderByCommand;
            public string HavingCommand;
            public string Bias;

        } //End Struct

        public clsClause()
        {

            Configuration config = new Configuration("Web.config");
            StrCon1 = config.GetConnectionString("SiteSqlServer");

        }  //End Behin Function

        public strcClause GetNewRow()
        {
            return new strcClause() ;

        }//End GetNewRow Function


        public int Delete(strcClause myStruct)
        {
                DEQ.DEQ DAdlt = new DEQ.DEQ(StrCon1);
                DEQ.DEQ_DS.DS_ClauseRow dtRow = new DEQ.DEQ_DS.DS_ClauseDataTable().NewDS_ClauseRow();

                if(myStruct.No != null)
                    dtRow.No = myStruct.No;
                if(myStruct.NoQuery != null)
                    dtRow.NoQuery = myStruct.NoQuery;
                if(myStruct.NoClause != null)
                    dtRow.NoClause = myStruct.NoClause;
                if(myStruct.SelectCommand != null)
                    dtRow.SelectCommand = myStruct.SelectCommand;
                if(myStruct.FromCommand != null)
                    dtRow.FromCommand = myStruct.FromCommand;
                if(myStruct.WhereCommand != null)
                    dtRow.WhereCommand = myStruct.WhereCommand;
                if(myStruct.IdClauseType != null)
                    dtRow.IdClauseType = myStruct.IdClauseType;
                if(myStruct.GroupByCommand != null)
                    dtRow.GroupByCommand = myStruct.GroupByCommand;
                if(myStruct.OrderByCommand != null)
                    dtRow.OrderByCommand = myStruct.OrderByCommand;
                if(myStruct.HavingCommand != null)
                    dtRow.HavingCommand = myStruct.HavingCommand;
                if(myStruct.Bias != null)
                    dtRow.Bias = myStruct.Bias;

                DAdlt.DS_ClauseDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From Clause where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcClause myStruct)
        {
            DEQ.DEQ DAinsrt = new DEQ.DEQ(StrCon1);
            DEQ.DEQ_DS.DS_ClauseRow dtRow = new DEQ.DEQ_DS.DS_ClauseDataTable().NewDS_ClauseRow();

            if(myStruct.No != null)
                dtRow.No = myStruct.No;
            if(myStruct.NoQuery != null)
                dtRow.NoQuery = myStruct.NoQuery;
            if(myStruct.NoClause != null)
                dtRow.NoClause = myStruct.NoClause;
            if(myStruct.SelectCommand != null)
                dtRow.SelectCommand = myStruct.SelectCommand;
            if(myStruct.FromCommand != null)
                dtRow.FromCommand = myStruct.FromCommand;
            if(myStruct.WhereCommand != null)
                dtRow.WhereCommand = myStruct.WhereCommand;
            if(myStruct.IdClauseType != null)
                dtRow.IdClauseType = myStruct.IdClauseType;
            if(myStruct.GroupByCommand != null)
                dtRow.GroupByCommand = myStruct.GroupByCommand;
            if(myStruct.OrderByCommand != null)
                dtRow.OrderByCommand = myStruct.OrderByCommand;
            if(myStruct.HavingCommand != null)
                dtRow.HavingCommand = myStruct.HavingCommand;
            if(myStruct.Bias != null)
                dtRow.Bias = myStruct.Bias;

            return DAinsrt.DS_ClauseDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcClause myStruct)
        {
            DEQ.DEQ DAupdt = new DEQ.DEQ(StrCon1);
            DEQ.DEQ_DS.DS_ClauseRow dtRow = new DEQ.DEQ_DS.DS_ClauseDataTable().NewDS_ClauseRow();

                if(myStruct.No != null)
                    dtRow.No = myStruct.No;
                if(myStruct.NoQuery != null)
                    dtRow.NoQuery = myStruct.NoQuery;
                if(myStruct.NoClause != null)
                    dtRow.NoClause = myStruct.NoClause;
                if(myStruct.SelectCommand != null)
                    dtRow.SelectCommand = myStruct.SelectCommand;
                if(myStruct.FromCommand != null)
                    dtRow.FromCommand = myStruct.FromCommand;
                if(myStruct.WhereCommand != null)
                    dtRow.WhereCommand = myStruct.WhereCommand;
                if(myStruct.IdClauseType != null)
                    dtRow.IdClauseType = myStruct.IdClauseType;
                if(myStruct.GroupByCommand != null)
                    dtRow.GroupByCommand = myStruct.GroupByCommand;
                if(myStruct.OrderByCommand != null)
                    dtRow.OrderByCommand = myStruct.OrderByCommand;
                if(myStruct.HavingCommand != null)
                    dtRow.HavingCommand = myStruct.HavingCommand;
                if(myStruct.Bias != null)
                    dtRow.Bias = myStruct.Bias;

            DAupdt.DS_ClauseDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Clause] SET " + FieldsSetString + " WHERE No = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            GClass.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public DataTable Select(string FieldName, string Value)
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From Clause Where " + FieldName + " = " + Value;
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public DataTable Select()
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From Clause";
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public strcClause SelectByNo(strcClause myStruct)
        {
            DEQ.DEQ DAslct = new DEQ.DEQ(StrCon1);
            //DEQ.DEQ_DS.DS_ClauseRow dtRow = new DEQ.DEQ_DS.DS_ClauseDataTable().NewDS_ClauseRow();

            DEQ.DEQ_DS.DS_ClauseDataTable dt = new DEQ.DEQ_DS.DS_ClauseDataTable();

            dt = DAslct.DS_ClauseDataTable_SelectCommand(" WHERE No = " + myStruct.No);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoQuery = 0;
                myStruct.NoClause = 0;
                myStruct.SelectCommand = "0";
                myStruct.FromCommand = "0";
                myStruct.WhereCommand = "0";
                myStruct.GroupByCommand = "0";
                myStruct.OrderByCommand = "0";
                myStruct.HavingCommand = "0";
                myStruct.Bias = "0";

                return myStruct ;
            }
            myStruct.No = (long)dt.Rows[0]["No"];
            myStruct.NoQuery = (int)dt.Rows[0]["NoQuery"];
            myStruct.NoClause = (int)dt.Rows[0]["NoClause"];
            myStruct.SelectCommand = (string)dt.Rows[0]["SelectCommand"];
            myStruct.FromCommand = (string)dt.Rows[0]["FromCommand"];
            myStruct.WhereCommand = (string)dt.Rows[0]["WhereCommand"];
            myStruct.IdClauseType = (short)dt.Rows[0]["IdClauseType"];
            myStruct.GroupByCommand = (string)dt.Rows[0]["GroupByCommand"];
            myStruct.OrderByCommand = (string)dt.Rows[0]["OrderByCommand"];
            myStruct.HavingCommand = (string)dt.Rows[0]["HavingCommand"];
            myStruct.Bias = (string)dt.Rows[0]["Bias"];

            return myStruct ;

        } // End SelectByNo


        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid)
        {
            string Qry1 = "SELECT  [No] , [NoQuery] as 'Clause.NoQuery' , [NoClause] as 'Clause.NoClause' , [SelectCommand] as 'Clause.SelectCommand' , [FromCommand] as 'Clause.FromCommand' , [WhereCommand] as 'Clause.WhereCommand' , [IdClauseType] as 'Clause.IdClauseType' , [GroupByCommand] as 'Clause.GroupByCommand' , [OrderByCommand] as 'Clause.OrderByCommand' , [HavingCommand] as 'Clause.HavingCommand' , [Bias] as 'Clause.Bias'  FROM Clause AS TClause";

            String FiledPersianName = "شناسه پرس و جو,شناسه عبارت,فرمان انتخاب شده,فرمان از,محدوده فرمان,نوع عبارت,گروه بندي شده با فرمان,تنظيم شده با فرمان,داشتن فرمان,Bias";
            String FiledEnglishName = "Clause.NoQuery,Clause.NoClause,Clause.SelectCommand,Clause.FromCommand,Clause.WhereCommand,Clause.IdClauseType,Clause.GroupByCommand,Clause.OrderByCommand,Clause.HavingCommand,Clause.Bias";

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
                string Qry1 = "SELECT  [No] , [NoQuery] as 'Clause.NoQuery' , [NoClause] as 'Clause.NoClause' , [SelectCommand] as 'Clause.SelectCommand' , [FromCommand] as 'Clause.FromCommand' , [WhereCommand] as 'Clause.WhereCommand' , [IdClauseType] as 'Clause.IdClauseType' , [GroupByCommand] as 'Clause.GroupByCommand' , [OrderByCommand] as 'Clause.OrderByCommand' , [HavingCommand] as 'Clause.HavingCommand' , [Bias] as 'Clause.Bias'  FROM Clause AS TClause";

                String FiledPersianName = "شناسه پرس و جو,شناسه عبارت,فرمان انتخاب شده,فرمان از,محدوده فرمان,نوع عبارت,گروه بندي شده با فرمان,تنظيم شده با فرمان,داشتن فرمان,Bias";
                String FiledEnglishName = "Clause.NoQuery,Clause.NoClause,Clause.SelectCommand,Clause.FromCommand,Clause.WhereCommand,Clause.IdClauseType,Clause.GroupByCommand,Clause.OrderByCommand,Clause.HavingCommand,Clause.Bias";

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


        /// <summary>
        /// This Function Will Fill Clause Table Into DataGrid By NoQuery Field
        /// </summary>
        /// <param name="tblName"></param>"
        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid , clsQuery.strcQuery myStruct )
        {
            DEQ.DEQ DAslct = new DEQ.DEQ(StrCon1);
            myGrid.DataSource = DAslct.DS_ClauseDataTable_SelectCommand(" WHERE NoQuery = " + myStruct.No);

            

            return 0 ;

        }

  //End FillTable Function


  //End FillComboBYTable


        public int FillIdClauseTypeComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 86 ORDER BY Num";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001375 + ")";

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

            string Str = "Select [Clause]." + FieldName[0] + " From Clause Where [No] = " + No;
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
                Str = "Update [Clause] SET [" + FieldName[0] + "] = '" + value + "' where [No] = " + No;
            else if (FieldName[1] == "6")
                Str = "Update [Clause] SET [" + FieldName[0] + "] = " + value + " where [No] = " + No;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public int FillThisDataGridView(System.Windows.Forms.DataGridView myGrid ,string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [No] , [NoQuery] as 'Clause.NoQuery' , [NoClause] as 'Clause.NoClause' , [SelectCommand] as 'Clause.SelectCommand' , [FromCommand] as 'Clause.FromCommand' , [WhereCommand] as 'Clause.WhereCommand' , [IdClauseType] as 'Clause.IdClauseType' , [GroupByCommand] as 'Clause.GroupByCommand' , [OrderByCommand] as 'Clause.OrderByCommand' , [HavingCommand] as 'Clause.HavingCommand' , [Bias] as 'Clause.Bias'  From Clause Where [Clause]." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            myGrid.DataSource = DAC.GeneralSelectCommand(cmd);
            

            return 0 ;

        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001375) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001375) " + 
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


        public strcClause GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsClause.strcClause myStruct = new clsClause.strcClause();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From Clause Where [Clause]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.No = (long)dt.Rows[0]["No"];
            myStruct.NoQuery = (int)dt.Rows[0]["NoQuery"];
            myStruct.NoClause = (int)dt.Rows[0]["NoClause"];
            myStruct.SelectCommand = (string)dt.Rows[0]["SelectCommand"];
            myStruct.FromCommand = (string)dt.Rows[0]["FromCommand"];
            myStruct.WhereCommand = (string)dt.Rows[0]["WhereCommand"];
            myStruct.IdClauseType = (short)dt.Rows[0]["IdClauseType"];
            myStruct.GroupByCommand = (string)dt.Rows[0]["GroupByCommand"];
            myStruct.OrderByCommand = (string)dt.Rows[0]["OrderByCommand"];
            myStruct.HavingCommand = (string)dt.Rows[0]["HavingCommand"];
            myStruct.Bias = (string)dt.Rows[0]["Bias"];


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