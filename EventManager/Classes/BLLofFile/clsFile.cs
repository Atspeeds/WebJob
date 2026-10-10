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
    public class clsFile
    {
        string StrCon1;

        public struct strcFile
        {
            public int NoEntity;
            public int NoFileType;
            public int NoFileLocation;
            public byte[] Stream;
            public string Name;

        } //End Struct

        public clsFile()
        {

            //Configuration config = new Configuration("Web.config");
            //StrCon1 = FormBuilder.Properties.Settings.Default.StrCon;
            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcFile GetNewRow()
        {
            return new strcFile() ;

        }//End GetNewRow Function


        public int Delete(strcFile myStruct)
        {
                File.File DAdlt = new File.File(StrCon1);
                File.File_DS.DS_FileRow dtRow = new File.File_DS.DS_FileDataTable().NewDS_FileRow();

                if(myStruct.NoEntity != null)
                    dtRow.NoEntity = myStruct.NoEntity;
                if(myStruct.NoFileType != null)
                    dtRow.NoFileType = myStruct.NoFileType;
                if(myStruct.NoFileLocation != null)
                    dtRow.NoFileLocation = myStruct.NoFileLocation;
                if(myStruct.Stream != null)
                    dtRow.Stream = myStruct.Stream;
                if(myStruct.Name != null)
                    dtRow.Name = myStruct.Name;

                DAdlt.DS_FileDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From File where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcFile myStruct)
        {
            File.File DAinsrt = new File.File(StrCon1);
            File.File_DS.DS_FileRow dtRow = new File.File_DS.DS_FileDataTable().NewDS_FileRow();

            if(myStruct.NoEntity != null)
                dtRow.NoEntity = myStruct.NoEntity;
            if(myStruct.NoFileType != null)
                dtRow.NoFileType = myStruct.NoFileType;
            if(myStruct.NoFileLocation != null)
                dtRow.NoFileLocation = myStruct.NoFileLocation;
            if(myStruct.Stream != null)
                dtRow.Stream = myStruct.Stream;
            if(myStruct.Name != null)
                dtRow.Name = myStruct.Name;

            return DAinsrt.DS_FileDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcFile myStruct)
        {
            File.File DAupdt = new File.File(StrCon1);
            File.File_DS.DS_FileRow dtRow = new File.File_DS.DS_FileDataTable().NewDS_FileRow();

                if(myStruct.NoEntity != null)
                    dtRow.NoEntity = myStruct.NoEntity;
                if(myStruct.NoFileType != null)
                    dtRow.NoFileType = myStruct.NoFileType;
                if(myStruct.NoFileLocation != null)
                    dtRow.NoFileLocation = myStruct.NoFileLocation;
                if(myStruct.Stream != null)
                    dtRow.Stream = myStruct.Stream;
                if(myStruct.Name != null)
                    dtRow.Name = myStruct.Name;

            DAupdt.DS_FileDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [File] SET " + FieldsSetString + " WHERE NoEntity = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            GClass.GeneralUpdateCommand(objCommand);
            return 0;
        }  //End Update Function


        public DataTable Select(string FieldName, string Value)
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [File] Where " + FieldName + " = " + Value;
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public DataTable Select()
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [File]";
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public strcFile SelectByNo(strcFile myStruct)
        {
            File.File DAslct = new File.File(StrCon1);
            //File.File_DS.DS_FileRow dtRow = new File.File_DS.DS_FileDataTable().NewDS_FileRow();

            File.File_DS.DS_FileDataTable dt = new File.File_DS.DS_FileDataTable();

            dt = DAslct.DS_FileDataTable_SelectCommand(" WHERE NoEntity = " + myStruct.NoEntity);

            if(dt.Rows.Count == 0 )
            {
                myStruct.NoEntity = 0;
                myStruct.NoFileType = 0;
                myStruct.NoFileLocation = 0;
                myStruct.Name = "0";

                return myStruct ;
            }
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.NoFileType = (int)dt.Rows[0]["NoFileType"];
            myStruct.NoFileLocation = (int)dt.Rows[0]["NoFileLocation"];
            myStruct.Stream = (byte[])dt.Rows[0]["Stream"];
            myStruct.Name = (string)dt.Rows[0]["Name"];

            return myStruct ;

        } // End SelectByNo


        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid)
        {
            string Qry1 = "SELECT  [NoEntity] , 'FileType.Name' = (SELECT [Name] FROM [FileType] WHERE TFile.[NoFileType] = [No]), 'FileLocation.Name' = (SELECT [Name] FROM [FileLocation] WHERE TFile.[NoFileLocation] = [No]), [Stream] as 'File.Stream' , [Name] as 'File.Name'  FROM File AS TFile";

            String FiledPersianName = "نام نوع فايل,نام محل فايل,باينري فايل,عنوان";
            String FiledEnglishName = "FileType.Name,FileLocation.Name,File.Stream,File.Name";

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
                string Qry1 = "SELECT  [NoEntity] , 'FileType.Name' = (SELECT [Name] FROM [FileType] WHERE TFile.[NoFileType] = [No]), 'FileLocation.Name' = (SELECT [Name] FROM [FileLocation] WHERE TFile.[NoFileLocation] = [No]), [Stream] as 'File.Stream' , [Name] as 'File.Name'  FROM File AS TFile";

                String FiledPersianName = "نام نوع فايل,نام محل فايل,باينري فايل,عنوان";
                String FiledEnglishName = "FileType.Name,FileLocation.Name,File.Stream,File.Name";

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
        /// This Function Will Fill File Table Into DataGrid By NoFileType Field
        /// </summary>
        /// <param name="tblName"></param>"
        

        /// <summary>
        /// This Function Will Fill File Table Into DataGrid By NoFileLocation Field
        /// </summary>
        /// <param name="tblName"></param>"
        

  //End FillTable Function


        public int FillNoFileTypeIntoComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            File.File DAslct = new File.File(StrCon1);
            myComboBox.DataSource = DAslct.DS_FileTypeDataTable_SelectCommand_SP();
            myComboBox.DisplayMember = "Name";
            myComboBox.ValueMember = "No";
            

            return 0 ;

        }
        public int FillNoFileLocationIntoComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            File.File DAslct = new File.File(StrCon1);
            myComboBox.DataSource = DAslct.DS_FileLocationDataTable_SelectCommand_SP();
            myComboBox.DisplayMember = "Name";
            myComboBox.ValueMember = "No";
            

            return 0 ;

        }
  //End FillComboBYTable


  //End FillComboBYLookUp






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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001036 + ")";

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



        public string GetAtt( int NoEntity , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [File]." + FieldName[0] + " From File Where [NoEntity] = " + NoEntity;
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
                Str = "Update [File] SET [" + FieldName[0] + "] = '" + value + "' where [NoEntity] = " + NoEntity;
            else if (FieldName[1] == "6")
                Str = "Update [File] SET [" + FieldName[0] + "] = " + value + " where [NoEntity] = " + NoEntity;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public int FillThisDataGridView(System.Windows.Forms.DataGridView myGrid ,string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoEntity] , 'FileType.Name' = (SELECT [Name] FROM [FileType] WHERE TFile.[NoFileType] = [No]), 'FileLocation.Name' = (SELECT [Name] FROM [FileLocation] WHERE TFile.[NoFileLocation] = [No]), [Stream] as 'File.Stream' , [Name] as 'File.Name'  From File Where [File]." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            myGrid.DataSource = DAC.GeneralSelectCommand(cmd);
            

            return 0 ;

        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001036) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001036) " + 
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


        public strcFile GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsFile.strcFile myStruct = new clsFile.strcFile();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From File Where [File]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            myStruct.NoFileType = (int)dt.Rows[0]["NoFileType"];
            myStruct.NoFileLocation = (int)dt.Rows[0]["NoFileLocation"];
            myStruct.Stream = (byte[])dt.Rows[0]["Stream"];
            myStruct.Name = (string)dt.Rows[0]["Name"];


            return myStruct;

        }




        public int FillThisListBox(System.Windows.Forms.ListBox myListBox ,string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            File.File DAslct = new File.File(StrCon1);
            File.File_DS.DS_FileDataTable dt = new File.File_DS.DS_FileDataTable();

            dt = DAslct.DS_FileDataTable_SelectCommand(" WHERE " + FieldName + " = " + Value );

            myListBox.DataSource = dt;
            myListBox.DisplayMember = "Name";
            myListBox.ValueMember = "NoEntity";
            

            return 0 ;

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