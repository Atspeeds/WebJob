using System;

namespace Behin
{
	public class CDate
	{		

		public CDate()
		{
			//
			// TODO: Add constructor logic here
			//

			InitializeDataMembers();
		}

		#region Static Members
		
		public static string[] mcMonths = { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
		public static int[] mcDays = { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 };

		#endregion

		#region Data Members

		private string mBaseG;
		private string mBaseP;

		#endregion

		#region Initialize Methods

		private void InitializeDataMembers()
		{
			mBaseG = "20030423";
			mBaseP = "13820203";
		}
		
		#endregion

		#region Private Methids

		private string	ShortDate( string LongDate )
		{
			string lReturn = LongDate;
			lReturn = CompeleteDate(lReturn);
			if( lReturn.Substring(4,1) == "/" ) lReturn = lReturn.Remove(4,1);
			if( lReturn.Substring(6,1) == "/" ) lReturn = lReturn.Remove(6,1);
			return lReturn;
		}
		private bool	IsGreaterThanBase(string curDate,int MOrS)
		{
			curDate = CompeleteDate(curDate);
			if( curDate.Substring(4,1) == "/" ) curDate = curDate.Remove(4,1);
			if( curDate.Substring(6,1) == "/" ) curDate = curDate.Remove(6,1);
			int curYear = System.Convert.ToInt32(curDate.Substring(0,4));
			int curMonth = System.Convert.ToInt32(curDate.Substring(4,2));
			int curDay = System.Convert.ToInt32(curDate.Substring(6,2));
			int baseYear;
			int baseMonth;
			int baseDay;
			
			if (MOrS == 1)
			{
				baseYear = System.Convert.ToInt32(mBaseG.Substring(0,4));
				baseMonth = System.Convert.ToInt32(mBaseG.Substring(4,2));
				baseDay = System.Convert.ToInt32(mBaseG.Substring(6,2));
			}
			else
			{
				baseYear = System.Convert.ToInt32(mBaseP.Substring(0,4));
				baseMonth = System.Convert.ToInt32(mBaseP.Substring(4,2));
				baseDay = System.Convert.ToInt32(mBaseP.Substring(6,2));
			}

			if (curYear < baseYear)
			{
				return false;
			}
			if (curYear > baseYear)
			{
				return true;
			}
			if (curYear == baseYear)
			{
				if (curMonth < baseMonth)
				{
					return false;
				}
				if (curMonth > baseMonth)
				{
					return true;
				}
				if (curMonth == baseMonth)
				{
					if (curDay < baseDay)
					{
						return false;
					}
					if (curDay >= baseDay)
					{
						return true;
					}
				}
			}
			return false;
		}
		private int		FindMonthDayNumber(int curMonth,string curDate,int MOrS)
		{
			if (MOrS == 1)
			{
				switch (curMonth)
				{
					case 1:
						return 31;
					case 2:
						if (IsLeapYear(curDate, 1))
							return 29;
						else
							return 28;
					case 3:
						return 31;
					case 4:
						return 30;
					case 5:
						return 31;
					case 6:
						return 30;
					case 7:
						return 31;
					case 8:
						return 31;
					case 9:
						return 30;
					case 10:
						return 31;
					case 11:
						return 30;
					case 12:
						return 31;
				}
			}
			else
			{
				if (curMonth < 7)
					return 31;
				else
					if (curMonth == 12)
						if (IsLeapYear(curDate,2))
							return 30;
						else
							return 29;
					else
						return 30;
			}
			return 0;
		}
		private string	ConvertDate(string curDate,int MOrS)
		{	//MorS: 1:(M to S) , 2:(S to M)
			string startDate;
			string endDate;
			string resultDate;
			int resFlag;
			long dayNumber = 0;
			long i;

			curDate = ShortDate( curDate );

			if (IsGreaterThanBase(curDate,MOrS))
			{
				if (MOrS == 1)
				{
					startDate = mBaseG;
					resultDate = mBaseP;
					resFlag = 2;
				}
				else
				{
					startDate = mBaseP;
					resultDate = mBaseG;
					resFlag = 1;
				}
				
				endDate = ShortDate( curDate );
				if (startDate == endDate)
				{		  
					return resultDate;
				}
				else
				{					
					while (startDate != endDate)
					{
						dayNumber++;
						startDate = FindNextDay(startDate,MOrS,1);
					}
				}
				for(i = 0;i < dayNumber;i ++)
				{
					resultDate = FindNextDay(resultDate,resFlag,1);
				}
				return resultDate;
			}
			else
			{
				startDate = curDate;
				if (MOrS == 1)
				{
					endDate = mBaseG;
					resultDate = mBaseP;
					resFlag = 2;
				}
				else
				{
					endDate = mBaseP;
					resultDate = mBaseG;
					resFlag = 1;
				}
				if (startDate == endDate)
				{		  
					return resultDate;
				}
				else
				{					
					while (startDate != endDate)
					{
						dayNumber += 1;
						startDate = FindNextDay(startDate,MOrS,1);
					}
				}

				for(i = 0;i < dayNumber;i ++)
				{
					resultDate = FindNextDay(resultDate,resFlag,-1);
				}
				return resultDate;
			}

		}
		private string	FindNextDay(string curDate,int MOrS,int direct)
		{
			curDate = ShortDate(curDate);
			int curYear = System.Convert.ToInt32(curDate.Substring(0,4));
			int curMonth = System.Convert.ToInt32(curDate.Substring(4,2));
			int curDay = System.Convert.ToInt32(curDate.Substring(6,2));
			switch (direct)
			{
				case 1 :
				{
					curDay++;
					if (curDay > FindMonthDayNumber(curMonth,curDate,MOrS))
					{
						curDay = 1;
						curMonth++;
					}
					if (curMonth > 12)
					{
						curMonth = 1;
						curYear++;
					}					
					break;
				}
				case -1 :
				{
					curDay--;
					if (curDay < 1)
					{
						curMonth--;
						if (curMonth < 1)
						{
							curMonth = 12;
							curYear--;
							curDay = FindMonthDayNumber(curMonth,curDate,MOrS);							
						}
						else
						{
							curDay = FindMonthDayNumber(curMonth,curDate,MOrS);
						}
					}
					break;
				}
			}
			string lReturn="";;
			lReturn = MakeDateString(curYear,curMonth,curDay);
			lReturn = ShortDate( lReturn );
			return lReturn;
		}
		

		#endregion

		#region Public Methods

		public string MakeDateString(int aCurYear,int aCurMonth,int aCurDay)
		{
			string lReturn = aCurYear.ToString();
			
			if (aCurMonth < 10)
				lReturn += "/0" + aCurMonth.ToString();
			else
				lReturn += "/"+ aCurMonth.ToString();

			if (aCurDay < 10)
				lReturn += "/0" + aCurDay.ToString();
			else
				lReturn += "/" + aCurDay.ToString();

			return lReturn;
		}
		public string MakeDateString(string aCurYear,int aCurMonth,string aCurDay)
		{
			string lReturn = aCurYear;
			
			if (aCurMonth < 10)
				lReturn += "/0" + aCurMonth.ToString();
			else
				lReturn += "/" + aCurMonth.ToString();

			int lCurDay = Convert.ToInt32(aCurDay);
			if( lCurDay < 10 )
				lReturn += "/0" + lCurDay.ToString();
			else
				lReturn += "/" + lCurDay.ToString();

			return lReturn;
		}
		public string MakeDateString(string aCurYear,int aCurMonth,int aCurDay)
		{
			string lReturn = aCurYear;
			
			if (aCurMonth < 10)
				lReturn += "/0" + aCurMonth.ToString();
			else
				lReturn += "/" + aCurMonth.ToString();

			if (aCurDay < 10)
				lReturn += "/0" + aCurDay.ToString();
			else
				lReturn += "/" + aCurDay.ToString();

			return lReturn;
		}
		public string LongDate( string aShortDate ) 
		{
			if( aShortDate.Length < 8 ) return aShortDate;

			string lReturn = "";
			lReturn  = aShortDate.Substring(0,4) + "/";
			lReturn += aShortDate.Substring(4,2) + "/";
			lReturn += aShortDate.Substring(6,2);
			return lReturn;
		}

		public string ConvertToShamsi(DateTime aDate)
		{
			string lYear = aDate.Year.ToString();
			string lMonth = aDate.Month.ToString();
			string lDay = aDate.Day.ToString();

			if (lMonth.Length == 1) lMonth = "0" + lMonth;
			if (lDay.Length == 1) lDay = "0" + lDay;

			string lDate = lYear + lMonth + lDay;
			lDate = ConvertDate(lDate, 1);

			string lReturn = LongDate(lDate);
			return lReturn;
		}
		public DateTime ConvertToMiladi(string aDate)
		{
			string lMiladiDate = ConvertDate(aDate, 2);

			int lYear = Convert.ToInt16(lMiladiDate.Substring(0, 4));
			int lMonth = Convert.ToInt16(lMiladiDate.Substring(4, 2));
			int lDay = Convert.ToInt16(lMiladiDate.Substring(6, 2));

			DateTime lDate = new DateTime(lYear,lMonth,lDay);
			return lDate;
		}

		#endregion

		#region Static Methods

		public static bool IsLeapYear(string curDate, int MOrS)  //Sale Kabiseh
		{
			int curYear = System.Convert.ToInt32(curDate.Substring(0, 4));
			switch (MOrS)
			{
				case 1:
					if ((curYear % 4) == 0)
						return true;
					else
						return false;
				case 2:
					if ((curYear % 4) == 3)
						return true;
					else
						return false;
			}
			return false;
		}
		public static string RemoveSpaces(string aDate)
		{
			string lDate = "";
			for (int i = 0; i < aDate.Length; i++)
				if (aDate[i] != ' ')
					lDate += aDate[i].ToString();
			return lDate;
		}
		public static int ValidateDate(string aDate)
		{
			//0:Valid, 1:Not Compelete, 2:Month Error, 3:Day Error, 4:Invalid Numbers
			int lReturn = 0;

			string lsYear = CStaticFunctions.GetWord(aDate, 1, '/').Trim();
			string lsMonth = CStaticFunctions.GetWord(aDate, 2, '/').Trim();
			string lsDay = CStaticFunctions.GetWord(aDate, 3, '/').Trim();

			if (lsYear == "" || lsMonth == "" || lsDay == "")
				return 1;

			if (!CStaticFunctions.IsNumeric(lsYear) ||
				!CStaticFunctions.IsNumeric(lsMonth) ||
				!CStaticFunctions.IsNumeric(lsDay))
				return 4;

			int lYear = Convert.ToInt16(lsYear);
			int lMonth = Convert.ToInt16(lsMonth);
			int lDay = Convert.ToInt16(lsDay);

			if (lMonth > 12 || lMonth < 1)
				lReturn = 2;
			else if (lMonth == 12)
			{
				bool lLeapYear = CDate.IsLeapYear(lsYear + lsMonth + lsDay, 2);
				if (!lLeapYear && lDay > 29)
					lReturn = 3;
				else if (lLeapYear && lDay > 30)
					lReturn = 3;
				else if (lDay < 1)
					lReturn = 3;
			}
			else if (lDay < 1 || lDay > CDate.mcDays[lMonth - 1])
				lReturn = 3;

			return lReturn;
		}
		public static string DateErrorMessage(int aDateErrorNumber)
		{
			string lMsg = "";
			switch (aDateErrorNumber)
			{
				case 1:
					lMsg = "تاریخ باید بصورت کامل وارد شود\n";
					lMsg += "در صورت تک رقمی بودن ماه و روز، یک صفر به آنها اضافه می شود\n";
					lMsg += "مثلا: چهارم اردیبشهت سال 1385 بصورت 1385/02/04 تایپ شود.";
					break;
				case 2:
					lMsg = "شماره ماه باید در محدوده 1 تا 12 باشد";
					break;
				case 3:
					lMsg = "شماره روز در محدوده ماه وارد شده نیست";
					break;
				case 4:
					lMsg = "لطفا فقط عدد وارد نمایید.";
					break;
			}
			return lMsg;
		}
		public static string CompeleteDate(string aDate)
		{
			string lReturn = "";
			string lYear, lMonth, lDay;
			int i1, i2, i3;

			i1 = aDate.IndexOf('/');
			if (i1 > -1)
			{
				lYear = aDate.Substring(0, i1);
				i2 = aDate.IndexOf('/', i1 + 1);
				if (i2 > -1)
				{
					lMonth = aDate.Substring(i1 + 1, i2 - i1 - 1);
					if (lMonth.Length == 1) lMonth = "0" + lMonth;
					i3 = aDate.IndexOf('/', i2 + 1);
					if (i3 == -1)
					{
						lDay = aDate.Substring(i2 + 1);
						if (lDay.Length == 1) lDay = "0" + lDay;
						lReturn = lYear + "/" + lMonth + "/" + lDay;
						lReturn = RemoveSpaces(lReturn);
					}
				}
			}
			else if (aDate.Length == 8)
				lReturn = aDate;

			return lReturn;
		}

		#endregion
	}
}
