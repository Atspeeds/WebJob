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
    public class clsFields
    {
        string StrCon1;
        string StrDb;

        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcFields
        {
            public int NO;
            public string Name;
            public string PersianName;
            public long TableNo;
            public short FieldDataTypeNo;
            public bool IsInkey;
            public bool IsIdentity;
            public bool IsNullable;
            public bool IsForeignKey;
            public int NoLookUpType;
            public short FieldTypeNo;
            public bool HasDescriptionField;
            public bool IsMandatory;
            public int RangeFrom;
            public int RangeTo;
            public int Length;
            public bool IsUnique;
            public bool IsInfo;
            public string SummaryName;
            public int NoOprationEnvironment;
            public string DefaultValue;
            public string HintMessage;
            public int NoQuery;

        } //End Struct

        public clsFields()
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

        public strcFields GetNewRow()
        {
            strcFields objNewStruct =  new strcFields() ;
            objNewStruct.Name = ""; 
            objNewStruct.PersianName = ""; 
            objNewStruct.SummaryName = ""; 
            objNewStruct.DefaultValue = ""; 
            objNewStruct.HintMessage = ""; 
            return objNewStruct;

        }//End GetNewRow Function


        public int Delete(strcFields myStruct)
        {
                Entity.Entity DAdlt = new Entity.Entity(StrCon1);
                Entity.Entity_DS.DS_FieldsRow dtRow = new Entity.Entity_DS.DS_FieldsDataTable().NewDS_FieldsRow();

                dtRow.NO = myStruct.NO;
                dtRow.Name = myStruct.Name;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.TableNo = myStruct.TableNo;
                dtRow.FieldDataTypeNo = myStruct.FieldDataTypeNo;
                dtRow.IsInkey = myStruct.IsInkey;
                dtRow.IsIdentity = myStruct.IsIdentity;
                dtRow.IsNullable = myStruct.IsNullable;
                dtRow.IsForeignKey = myStruct.IsForeignKey;
                dtRow.NoLookUpType = myStruct.NoLookUpType;
                dtRow.FieldTypeNo = myStruct.FieldTypeNo;
                dtRow.HasDescriptionField = myStruct.HasDescriptionField;
                dtRow.IsMandatory = myStruct.IsMandatory;
                dtRow.RangeFrom = myStruct.RangeFrom;
                dtRow.RangeTo = myStruct.RangeTo;
                dtRow.Length = myStruct.Length;
                dtRow.IsUnique = myStruct.IsUnique;
                dtRow.IsInfo = myStruct.IsInfo;
                dtRow.SummaryName = myStruct.SummaryName;
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
                dtRow.DefaultValue = myStruct.DefaultValue;
                dtRow.HintMessage = myStruct.HintMessage;
                dtRow.NoQuery = myStruct.NoQuery;

                DAdlt.DS_FieldsDataTable_DeleteCommand(dtRow);

                return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [Fields] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        public int Insert(strcFields myStruct)
        {
            Entity.Entity DAinsrt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_FieldsRow dtRow = new Entity.Entity_DS.DS_FieldsDataTable().NewDS_FieldsRow();

            string Qry ="SELECT MAX(No) FROM [Fields]";
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
            dtRow.TableNo = myStruct.TableNo;
            dtRow.FieldDataTypeNo = myStruct.FieldDataTypeNo;
            dtRow.IsInkey = myStruct.IsInkey;
            dtRow.IsIdentity = myStruct.IsIdentity;
            dtRow.IsNullable = myStruct.IsNullable;
            dtRow.IsForeignKey = myStruct.IsForeignKey;
            dtRow.NoLookUpType = myStruct.NoLookUpType;
            dtRow.FieldTypeNo = myStruct.FieldTypeNo;
            dtRow.HasDescriptionField = myStruct.HasDescriptionField;
            dtRow.IsMandatory = myStruct.IsMandatory;
            dtRow.RangeFrom = myStruct.RangeFrom;
            dtRow.RangeTo = myStruct.RangeTo;
            dtRow.Length = myStruct.Length;
            dtRow.IsUnique = myStruct.IsUnique;
            dtRow.IsInfo = myStruct.IsInfo;
            dtRow.SummaryName = myStruct.SummaryName;
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
            dtRow.DefaultValue = myStruct.DefaultValue;
            dtRow.HintMessage = myStruct.HintMessage;
            dtRow.NoQuery = myStruct.NoQuery;

            return DAinsrt.DS_FieldsDataTable_InsertCommand(dtRow);

        }  //End Insert Function


        public int Update(strcFields myStruct)
        {
            Entity.Entity DAupdt = new Entity.Entity(StrCon1);
            Entity.Entity_DS.DS_FieldsRow dtRow = new Entity.Entity_DS.DS_FieldsDataTable().NewDS_FieldsRow();

                dtRow.NO = myStruct.NO;
                dtRow.Name = myStruct.Name;
                dtRow.PersianName = myStruct.PersianName;
                dtRow.TableNo = myStruct.TableNo;
                dtRow.FieldDataTypeNo = myStruct.FieldDataTypeNo;
                dtRow.IsInkey = myStruct.IsInkey;
                dtRow.IsIdentity = myStruct.IsIdentity;
                dtRow.IsNullable = myStruct.IsNullable;
                dtRow.IsForeignKey = myStruct.IsForeignKey;
                dtRow.NoLookUpType = myStruct.NoLookUpType;
                dtRow.FieldTypeNo = myStruct.FieldTypeNo;
                dtRow.HasDescriptionField = myStruct.HasDescriptionField;
                dtRow.IsMandatory = myStruct.IsMandatory;
                dtRow.RangeFrom = myStruct.RangeFrom;
                dtRow.RangeTo = myStruct.RangeTo;
                dtRow.Length = myStruct.Length;
                dtRow.IsUnique = myStruct.IsUnique;
                dtRow.IsInfo = myStruct.IsInfo;
                dtRow.SummaryName = myStruct.SummaryName;
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
                dtRow.DefaultValue = myStruct.DefaultValue;
                dtRow.HintMessage = myStruct.HintMessage;
                dtRow.NoQuery = myStruct.NoQuery;

            DAupdt.DS_FieldsDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Fields] SET " + FieldsSetString + " WHERE NO = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcFields> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcFields> myStrcList = new List<strcFields>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Fields] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcFields myStruct = new strcFields();
                                myStruct.NO = (int)dt.Rows[i]["NO"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.TableNo = (long)dt.Rows[i]["TableNo"];
                myStruct.FieldDataTypeNo = (short)dt.Rows[i]["FieldDataTypeNo"];
                myStruct.IsInkey = (bool)dt.Rows[i]["IsInkey"];
                myStruct.IsIdentity = (bool)dt.Rows[i]["IsIdentity"];
                myStruct.IsNullable = (bool)dt.Rows[i]["IsNullable"];
                myStruct.IsForeignKey = (bool)dt.Rows[i]["IsForeignKey"];
                myStruct.NoLookUpType = (int)dt.Rows[i]["NoLookUpType"];
                myStruct.FieldTypeNo = (short)dt.Rows[i]["FieldTypeNo"];
                myStruct.HasDescriptionField = (bool)dt.Rows[i]["HasDescriptionField"];
                myStruct.IsMandatory = (bool)dt.Rows[i]["IsMandatory"];
                myStruct.RangeFrom = (int)dt.Rows[i]["RangeFrom"];
                myStruct.RangeTo = (int)dt.Rows[i]["RangeTo"];
                myStruct.Length = (int)dt.Rows[i]["Length"];
                myStruct.IsUnique = (bool)dt.Rows[i]["IsUnique"];
                myStruct.IsInfo = (bool)dt.Rows[i]["IsInfo"];
                myStruct.SummaryName = (string)dt.Rows[i]["SummaryName"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.DefaultValue = (string)dt.Rows[i]["DefaultValue"];
                myStruct.HintMessage = (string)dt.Rows[i]["HintMessage"];
                myStruct.NoQuery = (int)dt.Rows[i]["NoQuery"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public List<strcFields> Select(string FieldName, string Value,string strwhere)
        {

            DataTable dt;
            List<strcFields> myStrcList = new List<strcFields>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Fields] Where " + FieldName + " = " + Value + " and " + strwhere;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcFields myStruct = new strcFields();
                                myStruct.NO = (int)dt.Rows[i]["NO"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.TableNo = (long)dt.Rows[i]["TableNo"];
                myStruct.FieldDataTypeNo = (short)dt.Rows[i]["FieldDataTypeNo"];
                myStruct.IsInkey = (bool)dt.Rows[i]["IsInkey"];
                myStruct.IsIdentity = (bool)dt.Rows[i]["IsIdentity"];
                myStruct.IsNullable = (bool)dt.Rows[i]["IsNullable"];
                myStruct.IsForeignKey = (bool)dt.Rows[i]["IsForeignKey"];
                myStruct.NoLookUpType = (int)dt.Rows[i]["NoLookUpType"];
                myStruct.FieldTypeNo = (short)dt.Rows[i]["FieldTypeNo"];
                myStruct.HasDescriptionField = (bool)dt.Rows[i]["HasDescriptionField"];
                myStruct.IsMandatory = (bool)dt.Rows[i]["IsMandatory"];
                myStruct.RangeFrom = (int)dt.Rows[i]["RangeFrom"];
                myStruct.RangeTo = (int)dt.Rows[i]["RangeTo"];
                myStruct.Length = (int)dt.Rows[i]["Length"];
                myStruct.IsUnique = (bool)dt.Rows[i]["IsUnique"];
                myStruct.IsInfo = (bool)dt.Rows[i]["IsInfo"];
                myStruct.SummaryName = (string)dt.Rows[i]["SummaryName"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.DefaultValue = (string)dt.Rows[i]["DefaultValue"];
                myStruct.HintMessage = (string)dt.Rows[i]["HintMessage"];
                myStruct.NoQuery = (int)dt.Rows[i]["NoQuery"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Fields] Where " + FieldName + " = " + Value;
            String FiledPersianName = "نام فيلد,نام انگليسي,جداول,نوع داده اي فيلد,کليد است,شمارنده است,مي تواند خالي باشد,کليد خارجي است,شناسه لوک آپي,نوع فيلد,فيلد توصيفي دارد,اجباريست؟,محدوده عددي از,محدوده عددي تا,طول,مقدار منحصر بفرد,مقدار اطلاعاتي,نام اختصاري,شناسه سامانه شرکت,مقدار پيش فرض,راهنمايي ,شناسه کوئري";
            String FiledEnglishName = "Fields.Name,Fields.PersianName,Tables.Name,Fields.FieldDataTypeNo,Fields.IsInkey,Fields.IsIdentity,Fields.IsNullable,Fields.IsForeignKey,Fields.NoLookUpType,Fields.FieldTypeNo,Fields.HasDescriptionField,Fields.IsMandatory,Fields.RangeFrom,Fields.RangeTo,Fields.Length,Fields.IsUnique,Fields.IsInfo,Fields.SummaryName,Fields.NoOprationEnvironment,Fields.DefaultValue,Fields.HintMessage,Fields.NoQuery";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcFields> Select()
        {

            DataTable dt;
            List<strcFields> myStrcList = new List<strcFields>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [Fields]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcFields myStruct = new strcFields();
                                myStruct.NO = (int)dt.Rows[i]["NO"];
                myStruct.Name = (string)dt.Rows[i]["Name"];
                myStruct.PersianName = (string)dt.Rows[i]["PersianName"];
                myStruct.TableNo = (long)dt.Rows[i]["TableNo"];
                myStruct.FieldDataTypeNo = (short)dt.Rows[i]["FieldDataTypeNo"];
                myStruct.IsInkey = (bool)dt.Rows[i]["IsInkey"];
                myStruct.IsIdentity = (bool)dt.Rows[i]["IsIdentity"];
                myStruct.IsNullable = (bool)dt.Rows[i]["IsNullable"];
                myStruct.IsForeignKey = (bool)dt.Rows[i]["IsForeignKey"];
                myStruct.NoLookUpType = (int)dt.Rows[i]["NoLookUpType"];
                myStruct.FieldTypeNo = (short)dt.Rows[i]["FieldTypeNo"];
                myStruct.HasDescriptionField = (bool)dt.Rows[i]["HasDescriptionField"];
                myStruct.IsMandatory = (bool)dt.Rows[i]["IsMandatory"];
                myStruct.RangeFrom = (int)dt.Rows[i]["RangeFrom"];
                myStruct.RangeTo = (int)dt.Rows[i]["RangeTo"];
                myStruct.Length = (int)dt.Rows[i]["Length"];
                myStruct.IsUnique = (bool)dt.Rows[i]["IsUnique"];
                myStruct.IsInfo = (bool)dt.Rows[i]["IsInfo"];
                myStruct.SummaryName = (string)dt.Rows[i]["SummaryName"];
                myStruct.NoOprationEnvironment = (int)dt.Rows[i]["NoOprationEnvironment"];
                myStruct.DefaultValue = (string)dt.Rows[i]["DefaultValue"];
                myStruct.HintMessage = (string)dt.Rows[i]["HintMessage"];
                myStruct.NoQuery = (int)dt.Rows[i]["NoQuery"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcFields SelectByNo(strcFields myStruct)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            //Entity.Entity_DS.DS_FieldsRow dtRow = new Entity.Entity_DS.DS_FieldsDataTable().NewDS_FieldsRow();

            Entity.Entity_DS.DS_FieldsDataTable dt = new Entity.Entity_DS.DS_FieldsDataTable();

            dt = DAslct.DS_FieldsDataTable_SelectCommand(" WHERE NO = " + myStruct.NO);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NO = 0;
                myStruct.Name = "0";
                myStruct.PersianName = "0";
                myStruct.NoLookUpType = 0;
                myStruct.RangeFrom = 0;
                myStruct.RangeTo = 0;
                myStruct.Length = 0;
                myStruct.SummaryName = "0";
                myStruct.NoOprationEnvironment = 0;
                myStruct.DefaultValue = "0";
                myStruct.HintMessage = "0";
                myStruct.NoQuery = 0;

                return myStruct ;
            }
            myStruct.NO = (int)dt.Rows[0]["NO"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.TableNo = (long)dt.Rows[0]["TableNo"];
            myStruct.FieldDataTypeNo = (short)dt.Rows[0]["FieldDataTypeNo"];
            myStruct.IsInkey = (bool)dt.Rows[0]["IsInkey"];
            myStruct.IsIdentity = (bool)dt.Rows[0]["IsIdentity"];
            myStruct.IsNullable = (bool)dt.Rows[0]["IsNullable"];
            myStruct.IsForeignKey = (bool)dt.Rows[0]["IsForeignKey"];
            myStruct.NoLookUpType = (int)dt.Rows[0]["NoLookUpType"];
            myStruct.FieldTypeNo = (short)dt.Rows[0]["FieldTypeNo"];
            myStruct.HasDescriptionField = (bool)dt.Rows[0]["HasDescriptionField"];
            myStruct.IsMandatory = (bool)dt.Rows[0]["IsMandatory"];
            myStruct.RangeFrom = (int)dt.Rows[0]["RangeFrom"];
            myStruct.RangeTo = (int)dt.Rows[0]["RangeTo"];
            myStruct.Length = (int)dt.Rows[0]["Length"];
            myStruct.IsUnique = (bool)dt.Rows[0]["IsUnique"];
            myStruct.IsInfo = (bool)dt.Rows[0]["IsInfo"];
            myStruct.SummaryName = (string)dt.Rows[0]["SummaryName"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.DefaultValue = (string)dt.Rows[0]["DefaultValue"];
            myStruct.HintMessage = (string)dt.Rows[0]["HintMessage"];
            myStruct.NoQuery = (int)dt.Rows[0]["NoQuery"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NO] as 'PKNO', [Name] as 'نام فيلد' , [PersianName] as 'نام انگليسي' ,[TableNo] as 'FKTableNo', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TFields.[TableNo] = [NO]), [FieldDataTypeNo] as 'نوع داده اي فيلد' , [IsInkey] as 'کليد است' , [IsIdentity] as 'شمارنده است' , [IsNullable] as 'مي تواند خالي باشد' , [IsForeignKey] as 'کليد خارجي است' , [NoLookUpType] as 'شناسه لوک آپي' , [FieldTypeNo] as 'نوع فيلد' , [HasDescriptionField] as 'فيلد توصيفي دارد' , [IsMandatory] as 'اجباريست؟' , [RangeFrom] as 'محدوده عددي از' , [RangeTo] as 'محدوده عددي تا' , [Length] as 'طول' , [IsUnique] as 'مقدار منحصر بفرد' , [IsInfo] as 'مقدار اطلاعاتي' , [SummaryName] as 'نام اختصاري' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [DefaultValue] as 'مقدار پيش فرض' , [HintMessage] as 'راهنمايي ' , [NoQuery] as 'FKNoQuery'  FROM [Fields] AS TFields";
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
                string Qry1 = "SELECT  [NO] as 'PKNO', [Name] as 'نام فيلد' , [PersianName] as 'نام انگليسي' ,[TableNo] as 'FKTableNo', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TFields.[TableNo] = [NO]), [FieldDataTypeNo] as 'نوع داده اي فيلد' , [IsInkey] as 'کليد است' , [IsIdentity] as 'شمارنده است' , [IsNullable] as 'مي تواند خالي باشد' , [IsForeignKey] as 'کليد خارجي است' , [NoLookUpType] as 'شناسه لوک آپي' , [FieldTypeNo] as 'نوع فيلد' , [HasDescriptionField] as 'فيلد توصيفي دارد' , [IsMandatory] as 'اجباريست؟' , [RangeFrom] as 'محدوده عددي از' , [RangeTo] as 'محدوده عددي تا' , [Length] as 'طول' , [IsUnique] as 'مقدار منحصر بفرد' , [IsInfo] as 'مقدار اطلاعاتي' , [SummaryName] as 'نام اختصاري' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [DefaultValue] as 'مقدار پيش فرض' , [HintMessage] as 'راهنمايي ' , [NoQuery] as 'FKNoQuery'  FROM [Fields] AS TFields";

            }
            return 0 ;

        }


        /// <summary>
        /// This Function Will Fill Fields Table Into DataGrid By TableNo Field
        /// </summary>
        /// <param name="tblName"></param>"
        public DataTable FillThisDataGridView(clsTables.strcTables myStruct )
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            return DAslct.DS_FieldsDataTable_SelectCommand(" WHERE TableNo = " + myStruct.NO);

        }

        /// <summary>
        /// This Function Will Fill Fields Table Into DataGrid By NoQuery Field
        /// </summary>
        /// <param name="tblName"></param>"
        //public DataTable FillThisDataGridView(clsQuery.strcQuery myStruct )
        //{
        //    Entity.Entity DAslct = new Entity.Entity(StrCon1);
        //    return DAslct.DS_FieldsDataTable_SelectCommand(" WHERE NoQuery = " + myStruct.NoEntity);

        //}

  //End FillTable Function


        public DataTable GetDataTableDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Fields]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableTableNoDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableTableNoDescriptive(string strName)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%'";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableTableNoDescriptive(string strName,string strwhere)
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT NO as 'No',Name as 'Name' FROM [Tables] where Name like '" + strName + "%' and " + strwhere;
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001116 + ")";

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

            string Str = "Select [Fields]." + FieldName[0] + " From Fields Where [NO] = " + NO;
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
                Str = "Update [Fields] SET [" + FieldName[0] + "] = '" + value + "' where [NO] = " + NO;
            else if (FieldName[1] == "6")
                Str = "Update [Fields] SET [" + FieldName[0] + "] = " + value + " where [NO] = " + NO;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NO] as 'PKNO', [Name] as 'نام فيلد' , [PersianName] as 'نام انگليسي' ,[TableNo] as 'FKTableNo', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TFields.[TableNo] = [NO]), [FieldDataTypeNo] as 'نوع داده اي فيلد' , [IsInkey] as 'کليد است' , [IsIdentity] as 'شمارنده است' , [IsNullable] as 'مي تواند خالي باشد' , [IsForeignKey] as 'کليد خارجي است' , [NoLookUpType] as 'شناسه لوک آپي' , [FieldTypeNo] as 'نوع فيلد' , [HasDescriptionField] as 'فيلد توصيفي دارد' , [IsMandatory] as 'اجباريست؟' , [RangeFrom] as 'محدوده عددي از' , [RangeTo] as 'محدوده عددي تا' , [Length] as 'طول' , [IsUnique] as 'مقدار منحصر بفرد' , [IsInfo] as 'مقدار اطلاعاتي' , [SummaryName] as 'نام اختصاري' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [DefaultValue] as 'مقدار پيش فرض' , [HintMessage] as 'راهنمايي ' , [NoQuery] as 'FKNoQuery'  From [Fields] AS TFields Where TFields." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }


        public DataTable GetDataTableForGridSearch(string FieldName , string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NO] as 'PKNO', [Name] as 'نام فيلد' , [PersianName] as 'نام انگليسي' ,[TableNo] as 'FKTableNo', 'جداول' = (SELECT [Name] FROM [Tables] WHERE TFields.[TableNo] = [NO]), [FieldDataTypeNo] as 'نوع داده اي فيلد' , [IsInkey] as 'کليد است' , [IsIdentity] as 'شمارنده است' , [IsNullable] as 'مي تواند خالي باشد' , [IsForeignKey] as 'کليد خارجي است' , [NoLookUpType] as 'شناسه لوک آپي' , [FieldTypeNo] as 'نوع فيلد' , [HasDescriptionField] as 'فيلد توصيفي دارد' , [IsMandatory] as 'اجباريست؟' , [RangeFrom] as 'محدوده عددي از' , [RangeTo] as 'محدوده عددي تا' , [Length] as 'طول' , [IsUnique] as 'مقدار منحصر بفرد' , [IsInfo] as 'مقدار اطلاعاتي' , [SummaryName] as 'نام اختصاري' , [NoOprationEnvironment] as 'شناسه سامانه شرکت' , [DefaultValue] as 'مقدار پيش فرض' , [HintMessage] as 'راهنمايي ' , [NoQuery] as 'FKNoQuery'  From [Fields] AS TFields Where TFields." + FieldName + " like '%" +  Value  + "%'";
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001116) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001116) " + 
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


        public strcFields GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsFields.strcFields myStruct = new clsFields.strcFields();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [Fields] Where [Fields]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NO = (int)dt.Rows[0]["NO"];
            myStruct.Name = (string)dt.Rows[0]["Name"];
            myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            myStruct.TableNo = (long)dt.Rows[0]["TableNo"];
            myStruct.FieldDataTypeNo = (short)dt.Rows[0]["FieldDataTypeNo"];
            myStruct.IsInkey = (bool)dt.Rows[0]["IsInkey"];
            myStruct.IsIdentity = (bool)dt.Rows[0]["IsIdentity"];
            myStruct.IsNullable = (bool)dt.Rows[0]["IsNullable"];
            myStruct.IsForeignKey = (bool)dt.Rows[0]["IsForeignKey"];
            myStruct.NoLookUpType = (int)dt.Rows[0]["NoLookUpType"];
            myStruct.FieldTypeNo = (short)dt.Rows[0]["FieldTypeNo"];
            myStruct.HasDescriptionField = (bool)dt.Rows[0]["HasDescriptionField"];
            myStruct.IsMandatory = (bool)dt.Rows[0]["IsMandatory"];
            myStruct.RangeFrom = (int)dt.Rows[0]["RangeFrom"];
            myStruct.RangeTo = (int)dt.Rows[0]["RangeTo"];
            myStruct.Length = (int)dt.Rows[0]["Length"];
            myStruct.IsUnique = (bool)dt.Rows[0]["IsUnique"];
            myStruct.IsInfo = (bool)dt.Rows[0]["IsInfo"];
            myStruct.SummaryName = (string)dt.Rows[0]["SummaryName"];
            myStruct.NoOprationEnvironment = (int)dt.Rows[0]["NoOprationEnvironment"];
            myStruct.DefaultValue = (string)dt.Rows[0]["DefaultValue"];
            myStruct.HintMessage = (string)dt.Rows[0]["HintMessage"];
            myStruct.NoQuery = (int)dt.Rows[0]["NoQuery"];


            return myStruct;

        }




        public bool CheckDuplicate(string FieldName,string FieldValue)
        {

        try
            {
                string Qry1 = "SELECT * FROM [Fields] where " + FieldName + " = '" + FieldValue + "'";
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