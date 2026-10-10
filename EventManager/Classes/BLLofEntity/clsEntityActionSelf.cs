using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Behin
{
    public partial class clsEntityActionSelf
    {
        string StrCon1;

        public clsEntityActionSelf()
        {

            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }  //End Behin Function

        public string SelectAction(string strwhere)
        {
            string name = "";
            string str = "select Agent.Name + ' در تاریخ ' + convert(char, dbo.mtosh(EntityAction.Date)) + EntityMethod.NamePersian + ' را انجام داده است' as name  from EntityAction, Agent," +
                        " EntityMethod where EntityAction.NoAgentPerforming = Agent.NoEntity and EntityAction.NoEntityMethod = EntityMethod.No " +
                        " and " + strwhere;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            DataTable dt =   DAC.GeneralSelectCommand(new SqlCommand(str));
            for (int i=0;i< dt.Rows.Count;i++)
            {
                name += dt.Rows[i]["name"].ToString() + "***";
            }

            return name;
        }

        public int SelectMagnifierFolderEntity(int NoEntity,int NoAgent)
        {
            int no = -1;
            string Qry = "select * from MagnifierFolderEntity where noentity = " + NoEntity + " and NoAgent =" + NoAgent;
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(Qry));
            if (dt.Rows.Count > 0)
               no = int.Parse(dt.Rows[0]["no"].ToString());
            return no;
        }
    }
}
