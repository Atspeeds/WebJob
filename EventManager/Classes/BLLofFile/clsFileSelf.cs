using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace Behin
{
	public class clsFileSelf
	{
		string StrCon1;

		public struct strcFileSelf
		{
			public int NoEntity;
			public int NoFileType;
			public int NoFileLocation;
			public byte[] Stream;
			public string Name;

		} //End Struct

		public clsFileSelf()
		{

			//Configuration config = new Configuration("Web.config");
			//StrCon1 = FormBuilder.Properties.Settings.Default.StrCon;
			Configuration config = new Configuration("app.conf");
			StrCon1 = config.GetConnectionString("BehinCnn");

		}  //End Behin Function

		public strcFileSelf GetNewRow()
		{
			return new strcFileSelf();

		}//End GetNewRow Function


		public int Insert(strcFileSelf myStruct)
		{
			//File.File DAinsrt = new File.File(StrCon1);
			//File.File_DS.DS_FileRow dtRow = new File.File_DS.DS_FileDataTable().NewDS_FileRow();

			//if (myStruct.NoEntity != null)
			//    dtRow.NoEntity = myStruct.NoEntity;
			//if (myStruct.NoFileType != null)
			//    dtRow.NoFileType = myStruct.NoFileType;
			//if (myStruct.NoFileLocation != null)
			//    dtRow.NoFileLocation = myStruct.NoFileLocation;
			//if (myStruct.Stream != null)
			//    dtRow.Stream = myStruct.Stream;
			//if (myStruct.Name != null)
			//    dtRow.Name = myStruct.Name;
			GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);

			string Qry = "set identity_insert [File] ON \r\n Insert Into [File] (NoEntity,Name,NoFileType,NoFileLocation,Stream) values (" +
			             myStruct.NoEntity + ",'" + myStruct.Name + "'," + myStruct.NoFileType +
						 "," + myStruct.NoFileLocation + ",cast('" + myStruct.Stream + "' As varbinary(max))) \r\n set identity_insert [File] OFF";
			DAC.GeneralInsertCommand(new SqlCommand(Qry));
			//return DAinsrt.DS_FileDataTable_InsertCommand(dtRow);
			return 1;
		}  //End Insert Function

		public int GetNoFile()
		{
			// شمارنده فایل در برنامه منفی است و وقتی در اختیار کاربر قرار میگیرد مثبت میشود.
			int No = -2;
			string Qry = "";

			Qry = "SELECT MIN(NoEntity) FROM [File] where NoEntity < 0";
			try
			{
				GeneralDataAccess.GeneralDataAccessClass DAC = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
				No = int.Parse(DAC.GeneralSelectCommand(new SqlCommand(Qry)).Rows[0][0].ToString()) - 1;
			}
			catch
			{ }

			return No;
		}

		public int SaveFile(string FileName, int NoFile, string FilePath)
		{
			int NoFileLoc = 0;
			Behin.clsFile objFile = new Behin.clsFile();
			Behin.clsFile.strcFile myRowFile = new Behin.clsFile.strcFile();

			myRowFile.Name = FileName;
			//myRowFile.Stream = FileByte;
			string FileLocation = "Images/Icons";
			NoFileLoc = Insert(myRowFile, FileLocation, NoFile, FilePath);
			return NoFileLoc;
		}

		public int Insert(Behin.clsFile.strcFile myRowFile, string FileLocation, int NoFile, string FilePath)
		{
			//Insert
			clsFile objFile = new clsFile();
			clsFileLocation objFileLocation = new clsFileLocation();
			string Extension = System.IO.Path.GetExtension(myRowFile.Name);
			Byte[] FileByte = myRowFile.Stream;
			clsFileLocation.strcFileLocation myRowFileLocation = new clsFileLocation.strcFileLocation();
			myRowFileLocation.Name = FileLocation;
			int NoFileLocation = InsertFileLocation("insert into FileLocation (name) values ('" + FileLocation + "')SELECT SCOPE_IDENTITY()");
			//myRowFile.NoFileLocation = NoFileLocation;
			//myRowFile.Stream = new byte[0];
			//int NoFile = objFile.Insert(myRowFile);
			myRowFileLocation.No = NoFileLocation;
			myRowFileLocation.Name += "/ico" + NoFile + Extension;
			UpdateFileLocation("update FileLocation set name ='" + myRowFileLocation.Name + "' where no =" + NoFileLocation);
			//Save To Location
			if (FilePath != "")
			{
				//string path = System.Web.HttpContext.Current.Server.MapPath(".") + "/" + FileLocation + "/" + NoFile;
				System.IO.FileInfo lFI = new System.IO.FileInfo(Application.ExecutablePath);
				System.IO.Directory.SetCurrentDirectory(lFI.DirectoryName);
				if (System.IO.File.Exists(lFI.Directory.ToString().Substring(0, lFI.Directory.ToString().Length - 10) + "\\" + myRowFileLocation.Name) == true)
					System.IO.File.Delete(lFI.Directory.ToString().Substring(0, lFI.Directory.ToString().Length - 10) + "\\" + myRowFileLocation.Name);
				System.IO.File.Copy(FilePath, lFI.Directory.ToString().Substring(0, lFI.Directory.ToString().Length - 10) + "\\" + myRowFileLocation.Name);
				//System.IO.File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath(".") + "/" + FileLocation + "/" + NoFile + Extension, FileByte);
			}
			return NoFileLocation;
		}

		public void UpdateFile(int NoFile, string FilePath)
		{
			Behin.clsFile objFile = new Behin.clsFile();
			Behin.clsFileLocation objFileLocation = new Behin.clsFileLocation();
			System.IO.FileInfo lFI = new System.IO.FileInfo(Application.ExecutablePath);
			System.IO.Directory.SetCurrentDirectory(lFI.DirectoryName);

			DataTable lstfile = objFile.Select("NoEntity", NoFile.ToString());
			if (lstfile.Rows.Count > 0)
			{
				List<Behin.clsFileLocation.strcFileLocation> lstfileloc = objFileLocation.Select("No", lstfile.Rows[0]["NoFileLocation"].ToString());
				if (lstfileloc.Count > 0)
				{
					if (System.IO.File.Exists(lFI.Directory.ToString().Substring(0, lFI.Directory.ToString().Length - 10) + "\\" + lstfileloc[0].Name) == true)
						System.IO.File.Delete(lFI.Directory.ToString().Substring(0, lFI.Directory.ToString().Length - 10) + "\\" + lstfileloc[0].Name);
					if (FilePath != "")
					{
						System.IO.File.Copy(FilePath, lFI.Directory.ToString().Substring(0, lFI.Directory.ToString().Length - 10) + "\\" + lstfileloc[0].Name);
					}
				}
			}
		}

		public DataTable RunCommand(string SqlCommand)
		{
			DataTable dt = new DataTable();

			GeneralDataAccess.GeneralDataAccessClass clsGDA = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand(SqlCommand);
			dt =clsGDA.GeneralSelectCommand(sqlcmd);
			return dt;
		}

		public int InsertFileLocation(string SqlCommand)
		{
			GeneralDataAccess.GeneralDataAccessClass clsGDA = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand(SqlCommand);
			int NoFileLocation = int.Parse(clsGDA.GeneralSelectCommand(new SqlCommand(SqlCommand)).Rows[0][0].ToString());

			return NoFileLocation;
		}

		public void UpdateFileLocation(string SqlCommand)
		{
			GeneralDataAccess.GeneralDataAccessClass clsGDA = new GeneralDataAccess.GeneralDataAccessClass(StrCon1);
			System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand(SqlCommand);
			clsGDA.GeneralUpdateCommand(new SqlCommand(SqlCommand));

		}
		//Methods


	}  //End Class

}  //End namespace