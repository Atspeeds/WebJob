using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Behin
{
	public enum SelectTypes
	{
		sel_All,
		sel_None,
		sel_revers
	}

	/// <summary>
	/// Summary description for CStaticFunctions.
	/// </summary>
	public class CStaticFunctions
	{
		public CStaticFunctions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Public Static Data Members

		public static char mWordSeparator = ' ';

		#endregion

		#region Private Static Methods
		
		#endregion
		
		#region Public Static Methods

		
		public static int GetComboIndex( string aText , System.Windows.Forms.ComboBox aCombo )
		{
			int lReturn = -1;

			for( int i=0 ; i<aCombo.Items.Count  ; i++ )
			{
				if( aCombo.Items[i].ToString().Equals(aText) )
				{
					lReturn = i;
					break;
				}
			}

			return lReturn;
		}

		public static System.Windows.Forms.TreeNode FindNode( System.Windows.Forms.TreeNodeCollection aNodes , int aID ) 
		{
			System.Windows.Forms.TreeNode Tn = null;
			for( int i=0 ; i<aNodes.Count ; i++ )
			{
				if( Convert.ToInt32( aNodes[i].Tag ) == aID ) 
				{
					Tn = aNodes[i];
					break;
				}
				else 
				{
					Tn = FindNode( aNodes[i].Nodes , aID );
					if( Tn != null ) break;
				}
			}
			return Tn;
		}

		public static string GetMD5(string aText)
		{
			MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
			byte[] hashedBytes;
			UTF8Encoding encoder = new UTF8Encoding();
			hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(aText));
			string lStr = "";
			byte lByte, i;
			for (i = 0; i < hashedBytes.Length; i++)
			{
				lByte = hashedBytes[i];
				lStr += lByte.ToString("X2");
			}
			return lStr;
		}

		public static string GetWord( string aStr , int aWordNumber ) 
		{
			int lWordNumber = 0 , i=0;
			string lReturn = "";
			string lCurrentWord = "";
			char lChar;

			for( i=0 ; i < aStr.Length ; i++ )
			{
				lChar = aStr[i];
				if( lChar == mWordSeparator )
				{
					if( ++lWordNumber == aWordNumber ) 
						break;
					lCurrentWord = "";
				}
				else
					
					lCurrentWord += lChar.ToString();
			}

			lReturn = lCurrentWord;
			return lReturn;
		}
		public static string GetWord( string aString , int aWordNumber , char aWordSeparator ) 
		{
			int lWordNumber = 0 , i=0;
			string lReturn = "";
			string lCurrentWord = "";
			string aStr = aString;
			char lChar;
			if (aStr.Length == 0) return lReturn;

			if( aStr[aStr.Length-1] != aWordSeparator )
				aStr += aWordSeparator;

			for( i=0 ; i < aStr.Length ; i++ )
			{
				lChar = aStr[i];
				if( lChar == aWordSeparator )
				{
					if( ++lWordNumber == aWordNumber ) 
						break;
					lCurrentWord = "";
				}
				else
					lCurrentWord += lChar.ToString();
			}

			if( lWordNumber == aWordNumber )
				lReturn = lCurrentWord;
			return lReturn;
		}
		public static string GetWord( string aStr , int aWordNumber , string aWordSeparator ) 
		{
			if( aStr.Length == 0 ) return "";
			
			int lWordNumber = 0 , i, lIndex = 0;
			string lCurrentWord = "";

			int lExistSep = aStr.IndexOf( aWordSeparator , 0 );
			if( lExistSep == -1 )
				return aStr;

			do
			{
				i = aStr.IndexOf( aWordSeparator , lIndex );
				if( i > 0 )
				{
					lCurrentWord = aStr.Substring( lIndex , i-lIndex );
					if( ++lWordNumber == aWordNumber ) break;
					lIndex = i + aWordSeparator.Length;
				}
			}while( i>0 && lIndex<aStr.Length );
			
			if( lIndex == -1 && lCurrentWord.Length==0 && aWordNumber==1)
				lCurrentWord = aStr;
			else if( lIndex >= aStr.Length )
				lCurrentWord = "";
				
			return lCurrentWord;
		}

		
		public static int GetMaxWord( string aStr ) 
		{
			if( aStr == "" ) return 0;
			
			int lWordNumber = 1 , i=0;
			char lChar;

			for( i=0 ; i < aStr.Length ; i++ )
			{
				lChar = aStr[i];
				if( lChar == mWordSeparator )
					lWordNumber++;
			}

			return lWordNumber;
		}
		public static int GetMaxWord( string aStr , char aWordSeparator) 
		{
			if( aStr == "" ) return 0;
			
			int lWordNumber = 1 , i=0;
			char lChar;

			for( i=0 ; i < aStr.Length ; i++ )
			{
				lChar = aStr[i];
				if( lChar == aWordSeparator )
					lWordNumber++;
			}

			return lWordNumber;
		}
		public static int GetMaxWord( string aStr , string aWordSeparator ) 
		{
			if( aStr.Length == 0 ) return 0;
			
			int lWordNumber = 0 , i, lIndex = 0;
			string lCurrentWord = "";

			do
			{
				i = aStr.IndexOf( aWordSeparator , lIndex );
				if( i > 0 )
				{
					lCurrentWord = aStr.Substring( lIndex , i-lIndex );
					lWordNumber++;
					lIndex = i + aWordSeparator.Length;
				}
			}while( i>0 && lIndex<aStr.Length );
			
			if( lCurrentWord.Length == 0 )
				lWordNumber++;
				
			return lWordNumber;
		}


		public static bool IsNumeric( string aStr )
		{
			bool lReturn = true;
			for( int i=0 ; i<aStr.Length ; i++ )
				if( !char.IsNumber(aStr,i) )
				{
					lReturn = false;
					break;
				}
			return lReturn;
		}
		public static string RemoveSingleQoutation( string aStr )
		{
			string lReturn = ""; char lChar;
			if( aStr == null ) return lReturn;
			for( int i=0 ; i<aStr.Length ; i++ )
			{
				lChar = aStr[i];
				if( lChar == '\'')
					lReturn += lChar;
				lReturn += lChar;
			}
			return lReturn;
		}

        public static System.Windows.Forms.TreeNode SearchNodeInTree( System.Windows.Forms.TreeNodeCollection aNodes , System.Windows.Forms.TreeNode aNode ) 
		{
			System.Windows.Forms.TreeNode lNode = null;
			
			for( int i=0 ; i<aNodes.Count ; i++ ) 
			{
				if( aNodes[i].Text.Equals( aNode.Text ) )
				{
					lNode = aNodes[i];
					break;
				}
				else
				{
					if( aNodes[i].Nodes.Count > 0 ) 
					{
						lNode = SearchNodeInTree( aNodes[i].Nodes , aNode );
						if( lNode != null )
							break;
					}
				}
			}

			return lNode;
		}
		public static System.Windows.Forms.TreeNode SearchNodeInTree( System.Windows.Forms.TreeNodeCollection aNodes , string aText ) 
		{
			System.Windows.Forms.TreeNode lNode = null;
			int lLen;
			
			for( int i=0 ; i<aNodes.Count ; i++ ) 
			{
				lLen = ( (aNodes[i].Text.Length < aText.Length) ?
					aNodes[i].Text.Length : aText.Length );
				if( aNodes[i].Text.Substring(0,lLen).Equals( aText )  )
				{
					lNode = aNodes[i];
					break;
				}
				else
				{
					if( aNodes[i].Nodes.Count > 0 ) 
					{
						lNode = SearchNodeInTree( aNodes[i].Nodes , aText );
						if( lNode != null )
							break;
					}
				}
				
			}

			return lNode;
		}
		public static System.Windows.Forms.TreeNode SearchNodeInTree( System.Windows.Forms.TreeNodeCollection aNodes , int aTag ) 
		{
			System.Windows.Forms.TreeNode lNode = null;
			
			for( int i=0 ; i<aNodes.Count ; i++ ) 
			{
				if( Convert.ToInt32( aNodes[i].Tag ) == aTag )
				{
					lNode = aNodes[i];
					break;
				}
				else
				{
					if( aNodes[i].Nodes.Count > 0 ) 
					{
						lNode = SearchNodeInTree( aNodes[i].Nodes , aTag );
						if( lNode != null )
							break;
					}
				}
			}

			return lNode;
		}

		public static int SearchInDataGrid( System.Windows.Forms.DataGrid aGrid , string aText , int aCol ) 
		{
			System.Data.DataTable lTbl = (System.Data.DataTable)aGrid.DataSource;
			int lReturn = -1;
			int lLen;

			for( int i=0 ; i<lTbl.Rows.Count ; i++ ) 
			{
				lLen = ( (aGrid[i,aCol].ToString().Length < aText.Length) ?
					aGrid[i,aCol].ToString().Length : aText.Length );
				if( aGrid[i,aCol].ToString().Substring(0,lLen).Equals( aText )  )
				{
					lReturn = i;
					break;
				}
			}

			return lReturn;
		}

        public static void  SelectAllNodes( System.Windows.Forms.TreeNodeCollection aNodes , bool aState ) 
		{
			for( int i=0 ; i<aNodes.Count ; i++ ) 
			{
				if( aNodes[i].Checked != aState )
					aNodes[i].Checked = aState;
				if( aNodes[i].Nodes.Count > 0 )
					SelectAllNodes( aNodes[i].Nodes , aState );
			}
		}
		public static void  SelectLevelNodes( System.Windows.Forms.TreeNodeCollection aNodes , bool aState ) 
		{
			for( int i=0 ; i<aNodes.Count ; i++ ) 
				aNodes[i].Checked = aState;
		}
		public static int   NodesCount( System.Windows.Forms.TreeNodeCollection aNodes ) 
		{
			int lReturn;

			lReturn = aNodes.Count;
			if( lReturn > 0 ) 
			{
				for( int i=0 ; i<aNodes.Count ; i++ ) 
					lReturn += NodesCount( aNodes[i].Nodes );
			}

			return lReturn;
		}
		public static void SelectNodesInvers(System.Windows.Forms.TreeNodeCollection aNodes)
		{
			foreach( TreeNode lNode in aNodes )
			{
				lNode.Checked = !lNode.Checked;
				if (lNode.Nodes.Count > 0)
					SelectNodesInvers(lNode.Nodes);
			}
		}
		
		public static void ExpandLevels( System.Windows.Forms.TreeNodeCollection aNodes , int aLevels ) 
		{
			if( aNodes.Count == 0 ) return;
			for( int i=0 ; i<aNodes.Count ; i++ ) 
			{
				aNodes[i].Expand();
				if( aLevels > 1 )
					ExpandLevels( aNodes[i].Nodes , aLevels-1 );
			}
		}

		public static string MidTrim( string aText ) 
		{
			string lText = "";
			bool lSpace = false;
			char ch;
			int i;

			for( i=0 ; i<aText.Length ; i++ ) 
			{
				ch = aText[i];
				if (ch != 32)
				{
					lSpace = false;
					lText += ch.ToString();
				}
				else
				{
					if (!lSpace && i > 0 && i != (aText.Length - 1))
						lText += " ";
					lSpace = true;
				}
			}

			return lText;
		}
		public static string RemoveCR( string aText ) 
		{
			string lReturn = "";
			char ch;
			int i;

			for( i=0 ; i<aText.Length ; i++ ) 
			{
				ch = aText[i];
				if( !ch.Equals('\n') ) 
					lReturn += ch;
				else
					lReturn += ' ';
			}

			return lReturn;
		}
		public static string MakeChars( char aCh , int aCount ) 
		{
			string lStr = "";
			for( int i=0 ; i<aCount ; i++ ) 
			{
				lStr += aCh.ToString();
			}
			return lStr;
		}
		public static string CompeleteString(string aString,char aCh, int aCount)
		{
			string lStr = aString;
			for (int i = 0; i < (aCount - aString.Length); i++)
			{
				lStr = aCh.ToString() + lStr;
			}
			return lStr;
		}
		public static string CompeleteString(string aString, int aCount)
		{
			string lStr = aString;
			for (int i = 0; i < (aCount - aString.Length); i++)
			{
				lStr = " " + lStr;
			}
			return lStr;
		}
		public static string RemoveChars(string aStr, char aChar) 
		{
			string lReturn = "";
			int lLen = aStr.Length;

			for( int i=0 ; i<lLen ; i++ ) 
			{
				if( aStr[i] != aChar )
					lReturn += aStr[i].ToString();
			}

			return lReturn;
		}

		public static string Dec2Hex( int aDec ) 
		{
			string lHex = "";
			int lDec = aDec, lRem;

			while( lDec > 0 ) 
			{
				lRem = lDec % 16;
				if( lRem < 10 )
					lHex = lRem.ToString() + lHex;
				else if( lRem < 16 )
					lHex = ((char)(lRem + 55)).ToString() + lHex;
				lDec = (int)(System.Decimal.Truncate(lDec / 16));
			}
			return lHex;
		}
		public static string Str2Hex( string lStr ) 
		{
			int lCode;
			string lHex, lReturn = "";
			byte[] lBytes;

			lBytes = System.Text.UTF8Encoding.Default.GetBytes(lStr);
			int lLen = lBytes.GetLength(0);
			for( int i=0 ; i<lLen ; i++ ) 
			{
				lCode	= Convert.ToInt32( lBytes.GetValue(i) );
				if( lCode < 128 )
				{
					lHex	= lStr[i].ToString();
					lReturn	+= lHex;
				}
				else
				{
					lHex	= CStaticFunctions.Dec2Hex( lCode );
					if( lHex.Length > 0 )
						lReturn	+= "\\'" + lHex;
				}
							}

			return lReturn;
		}

		public static int SearchInArray2D(Array aArray, string aValue, int aCol)
		{
			if (aArray == null) return -1;
			int lLen = aArray.GetLength(aCol);
			string lValue = "";
			int lReturn = -1;

			for (int i = 0; i < lLen; i++)
			{
				lValue = aArray.GetValue(i, aCol).ToString();
				if (lValue.Equals(aValue))
				{
					lReturn = i;
					break;
				}
			}

			return lReturn;
		}

		public static System.Collections.ArrayList GetDirectoryFiles( string aPath , string aFileType ) 
		{
			System.Collections.ArrayList lFileList = new System.Collections.ArrayList();
			string[] lFiles;

			lFileList.Clear();
			try
			{
				lFiles = Directory.GetFiles( aPath ,  aFileType );
				foreach( string lFile in lFiles ) 
					lFileList.Add( lFile );
			}
			catch
			{
			}

			return lFileList;
		}

		public static System.Windows.Forms.ListViewItem SearchItemInListView(System.Windows.Forms.ListView.ListViewItemCollection aItems, int aTag)
		{
			ListViewItem lReturn = null;

			foreach (ListViewItem lItem in aItems)
			{
				if (Convert.ToInt32(lItem.Tag) == aTag)
				{
					lReturn = lItem;
					break;
				}
			}

			return lReturn;
		}
		public static System.Windows.Forms.ListViewItem SearchItemInListView(System.Windows.Forms.ListView.ListViewItemCollection aItems, string aText)
		{
			ListViewItem lReturn = null;

			foreach (ListViewItem lItem in aItems)
			{
				if (lItem.Text == aText)
				{
					lReturn = lItem;
					break;
				}
			}

			return lReturn;
		}

		public static int SearchInCombo(ComboBox aCombo, string aText)
		{
			int lReturn = -1;

			string lText;
			for (int i = 0; i < aCombo.Items.Count; i++)
			{
				lText = aCombo.Items[i].ToString().Substring(0,aText.Length);
				if( lText == aText )
				{
					lReturn = i;
					break;
				}
			}

			return lReturn;
		}
		public static int SearchInCombo(ComboBox aCombo)
		{
			int lReturn = -1;

			string lText, lFullText, lSearchText;
			string lComboText = aCombo.Text;
			if (lComboText != "")
			{
				for (int i = 0; i < aCombo.Items.Count; i++)
				{
					lSearchText = lComboText;
					if (aCombo.DisplayMember == "")
						lFullText = aCombo.Items[i].ToString();
					else
						lFullText = ((DataRowView)(aCombo.Items[i]))[aCombo.DisplayMember].ToString();
					
					if (lFullText.Length > lSearchText.Length)
					{
						lText = lFullText.Substring(0, lSearchText.Length);
						if (lText == lSearchText)
						{
							aCombo.Text = lFullText;
							aCombo.Select(lSearchText.Length, lFullText.Length - lSearchText.Length);
							lReturn = i;
							break;
						}
					}
				}
			}
			return lReturn;
		}
		public static int SearchInComboDataSource(ComboBox aCombo)
		{
			int lReturn = -1;

			string lText, lFullText, lSearchText;
			lSearchText = aCombo.Text;
			if( lSearchText.Length > 0 )
				for (int i = 0; i < aCombo.Items.Count; i++)
				{
					lFullText = ((DataTable)(aCombo.DataSource)).Rows[i].ItemArray.GetValue(1).ToString();
					if (lSearchText.Length > lFullText.Length) continue;
					lText = lFullText.Substring(0, lSearchText.Length);
					if (lText == lSearchText)
					{
						aCombo.Text = lFullText;
						aCombo.Select(lSearchText.Length, lFullText.Length - lSearchText.Length);
						lReturn = i;
						break;
					}
				}

			return lReturn;
		}

		public static Control GetFocusedControl(Control.ControlCollection aControls ) 
		{
			Control lReturn = null;

			string lCtl_Type;
			foreach (Control lCtl in aControls)
			{
				lCtl_Type = lCtl.GetType().Name;
				if (lCtl.Focused)
				{
					lReturn = lCtl;
					break;
				}
				if (lCtl.Controls.Count > 0)
				{
					lReturn = GetFocusedControl(lCtl.Controls);
					if (lReturn != null)
					{
						if (lCtl_Type != "GroupBox" && lCtl_Type != "Panel" && lCtl_Type != "FlowLayoutPanel")
							lReturn = lCtl;
						break;
					}
				}
			}

			return lReturn;
		}		

		#endregion

		#region Grid Methods

		public static void MakeTableColumn( DataTable aTable , string aColumnName , System.Type aColumnType )
		{
			DataColumn idColumn = new  DataColumn();
			idColumn.DataType = aColumnType;
			idColumn.ColumnName = aColumnName;
			aTable.Columns.Add(idColumn);
		}
		public static void MakeTableColumn( DataTable aTable , string aColumnName , System.Type aColumnType , string aColumnCaption )
		{
			DataColumn idColumn = new  DataColumn();
			idColumn.DataType = aColumnType;
			idColumn.ColumnName = aColumnName;
			idColumn.Caption = aColumnCaption;
			aTable.Columns.Add(idColumn);
		}
		public static void CreateNewDataRow( DataTable aTable , string[] aFields )
		{
			if( aFields.GetLength(0) != aTable.Columns.Count ) return;

			DataRow lRow;
			lRow = aTable.NewRow();
			// Then add the new row to the collection.
			for( int i=0 ; i<aTable.Columns.Count ; i++ )
			{
				lRow[i] = aFields.GetValue(i);
			}
			aTable.Rows.Add(lRow);
		}
		public static void MakeDataRelation( DataTable aTable , DataColumn aParentColumn , DataColumn aChildColumn )
		{
			DataRelation lDataRelation;
			lDataRelation = new DataRelation("Parent2Child", aParentColumn, aChildColumn);
			aTable.ParentRelations.Add(lDataRelation);
		}
		private void BindToDataGrid( DataSet aDataSet , System.Windows.Forms.DataGrid aDataGrid , string aParentTable )
		{
			aDataGrid.SetDataBinding( aDataSet , aParentTable );
		}
		public static void DataGridSelectRow( System.Windows.Forms.DataGrid aGrid , int aID ) 
		{
			DataTable lTbl = (DataTable)aGrid.DataSource;
			int lRows = lTbl.Rows.Count;
			for( int lComID = 0 ; lComID <lRows ; lComID++ ) 
			{
				if( Convert.ToInt32( aGrid[lComID,0] ) == aID ) 
				{
					if( aGrid.CurrentRowIndex > -1 )
						aGrid.UnSelect(aGrid.CurrentRowIndex);
					aGrid.CurrentRowIndex = lComID;
					aGrid.Select( lComID );
					break;
				}
			}
		}
        public static void ClearDataGrid( System.Windows.Forms.DataGrid aGrid )
		{
			System.Data.DataTable lTbl = new System.Data.DataTable();
			aGrid.DataSource = lTbl;
		}

		public static int SelectRow(DataGridView aGridView, int aRowIndex , int aCurrentCellIndex)
		{
			int lReturn;
			if (aGridView.Rows.Count > 0)
			{
				if (aRowIndex > (aGridView.Rows.Count - 1)) aRowIndex = aGridView.Rows.Count - 1;
				aGridView.Rows[aRowIndex].Selected = true;
				//aGridView.Rows[aRowIndex].Cells[aCurrentCellIndex].Selected = true;
				try
				{
					aGridView.CurrentCell = aGridView.Rows[aRowIndex].Cells[aCurrentCellIndex];
				}
				catch
				{
				}
				aGridView.CurrentRow.Selected = true;
				lReturn = aRowIndex;
			}
			else
				lReturn = -1;
			return lReturn;
		}
		public static int SelectRow(DataGridView aGridView, int aRowIndex , string aCellName)
		{
			int lReturn;
			if (aGridView.Rows.Count > 0)
			{
				if (aRowIndex > (aGridView.Rows.Count - 1)) aRowIndex = aGridView.Rows.Count - 1;
				aGridView.Rows[aRowIndex].Selected = true;
				aGridView.CurrentCell = aGridView.Rows[aRowIndex].Cells[aCellName];
				//aGridView.CurrentRow.Selected = true;
				lReturn = aRowIndex;
			}
			else
				lReturn = -1;
			return lReturn;
		}

		#endregion

		#region Public Static Events & Form Functions
		public static void SetSimpleComboKeyDownEvents(Control aParent, string aExcept)
		{
			if (aParent.Controls == null) return;
			if (aExcept != "")
			{
				if (aExcept.Substring(0, 1) != ",")
					aExcept = "," + aExcept;

				if (aExcept.Substring(0, aExcept.Length - 1) != ",")
					aExcept += ",";
			}

			foreach (Control lCtl in aParent.Controls)
			{
				string lCtlName = lCtl.Name;
				string lCtlType = lCtl.GetType().Name.ToLower();
				if (lCtlType == "combobox" && aExcept.IndexOf("," + lCtlName + ",") == -1)
					if (((ComboBox)lCtl).DropDownStyle != ComboBoxStyle.DropDown)
						lCtl.KeyDown += new KeyEventHandler(cmbSimple_KeyDown);
			}
		}
		public static void cmbSimple_KeyDown(object sender, KeyEventArgs e)
		{
			ComboBox lCombo = (ComboBox)sender;
			if (e.KeyCode == Keys.Delete)
				lCombo.SelectedIndex = -1;
		}
		#endregion
	}
}
