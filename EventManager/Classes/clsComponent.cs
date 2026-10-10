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
    public partial class clsComponent
    {
        string StrCon1;

        public struct strcComponent
        {
            public int NoEntity;
            public string Name;
            public string PersianName;
            public int topX;
            public int topY;
            public int bottomX;
            public int bottomY;
            public int NoComponentType;
            public string icon;
            public int NoParent;
            public string DefaultValue;
            public short IdViewType;
            public int NoSkin;
            public int NoWebpartDisplay;
            public string Text;
            public string ToolTip;
            public int NoPortal;
            public short IdOrderType;
			public short Mode;

            public strcComponent(short INTinit)
            {
                NoEntity = INTinit;
                Name = "";
                PersianName = "";
                icon = "";
                bottomX = INTinit;
                bottomY = INTinit;
                NoComponentType = INTinit;
                IdViewType = INTinit;
                topX = INTinit;
                topY = INTinit;
                NoSkin = INTinit;
                NoWebpartDisplay = INTinit;
                Text = "";
                ToolTip = "";
                NoParent = INTinit;
                NoPortal = INTinit;
                IdOrderType = INTinit;
                DefaultValue = "";
				Mode = INTinit;
            } 

        } //End Struct

        public clsComponent()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public strcComponent GetNewRow()
        {
            strcComponent objstrcComponent = new strcComponent();
            return objstrcComponent;

        }//End GetNewRow Function


        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance DELETE : Component --> DeskTop --> View --> 
        /// </summary>
        public int Delete(strcComponent myStruct)
        {
            if(CheckDelete(myStruct.NoEntity))
            {

                Component.Component DAdlt = new Component.Component(StrCon1);
                Component.Component_DS.DS_ComponentRow dtRow = new Component.Component_DS.DS_ComponentDataTable().NewDS_ComponentRow();

                if(myStruct.NoEntity != null)
                    dtRow.NoEntity = myStruct.NoEntity;

                DAdlt.DS_ComponentDataTable_DeleteCommand_SP(dtRow);

            }

            return 0 ;

        }



        public int Delete(string FieldName,string Value )
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "DELETE From Component where " + FieldName + " = " + Value ;
            DAC.GeneralDeleteCommand(new SqlCommand(Str));

            return 0;

        }  //End Delete Function

        //Make Main Body
        /// <summary>
        /// This function has inherited from these tables for Inheritance INSERT: Component --> 
        /// </summary>
        public int Insert( clsComponent.strcComponent myStructComponent )
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
			Dic.Add("Component_Mode", myStructComponent.Mode);

            return DAinsrt.DS_ComponentDataTable_InsertCommand_SP(Dic);



        }  //End Insert Function


        public int Update(strcComponent myStruct)
        {
            Component.Component DAupdt = new Component.Component(StrCon1);
            Component.Component_DS.DS_ComponentRow dtRow = new Component.Component_DS.DS_ComponentDataTable().NewDS_ComponentRow();

                if(myStruct.NoEntity != null)
                    dtRow.NoEntity = myStruct.NoEntity;
                if(myStruct.Name != null)
                    dtRow.Name = myStruct.Name;
                if(myStruct.PersianName != null)
                    dtRow.PersianName = myStruct.PersianName;
                if(myStruct.topX != null)
                    dtRow.topX = myStruct.topX;
                if(myStruct.topY != null)
                    dtRow.topY = myStruct.topY;
                if(myStruct.bottomX != null)
                    dtRow.bottomX = myStruct.bottomX;
                if(myStruct.bottomY != null)
                    dtRow.bottomY = myStruct.bottomY;
                if(myStruct.NoComponentType != null)
                    dtRow.NoComponentType = myStruct.NoComponentType;
                if(myStruct.icon != null)
                    dtRow.icon = myStruct.icon;
                if(myStruct.NoParent != null)
                    dtRow.NoParent = myStruct.NoParent;
                if(myStruct.DefaultValue != null)
                    dtRow.DefaultValue = myStruct.DefaultValue;
                if(myStruct.IdViewType != null)
                    dtRow.IdViewType = myStruct.IdViewType;
                if(myStruct.NoSkin != null)
                    dtRow.NoSkin = myStruct.NoSkin;
                if(myStruct.NoWebpartDisplay != null)
                    dtRow.NoWebpartDisplay = myStruct.NoWebpartDisplay;
                if(myStruct.Text != null)
                    dtRow.Text = myStruct.Text;
                if(myStruct.ToolTip != null)
                    dtRow.ToolTip = myStruct.ToolTip;
                if(myStruct.NoPortal != null)
                    dtRow.NoPortal = myStruct.NoPortal;
                if(myStruct.IdOrderType != null)
                    dtRow.IdOrderType = myStruct.IdOrderType;
				dtRow.Mode = myStruct.Mode;
            DAupdt.DS_ComponentDataTable_UpdateCommand_SP(dtRow);

            return 0 ;

        }

        public int CustomUpdate(string FieldsSetString , string KeyValue)
        {
            string Qry ="Update [Component] SET " + FieldsSetString + " WHERE NoEntity = " + KeyValue;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            SqlCommand objCommand = new SqlCommand(Qry);
            GClass.GeneralUpdateCommand(objCommand);
            return 0;

        }  //End Update Function


        public DataTable Select(string FieldName, string Value)
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From Component Where " + FieldName + " = " + Value;
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public DataTable Select()
        {

            DataTable res;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Str = "SELECT * From Component";
            res = DAC.GeneralSelectCommand(new SqlCommand(Str));
            return res;
        }

        public strcComponent SelectByNo(strcComponent myStruct)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            //Component.Component_DS.DS_ComponentRow dtRow = new Component.Component_DS.DS_ComponentDataTable().NewDS_ComponentRow();

            Component.Component_DS.DS_ComponentDataTable dt = new Component.Component_DS.DS_ComponentDataTable();

            dt = DAslct.DS_ComponentDataTable_SelectCommand(" WHERE NoEntity = " + myStruct.NoEntity);

            if(dt.Rows.Count == 0 )
            {
                myStruct.NoEntity = 0;
                myStruct.Name = "0";
                myStruct.PersianName = "0";
                myStruct.topX = 0;
                myStruct.topY = 0;
                myStruct.bottomX = 0;
                myStruct.bottomY = 0;
                myStruct.NoComponentType = 0;
                myStruct.icon = "0";
                myStruct.NoParent = 0;
                myStruct.DefaultValue = "0";
                myStruct.NoSkin = 0;
                myStruct.NoWebpartDisplay = 0;
                myStruct.Text = "0";
                myStruct.ToolTip = "0";
                myStruct.NoPortal = 0;

                return myStruct ;
            }
            if(dt.Rows[0]["NoEntity"] != System.DBNull.Value)
                myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            if(dt.Rows[0]["Name"] != System.DBNull.Value)
                myStruct.Name = (string)dt.Rows[0]["Name"];
            if(dt.Rows[0]["PersianName"] != System.DBNull.Value)
                myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            if(dt.Rows[0]["topX"] != System.DBNull.Value)
                myStruct.topX = (int)dt.Rows[0]["topX"];
            if(dt.Rows[0]["topY"] != System.DBNull.Value)
                myStruct.topY = (int)dt.Rows[0]["topY"];
            if(dt.Rows[0]["bottomX"] != System.DBNull.Value)
                myStruct.bottomX = (int)dt.Rows[0]["bottomX"];
            if(dt.Rows[0]["bottomY"] != System.DBNull.Value)
                myStruct.bottomY = (int)dt.Rows[0]["bottomY"];
            if(dt.Rows[0]["NoComponentType"] != System.DBNull.Value)
                myStruct.NoComponentType = (int)dt.Rows[0]["NoComponentType"];
            if(dt.Rows[0]["icon"] != System.DBNull.Value)
                myStruct.icon = (string)dt.Rows[0]["icon"];
            if(dt.Rows[0]["NoParent"] != System.DBNull.Value)
                myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            if(dt.Rows[0]["DefaultValue"] != System.DBNull.Value)
                myStruct.DefaultValue = (string)dt.Rows[0]["DefaultValue"];
            if(dt.Rows[0]["IdViewType"] != System.DBNull.Value)
                myStruct.IdViewType = (short)dt.Rows[0]["IdViewType"];
            if(dt.Rows[0]["NoSkin"] != System.DBNull.Value)
                myStruct.NoSkin = (int)dt.Rows[0]["NoSkin"];
            if(dt.Rows[0]["NoWebpartDisplay"] != System.DBNull.Value)
                myStruct.NoWebpartDisplay = (int)dt.Rows[0]["NoWebpartDisplay"];
            if(dt.Rows[0]["Text"] != System.DBNull.Value)
                myStruct.Text = (string)dt.Rows[0]["Text"];
            if(dt.Rows[0]["ToolTip"] != System.DBNull.Value)
                myStruct.ToolTip = (string)dt.Rows[0]["ToolTip"];
            if(dt.Rows[0]["NoPortal"] != System.DBNull.Value)
                myStruct.NoPortal = (int)dt.Rows[0]["NoPortal"];
            if(dt.Rows[0]["IdOrderType"] != System.DBNull.Value)
                myStruct.IdOrderType = (short)dt.Rows[0]["IdOrderType"];
			if (dt.Rows[0]["Mode"] != System.DBNull.Value)
				myStruct.Mode = (short)dt.Rows[0]["Mode"];
            return myStruct ;

        } // End SelectByNo


        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid)
        {
            string Qry1 = "SELECT  [NoEntity] , [Name] as 'Component.Name' , [PersianName] as 'Component.PersianName' , [topX] as 'Component.topX' , [topY] as 'Component.topY' , [bottomX] as 'Component.bottomX' , [bottomY] as 'Component.bottomY' , 'ComponentType.Name' = (SELECT [Name] FROM [ComponentType] WHERE TComponent.[NoComponentType] = [No]), [icon] as 'Component.icon' , 'Component.PersianName' = (SELECT [PersianName] FROM [Component] WHERE TComponent.[NoParent] = [NoEntity]), [DefaultValue] as 'Component.DefaultValue' , [IdViewType] as 'Component.IdViewType' , [NoSkin] as 'Component.NoSkin' , [NoWebpartDisplay] as 'Component.NoWebpartDisplay' , [Text] as 'Component.Text' , [ToolTip] as 'Component.ToolTip' , [NoPortal] as 'Component.NoPortal' , [IdOrderType] as 'Component.IdOrderType'  FROM Component AS TComponent";

            String FiledPersianName = "نام انگليسي,عنوان فارسي,فاصله از چپ بالا,فاصله از بالا,فاصله از راست ,فاصله از پايين,عنوان,تصوير,عنوان فارسي,مقدار پيش فرض,انواع ديد,شناسه پوسته,شناسه نمايش وب پارت,متن,نوار ابزار,شناسه پرتال,نوع دستور";
            String FiledEnglishName = "Component.Name,Component.PersianName,Component.topX,Component.topY,Component.bottomX,Component.bottomY,ComponentType.Name,Component.icon,Component.PersianName,Component.DefaultValue,Component.IdViewType,Component.NoSkin,Component.NoWebpartDisplay,Component.Text,Component.ToolTip,Component.NoPortal,Component.IdOrderType";

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
                string Qry1 = "SELECT  [NoEntity] , [Name] as 'Component.Name' , [PersianName] as 'Component.PersianName' , [topX] as 'Component.topX' , [topY] as 'Component.topY' , [bottomX] as 'Component.bottomX' , [bottomY] as 'Component.bottomY' , 'ComponentType.Name' = (SELECT [Name] FROM [ComponentType] WHERE TComponent.[NoComponentType] = [No]), [icon] as 'Component.icon' , 'Component.PersianName' = (SELECT [PersianName] FROM [Component] WHERE TComponent.[NoParent] = [NoEntity]), [DefaultValue] as 'Component.DefaultValue' , [IdViewType] as 'Component.IdViewType' , [NoSkin] as 'Component.NoSkin' , [NoWebpartDisplay] as 'Component.NoWebpartDisplay' , [Text] as 'Component.Text' , [ToolTip] as 'Component.ToolTip' , [NoPortal] as 'Component.NoPortal' , [IdOrderType] as 'Component.IdOrderType'  FROM Component AS TComponent";

                String FiledPersianName = "نام انگليسي,عنوان فارسي,فاصله از چپ بالا,فاصله از بالا,فاصله از راست ,فاصله از پايين,عنوان,تصوير,عنوان فارسي,مقدار پيش فرض,انواع ديد,شناسه پوسته,شناسه نمايش وب پارت,متن,نوار ابزار,شناسه پرتال,نوع دستور";
                String FiledEnglishName = "Component.Name,Component.PersianName,Component.topX,Component.topY,Component.bottomX,Component.bottomY,ComponentType.Name,Component.icon,Component.PersianName,Component.DefaultValue,Component.IdViewType,Component.NoSkin,Component.NoWebpartDisplay,Component.Text,Component.ToolTip,Component.NoPortal,Component.IdOrderType";

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
        /// This Function Will Fill Component Table Into DataGrid By NoComponentType Field
        /// </summary>
        /// <param name="tblName"></param>"
        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid , clsComponentType.strcComponentType myStruct )
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            myGrid.DataSource = DAslct.DS_ComponentDataTable_SelectCommand(" WHERE NoComponentType = " + myStruct.No);

            

            return 0 ;

        }

        /// <summary>
        /// This Function Will Fill Component Table Into DataGrid By NoParent Field
        /// </summary>
        /// <param name="tblName"></param>"
        public int FillThisDataGridView( System.Windows.Forms.DataGridView myGrid , clsComponent.strcComponent myStruct )
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            myGrid.DataSource = DAslct.DS_ComponentDataTable_SelectCommand(" WHERE NoParent = " + myStruct.NoEntity);

            

            return 0 ;

        }

  //End FillTable Function


        public int FillNoComponentTypeIntoComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            myComboBox.DataSource = DAslct.DS_ComponentTypeDataTable_SelectCommand_SP();
            myComboBox.DisplayMember = "Name";
            myComboBox.ValueMember = "No";
            

            return 0 ;

        }
        public int FillNoParentIntoComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            Component.Component DAslct = new Component.Component(StrCon1);
            myComboBox.DataSource = DAslct.DS_ComponentDataTable_SelectCommand_SP();
            myComboBox.DisplayMember = "PersianName";
            myComboBox.ValueMember = "NoEntity";
            

            return 0 ;

        }
  //End FillComboBYTable


        public int FillIdViewTypeComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 53 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            myComboBox.DataSource = GClass.GeneralSelectCommand(scmd_Class);
            myComboBox.DisplayMember = "Name";
            myComboBox.ValueMember = "No";
            

            return 0 ;

        }        
		public int FillIdOrderTypeComboBox( System.Windows.Forms.ComboBox myComboBox)
        {
            string Qry = "SELECT No, Name FROM LookUp WHERE NoLookUpType = 135 ORDER BY Num";
            SqlCommand scmd_Class = null;
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            scmd_Class = new SqlCommand(Qry);
            myComboBox.DataSource = GClass.GeneralSelectCommand(scmd_Class);
            myComboBox.DisplayMember = "Name";
            myComboBox.ValueMember = "No";
            

            return 0 ;

        }  //End FillComboBYLookUp



        public void NoParent_SetTree(int NoParent, ref TreeView treeView)
        {
            treeView.Nodes.Clear();
            ArrayList arrNo = new ArrayList();
            ArrayList arrName = new ArrayList();
            arrNo = GetArrayValues(NoParent ,"NoParent" , "No");
            arrName = GetArrayValues(NoParent ,"NoParent" , "Name");
            for (int i = 0; i < arrNo.Count; i++)
            {
                TreeNode Node = new TreeNode(arrName[i].ToString());
                treeView.Nodes.Add(Node);
                Node.Tag = arrNo[i].ToString();
                    SetTree(Convert.ToInt32(arrNo[i]),ref Node ,"NoParent");
            }
        }
        private void SetTree(int NoParent, ref TreeNode mainNode , string FieldName)
        {
            ArrayList arrNo = new ArrayList();
            ArrayList arrName = new ArrayList();
            arrNo = GetArrayValues(NoParent , FieldName , "No");
            arrName = GetArrayValues(NoParent , FieldName  , "Name");
            for (int i = 0; i < arrNo.Count; i++)
            {
                TreeNode Node = new TreeNode(arrName[i].ToString());
                mainNode.Nodes.Add(Node);
                Node.Tag = arrNo[i].ToString();
                    SetTree(Convert.ToInt32(arrNo[i]) , ref Node , FieldName);
            }
        }

        private ArrayList GetArrayValues(int NoParent,string FieldName,string Mode)
        {
            ArrayList arr = new ArrayList();
            if(Mode == "No")
            {
                string Query = "select Component.NoEntity from Component where Component." + FieldName + " = "+NoParent ;
                SqlCommand scmd_Class = null;
                GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                scmd_Class = new SqlCommand(Query);
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1 = GClass.GeneralSelectCommand(scmd_Class);
                if(dt1 != null)
                {
                    for(int i = 0 ; i < dt1.Rows.Count ; i++)
                    {
                        arr.Add(dt1.Rows[i][0].ToString());
                    }
                }
            }
            if(Mode == "Name")
            {
                string Query = "select Component.PersianName from Component where Component." + FieldName + " = "+NoParent ;
                SqlCommand scmd_Class = null;
                GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                scmd_Class = new SqlCommand(Query);
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1 = GClass.GeneralSelectCommand(scmd_Class);
                if(dt1 != null)
                {
                    for(int i = 0 ; i < dt1.Rows.Count ; i++)
                    {
                        arr.Add(dt1.Rows[i][0].ToString());
                    }
                }
            }
            return arr;
        }




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

            string Query = "Select [NO],[Fill] From Fields where TableNo = dbo.FuncEncryptTableNo(" + 1001123 + ")";

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

            string Str = "Select [Component]." + FieldName[0] + " From Component Where [NoEntity] = " + NoEntity;
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
                Str = "Update [Component] SET [" + FieldName[0] + "] = '" + value + "' where [NoEntity] = " + NoEntity;
            else if (FieldName[1] == "6")
                Str = "Update [Component] SET [" + FieldName[0] + "] = " + value + " where [NoEntity] = " + NoEntity;
            SqlCommand cmd = new SqlCommand(Str);

            DAC.GeneralUpdateCommand(cmd);

        }


        public int FillThisDataGridView(System.Windows.Forms.DataGridView myGrid ,string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select  [NoEntity] , [Name] as 'Component.Name' , [PersianName] as 'Component.PersianName' , [topX] as 'Component.topX' , [topY] as 'Component.topY' , [bottomX] as 'Component.bottomX' , [bottomY] as 'Component.bottomY' , 'ComponentType.Name' = (SELECT [Name] FROM [ComponentType] WHERE TComponent.[NoComponentType] = [No]), [icon] as 'Component.icon' , 'Component.PersianName' = (SELECT [PersianName] FROM [Component] WHERE TComponent.[NoParent] = [NoEntity]), [DefaultValue] as 'Component.DefaultValue' , [IdViewType] as 'Component.IdViewType' , [NoSkin] as 'Component.NoSkin' , [NoWebpartDisplay] as 'Component.NoWebpartDisplay' , [Text] as 'Component.Text' , [ToolTip] as 'Component.ToolTip' , [NoPortal] as 'Component.NoPortal' , [IdOrderType] as 'Component.IdOrderType'  From Component Where [Component]." + FieldName + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            myGrid.DataSource = DAC.GeneralSelectCommand(cmd);
            

            return 0 ;

        }






        private bool CheckDelete(int No)
        {

            string str = "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.DestTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.DestFieldNo = Fields.NO " + 
                "where SrcTableNo = dbo.FuncEncryptTableNo(1001123) " + 
                "and RelTypeNo in (0,2) " + 
                "Union " + 
                "Select Tables.Name as TableName , Fields.Name as FieldName From ((Relations join Tables on Relations.SrcTableNo = dbo.FuncEncryptTableNo(Tables.NO)) join RelationItem on  RelationItem.RelationNo = Relations.NO) join Fields on RelationItem.SrcFieldNo = Fields.NO " + 
                "Where DestTableNo = dbo.FuncEncryptTableNo(1001123) " + 
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


        public strcComponent GetUnique(int NoField , string Value)
        {

            string[] FieldName = GetFieldName(NoField);
            clsComponent.strcComponent myStruct = new clsComponent.strcComponent();

            DataTable dt = new DataTable();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Str = "Select * From Component Where [Component]." + FieldName[0] + " = " +  Value ;
            SqlCommand cmd = new SqlCommand(Str);

            dt = DAC.GeneralSelectCommand(cmd);
            if(dt.Rows[0]["NoEntity"] != System.DBNull.Value)
                myStruct.NoEntity = (int)dt.Rows[0]["NoEntity"];
            if(dt.Rows[0]["Name"] != System.DBNull.Value)
                myStruct.Name = (string)dt.Rows[0]["Name"];
            if(dt.Rows[0]["PersianName"] != System.DBNull.Value)
                myStruct.PersianName = (string)dt.Rows[0]["PersianName"];
            if(dt.Rows[0]["topX"] != System.DBNull.Value)
                myStruct.topX = (int)dt.Rows[0]["topX"];
            if(dt.Rows[0]["topY"] != System.DBNull.Value)
                myStruct.topY = (int)dt.Rows[0]["topY"];
            if(dt.Rows[0]["bottomX"] != System.DBNull.Value)
                myStruct.bottomX = (int)dt.Rows[0]["bottomX"];
            if(dt.Rows[0]["bottomY"] != System.DBNull.Value)
                myStruct.bottomY = (int)dt.Rows[0]["bottomY"];
            if(dt.Rows[0]["NoComponentType"] != System.DBNull.Value)
                myStruct.NoComponentType = (int)dt.Rows[0]["NoComponentType"];
            if(dt.Rows[0]["icon"] != System.DBNull.Value)
                myStruct.icon = (string)dt.Rows[0]["icon"];
            if(dt.Rows[0]["NoParent"] != System.DBNull.Value)
                myStruct.NoParent = (int)dt.Rows[0]["NoParent"];
            if(dt.Rows[0]["DefaultValue"] != System.DBNull.Value)
                myStruct.DefaultValue = (string)dt.Rows[0]["DefaultValue"];
            if(dt.Rows[0]["IdViewType"] != System.DBNull.Value)
                myStruct.IdViewType = (short)dt.Rows[0]["IdViewType"];
            if(dt.Rows[0]["NoSkin"] != System.DBNull.Value)
                myStruct.NoSkin = (int)dt.Rows[0]["NoSkin"];
            if(dt.Rows[0]["NoWebpartDisplay"] != System.DBNull.Value)
                myStruct.NoWebpartDisplay = (int)dt.Rows[0]["NoWebpartDisplay"];
            if(dt.Rows[0]["Text"] != System.DBNull.Value)
                myStruct.Text = (string)dt.Rows[0]["Text"];
            if(dt.Rows[0]["ToolTip"] != System.DBNull.Value)
                myStruct.ToolTip = (string)dt.Rows[0]["ToolTip"];
            if(dt.Rows[0]["NoPortal"] != System.DBNull.Value)
                myStruct.NoPortal = (int)dt.Rows[0]["NoPortal"];
            if(dt.Rows[0]["IdOrderType"] != System.DBNull.Value)
                myStruct.IdOrderType = (short)dt.Rows[0]["IdOrderType"];


            return myStruct;

        }




        public int FillThisListBox(System.Windows.Forms.ListBox myListBox ,string FieldName , string Value)
        {

            //string[] FieldName = GetFieldName(NoField);
            Component.Component DAslct = new Component.Component(StrCon1);
            Component.Component_DS.DS_ComponentDataTable dt = new Component.Component_DS.DS_ComponentDataTable();

            dt = DAslct.DS_ComponentDataTable_SelectCommand(" WHERE " + FieldName + " = " + Value );

            myListBox.DataSource = dt;
            myListBox.DisplayMember = "PersianName";
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