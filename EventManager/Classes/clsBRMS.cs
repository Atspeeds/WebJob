using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behin;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Behin
{
	class clsBRMS
    {
		string StrCon1;
		public static string LinkCorrect = "صحیح";
		public static string LinkNotCorrect = "نا صحیح";

		public clsBRMS()
        {
            Configuration config = new Configuration("app.conf");
            StrCon1 = config.GetConnectionString("BehinCnn");
        }

		public void FillIntoCombo(ComboBox myCombo, string NoField)
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Query;

            Query = " SELECT dbo.Tables.Name ,dbo.Tables.No " +
                    " FROM dbo.Tables INNER JOIN " +
                    " dbo.Relations ON dbo.Tables.NO = dbo.FuncDecryptTableNo(dbo.Relations.SrcTableNo) INNER JOIN " +
                    " dbo.RelationItem ON dbo.Relations.NO = dbo.RelationItem.RelationNo " +
					" WHERE (dbo.RelationItem.DestFieldNo = " + NoField + ")";
			
			DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(Query));
			if (dt.Rows.Count > 0)
			{
                Query = "select Name from Fields where tableno = dbo.FuncEncryptTableNo(" + dt.Rows[0]["No"].ToString() + ") and isinkey = 1";
                DataTable dt1 = DAC.GeneralSelectCommand(new SqlCommand(Query));
                if (dt1.Rows.Count > 0)
                {
                    Query = "select  " + dt1.Rows[0]["Name"].ToString() + " as No,Name from " + dt.Rows[0]["Name"].ToString() + " order by name ";
                }
			}
			myCombo.DataSource = DAC.GeneralSelectCommand(new SqlCommand(Query));			
			myCombo.DisplayMember = "Name";
			myCombo.ValueMember = "No";
		}

		public void FillIntoList(CheckedListBox myCombo, string NoField)
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Query;

            Query = " SELECT dbo.Tables.Name ,dbo.Tables.No " +
					" FROM dbo.Tables INNER JOIN " +
					" dbo.Relations ON dbo.Tables.NO = dbo.FuncDecryptTableNo(dbo.Relations.SrcTableNo) INNER JOIN " +
					" dbo.RelationItem ON dbo.Relations.NO = dbo.RelationItem.RelationNo " +
					" WHERE (dbo.RelationItem.DestFieldNo = " + NoField + ")";

			DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(Query));
			if (dt.Rows.Count > 0)
			{
                Query = "select Name from Fields where tableno = dbo.FuncEncryptTableNo(" + dt.Rows[0]["No"].ToString() + ") and isinkey = 1";
                DataTable dt1 = DAC.GeneralSelectCommand(new SqlCommand(Query));
                if (dt1.Rows.Count > 0)
                {
                    Query = "select  " + dt1.Rows[0]["Name"].ToString() + " as No,Name from " + dt.Rows[0]["Name"].ToString() + " order by name ";
                }
            }
			myCombo.DataSource = DAC.GeneralSelectCommand(new SqlCommand(Query));
			myCombo.DisplayMember = "Name";
			myCombo.ValueMember = "No";
		}

		public int FindTables(string NoField)
		{
			int Notable = 0;
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			string Query;

			Query = " SELECT dbo.Tables.NO FROM dbo.EntityType INNER JOIN " +
                    " dbo.EntityClass ON dbo.EntityType.No = dbo.EntityClass.NoEntityType INNER JOIN " +
                    " dbo.Tables ON dbo.EntityClass.NoTable = dbo.FuncEncryptTableNo(dbo.Tables.NO) INNER JOIN " +
					" dbo.Fields ON dbo.FuncEncryptTableNo(dbo.Tables.NO) = dbo.Fields.TableNo AND dbo.Fields.NO = " + NoField;

			DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(Query));
			if (dt.Rows.Count > 0)
			{
				Notable = int.Parse(dt.Rows[0]["NO"].ToString());
			}
			return Notable;
		}
        public string DMSMotor(int NoEvent, string dateto, int EventEffect,string Time)
        {
            clsEvent objEvent = new clsEvent();
            clsPublic objPublic = new clsPublic();
            clsEvent.strcEvent myRowEvent = new clsEvent.strcEvent();
            string dmsmotor = "";

            if (DateTime.Now >=DateTime.Parse( dateto))
            {
                myRowEvent.No = NoEvent;
                myRowEvent = objEvent.SelectByNo(myRowEvent);

                //myRowEvent.Date = lstEvent[0].Date;
                //myRowEvent.General = lstEvent[0].General;
                //myRowEvent.General2 = lstEvent[0].General2;
                //myRowEvent.HasNeedDetail = lstEvent[0].HasNeedDetail;
                //myRowEvent.IdEvent = lstEvent[0].IdEvent;
                //myRowEvent.IsOccured = true;
                //myRowEvent.Message = lstEvent[0].Message;
                //myRowEvent.Name = lstEvent[0].Name;
                //myRowEvent.No = lstEvent[0].No;
                //myRowEvent.NoEventSource = lstEvent[0].NoEventSource;
                //myRowEvent.NoEventSourceType = lstEvent[0].NoEventSourceType;
                //myRowEvent.NoOprationEnvironment = lstEvent[0].NoOprationEnvironment;
                //myRowEvent.NoRuleNode = lstEvent[0].NoRuleNode;
                //myRowEvent.NoRun = lstEvent[0].NoRun;
                //myRowEvent.NoStepRelation = lstEvent[0].NoStepRelation;
                //myRowEvent.NoSrc = lstEvent[0].NoSrc;
                //myRowEvent.NoTables = lstEvent[0].NoTables;
                //myRowEvent.IDEventEffect = lstEvent[0].IDEventEffect;
                //myRowEvent.IDEventType = lstEvent[0].IDEventType;
                //myRowEvent.IDReminderType = lstEvent[0].IDReminderType;
                //objEvent.Update(myRowEvent);
                if (EventEffect == (int)ENUM.EffectType.EffectOnDiagram)
                    dmsmotor = myRowEvent.General + "&" + myRowEvent.NoRun + "&" + myRowEvent.NoStepRelation + "&" + myRowEvent.NoSrc;
                else if (EventEffect == (int)ENUM.EffectType.Waiting)
                    dmsmotor = myRowEvent.General.ToString();
                else if (EventEffect == (int)ENUM.EffectType.TaskExpire)
                    dmsmotor = myRowEvent.General.ToString();
                else if (EventEffect == (int)ENUM.EffectType.Trace)
                    dmsmotor = myRowEvent.General.ToString();
                else if (EventEffect == (int)ENUM.EffectType.Session)
                {
                    if (myRowEvent.General2 == Time)
                        dmsmotor = myRowEvent.Name.ToString();
                }
                else if (EventEffect == (int)ENUM.EffectType.Lenz)
                {
                    clsEntityActionSelf objEntAction = new clsEntityActionSelf();
                    dmsmotor = objEntAction.SelectAction("EntityAction.NoEntity = " + myRowEvent.NoSrc.ToString() + " and EntityAction.Date between '" + myRowEvent.Date.AddHours(-1) + "' and '" + myRowEvent.Date + "'");
                }
            }

            return dmsmotor;
        }
		public string BRMSMotor(int NoRuleNode,string dateto)
		{
            clsTablesSelf objTblSelf = new clsTablesSelf();
			List<int> lstRule = FillListRule(NoRuleNode);
			string strConclusion = "";
			clsRule objRule = new clsRule();
			clsRuleNode objRuleNode = new clsRuleNode();
            clsEvent objEvent = new clsEvent();
            clsEvent.strcEvent myRowEvent = new clsEvent.strcEvent();

			for (int rl = 0; rl < lstRule.Count; rl++)
			{
				List<clsRule.strcRule> lstrule = objRule.Select("NoEntity", lstRule[rl].ToString());
				List<clsRuleNode.strcRuleNode> lstRuleNode = objRuleNode.Select("NoParent", lstrule[0].NoRuleNode.ToString());
				for (int i = 0; i < lstRuleNode.Count; i++)
				{
					strConclusion = FuncRunConclusion(lstRuleNode[i].No);
                    if (strConclusion == "")
                    {
                        if (FuncRunBRMS(lstRuleNode[i].No, dateto) == lstRuleNode[i].IsYes)
                        {
                            strConclusion = BRMSMotorChild(lstRuleNode[i].No, 1, dateto);
                        }
                        else
                        {
                            strConclusion = BRMSMotorChild(lstRuleNode[i].No, 0, dateto);
                        }
                    }
                    else
                    {
                        List<clsEvent.strcEvent> lstEvent = objEvent.Select("Norulenode", lstRuleNode[i].NoParent.ToString());
                        myRowEvent.Date = lstEvent[0].Date;
                        myRowEvent.General = lstEvent[0].General;
                        myRowEvent.General2 = lstEvent[0].General2;
                        myRowEvent.HasNeedDetail = lstEvent[0].HasNeedDetail;
                        myRowEvent.IdEvent = lstEvent[0].IdEvent;
                        myRowEvent.IsOccured = true;
                        myRowEvent.Message = lstEvent[0].Message;
                        myRowEvent.Name = lstEvent[0].Name;
                        myRowEvent.No = lstEvent[0].No;
                        myRowEvent.NoEventSource = lstEvent[0].NoEventSource;
                        myRowEvent.NoEventSourceType = lstEvent[0].NoEventSourceType;
                        myRowEvent.NoOprationEnvironment = lstEvent[0].NoOprationEnvironment;
                        myRowEvent.NoRuleNode = lstEvent[0].NoRuleNode;
                        myRowEvent.NoRun = lstEvent[0].NoRun;
                        myRowEvent.NoStepRelation=lstEvent[0].NoStepRelation;
                        objEvent.Update(myRowEvent);
                    }
				}
			}
			return strConclusion;
		}

		private List<int> FillListRule(int NoRuleType)
		{
			List<int> ListRuleCond = new List<int>();
			List<int> ListRuleNode = new List<int>();
			List<int> ListRule = new List<int>();
			clsTablesSelf objTblSelf = new clsTablesSelf();
			clsRuleCondition objRuleCond = new clsRuleCondition();
			clsRule objRule = new clsRule();
			clsFields objFld = new clsFields();
			clsRuleSimple objrulesimple = new clsRuleSimple();

			//DataTable dt = objFld.Select("TableNo", "dbo.FuncEncryptTableNo(" + NoEntity + ")");
			//for (int i = 0; i < dt.Rows.Count; i++)
			//{
			//    List<clsRuleSimple.strcRuleSimple> lst = objrulesimple.Select("General1", dt.Rows[i]["No"].ToString());
			//    for (int j = 0; j < lst.Count; j++)
			//    {
			//        if (!ListRuleCond.Contains(lst[j].NoRuleCondition))
			//            ListRuleCond.Add(lst[j].NoRuleCondition);
			//    }
			//}
			//List<clsRuleCondition.strcRuleCondition> lstrule = objRuleCond.Select("General1", NoEntityType.ToString());
			//for (int j = 0; j < lstrule.Count; j++)
			//{
			//    if (!ListRuleCond.Contains(lstrule[j].No))
			//        ListRuleCond.Add(lstrule[j].No);
			//}
			//for (int j = 0; j < ListRuleCond.Count; j++)
			//{
			//    List<clsRuleCondition.strcRuleCondition> lstrulecond = objRuleCond.Select("No", ListRuleCond[j].ToString());
			//    for (int jj = 0; jj < lstrulecond.Count; jj++)
			//    {
			//        if (!ListRuleNode.Contains(lstrulecond[jj].NoRuleNode))
			//            ListRuleNode.Add(lstrulecond[jj].NoRuleNode);
			//    }
			//}
			//for (int j = 0; j < ListRuleNode.Count; j++)
			//{
			//    int noparent = FindNoRule(ListRuleNode[j]);
			//    List<clsRule.strcRule> lstrules = objRule.Select("NoRuleNode", noparent.ToString());
			//    for (int jj = 0; jj < lstrules.Count; jj++)
			//    {
			//        if (!ListRule.Contains(lstrules[jj].NoEntity))
			//            ListRule.Add(lstrules[jj].NoEntity);
			//    }
			//}

			int noparent = FindNoRule(NoRuleType);
			List<clsRule.strcRule> lstrules = objRule.Select("NoRuleNode", noparent.ToString());
			for (int jj = 0; jj < lstrules.Count; jj++)
			{
				if (!ListRule.Contains(lstrules[jj].NoEntity))
					ListRule.Add(lstrules[jj].NoEntity);
			}

			return ListRule;
		}

        private int FindNoRule(int NoRuleNode)
        {
            int NoParent = -1;
            clsRuleNode objRuleNode = new clsRuleNode();
            List<clsRuleNode.strcRuleNode> lstrules = objRuleNode.Select("No", NoRuleNode.ToString());
            if (lstrules.Count > 0)
            {
                if (lstrules[0].NoParent == 0)
                {
                    NoParent = lstrules[0].No; 
                    return NoParent;
                }
                else
                    NoParent = FindNoRule(lstrules[0].NoParent);
            }
            return NoParent;
        }
		public string BRMSMotorChild(int NoRuleNode, int IsYes, string dateto)
        {
            string strConclusion = "";
            clsRuleConditionSelf objRuleNode = new clsRuleConditionSelf();

            DataTable lstRuleNode = objRuleNode.Select(NoRuleNode.ToString(),IsYes);
            for (int i = 0; i < lstRuleNode.Rows.Count; i++)
            {
                strConclusion = FuncRunConclusion(int.Parse(lstRuleNode.Rows[i]["No"].ToString()));
                if (strConclusion == "")
                {
					if (FuncRunBRMS(int.Parse(lstRuleNode.Rows[i]["No"].ToString()), dateto) == Convert.ToBoolean(lstRuleNode.Rows[i]["IsYes"].ToString()))
                    {
						strConclusion = BRMSMotorChild(int.Parse(lstRuleNode.Rows[i]["No"].ToString()), 1, dateto);
                    }
                    else
                    {
						strConclusion = BRMSMotorChild(int.Parse(lstRuleNode.Rows[i]["No"].ToString()), 0, dateto);
                    }
                }
            }
            return strConclusion;
        }

		private bool FuncRunBRMS(int NoRuleNode, string dateto)
        {
            bool blRun = false;
            string StrSimple = "";
			clsRuleCondition objRuleCond = new clsRuleCondition();
			clsOnTime objOnTime = new clsOnTime();

            SortedList<int, bool> compositionlist = new SortedList<int,bool>();

			List<clsRuleCondition.strcRuleCondition> lstRuleCond = objRuleCond.Select("NoRuleNode", NoRuleNode.ToString());
            for (int i = 0; i < lstRuleCond.Count;i++ )
            {
				bool blrun = false;
				List<clsOnTime.strcOnTime> lstOnTime = objOnTime.Select("NoRuleCondition", lstRuleCond[i].No.ToString());
				if (lstOnTime.Count > 0)
				{

                    if (Convert.ToDateTime(lstOnTime[0].DateTimeTo).ToString("yyyy-MM-dd HH:mm") == dateto)
					{
						blrun = true;
					}
				}
				else
				{
					blrun = true;
				}
				if (blrun == true)
				{
					switch (lstRuleCond[i].IDRuleConditionType)
					{
						case (int)ENUM.RuleConditionType.AllInstance:
						case (int)ENUM.RuleConditionType.InstanceState:
						case (int)ENUM.RuleConditionType.InstanceStateChange:
						case (int)ENUM.RuleConditionType.EntityMethod:
						case (int)ENUM.RuleConditionType.EntityMethodParameter:
							StrSimple = FuncRunEntity(lstRuleCond[i].No);
							break;
						case (int)ENUM.RuleConditionType.Simple:
							StrSimple = FuncRunSimple(lstRuleCond[i].No);
							break;
						case (int)ENUM.RuleConditionType.Command:
						case (int)ENUM.RuleConditionType.CommandState:
						case (int)ENUM.RuleConditionType.DiagramState:
						case (int)ENUM.RuleConditionType.step:
						case (int)ENUM.RuleConditionType.StepRelation:
						case (int)ENUM.RuleConditionType.Section:
                            StrSimple = FuncRunDiagram(lstRuleCond[i].No, dateto);
							break;
					}
					if (StrSimple != "")
					{
						if (StrSimple == "true")
						{
							compositionlist.Add(lstRuleCond[i].No, true);
							blRun = true;
						}
						else
						{
							GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
							DataTable dtt = DAC.GeneralSelectCommand(new SqlCommand(StrSimple));
							if (dtt.Rows.Count > 0)
							{
								compositionlist.Add(lstRuleCond[i].No, true);
								blRun = true;
							}
							else
							{
								compositionlist.Add(lstRuleCond[i].No, false);
								blRun = false;
							}
						}
					}
					else
					{
						blRun = FuncRunComposition(lstRuleCond[i].No, compositionlist);
					}
				}
            }
			return blRun;
        }

		private string FuncRunEntity(int NoRuleCondition)
        {
            string RunEntity="";
            GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
            clsRuleCondition objRuleCond = new clsRuleCondition();
            List<clsRuleCondition.strcRuleCondition> lstRuleCond = objRuleCond.Select("No", NoRuleCondition.ToString());
			//if (NoEntityType == int.Parse(lstRuleCond[0].General1))
			//{
			//    if (NoEntity == int.Parse(lstRuleCond[0].General2))
			//    {
			//        if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.AllInstance & NoEvent == 11)
			//            RunEntity = "select * from EntityAction where noEntity = " + NoEntity;
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.InstanceState & NoEvent == 7)
			//            RunEntity = "select * from entitystatechange where noEntity = " + NoEntity + " and Noentitystate = " + General;
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.InstanceStateChange & NoEvent == 10)
			//            RunEntity = "select * from entitystatechange where noEntity = " + NoEntity + " and Noentitystate in (" + General + "," + General1 + ")";
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.EntityMethod & NoEvent == 5)
			//            RunEntity = "select * from EntityAction where noEntity = " + NoEntity + " and Noentitymethod = " + General;
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.EntityMethodParameter)  ////////// i dont know
			//            RunEntity = "select * from EntityAction where noEntity = " + NoEntity + " and Noentitymethod = " + General;
			//    }
			//}
            
            return RunEntity;
        }

		private string FuncRunDiagram(int NoRuleCondition,string dateto)
		{
			string RunEntity = "";
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			clsRun objrun = new clsRun();

			//if (NoEvent == 1) //-----------ورود به بخش
			//{
			//    RunEntity = " select * from sectionsteprelation where NoRuleCondition = " + NoRuleCondition +
			//                " and NoStepRelation = " + General + " and IsStart = 1";
			//}
			//else if (NoEvent == 2) //-----------حرکت روی پیوند
			//{
			//    RunEntity = "";
			//}
			//else if (NoEvent == 3) //-----------خروج از بخش
			//{
			//    RunEntity = " select * from sectionsteprelation where NoRuleCondition = " + NoRuleCondition +
			//                " and NoStepRelation = " + General + " and IsStart = 0";
			//}
			//else if (NoEvent == 4) //----------اجرای یک فرمان
			//{
			//    List<clsRun.strcRun> lstrun = objrun.Select("NoDiagram", NoDiagram.ToString());
			//    RunEntity = "select * from Tasklist where noCommand =" + General + " and NoRunStep = " + lstrun[0].No;
			//}
			//else if (NoEvent == 6) //---------------عملیات روی چرخه
			//{
			//    RunEntity = "select * from Run where NoDiagram = " + NoDiagram + " and IDDiagramState = " + General;
			//}
			//else if (NoEvent == 8) //--------------عملیات روی یک وظیفه
			//{
			//    List<clsRun.strcRun> lstrun = objrun.Select("NoDiagram", NoDiagram.ToString());
			//    RunEntity = "select * from Tasklist where noCommand =" + General + " and NoRunStep = " + lstrun[0].No + " and NoEntityState = " + General1;						
			//}
			clsRuleCondition objRuleCond = new clsRuleCondition();
			List<clsRuleCondition.strcRuleCondition> lstRuleCond = objRuleCond.Select("No", NoRuleCondition.ToString());

			if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.step)
			{
				List<clsRun.strcRun> lstrun = objrun.Select("NoDiagram", lstRuleCond[0].General2);
				if (lstrun.Count > 0)
				{
                    //GeneralDataAccess.GeneralDataAccessClass GClassBehin = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
                    //string Qry = "select dbo.mtosh(getdate()) as datesql,left(CONVERT (time, GETDATE()),5) as timesql";
                    //string StrDateSql = "";
                    //string StrTimeSql = "";

                    //DataTable Dt = GClassBehin.GeneralSelectCommand(new SqlCommand(Qry));
                    //if (Dt.Rows.Count > 0)
                    //{
                    //    StrDateSql = Dt.Rows[0]["datesql"].ToString();
                    //    StrTimeSql = Dt.Rows[0]["timesql"].ToString();
                    //}
                    //StrDateSql = StrDateSql + StrTimeSql;
                    RunEntity = "select * from [event] where nosteprelation = " + lstRuleCond[0].General3 + " and norulenode = " + lstRuleCond[0].NoRuleNode + " and substring(CONVERT(varchar(23), [DATE], 121),1,16) = '" + dateto + "'";
                    //if (lstRuleCond[0].General4 == "0")
                    //    RunEntity = "select * from runstep where norun = " + lstrun[0].No + " and nostep = " + lstRuleCond[0].General3 + " and dateend = '1800-01-01 00:00:00.000'";
                    //else
                    //    RunEntity = "select * from runstep where norun = " + lstrun[0].No + " and nostep = " + lstRuleCond[0].General3 + " and dateend > '1800-01-01 00:00:00.000'";
				}
			}


			//if (NoEntityType == int.Parse(lstRuleCond[0].General1))
			//{
			//    if (NoDiagram == int.Parse(lstRuleCond[0].General2))
			//    {
			//        if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.StepRelation & NoEvent == 2)
			//        {
			//            if (lstRuleCond[0].General3 == General)
			//                RunEntity = "true";
			//        }
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.step & NoEvent == 9)
			//        {
			//            List<clsRun.strcRun> lstrun = objrun.Select("NoDiagram", NoDiagram.ToString());
			//            if (lstRuleCond[0].General4 == "0")
			//                RunEntity = "select * from runstep where norun = " + lstrun[0].No + " and nostep = " + General + " and dateend = '1800-01-01 00:00:00.000'";
			//            else
			//                RunEntity = "select * from runstep where norun = " + lstrun[0].No + " and nostep = " + General + " and dateend > '1800-01-01 00:00:00.000'";
			//        }
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.Command & NoEvent == 4)
			//        {
			//            List<clsRun.strcRun> lstrun = objrun.Select("NoDiagram", NoDiagram.ToString());
			//            RunEntity = "select * from Tasklist where noCommand =" + General + " and NoRunStep = " + lstrun[0].No;
			//        }
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.CommandState & NoEvent == 8)
			//        {
			//            List<clsRun.strcRun> lstrun = objrun.Select("NoDiagram", NoDiagram.ToString());
			//            RunEntity = "select * from Tasklist where noCommand =" + General + " and NoRunStep = " + lstrun[0].No + " and NoEntityState = " + General;
			//        }
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.DiagramState & NoEvent == 6)
			//            RunEntity = "select * from Run where NoDiagram = " + NoDiagram + " and IDDiagramState = " + General;
			//        else if (lstRuleCond[0].IDRuleConditionType == (int)ENUM.RuleConditionType.Section)
			//        {
			//            if (NoEvent == 1)
			//                RunEntity = " select * from sectionsteprelation where NoRuleCondition = " + NoRuleCondition +
			//                            " and NoStepRelation = " + General + " and IsStart = 1";
			//            else if (NoEvent == 3)
			//                RunEntity = " select * from sectionsteprelation where NoRuleCondition = " + NoRuleCondition +
			//                            " and NoStepRelation = " + General + " and IsStart = 0";
			//        }
			//    }
			//}

			return RunEntity;
		}

        private string FuncRunConclusion(int NoRuleNode)
		{
			string blRun = "";

			clsRuleConclusion objruleCon = new clsRuleConclusion();
			List<clsRuleConclusion.strcRuleConclusion> lstCon = objruleCon.Select("NoRuleNode", NoRuleNode.ToString());
            if (lstCon.Count > 0)
            {
                blRun = lstCon[0].Name;
            }
            return blRun;
		}

		private string FuncRunSimple(int NoRuleCondition)
		{
			bool blRun = false;
			string fldname = "";
			string tblName = "";
			string strSelect = "";
			clsRuleSimple objrulesimple = new clsRuleSimple();

			List<clsRuleSimple.strcRuleSimple> lst = objrulesimple.Select("NoRuleCondition", NoRuleCondition.ToString());
			if (lst.Count > 0)
			{
				clsFields objfields = new clsFields();
				List<clsFields.strcFields> dt = objfields.Select("No", lst[0].General1);
				if (dt.Count > 0)
				{
					fldname = dt[0].Name;
					clsTables objtbl = new clsTables();
					List<clsTables.strcTables> lsttbl = objtbl.Select("dbo.FuncEncryptTableNo(No)", dt[0].TableNo.ToString());
					tblName = lsttbl[0].Name;
				}
				switch (lst[0].SrcMode)
				{
					case 1:
						strSelect = "select * from " + tblName + " where " + fldname + " Like '%" + lst[0].ValueFrom + "%'";
						break;

					case 20:
						clsLookUpSelf objlookupself = new clsLookUpSelf();
						strSelect = "select * from " + tblName + " where " + fldname + objlookupself.GetName(lst[0].IDCompareOperator) + lst[0].ValueFrom;
						break;

					case 21:
						strSelect = "select * from " + tblName + " where " + fldname + " Between " + lst[0].ValueFrom + " and " + lst[0].ValueTo;
						break;

					case 22:
						string strvv = "";
						clsVariantValue objvv = new clsVariantValue();
						List<clsVariantValue.strcVariantValue> lstvv = objvv.Select("NoRuleSimple", NoRuleCondition.ToString());
						for (int i = 0; i < lstvv.Count; i++)
						{
							strvv += lstvv[i].Value + ",";
						}
						strvv = strvv.Substring(0, strvv.Length - 1);
						strSelect = "select * from " + tblName + " where " + fldname + " in (" + strvv + ")";
						break;

					case 23:
						clsParameterSelf objParaself = new clsParameterSelf();
						strSelect = "select * from " + tblName + " where " + fldname + " = " + objParaself.GetName(int.Parse(lst[0].ValueFrom));
						break;

					case 24:
						strSelect = "select * from " + tblName + " where " + fldname + " = " + lst[0].General2;
						break;

					case 3:
						if (lst[0].ValueFrom == "sysdate")
						{
                            clsPublic objPublic = new clsPublic();
                            strSelect = "select * from " + tblName + " where dbo.mtosh(" + fldname + ") = '" + objPublic.DateToString(DateTime.Now) + "'";
						}
						else
                            strSelect = "select * from " + tblName + " where dbo.mtosh(" + fldname + ") Between '" + lst[0].ValueFrom + "') and '" + lst[0].ValueTo + "')";
						break;

					case 4:
						if (lst[0].ValueFrom == "systime")
						{
                            strSelect = "select * from " + tblName + " where " + fldname + " = '" + DateTime.Now.ToShortTimeString().Substring(0, 5) + "'";
						}
						else
							strSelect = "select * from " + tblName + " where " + fldname + " Between '" + lst[0].ValueFrom + "' and '" + lst[0].ValueTo + "'";
						break;

					case 5:
						string strvv1 = "";
                        if (lst[0].ValueFrom == "")
                        {
                            clsVariantValue objvv1 = new clsVariantValue();
                            List<clsVariantValue.strcVariantValue> lstvv1 = objvv1.Select("NoRuleSimple", NoRuleCondition.ToString());
                            for (int i = 0; i < lstvv1.Count; i++)
                            {
                                strvv1 += lstvv1[i].Value + ",";
                            }
                            strvv1 = strvv1.Substring(0, strvv1.Length - 1);
                            strSelect = "select * from " + tblName + " where " + fldname + " in (" + strvv1 + ")";
                        }
                        else
                        {
                            strSelect = "select * from " + tblName + " where " + fldname + " = " + strvv1;
                        }
						break;
				}
			}
            return strSelect;
		}

        private bool FuncRunComposition(int NoRuleCondition,SortedList<int, bool> ComposList)
		{
			bool blRun = false;
            bool blRunSrc = false;
            bool blRunDest = false;

            clsComposition objComp = new clsComposition();
            List <clsComposition.strcComposition> lst = objComp.Select("NoRuleCondition", NoRuleCondition.ToString());
            if (lst.Count > 0)
            {
                if (ComposList.ContainsKey(lst[0].NoSrcRuleSimple))
                {
                    blRunSrc = ComposList[lst[0].NoSrcRuleSimple];
                }
                if (ComposList.ContainsKey(lst[0].NoDestRuleSimple))
                {
                    blRunDest = ComposList[lst[0].NoDestRuleSimple];
                }
             
                if (lst[0].IDLogicalOperator == 632)
                {
                    if (blRunSrc == true & blRunDest==true)
                        blRun = true;
                }
                else if (lst[0].IDLogicalOperator == 633)
                {
                    if (blRunSrc == true | blRunDest == true)
                        blRun = true;
                }
            }

			return blRun;
		}

		public DataTable Select(string strSelect)
		{
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			DataTable dt = DAC.GeneralSelectCommand(new SqlCommand(strSelect));
			return dt;
		}
    }
}
