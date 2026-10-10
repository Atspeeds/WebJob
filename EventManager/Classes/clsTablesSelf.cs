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
using Telerik.WinControls.UI;

namespace Behin
{
    public class clsTablesSelf
    {
        string StrCon1;

        public clsTablesSelf()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");

        }  //End Behin Function

        public void SetTreeOfEntity(System.Windows.Forms.TreeView myTreeView)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            
            string Query =(" SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables WHERE No IN (Select dbo.FuncDecryptTableNo( NoTable) From EntityClass where IsMain=1) order by name ");
            DataTable datTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

            System.Windows.Forms.TreeNode myNode;
            System.Windows.Forms.TreeNode Root = new System.Windows.Forms.TreeNode();
            Root.Text = "موجودیت";
            Root.Name = "0";
            myTreeView.Nodes.Add(Root);
            foreach (DataRow R in datTables.Rows)
            {
                string No = R["No"].ToString();
                string Name = R["Name"].ToString();
                myNode = new System.Windows.Forms.TreeNode();
                myNode.Text = Name;
                myNode.Name = No;
                SetNodeChild(myNode);
                Root.Nodes.Add(myNode);
            }
            if (Root.Nodes.Count > 0)
                Root.Expand();
        }
        public void SetTreeOfEntity(Telerik.WinControls.UI.RadTreeView myTreeView)
        {
			try
			{
				GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

				string Query = (" SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables WHERE No IN (Select dbo.FuncDecryptTableNo( NoTable) From EntityClass where IsMain=1) order by Name ");
				DataTable datTables = DAC.GeneralSelectCommand(new SqlCommand(Query));
				Telerik.WinControls.UI.RadTreeNode myNode;
				Telerik.WinControls.UI.RadTreeNode Root = new Telerik.WinControls.UI.RadTreeNode();
				Root.Text = "موجودیت";
				Root.Name = "0";
				myTreeView.Nodes.Add(Root);
				foreach (DataRow R in datTables.Rows)
				{
					string No = R["No"].ToString();
					string Name = R["Name"].ToString();
					myNode = new Telerik.WinControls.UI.RadTreeNode();
					myNode.Text = Name;
					myNode.Name = No;
					SetNodeChild(myNode);
					Root.Nodes.Add(myNode);
				}
				if (Root.Nodes.Count > 0)
					Root.Expand();
			}
			catch(Exception e)
			{
				MessageBox.Show("در ارتباط با سرور اشکال وجود دارد.");
			}
        }
        public void SetNodeChild(System.Windows.Forms.TreeNode ParentNode)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Query = " SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables WHERE No IN (Select dbo.FuncDecryptTableNo(DestTableNo) From Relations WHERE dbo.FuncDecryptTableNo(SrcTableNo) = " + ParentNode.Name + " AND RelTypeNo = 2)  order by Name";
            DataTable datTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

            System.Windows.Forms.TreeNode myNode;
            foreach (DataRow R in datTables.Rows)
            {
                string No = R["No"].ToString();
                string Name = R["Name"].ToString();
                myNode = new System.Windows.Forms.TreeNode();
                myNode.Text = Name;
                myNode.Name = No;
                SetNodeChild(myNode);
                ParentNode.Nodes.Add(myNode);
            }
        }
        public void SetNodeChild(RadTreeNode ParentNode)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            string Query = " SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables WHERE No IN (Select dbo.FuncDecryptTableNo(DestTableNo) From Relations WHERE dbo.FuncDecryptTableNo(SrcTableNo) = " + ParentNode.Name + " AND RelTypeNo = 2) order by Name ";
            DataTable datTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

            RadTreeNode myNode;
            foreach (DataRow R in datTables.Rows)
            {
                string No = R["No"].ToString();
                string Name = R["Name"].ToString();
                myNode = new RadTreeNode();
                myNode.Text = Name;
                myNode.Name = No;
                SetNodeChild(myNode);
                ParentNode.Nodes.Add(myNode);
            }
        }

        public void SetListBoxAllTables(System.Windows.Forms.ListBox myListBox)
        {
            string Query = "";
            Query = "SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables ORDER BY Tables.PersianName";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

            myListBox.DataSource = dataTables;
            myListBox.DisplayMember = "Name";
            myListBox.ValueMember = "No";
        }
        public void FillListBoxAllTables(RadListBox myListBox)
        {
            string Query = "";
            Query = "SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables ORDER BY Tables.PersianName";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

            myListBox.DataSource = dataTables;
            myListBox.DisplayMember = "Name";
            myListBox.ValueMember = "No";
        }
        public void FillComboBoxAllTables(RadComboBox myListBox)
        {
            string Query = "";
            Query = "SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables ORDER BY Tables.PersianName";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

            DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

            myListBox.DataSource = dataTables;
            myListBox.DisplayMember = "Name";
            myListBox.ValueMember = "No";
        }
		public void FillComboBoxAllTable(ComboBox myListBox)
		{
			string Query = "";
			Query = "SELECT No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' FROM Tables ORDER BY Tables.PersianName";
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

			myListBox.DataSource = dataTables;
			myListBox.DisplayMember = "Name";
			myListBox.ValueMember = "No";
		}
		public void FillComboBoxRelTable(ComboBox myListBox,int NoTable)
		{
			string Query = "";
			Query = " SELECT Tables.No, Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' " +
				    " FROM dbo.Relations INNER JOIN dbo.Tables ON dbo.Relations.DestTableNo = dbo.FuncEncryptTableNo(dbo.Tables.NO) " +
					" where srctableno = dbo.FuncEncryptTableNo(" + NoTable + ") and reltypeno = 0 ORDER BY Tables.PersianName";
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

			myListBox.DataSource = dataTables;
			myListBox.DisplayMember = "Name";
			myListBox.ValueMember = "No";
		}
        public string GetAllNosOfInherit(int NoTable)
        {
           string Res =NoTable.ToString();
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Qry = "SELECT dbo.FuncDecryptTableNo(SrcTableNo) FROM Relations WHERE dbo.FuncDecryptTableNo(DestTableNo) = " + NoTable + " AND RelTypeNo = 2 ";
            DataTable Data = DAC.GeneralSelectCommand(new SqlCommand(Qry));
            if (Data.Rows.Count > 0)
                Res += ", " + GetAllNosOfInherit(int.Parse(Data.Rows[0][0].ToString()));

            return Res;
        }

        public DataTable  LeftJoinTable(string TableNameSrc ,string TableNameDest , string PKTableSrc , string Where)
        {
            try
            {
            GeneralDataAccess.GeneralDataAccessClass DAC =new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Query =string.Format("Select {1}.* From {0} left Join {1} on {0}.{2} = {1}.No{0}  {3}" ,TableNameSrc ,TableNameDest ,PKTableSrc,Where);
            return DAC.GeneralSelectCommand(new SqlCommand(Query));
            }
            catch
            {
                return new DataTable(); 
            }
        }

		public void FillComboBoxEntityType(ComboBox myListBox)
		{
			string Query = "";
			Query = " SELECT NO,Name FROM EntityType ORDER BY Name ";
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));
			ExternalLibrary2.External objext = new ExternalLibrary2.External();
			objext.FillCombo(myListBox, dataTables);
			//myListBox.DataSource = dataTables;
			//myListBox.DisplayMember = "Name";
			//myListBox.ValueMember = "No";
		}

		public void FillComboEntityType(ComboBox myListBox)
		{
			string Query = "";
			Query = " select Tables.No ,Name = Tables.PersianName + '  ' + ' ( ' + Tables.Name + ' ) ' from entitytype,entityclass,Tables where entitytype.No = EntityClass.NoEntityType and " + 
                    " entityclass.NoTable = dbo.FuncEncryptTableNo(Tables.no) ORDER BY Name ";
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));

			ExternalLibrary2.External ext = new ExternalLibrary2.External();
			ext.FillCombo(myListBox, dataTables);
		}

		public int GetEntityTypeNo(int NoTable)
		{
			string Query = "";
			Query = " select EntityType.No from entitytype,entityclass,Tables where entitytype.No = EntityClass.NoEntityType and " +
					" entityclass.NoTable = dbo.FuncEncryptTableNo(Tables.no) and Tables.NO = " + NoTable;
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			DataTable dataTables = DAC.GeneralSelectCommand(new SqlCommand(Query));
			int NoEntityType = int.Parse(dataTables.Rows[0]["No"].ToString());
			return NoEntityType;
		}

    }  //End Class

}  //End namespace