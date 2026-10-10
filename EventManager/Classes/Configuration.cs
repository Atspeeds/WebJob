using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Behin
{
    public class Configuration
    {
        #region variables

        List<ConnectionData> connections = new List<ConnectionData>();
        XmlDocument appConfDocument;

        #endregion

        #region properties

        public List<ConnectionData> Connections
        {
            get
            {
                return connections;
            }
        }

        #endregion

        #region methods

        public  Configuration(string path)
        {
            try
            {
                appConfDocument = new XmlDocument();
                appConfDocument.Load(System.Windows.Forms.Application.StartupPath + "\\" + path);
                LoadConnections();
            }
            catch(Exception ex)
            {
                string F = ex.Message;
            }
        }
        
        private void LoadConnections()
        {
            char[] trimChar = new char[2]{ '\r', '\n' };


            XmlNodeList nodes = appConfDocument.DocumentElement.SelectSingleNode("connectionStrings")
                .SelectNodes("add");

            for (int indx = 0;indx < nodes.Count; indx++)
            {
                ConnectionData cnnData = new ConnectionData();
                cnnData.ConnectionName = nodes[indx].Attributes["name"].Value.ToString();
                cnnData.ConnectionString = nodes[indx].Attributes["connectionString"].Value.ToString();
				string dsource = cnnData.ConnectionString.Split(';')[0];
				dsource = dsource.Split('=')[1];
				cnnData.DataSource = dsource;
				dsource = cnnData.ConnectionString.Split(';')[1];
				dsource = dsource.Split('=')[1];
				cnnData.IntialCatalog = dsource;
				dsource = cnnData.ConnectionString.Split(';')[2];
				dsource = dsource.Split('=')[1];
				cnnData.UserID = dsource;
				dsource = cnnData.ConnectionString.Split(';')[3];
				dsource = dsource.Split('=')[1];
				cnnData.Password = dsource;

                connections.Add(cnnData);
            }
        }
		public void WriteFile(string servername,string database,string Userid,string Password)
		{
			if (System.IO.File.Exists("app.conf") == true)
			{
				//XmlNodeList nodes = appConfDocument.DocumentElement.SelectSingleNode("connectionStrings")
				//                    .SelectNodes("add");

				string strWrite = "";

				strWrite ="<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + 
					      "\r\n <configuration> \r\n <connectionStrings>";
				//for (int indx = 0; indx < connections.Count; indx++)
				//{
				strWrite += "\r\n <add name=\"BehinCnn\" connectionString=\"Data Source=" + servername + ";Initial Catalog=" + database + ";User ID=" + Userid + ";Password=" + Password + ";Asynchronous Processing=true\"" +
                            "\r\nproviderName=\"System.Data.SqlClient\"/>" +
							"\r\n<add name=\"BehinBaseDB\" connectionString=\"Data Source=" + servername + ";Initial Catalog=BehinBaseDB;User ID=" + Userid + ";Password=" + Password + "\"" +
                            "\r\nproviderName=\"System.Data.SqlClient\"/>";
				//}
				strWrite += "\r\n</connectionStrings> \r\n </configuration>";

				StreamWriter lFs = new StreamWriter("app.conf");
				lFs.Write(strWrite);
				lFs.Close();
			}
		}
        public string GetConnectionString(string connectionName)
        {
            string resconnectionString="";
            foreach (ConnectionData cnnData in Connections)
            {
                if (cnnData.ConnectionName == connectionName)
                {
                    resconnectionString = cnnData.ConnectionString;
                    break;
                }
                
            }
            return resconnectionString;
        }

        //static public string GetGeneralDataAccssClassPath()
        //{
        //    string addrss = String.Empty;
        //    XmlNode node = null;
        //    try
        //    {
        //        node = appConfDocument.DocumentElement.SelectSingleNode("dlls")
        //            .SelectSingleNode("GDA");
        //        addrss = node.Attributes["path"].Value.ToString();
        //    }
        //    catch
        //    {

        //    }
        //    return addrss;
        //}

        #endregion
    }

    
    public class ConnectionData
    {
        #region variables

        string connectionName;
        string dataSource;
        string intialCatalog;
        //string integratedSecurity;
        string connectionstring;
		string userid;
		string password;
        #endregion


        #region properties
        
        //public string ConnectionString
        //{
        //    set 
        //    {
        //        connectionstring = value;
        //    }
        //    get
        //    {
        //        return connectionstring;
        //    }
        //}

        public string ConnectionName
        {
            set
            {
                connectionName = value;
            }
            get
            {
                return connectionName;
            }
        }

		public string IntialCatalog
		{
			set
			{
				intialCatalog = value;
			}
			get
			{
				return intialCatalog;
			}
		}
		public string UserID
		{
			set
			{
				userid = value;
			}
			get
			{
				return userid;
			}
		}
		public string Password
		{
			set
			{
				password = value;
			}
			get
			{
				return password;
			}
		}
        //public string IntegratedSecurity
        //{
        //    set
        //    {
        //        integratedSecurity = value;
        //    }
        //    get
        //    {
        //        return integratedSecurity;
        //    }
        //}

        public string ConnectionString
        {
            get
            {
                //return ("Data Source=" + dataSource + ";Initial Catalog=" + intialCatalog + ";Integrated Security=" + integratedSecurity);
                return connectionstring;
            }
            set
            {
                connectionstring = value;
            }
        }
        public string DataSource
        {
            get
            {
                //return ("Data Source=" + dataSource + ";Initial Catalog=" + intialCatalog + ";Integrated Security=" + integratedSecurity);
                return dataSource;
            }
            set
            {
                dataSource = value;
            }
        }

        #endregion

        #region methods
        public ConnectionData()
        {
            connectionName = "";

            dataSource = "";
			userid = "";
			password = "";
            //intialCatalog = "";
            //integratedSecurity = "";

        }
        #endregion
    }
}
