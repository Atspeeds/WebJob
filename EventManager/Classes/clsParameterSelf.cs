using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behin;
using System.Data;
using System.Data.SqlClient;

namespace Behin
{
	class clsParameterSelf
	{
		string StrCon1;

		public clsParameterSelf()
		{
			Configuration config = new Configuration("app.conf");
			StrCon1 = config.GetConnectionString("BehinCnn");
		}

		public DataTable Select()
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			string Qry = "Select * from RuleParameter order by name ";
			DataTable datTables = DAC.GeneralSelectCommand(new SqlCommand(Qry));

			return datTables;
		}

        public string GetName(int No)
        {
            GeneralDataAccess.GeneralDataAccessClass GClass = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Qry = "SELECT Name FROM RuleParameter WHERE NO = " + No;

            if (GClass.GeneralSelectCommand(new SqlCommand(Qry)).Rows.Count != 0)
                return GClass.GeneralSelectCommand(new SqlCommand(Qry)).Rows[0][0].ToString();
            else
                return "";

        }

	}
}
