using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Behin
{
    public class clsPublic
    {
        public int NoEmployee = 28;
        public ArrayList ComboArray = new ArrayList();
      //  private BEDA clDA;// = new BEDA();
        public DataSet  dsCombo;
        public SqlDataAdapter sdCombo;
        public SqlConnection cn;
        public DataTable dtCombo;
        
        DataSet objDataSet;
        SqlCommand objCommand;
        SqlDataAdapter objDataAdapter;
        public static int CurentAgent;
        public static int CurentActor;
        public static int CurentOU;
        public class ComboItem
        {
            
            private int no;
            public int No
            {
                get { return no; }
                set { no = value; }
            }

            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public ComboItem SetProperties(int index, string strName)
            {
                ComboItem obj = new ComboItem();
                obj.No = index;
                obj.Name = strName;
                return obj;
            }
        }

        #region SetCheckedBoxItem

        public class CheckItem
        {
            private int no;

            public int No
            {
                get { return no; }
                set { no = value; }
            }

            private int checkbox;

            public int Checkbox
            {
                get { return checkbox; }
                set { checkbox = value; }
            }
        }

        public CheckItem GetChecked(int index, int value)
        {
            CheckItem obj = new CheckItem();
            obj.No = index;
            obj.Checkbox = value;
            return obj;
        }

        #endregion

        #region Mode

        public class Mode
        {
            public enum Type { New, Edit, Cancel };
        }

        #endregion

        #region ShowTreeView

        //public TreeNode ShowTreeView(string tableName, string root, int NodeNo, ref TreeNode mainNode)
        //{
        //    ArrayList arrNo = new ArrayList();
        //    ArrayList arrName = new ArrayList();
        //    ArrayList arrNoParent = new ArrayList();
        //    Boolean haveChild = false;
        //    clDA = new BEDA();

        //    arrNo = clDA.GetNoWithNoParent(tableName, NodeNo);
        //    arrName = clDA.GetNameWithNoParent(tableName, NodeNo);

        //    for (int i = 0; i < arrNo.Count; i++)
        //    {
        //        TreeNode Node = new TreeNode(arrName[i].ToString());
        //        mainNode.Nodes.Add(Node);
        //        haveChild = clDA.GetNoParent(tableName, Convert.ToInt32(arrNo[i]));
        //        if (haveChild == true)
        //        {
        //            ShowTreeView(tableName, arrName[i].ToString(), Convert.ToInt32(arrNo[i]), ref Node);
        //        }
        //    }
        //    return mainNode;
        //}

        #endregion

        #region SetCombo
        public DateTime PersianDateToMiladi(string Y, string M, string D)
        {
            return new System.Globalization.PersianCalendar().ToDateTime(int.Parse(Y), int.Parse(M), int.Parse(D), 0, 0, 0, 0);
        }

        public DateTime PersianDateToMiladi(int Y, int M, int D)
        {
            return new System.Globalization.PersianCalendar().ToDateTime(Y, M, D, 0, 0, 0, 0);
        }

        public DateTime TimeToDateTime(string Hour, string Minute)
        {
            return new DateTime(1, 1, 1, int.Parse(Hour), int.Parse(Minute), 0);
        }

        public DateTime TimeToDateTime(int Hour, int Minute)
        {
            return new DateTime(1, 1, 1, Hour, Minute, 0);
        }

        public DateTime PersianDateToMiladi(string PersDate)
        {
            
            string[] AllParts = PersDate.Split('/');

            int A, B, C;

            if (AllParts[0].Length > 2)
            {
                A = int.Parse(AllParts[0]);
                B = int.Parse(AllParts[1]);
                C = int.Parse(AllParts[2]);
            }
            else
            {
                A = int.Parse(AllParts[2]);
                B = int.Parse(AllParts[1]);
                C = int.Parse(AllParts[0]);
            }

            return new System.Globalization.PersianCalendar().ToDateTime(A, B, C, 0, 0, 0, 0);
        }

        public DateTime TimeToDateTime(string strTime)
        {
            string[] AllParts = strTime.Split(':');

            int A, B;

            A = int.Parse(AllParts[0]);
            B = int.Parse(AllParts[1]);

            return new System.Globalization.PersianCalendar().ToDateTime(1, 1, 1, A, B, 0, 0);
        }

        public string DateToString(DateTime myDT)
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();

            string res = PC.GetYear(myDT).ToString("0000") + "/" + PC.GetMonth(myDT).ToString("00") + "/" + PC.GetDayOfMonth(myDT).ToString("00");

            return res;
        }

        public string TimeToString(DateTime myDT)
        {
            string res = myDT.Hour.ToString("00") + ":" + myDT.Minute.ToString("00");

            return res;
        }
        //public void SetCombo(ComboBox cmb, string tableName, string condition)
        //{
        //    clDA = new BEDA();
        //    objDataSet = new DataSet();
        //    string commText = "SELECT Name, No FROM " + tableName + " " + condition;
        //    clDA.conn.Open();
        //    objDataAdapter = new SqlDataAdapter(commText, clDA.conn);
        //    objDataAdapter.Fill(objDataSet, "table");
        //    clDA.conn.Close();

        //    cmb.DataSource = objDataSet;
        //    cmb.DisplayMember = "table.Name";
        //    cmb.ValueMember = "table.No";
        //}

        //public void SetCombo(ComboBox cmb, string Qry)
        //{
        //    clDA = new BEDA();
        //    objDataSet = new DataSet();
        //    string commText = Qry;
        //    clDA.conn.Open();
        //    objDataAdapter = new SqlDataAdapter(commText, clDA.conn);
        //    objDataAdapter.Fill(objDataSet, "table");
        //    clDA.conn.Close();

        //    cmb.DataSource = objDataSet;
        //    cmb.DisplayMember = "table.Name";
        //    cmb.ValueMember = "table.No";
        //}

        #region FillCombo

        public SqlDataAdapter FillData(String Qry, SqlConnection Cnn)
        {
            dtCombo = new DataTable();
            //comMasterEnity = new SqlCommand(Qry , Cnn);
            Cnn.Open();
            sdCombo = new SqlDataAdapter(Qry, Cnn);
            Cnn.Close();
            return sdCombo;
        }

        //public void SetCombo(String SQry, String IdItem, String TextItem, String TableItem,
        //                      System.Windows.Forms.ComboBox cmb)
        //{
        //    clDA = new BEDA();
        //    dsCombo = new DataSet();
        //    sdCombo = new SqlDataAdapter();
        //    sdCombo = FillData(SQry, clDA.conn);
        //    sdCombo.Fill(dsCombo, TableItem);
        //    try
        //    {
        //        cmb.DataSource = dsCombo.Tables[TableItem];
        //        cmb.DisplayMember = TextItem;
        //        cmb.ValueMember = IdItem;
        //    }
        //    catch
        //    {
        //    }
        //    //sdEntity.Dispose();
        //}

        #endregion
        
        #endregion 

        #region SetCheckedListBox

        

        //public void SetToolsCheckedListBox(CheckedListBox lst)
        //{
        //    clDA = new BEDA();
        //    string Qry = "select name, no from lookup where nolookuptype = 20 order by name";
        //    clDA.SetCheckListBox(lst, Qry);
        //}

  #endregion

        //public void SetListBox(ListBox lst, string Qry)
        //{
        //    objDataSet = new DataSet();
        //    string commText = Qry;

        //    clDA = new BEDA();
        //    clDA.conn.Open();
        //    objDataAdapter = new SqlDataAdapter(commText, clDA.conn);
        //    objDataAdapter.Fill(objDataSet, "table");
        //    clDA.conn.Close();

        //    lst.DataSource = objDataSet;
        //    lst.DisplayMember = "table.Name";
        //    lst.ValueMember = "table.No";
        //}

      

        //public Boolean PermissionForValidNo(string Qry)
        //{
        //    clDA = new BEDA();
        //    Boolean flag;
        //    objCommand = new SqlCommand(Qry, clDA.conn);
        //    clDA.conn.Open();
        //    SqlDataReader read = objCommand.ExecuteReader();
        //    if (read.HasRows)
        //    {
        //        flag = true;
        //        clDA.conn.Close();
        //        return flag;
        //    }
        //    else
        //    {
        //        clDA.conn.Close();
        //        flag = false;
        //    }
        //    return flag;
        //}

        //public int GetNo(string Qry)
        //{
        //    clDA = new BEDA();
        //    int No;
        //    objCommand = new SqlCommand(Qry, clDA.conn);
        //    clDA.conn.Open();
        //    No = Convert.ToInt32(objCommand.ExecuteScalar());
        //    clDA.conn.Close();
        //    return No;
        //}

        //public DataTable SetGrid(string Qry)
        //{
        //    SqlCommand scmd_Class = null;
        //    GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass
        //                                                    ((new BEDA()).conn.ConnectionString);
        //    scmd_Class = new SqlCommand(Qry);
        //    return GClass.GeneralSelectCommand(scmd_Class);
        //}
        
        #region SetCurrentCombo

        
        

       
       
 
        //public void SetLookUpCombo(ComboBox cmb, int NoLookUpType)
        //{
        //    clDA = new BEDA();
        //    string Qry = "Select No, Name From LookUp where nolookuptype = " + NoLookUpType + " order by name";
        //    clDA.SetCombo(cmb, Qry);
        //}

       

       
        #endregion

        #region SetImage

        public void SetImage(Button btn, string ImageName)
        {
            //btn.Image = Image.FromFile(Application.StartupPath + @"\Icons\" + ImageName + ".Ico");
            btn.Image = Image.FromFile(Application.StartupPath + @"\Icons\" + ImageName);
            if (btn.RightToLeft.ToString().ToLower() == "yes")
                btn.ImageAlign = ContentAlignment.MiddleRight;
            else
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                      
        }

        #endregion

        public int FuncEncryptTableNo(int tableNo)
        {
            int sumTable;
            int divTable;
            int modTable;

            sumTable = tableNo + 964;
            divTable = sumTable / 256;
            modTable = sumTable % 256;

            return ((divTable * 1000) + modTable);
        }

        public int FuncDecryptTableNo(int tableNo)
        {
            int divTable;
            int modTable;

            divTable = tableNo / 1000;
            modTable = tableNo % 1000;
            divTable = divTable * 256;

            divTable = divTable + modTable;
            return divTable - 964;
        }

    }
}
