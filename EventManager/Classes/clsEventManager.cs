using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Behin;
using System.Data;
using System.Windows.Forms;

namespace EventManager.Classes
{
	class clsEventManager
	{
        clsRuleNode objRuleNode = new clsRuleNode();
        clsRuleNode.strcRuleNode strRuleNode = new clsRuleNode.strcRuleNode();
        clsEvent objEvent = new clsEvent();
        clsRule objRule = new clsRule();
        clsRule.strcRule strRule = new clsRule.strcRule();
        clsRuleCondition objRuleCond = new clsRuleCondition();
        clsRuleCondition.strcRuleCondition strRuleCond = new clsRuleCondition.strcRuleCondition();
        clsRuleConclusion objRuleConc = new clsRuleConclusion();
        clsRuleConclusion.strcRuleConclusion strRuleConc = new clsRuleConclusion.strcRuleConclusion();
        clsOnTime objOnTime = new clsOnTime();
        clsOnTime.strcOnTime strOnTime = new clsOnTime.strcOnTime();
		
        public void FillEventDiagram(DateTime DateTo,int EventEffect,int EventType,int noentity,int NoTable,int ReminderType,object Parameter)
		{
			clsRuleNode objRuleNode = new clsRuleNode();
			clsRuleNode.strcRuleNode strRuleNode = new clsRuleNode.strcRuleNode();
			clsRule objRule = new clsRule();
			clsRule.strcRule strRule = new clsRule.strcRule();
			clsRuleCondition objRuleCond = new clsRuleCondition();
			clsRuleCondition.strcRuleCondition strRuleCond = new clsRuleCondition.strcRuleCondition();
			clsRuleConclusion objRuleConc = new clsRuleConclusion();
			clsRuleConclusion.strcRuleConclusion strRuleConc = new clsRuleConclusion.strcRuleConclusion();
			clsOnTime objOnTime = new clsOnTime();
            //clsOnTime.strcOnTime strOnTime = new clsOnTime.strcOnTime();
			clsEvent objEvent = new clsEvent();
			clsEvent.strcEvent strEvent = new clsEvent.strcEvent();
            clsEntityClass objEntCls = new clsEntityClass();
            clsEntityClassDiagram objEntClsD = new clsEntityClassDiagram();
            clsEntityType objEntType = new clsEntityType();
            int noentitytype = 0;
            try {
                if (EventEffect == (int)ENUM.EffectType.Waiting) //---------------------معوقه
                {
                    int parameter = (int)Parameter;
                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.ToString();
                    strEvent.General2 = "";
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "معوقه");

                }
                else if (EventEffect == (int)ENUM.EffectType.Session) //---------------------جلسه
                {
                    List<BehinSignalR.clsSignalRStruct.Parametervalue> parameter = (List<BehinSignalR.clsSignalRStruct.Parametervalue>)Parameter;
                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = parameter[0].Message;
                    strEvent.General = parameter[0].General;
                    strEvent.General2 = parameter[0].General2;
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    strEvent.IdEvent = -1;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "جلسه");

                }
                else if (EventEffect == (int)ENUM.EffectType.Trace) //---------------------پیگیری
                {
                    BehinSignalR.clsSignalRStruct.srtcTrace parameter = (BehinSignalR.clsSignalRStruct.srtcTrace)Parameter;
                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.NoAgent.ToString();
                    strEvent.General2 = parameter.Comment;
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);

                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.NoAgentMid.ToString();
                    strEvent.General2 = parameter.Comment;
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);

                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.NoAgentDest.ToString();
                    strEvent.General2 = parameter.Comment;
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "پیگیری");

                }
                else if (EventEffect == (int)ENUM.EffectType.EffectOnDiagram) //---------------------زمان سنج چرخه
                {
                    BehinSignalR.clsSignalRStruct.strcStepRelationEvent parameter = (BehinSignalR.clsSignalRStruct.strcStepRelationEvent)Parameter;
                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = parameter.NoRun;
                    strEvent.NoStepRelation = parameter.NoStepRelation;
                    strEvent.Name = "";
                    strEvent.General = parameter.NoDiagram.ToString();
                    strEvent.General2 = "";
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "چرخه");
                }
                else if (EventEffect == (int)ENUM.EffectType.Waiting) //---------------------منقضی شدن
                {
                    int parameter = (int)Parameter;
                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.ToString();
                    strEvent.General2 = "";
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "منقضی");

                }
                else if (EventEffect == (int)ENUM.EffectType.Waiting) //---------------------منقضی شدن
                {
                    BehinSignalR.clsSignalRStruct.srtcTrace parameter = (BehinSignalR.clsSignalRStruct.srtcTrace)Parameter;
                    strEvent.Date = DateTo;
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.NoAgent + "&" + parameter.NoAgentDest + "&" + parameter.NoAgentMid;
                    strEvent.General2 = "";
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "نقضی");

                }
                else if (EventEffect == (int)ENUM.EffectType.Lenz) //---------------------ذره بین
                {
                    BehinSignalR.clsSignalRStruct.strcLenz parameter = (BehinSignalR.clsSignalRStruct.strcLenz)Parameter;

                    strEvent.Date = DateTo.AddHours(1);
                    strEvent.NoRuleNode = -1;
                    strEvent.NoRun = -1;
                    strEvent.NoStepRelation = -1;
                    strEvent.Name = "";
                    strEvent.General = parameter.NoAgent.ToString();
                    strEvent.General2 = "";
                    strEvent.Message = "";
                    strEvent.NoEventType = EventType;
                    strEvent.IDReminderType = ReminderType;
                    strEvent.IDEventEffect = EventEffect;
                    strEvent.NoTables = NoTable;
                    strEvent.NoSrc = noentity;
                    objEvent.Insert(strEvent);
                    System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + "ذره بین");

                    //List<clsEntityClass.strcEntityClass> lstEntCls = objEntCls.Select("dbo.FuncDecryptTableNo(NoTable)", NoTable.ToString());
                    //if (lstEntCls.Count > 0)
                    //{
                    //    List<clsEntityType.strcEntityType> lstEntType = objEntType.Select("no", lstEntCls[0].NoEntityType.ToString());
                    //    if (lstEntType.Count > 0)
                    //        noentitytype = lstEntType[0].No;
                    //}
                    //strRuleNode.Name = "موجودیت";
                    //strRuleNode.IsCondition = true;
                    //strRuleNode.IsYes = true;
                    //strRuleNode.NoParent = 0;
                    //strRuleNode.TopNode = 50;
                    //strRuleNode.HeightNode = 50;
                    //strRuleNode.WidthNode = 100;
                    //strRuleNode.LeftNode = 400;
                    //strRuleNode.AbstractCode = "";
                    //int norulenode = objRuleNode.Insert(strRuleNode);

                    //strRule.IDRuleMode = 660;
                    //strRule.NoRuleNode = norulenode;
                    //strRule.NoEntityType = 0;
                    //strRule.NoEntityState = 0;
                    //strRule.Name = "موجودیت";
                    //strRule.IsActive = true;
                    //strRule.Version = 1;
                    //strRule.General = "";
                    //objRule.Insert(strRule);

                    //strRuleNode.Name = "شرط1";
                    //strRuleNode.IsCondition = true;
                    //strRuleNode.IsYes = true;
                    //strRuleNode.NoParent = norulenode;
                    //strRuleNode.TopNode = 200;
                    //strRuleNode.HeightNode = 50;
                    //strRuleNode.WidthNode = 100;
                    //strRuleNode.LeftNode = 400;
                    //int norulenodecond = objRuleNode.Insert(strRuleNode);

                    //strEvent.Date = DateTo;
                    //strEvent.NoRuleNode = norulenodecond;
                    //strEvent.NoRun = -1;
                    //strEvent.NoStepRelation = -1;
                    //strEvent.Name = "";
                    //strEvent.General = "";
                    //strEvent.General2 = "";
                    //strEvent.Message = "";
                    //objEvent.Insert(strEvent);

                    //strRuleCond.Name = "شرط1";
                    //strRuleCond.NoRuleNode = norulenodecond;
                    //strRuleCond.IsMain = true;
                    //strRuleCond.IDRuleConditionType = 648;
                    //strRuleCond.General1 = noentitytype.ToString();
                    //strRuleCond.General2 = noentity.ToString();
                    //strRuleCond.General3 = "";
                    //strRuleCond.General4 = "";
                    //strRuleCond.General5 = "";
                    //int norulenodecond1 = objRuleCond.Insert(strRuleCond);

                    //strRuleNode.Name = parameter.NoAgent + "&" + parameter.NoEntity;
                    //strRuleNode.IsCondition = false;
                    //strRuleNode.IsYes = true;
                    //strRuleNode.NoParent = norulenodecond;
                    //strRuleNode.TopNode = 350;
                    //strRuleNode.HeightNode = 50;
                    //strRuleNode.WidthNode = 100;
                    //strRuleNode.LeftNode = 350;
                    //int norulenodeconc = objRuleNode.Insert(strRuleNode);

                    //strRuleConc.NoRuleNode = norulenodeconc;
                    //strRuleConc.Name = parameter.NoAgent + "&" + parameter.NoEntity;
                    //strRuleConc.NoRule = norulenode;
                    //strRuleConc.Comment = "";
                    //objRuleConc.Insert(strRuleConc);
                }
                //if (EventEffect == (int)ENUM.EffectType.SystemProcedure) //---------------------رویه
                //{
                //    List<clsEntityClassDiagram.strcEntityClassDiagram> lstEntClsD = objEntClsD.Select("nodiagram", nodiagram.ToString());
                //    if (lstEntClsD.Count > 0)
                //    {
                //        List<clsEntityClass.strcEntityClass> lstEntCls = objEntCls.Select("No", lstEntClsD[0].NoEntityClass.ToString());
                //        if (lstEntCls.Count > 0)
                //        {
                //            List<clsEntityType.strcEntityType> lstEntType = objEntType.Select("no", lstEntCls[0].NoEntityType.ToString());
                //            if (lstEntType.Count > 0)
                //                noentitytype = lstEntType[0].No;
                //        }
                //    }

                //    strRuleNode.Name = "چرخه";
                //    strRuleNode.IsCondition = true;
                //    strRuleNode.IsYes = true;
                //    strRuleNode.NoParent = 0;
                //    strRuleNode.TopNode = 50;
                //    strRuleNode.HeightNode = 50;
                //    strRuleNode.WidthNode = 100;
                //    strRuleNode.LeftNode = 400;
                //    strRuleNode.AbstractCode = "";
                //    int norulenode = objRuleNode.Insert(strRuleNode);

                //    strRule.IDRuleMode = 660;
                //    strRule.NoRuleNode = norulenode;
                //    strRule.NoEntityType = 0;
                //    strRule.NoEntityState = 0;
                //    strRule.Name = "چرخه";
                //    strRule.IsActive = true;
                //    strRule.Version = 1;
                //    strRule.General = "";
                //    objRule.Insert(strRule);

                //    strRuleNode.Name = "شرط1";
                //    strRuleNode.IsCondition = true;
                //    strRuleNode.IsYes = true;
                //    strRuleNode.NoParent = norulenode;
                //    strRuleNode.TopNode = 200;
                //    strRuleNode.HeightNode = 50;
                //    strRuleNode.WidthNode = 100;
                //    strRuleNode.LeftNode = 400;
                //    int norulenodecond = objRuleNode.Insert(strRuleNode);

                //    strEvent.Date = DateTo;
                //    strEvent.NoRuleNode = norulenodecond;
                //    strEvent.NoRun = Norun;
                //    strEvent.NoStepRelation = nostep;
                //    strEvent.Name = "";
                //    strEvent.General = "";
                //    strEvent.General2 = "";
                //    strEvent.Message = "";
                //    objEvent.Insert(strEvent);

                //    strRuleCond.Name = "شرط1";
                //    strRuleCond.NoRuleNode = norulenodecond;
                //    strRuleCond.IsMain = true;
                //    strRuleCond.IDRuleConditionType = 655;
                //    strRuleCond.General1 = noentitytype.ToString();
                //    strRuleCond.General2 = nodiagram.ToString();
                //    strRuleCond.General3 = nostep.ToString();
                //    strRuleCond.General4 = "1";
                //    strRuleCond.General5 = noentity.ToString();
                //    int norulenodecond1 = objRuleCond.Insert(strRuleCond);

                //    //strOnTime.NoRuleCondition = norulenodecond1;
                //    //strOnTime.DateTimeFrom = DateFrom;
                //    //strOnTime.DateTimeTo = DateTo;
                //    //objOnTime.Insert(strOnTime);

                //    strRuleNode.Name = DateFrom.ToString("yyyy-MM-dd HH:mm") + "&" + nodiagram.ToString() + "&" + Norun.ToString() + "&" + noentity.ToString() + "&" + nostep.ToString() + "&" + DateTo.ToString("yyyy-MM-dd HH:mm");
                //    strRuleNode.IsCondition = false;
                //    strRuleNode.IsYes = true;
                //    strRuleNode.NoParent = norulenodecond;
                //    strRuleNode.TopNode = 350;
                //    strRuleNode.HeightNode = 50;
                //    strRuleNode.WidthNode = 100;
                //    strRuleNode.LeftNode = 350;
                //    int norulenodeconc = objRuleNode.Insert(strRuleNode);

                //    strRuleConc.NoRuleNode = norulenodeconc;
                //    strRuleConc.Name = DateFrom.ToString("yyyy-MM-dd HH:mm") + "&" + nodiagram.ToString() + "&" + Norun.ToString() + "&" + noentity.ToString() + "&" + nostep.ToString() + "&" + DateTo.ToString("yyyy-MM-dd HH:mm");
                //    strRuleConc.NoRule = norulenode;
                //    strRuleConc.Comment = "";
                //    objRuleConc.Insert(strRuleConc);
                //}
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Application.StartupPath + "\\Log\\Logevent.txt", Environment.NewLine + ex.ToString());
            }
        }

        public void RemoveEventDiagram(DateTime DateTo, int EventEffect, int EventType, int noentity, int NoTable, int ReminderType, BehinSignalR.clsSignalRStruct.strcStepRelationEvent Parameter)
        {
            if (EventEffect == (int)ENUM.EffectType.EffectOnDiagram)
            {
                //objEvent.Deleteevent("NoSrc = " + Parameter.NoEntity + " and NoTables = " + NoTable + " and IDEventType = " + EventType + " and IDEventEffect = " + EventEffect + " and IDReminderType " + ReminderType);
                objEvent.Deleteevent("norun = " + Parameter.NoRun + " and nosteprelation = " + Parameter.NoStepRelation);           
            }
            //DataTable dt = objEvent.selectevent("norun = " + Norun + " and nosteprelation = " + nostep);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    List<clsRuleNode.strcRuleNode> lstrulenode = objRuleNode.Select("No", dt.Rows[i]["NoRuleNode"].ToString());
            //    strRuleNode.No = lstrulenode[0].NoParent;
            //    objRuleNode.Delete(strRuleNode);
            //    strRule.NoRuleNode = lstrulenode[0].NoParent;
            //    objRule.Delete(strRule);

            //    List<clsRuleCondition.strcRuleCondition> lstrulecond = objRuleCond.Select("NoRuleNode", lstrulenode[0].No.ToString());
            //    strRuleCond.No = lstrulecond[0].No;
            //    objRuleCond.Delete(strRuleCond);
            //    strOnTime.NoRuleCondition = lstrulecond[0].No;
            //    objOnTime.Delete(strOnTime);

            //    List<clsRuleNode.strcRuleNode> lstrulenode1 = objRuleNode.Select("NoParent", dt.Rows[i]["NoRuleNode"].ToString());
            //    strRuleConc.NoRuleNode = lstrulenode1[0].No;
            //    objRuleConc.Delete(strRuleConc);
            //    strRuleNode.No = lstrulenode1[0].No;
            //    objRuleNode.Delete(strRuleNode);

            //    strRuleNode.No = lstrulenode[0].No;
            //    objRuleNode.Delete(strRuleNode);
            //}
           // objEvent.Deleteevent("norun = " + Norun + " and nosteprelation = " + nostep);
        }
    }
}
