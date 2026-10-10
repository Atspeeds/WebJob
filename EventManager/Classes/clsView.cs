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
    public partial class clsView
    {
		string StrCon1;
        public String[] PFieldsName ;
        public String[] EFieldsName ;
        public struct strcView
        {
            public int NoComponent;
            public int NoEntityClass;
            public string dllName;
            public string persianName;
            public int IdType;
            public short IdFormType;
            public short IdActionType;
            public short IdManageType;
            public short IdPermissionType;
            public string General;
            public string Description;
            public bool IsActive;
            public int NoEntityType;
            public short IdSize;
            public short IdComponentType;
            public int TabCount;
            public bool HasMasterTab;
            public bool HasWizardButton;

        } //End Struct

        public clsView()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public strcView GetNewRow()
        {
            return new strcView() ;

        }//End GetNewRow Function


        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance DELETE : Component --> View --> WeblogWebpart --> ForumWebpart --> RegisterWebpart --> ContentWebpart --> LoginWebpart --> SearchWebPart --> FAQWebPart --> ResourceWebPart --> MemberShipWebPart --> NewsWebPart --> GalleryWebPart --> AuctionTenderWebPart --> AdvertiseWebPart --> MovingNewsWebPart --> DownLoadUploadWebpart --> WeatherWebPart --> MenuWebPart --> 
        /// </summary>
        public int Delete(strcView myStruct)
        {
            if(CheckDelete(myStruct.NoComponent))
            {

                Component.Component DAdlt = new Component.Component(StrCon1);
                Component.Component_DS.DS_ViewRow dtRow = new Component.Component_DS.DS_ViewDataTable().NewDS_ViewRow();

                dtRow.NoComponent = myStruct.NoComponent;
                dtRow.NoEntityClass = myStruct.NoEntityClass;
                dtRow.dllName = myStruct.dllName;
                dtRow.persianName = myStruct.persianName;
                dtRow.IdType = myStruct.IdType;
                dtRow.IdFormType = myStruct.IdFormType;
                dtRow.IdActionType = myStruct.IdActionType;
                dtRow.IdManageType = myStruct.IdManageType;
                dtRow.IdPermissionType = myStruct.IdPermissionType;
                dtRow.General = myStruct.General;
                dtRow.Description = myStruct.Description;
                dtRow.IsActive = myStruct.IsActive;
                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.IdSize = myStruct.IdSize;
                dtRow.IdComponentType = myStruct.IdComponentType;
                dtRow.TabCount = myStruct.TabCount;
                dtRow.HasMasterTab = myStruct.HasMasterTab;

                DAdlt.DS_ViewDataTable_DeleteCommand_SP(dtRow);

            }

            return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From [View] where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance INSERT: Component --> View --> 
        /// </summary>
        public int Insert( clsComponent.strcComponent myStructComponent , clsView.strcView myStructView )
        {
            
            Dictionary<string, object> Dic = new Dictionary<string, object>();

            Component.Component DAinsrt = new Component.Component(StrCon1);

            Dic.Add( "Component_NoEntity" , myStructComponent.NoEntity);
            Dic.Add( "Component_Name" , myStructComponent.Name);
            Dic.Add( "Component_PersianName" , myStructComponent.PersianName);
            Dic.Add( "Component_topX" , myStructComponent.topX);
            Dic.Add( "Component_topY" , myStructComponent.topY);
            Dic.Add( "Component_bottomX" , myStructComponent.bottomX);
            Dic.Add( "Component_bottomY" , myStructComponent.bottomY);
            Dic.Add( "Component_NoComponentType" , myStructComponent.NoComponentType);
            Dic.Add( "Component_icon" , myStructComponent.icon);
            Dic.Add( "Component_NoParent" , myStructComponent.NoParent);
            Dic.Add( "Component_DefaultValue" , myStructComponent.DefaultValue);
            Dic.Add( "Component_IdViewType" , myStructComponent.IdViewType);
            Dic.Add( "Component_NoSkin" , myStructComponent.NoSkin);
            Dic.Add( "Component_NoWebpartDisplay" , myStructComponent.NoWebpartDisplay);
            Dic.Add( "Component_Text" , myStructComponent.Text);
            Dic.Add( "Component_ToolTip" , myStructComponent.ToolTip);
            Dic.Add( "Component_NoPortal" , myStructComponent.NoPortal);
            Dic.Add( "Component_IdOrderType" , myStructComponent.IdOrderType);
			Dic.Add( "Component_Mode", myStructComponent.Mode);

            Dic.Add( "View_NoComponent" , myStructView.NoComponent);
            Dic.Add( "View_NoEntityClass" , myStructView.NoEntityClass);
            Dic.Add( "View_dllName" , myStructView.dllName);
            Dic.Add( "View_persianName" , myStructView.persianName);
            Dic.Add( "View_IdType" , myStructView.IdType);
            Dic.Add( "View_IdFormType" , myStructView.IdFormType);
            Dic.Add( "View_IdActionType" , myStructView.IdActionType);
            Dic.Add( "View_IdManageType" , myStructView.IdManageType);
            Dic.Add( "View_IdPermissionType" , myStructView.IdPermissionType);
            Dic.Add( "View_General" , myStructView.General);
            Dic.Add( "View_Description" , myStructView.Description);
            Dic.Add( "View_IsActive" , myStructView.IsActive);
            Dic.Add( "View_NoEntityType" , myStructView.NoEntityType);
            Dic.Add( "View_IdSize" , myStructView.IdSize);
            Dic.Add( "View_IdComponentType" , myStructView.IdComponentType);
            Dic.Add( "View_TabCount" , myStructView.TabCount);
            Dic.Add( "View_HasMasterTab" , myStructView.HasMasterTab);
			Dic.Add("View_HasWizardButton", myStructView.HasWizardButton);
            return DAinsrt.DS_ViewDataTable_InsertCommand_SP(Dic);



        }  //End Insert Function


        public int Update(strcView myStruct)
        {
            Component.Component DAupdt = new Component.Component(StrCon1);
            Component.Component_DS.DS_ViewRow dtRow = new Component.Component_DS.DS_ViewDataTable().NewDS_ViewRow();

                dtRow.NoComponent = myStruct.NoComponent;
                dtRow.NoEntityClass = myStruct.NoEntityClass;
                dtRow.dllName = myStruct.dllName;
                dtRow.persianName = myStruct.persianName;
                dtRow.IdType = myStruct.IdType;
                dtRow.IdFormType = myStruct.IdFormType;
                dtRow.IdActionType = myStruct.IdActionType;
                dtRow.IdManageType = myStruct.IdManageType;
                dtRow.IdPermissionType = myStruct.IdPermissionType;
                dtRow.General = myStruct.General;
                dtRow.Description = myStruct.Description;
                dtRow.IsActive = myStruct.IsActive;
                dtRow.NoEntityType = myStruct.NoEntityType;
                dtRow.IdSize = myStruct.IdSize;
                dtRow.IdComponentType = myStruct.IdComponentType;
                dtRow.TabCount = myStruct.TabCount;
                dtRow.HasMasterTab = myStruct.HasMasterTab;
                dtRow.HasWizardButton = myStruct.HasWizardButton;

            DAupdt.DS_ViewDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [View] SET " + FieldsSetString + " WHERE NoComponent = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            DAC.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public List<strcView> Select(string FieldName, string Value)
        {

            DataTable dt;
            List<strcView> myStrcList = new List<strcView>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [View] Where " + FieldName + " = " + Value;
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcView myStruct = new strcView();
                myStruct.NoComponent = (int)dt.Rows[i]["NoComponent"];
                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.dllName = (string)dt.Rows[i]["dllName"];
                myStruct.persianName = (string)dt.Rows[i]["persianName"];
                myStruct.IdType = (int)dt.Rows[i]["IdType"];
                myStruct.IdFormType = (short)dt.Rows[i]["IdFormType"];
                myStruct.IdActionType = (short)dt.Rows[i]["IdActionType"];
                myStruct.IdManageType = (short)dt.Rows[i]["IdManageType"];
                myStruct.IdPermissionType = (short)dt.Rows[i]["IdPermissionType"];
                myStruct.General = (string)dt.Rows[i]["General"];
                myStruct.Description = (string)dt.Rows[i]["Description"];
                myStruct.IsActive = (bool)dt.Rows[i]["IsActive"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.IdSize = (short)dt.Rows[i]["IdSize"];
                myStruct.IdComponentType = (short)dt.Rows[i]["IdComponentType"];
                myStruct.TabCount = (int)dt.Rows[i]["TabCount"];
                myStruct.HasMasterTab = (bool)dt.Rows[i]["HasMasterTab"];
				myStruct.HasWizardButton = (bool)dt.Rows[i]["HasWizardButton"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public DataTable SelectDataTable(string FieldName, string Value)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [View] Where " + FieldName + " = " + Value;
            String FiledPersianName = "عنوان فارسي,نام کلاس,نام دي ال ال ,نام فارسي,نوع,نوع فرم,نوع عمليات,نوع مديريت,نوع دسترسي ها,عمومي,توصيفات,فعال است؟,نام موجوديت,اندازه,نوع قطعه,تعداد زبانه,داراي تب اصلي";
            String FiledEnglishName = "Component.PersianName,EntityClass.Name,View.dllName,View.persianName,View.IdType,View.IdFormType,View.IdActionType,View.IdManageType,View.IdPermissionType,View.General,View.Description,View.IsActive,EntityType.Name,View.IdSize,View.IdComponentType,View.TabCount,View.HasMasterTab";

            PFieldsName = FiledPersianName.Split(',');
            EFieldsName = FiledEnglishName.Split(',');

            return DAC.GeneralSelectCommand(new SqlCommand(Str));

        }

        public List<strcView> Select()
        {

            DataTable dt;
            List<strcView> myStrcList = new List<strcView>();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From [View]";
            dt = DAC.GeneralSelectCommand(new SqlCommand(Str));

            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                strcView myStruct = new strcView();
                                myStruct.NoComponent = (int)dt.Rows[i]["NoComponent"];
                myStruct.NoEntityClass = (int)dt.Rows[i]["NoEntityClass"];
                myStruct.dllName = (string)dt.Rows[i]["dllName"];
                myStruct.persianName = (string)dt.Rows[i]["persianName"];
                myStruct.IdType = (int)dt.Rows[i]["IdType"];
                myStruct.IdFormType = (short)dt.Rows[i]["IdFormType"];
                myStruct.IdActionType = (short)dt.Rows[i]["IdActionType"];
                myStruct.IdManageType = (short)dt.Rows[i]["IdManageType"];
                myStruct.IdPermissionType = (short)dt.Rows[i]["IdPermissionType"];
                myStruct.General = (string)dt.Rows[i]["General"];
                myStruct.Description = (string)dt.Rows[i]["Description"];
                myStruct.IsActive = (bool)dt.Rows[i]["IsActive"];
                myStruct.NoEntityType = (int)dt.Rows[i]["NoEntityType"];
                myStruct.IdSize = (short)dt.Rows[i]["IdSize"];
                myStruct.IdComponentType = (short)dt.Rows[i]["IdComponentType"];
                myStruct.TabCount = (int)dt.Rows[i]["TabCount"];
                myStruct.HasMasterTab = (bool)dt.Rows[i]["HasMasterTab"];
				myStruct.HasWizardButton = (bool)dt.Rows[i]["HasWizardButton"];

                myStrcList.Add(myStruct);
            }

            return myStrcList;
        }

        public strcView SelectByNo(strcView myStruct)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            //Component.Component_DS.DS_ViewRow dtRow = new Component.Component_DS.DS_ViewDataTable().NewDS_ViewRow();

            Component.Component_DS.DS_ViewDataTable dt = new Component.Component_DS.DS_ViewDataTable();

            dt = DAslct.DS_ViewDataTable_SelectCommand(" WHERE NoComponent = " + myStruct.NoComponent);

            if(dt.Rows.Count == 0 )
            {
                                myStruct.NoComponent = 0;
                myStruct.NoEntityClass = 0;
                myStruct.dllName = "0";
                myStruct.persianName = "0";
                myStruct.IdType = 0;
                myStruct.General = "0";
                myStruct.Description = "0";
                myStruct.NoEntityType = 0;
                myStruct.TabCount = 0;

                return myStruct ;
            }
            myStruct.NoComponent = (int)dt.Rows[0]["NoComponent"];
            myStruct.NoEntityClass = (int)dt.Rows[0]["NoEntityClass"];
            myStruct.dllName = (string)dt.Rows[0]["dllName"];
            myStruct.persianName = (string)dt.Rows[0]["persianName"];
            myStruct.IdType = (int)dt.Rows[0]["IdType"];
            myStruct.IdFormType = (short)dt.Rows[0]["IdFormType"];
            myStruct.IdActionType = (short)dt.Rows[0]["IdActionType"];
            myStruct.IdManageType = (short)dt.Rows[0]["IdManageType"];
            myStruct.IdPermissionType = (short)dt.Rows[0]["IdPermissionType"];
            myStruct.General = (string)dt.Rows[0]["General"];
            myStruct.Description = (string)dt.Rows[0]["Description"];
            myStruct.IsActive = (bool)dt.Rows[0]["IsActive"];
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.IdSize = (short)dt.Rows[0]["IdSize"];
            myStruct.IdComponentType = (short)dt.Rows[0]["IdComponentType"];
            myStruct.TabCount = (int)dt.Rows[0]["TabCount"];
            myStruct.HasMasterTab = (bool)dt.Rows[0]["HasMasterTab"];
			myStruct.HasWizardButton = (bool)dt.Rows[0]["HasWizardButton"];

            return myStruct ;

        } // End SelectByNo


        public DataTable GetDataTableForGrid()
        {
            string Qry1 = "SELECT  [NoComponent] as 'PKNoComponent' , 'عنوان فارسي' = (SELECT PersianName FROM Component"+
                " WHERE TView.[NoComponent] = NoEntity),[NoEntityClass] as 'FKNoEntityClass', 'نام کلاس' = (SELECT [Name] FROM [EntityClass] WHERE TView.[NoEntityClass] = [No]), [dllName] as 'نام دي ال ال ' , [persianName] as 'نام فارسي' , [IdType] as 'نوع' , [IdFormType] as 'نوع فرم' , [IdActionType] as 'نوع عمليات' , [IdManageType] as 'نوع مديريت' , [IdPermissionType] as 'نوع دسترسي ها' , [General] as 'عمومي' , [Description] as 'توصيفات' , [IsActive] as 'فعال است؟' ,[NoEntityType] as 'FKNoEntityType', 'نام موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TView.[NoEntityType] = [No]), [IdSize] as 'اندازه' , [IdComponentType] as 'نوع قطعه' , [TabCount] as 'تعداد زبانه' , [HasMasterTab] as 'داراي تب اصلي'  FROM [View] AS TView";
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
                string Qry1 = "SELECT  [NoComponent] as 'PKNoComponent' , 'عنوان فارسي' = (SELECT PersianName FROM Component"+
                " WHERE TView.[NoComponent] = NoEntity),[NoEntityClass] as 'FKNoEntityClass', 'نام کلاس' = (SELECT [Name] FROM [EntityClass] WHERE TView.[NoEntityClass] = [No]), [dllName] as 'نام دي ال ال ' , [persianName] as 'نام فارسي' , [IdType] as 'نوع' , [IdFormType] as 'نوع فرم' , [IdActionType] as 'نوع عمليات' , [IdManageType] as 'نوع مديريت' , [IdPermissionType] as 'نوع دسترسي ها' , [General] as 'عمومي' , [Description] as 'توصيفات' , [IsActive] as 'فعال است؟' ,[NoEntityType] as 'FKNoEntityType', 'نام موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TView.[NoEntityType] = [No]), [IdSize] as 'اندازه' , [IdComponentType] as 'نوع قطعه' , [TabCount] as 'تعداد زبانه' , [HasMasterTab] as 'داراي تب اصلي'  FROM [View] AS TView";

            }
            return 0 ;

        }


        ///// <summary>
        ///// This Function Will Fill View Table Into DataGrid By NoEntityClass Field
        ///// </summary>
        ///// <param name="tblName"></param>"
        //public DataTable FillThisDataGridView(clsEntityClass.strcEntityClass myStruct )
        //{
        //    Component.Component DAslct = new Component.Component(StrCon1);
        //    return DAslct.DS_ViewDataTable_SelectCommand(" WHERE NoEntityClass = " + myStruct.No);

        //}

        ///// <summary>
        ///// This Function Will Fill View Table Into DataGrid By NoEntityType Field
        ///// </summary>
        ///// <param name="tblName"></param>"
        //public DataTable FillThisDataGridView(clsEntityType.strcEntityType myStruct )
        //{
        //    Component.Component DAslct = new Component.Component(StrCon1);
        //    return DAslct.DS_ViewDataTable_SelectCommand(" WHERE NoEntityType = " + myStruct.No);

        //}

  //End FillTable Function


        public DataTable GetDataTableNoComponentDescriptive()
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            string Qry = "SELECT NoEntity as 'No',PersianName as 'Name' FROM [Component]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityClassDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityClass]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableDescriptive()
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            string Qry = "SELECT NoComponent as 'No',dllName as 'Name' FROM [View]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableNoEntityTypeDescriptive()
        {
            Entity.Entity DAslct = new Entity.Entity(StrCon1);
            string Qry = "SELECT No as 'No',Name as 'Name' FROM [EntityType]";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
  //End FillComboBYTable


        public DataTable GetDataTableIdTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 162 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdFormTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 97 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdActionTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 98 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdManageTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 99 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdPermissionTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 100 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdSizeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 167 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            return DAC.GeneralSelectCommand(scmd_Class);
        }
        public DataTable GetDataTableIdComponentTypeDescriptive()
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 168 ORDER BY Num";
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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001122 + ")";

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



        public string GetAtt( int NoComponent , int NoField)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select [View]." + FieldName[0] + " From View Where [NoComponent] = " + NoComponent;
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

        public void SetAtt( int NoComponent , int NoField, string value)
        {

            string[] FieldName = GetFieldName(NoField);
            DataRow dr = new DataTable().NewRow();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "";

            if(FieldName[1] != "6")
                Str = "Update [View] SET [" + FieldName[0] + "] = '" + value + "' where [NoComponent] = " + NoComponent;
            else if (FieldName[1] == "6")
                Str = "Update [View] SET [" + FieldName[0] + "] = " + value + " where [NoComponent] = " + NoComponent;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public DataTable GetDataTableForGrid(string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoComponent] as 'PKNoComponent' , 'عنوان فارسي' = (SELECT PersianName FROM Component"+
                " WHERE TView.[NoComponent] = NoEntity),[NoEntityClass] as 'FKNoEntityClass', 'نام کلاس' = (SELECT [Name] FROM [EntityClass] WHERE TView.[NoEntityClass] = [No]), [dllName] as 'نام دي ال ال ' , [persianName] as 'نام فارسي' , [IdType] as 'نوع' , [IdFormType] as 'نوع فرم' , [IdActionType] as 'نوع عمليات' , [IdManageType] as 'نوع مديريت' , [IdPermissionType] as 'نوع دسترسي ها' , [General] as 'عمومي' , [Description] as 'توصيفات' , [IsActive] as 'فعال است؟' ,[NoEntityType] as 'FKNoEntityType', 'نام موجوديت' = (SELECT [Name] FROM [EntityType] WHERE TView.[NoEntityType] = [No]), [IdSize] as 'اندازه' , [IdComponentType] as 'نوع قطعه' , [TabCount] as 'تعداد زبانه' , [HasMasterTab] as 'داراي تب اصلي'  From [View] AS TView Where TView." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            return DAC.GeneralSelectCommand(cmd);
        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001122) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001122) " + 
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


        public strcView GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsView.strcView myStruct = new clsView.strcView();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From [View] Where [View]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            myStruct.NoComponent = (int)dt.Rows[0]["NoComponent"];
            myStruct.NoEntityClass = (int)dt.Rows[0]["NoEntityClass"];
            myStruct.dllName = (string)dt.Rows[0]["dllName"];
            myStruct.persianName = (string)dt.Rows[0]["persianName"];
            myStruct.IdType = (int)dt.Rows[0]["IdType"];
            myStruct.IdFormType = (short)dt.Rows[0]["IdFormType"];
            myStruct.IdActionType = (short)dt.Rows[0]["IdActionType"];
            myStruct.IdManageType = (short)dt.Rows[0]["IdManageType"];
            myStruct.IdPermissionType = (short)dt.Rows[0]["IdPermissionType"];
            myStruct.General = (string)dt.Rows[0]["General"];
            myStruct.Description = (string)dt.Rows[0]["Description"];
            myStruct.IsActive = (bool)dt.Rows[0]["IsActive"];
            myStruct.NoEntityType = (int)dt.Rows[0]["NoEntityType"];
            myStruct.IdSize = (short)dt.Rows[0]["IdSize"];
            myStruct.IdComponentType = (short)dt.Rows[0]["IdComponentType"];
            myStruct.TabCount = (int)dt.Rows[0]["TabCount"];
            myStruct.HasMasterTab = (bool)dt.Rows[0]["HasMasterTab"];
			myStruct.HasWizardButton = (bool)dt.Rows[0]["HasWizardButton"];

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