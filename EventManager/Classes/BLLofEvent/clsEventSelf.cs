using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Behin
{
    public partial class clsEvent
    {
        public DataTable selectevent(string strwhere)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Qry = " select * from [event] where " + strwhere;
            DataTable Data = DAC.GeneralSelectCommand(new SqlCommand(Qry));

            return Data;
        }

        public void Deleteevent(string strwhere)
        {
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            string Qry = " Delete from [event] where " + strwhere;
            DAC.GeneralDeleteCommand(new SqlCommand(Qry));

        }

    }
}
