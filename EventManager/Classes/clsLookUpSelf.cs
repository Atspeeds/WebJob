using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using System.Data;

namespace Behin
{
    class clsLookUpSelf
    {
        string StrCon1;

        public clsLookUpSelf()
        {
            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }

        public string GetName(int NoLookUp)
        {
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Qry = "SELECT Name FROM LookUp WHERE NO = " + NoLookUp;
            
            if (GClass.GeneralSelectCommand(new SqlCommand(Qry)).Rows.Count != 0)
                return GClass.GeneralSelectCommand(new SqlCommand(Qry)).Rows[0][0].ToString();
            else
                return "";

        }
        public void FillLookupIntoCombo(RadComboBox myCombo, int NoLookUpType)
        {

            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Query; 
			if (NoLookUpType == 162)
				Query = string.Format("Select * From LookUp where Num in (1,5,20,11,18,23,24,25,26,27,28,29,30) and NoLookUpType={0}", NoLookUpType);
			else
				Query = string.Format("Select * From LookUp where NoLookUpType={0} ",NoLookUpType);
            myCombo.DataSource = DAC.GeneralSelectCommand(new SqlCommand(Query));
            myCombo.DisplayMember = "Name";
            myCombo.ValueMember = "No";
        }
		public void FillLookupIntoComboBox(ComboBox myCombo, int NoLookUpType)
		{

			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Query;
			if (NoLookUpType == 162)
				Query = string.Format("Select * From LookUp where Num in (1,5,10,11,18,23,24,25,26,27,28,29) and NoLookUpType={0}", NoLookUpType);
			else
				Query = string.Format("Select * From LookUp where NoLookUpType={0} ", NoLookUpType);
			DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(Query));
			ExternalLibrary2.External ext = new ExternalLibrary2.External();
			ext.FillCombo(myCombo, dt);

			//myCombo.DataSource = DAC.GeneralSelectCommand(new SqlCommand(Query));
			//myCombo.DisplayMember = "Name";
			//myCombo.ValueMember = "No";
		}
    }
}
