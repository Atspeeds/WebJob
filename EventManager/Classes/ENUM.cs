using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Behin
{
    public class ENUM
    {
        public enum DiagramType : int
        {
            Condition = 1,
            Conclusion = 2 
        }

		public enum FieldType : int
		{
			Number = 2,
			Character = 1 ,
			Date = 3,
			Time = 4,
			FKey = 5
		}

		public enum RuleConditionType : int
		{
			Composition = 646,
			Simple = 647,
			AllInstance = 648,
			InstanceState = 649,
			InstanceStateChange = 650 ,
			EntityMethod = 651 ,
			EntityMethodParameter = 652,
			OnTime = 653 ,
			StepRelation = 654,
			step = 655 ,
            Command = 656,
            CommandState = 657,
            DiagramState = 658,
			Section = 661
		}
		public enum RuleMode : int
        {
            Event = 659,
            Diagram = 660
        }
        public enum EventType : int
        {
            Index = 1,
            Lenz = 2,
            Procedure = 3
        }
        public enum EffectType : int
        {
            EffectOnDiagram = 825,
            SystemProcedure = 826,
            Lenz = 827,
            Waiting = 828,
            Trace = 829,
            TraceDoer = 830,
            TaskExpire = 831,
            Session = 969
        }
        public enum ReminderType : int
        {
            System = 832,
            Sms = 833,
            Telegram = 834,
            Email = 835,
            Run = 836
        }
    }
}