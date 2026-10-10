using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behin;
using System.Data;
using System.Data.SqlClient;

namespace Behin
{
	class clsRuleConditionSelf
	{
		string StrCon1;

		public clsRuleConditionSelf()
		{
			Configuration config = new Configuration("app.conf");
			StrCon1 = config.GetConnectionString("BehinCnn");
		}

		public int FillRuleConditionIntoComboBox(System.Windows.Forms.ComboBox myComboBox, int EntityNo)
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Qry = " Select * from RuleCondition where NoRuleNode = " + EntityNo;
			DataTable Data = DAC.GeneralSelectCommand(new SqlCommand(Qry));

			myComboBox.DataSource = Data;
			myComboBox.DisplayMember = "Name";
			myComboBox.ValueMember = "No";

			return 0;
		}

		public int FillRuleConditionIntoListBox(System.Windows.Forms.ListBox myListBox , int EntityNo)
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Qry = " Select * from RuleCondition where NoRuleNode = " + EntityNo;
			DataTable Data = DAC.GeneralSelectCommand(new SqlCommand(Qry));

			myListBox.DataSource = Data;
			myListBox.DisplayMember = "Name";
			myListBox.ValueMember = "No";

			return 0;
		}

		public int Select(int No)
		{
			String Qry = "";
			Qry = " Select * From RuleCondition Where No = " + No;
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			DataTable Data = DAC.GeneralSelectCommand(new SqlCommand(Qry));

			if (Data.Rows.Count > 0)
				if (Convert.ToBoolean(Data.Rows[0]["IsMain"]) == true)
				{
					return 1;
				}
			return (0);
		}

		public int FillRuleIntoComboBox(System.Windows.Forms.ComboBox myComboBox)
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Qry = " Select Noentity as No ,Name from [Rule] order by name ";
			DataTable Data = DAC.GeneralSelectCommand(new SqlCommand(Qry));

			myComboBox.DataSource = Data;
			myComboBox.DisplayMember = "Name";
			myComboBox.ValueMember = "No";

			return 0;
		}

		public DataTable Select(string Value,int IsYes)
		{
			DataTable dt;
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Str = "SELECT * From [RuleNode] Where NoParent = " + Value + " and isyes = " + IsYes;
			dt = DAC.GeneralSelectCommand(new SqlCommand(Str));
			return dt;
		}

		public DataTable SelectRuleCondition(string FieldName, string Value)
		{
			DataTable dt;
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Str = "SELECT * From [RuleCondition] Where " + FieldName + " = " + Value + " and ismain = 1";
			dt = DAC.GeneralSelectCommand(new SqlCommand(Str));
			return dt;
		}

	}
}
