using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;

namespace DNMReport.Util
{
    public class ServiceFtp : IFileService
    {
        NetworkCredential credential = null;
        int timeout = 7000;
        string uriPath = string.Empty;
        public ServiceFtp()
        {
        }
        public ServiceFtp(string user, string pw, int timeout_, string uri_)
        {
            credential = new NetworkCredential(user, pw);
            UriPath = uri_;
            timeout = timeout_;
        }

        public string UriPath
        {
            get { return uriPath; }
            set
            {
                string temp = value.ToUpper();
                if (temp.IndexOf("FTP") != 0)
                    uriPath = temp + @"ftp://" + value;
                else
                    uriPath = value;
                if (uriPath.LastIndexOf('/') < uriPath.Length - 1)
                    uriPath += "/";
            }
        }
        

        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }
        public NetworkCredential Credntial
        {
            get { return credential; }
            set { credential = value; }
        }
        public DriveType ConnectionDrive
        { get { return DriveType.FTP; } }
        public MemoryStream ReadFile(string fileName, string folder = null)
        {
            if (fileName == null || fileName == string.Empty || fileName == "")
                throw new FileNotFoundException("Read File, File name null");
            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder + "/";
            strFTPFileFolder += fileName;
            Uri uri = new Uri(strFTPFileFolder);

            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;

                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    Stream stream = resFtp.GetResponseStream();
                    MemoryStream memStream = new MemoryStream();
                    stream.CopyTo(memStream);
                    resFtp.Close();
                    return memStream;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "WriteFile, " + ex.Message);
                }
            }
        }
        public FileInfo ReadFileInfo(string fileName, string folder = null)
        {
            return null;
        }
        public void WriteFile(string fileName, MemoryStream stream, string folder = null)
        {
            if (fileName == null || fileName == string.Empty || fileName == "")
                throw new IOException("Write File, File name null");
            if (stream == null)
                throw new IOException("Write File, File Stream null");

            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder + "/";
            strFTPFileFolder += fileName;
            Uri uri = new Uri(strFTPFileFolder);

            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            byte[] fileContents = stream.ToArray();
            reqFtp.ContentLength = fileContents.Length;
            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;

                    Stream requestStream = reqFtp.GetRequestStream();
                    requestStream.Write(fileContents, 0, fileContents.Length);
                    requestStream.Close();
                    FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                    if (response.StatusCode != FtpStatusCode.CommandOK)
                    {
                        if (response.StatusDescription.IndexOf("226") < 0)
                        {
                            string msg = response.StatusDescription;
                            response.Close();
                            throw new IOException("Write File, " + msg);
                        }
                    }
                    response.Close();
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "WriteFile, " + ex.Message);
                }
            }
        }

        public void WriteFileDateTime(string fileName, DateTime time, string folder = null)
        {
            throw new FileNotFoundException("WriteFileDateTime, Ftp not support");
        }
        public List<FileInfo> GetFileList(string folder = null, string extenstionFilter = null)
        {
            List<FileInfo> files = new List<FileInfo>();

            
            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder + "/";

            Uri uri = new Uri(strFTPFileFolder);
            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;
                    
                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    StreamReader reader = new StreamReader(resFtp.GetResponseStream());
                    string strData = reader.ReadToEnd();
                    resFtp.Close();
                    string[] filesInDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strName in filesInDirectory)
                    {
                        FileInfo info = new FileInfo();
                        if (info.From(strName) == true)
                        {
                            if (!info.IsFolder)
                            {
                                if (extenstionFilter == null || extenstionFilter == string.Empty)
                                {
                                    files.Add(info);
                                }
                                else
                                {
                                    if (info.Extension.ToUpper() == extenstionFilter.ToUpper())
                                        files.Add(info);
                                }
                            }
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "GetFileList, " + ex.Message);
                }
            }
            return files;
        }
        public void ChangFileName(string name, string toName, string folder = null)
        {
            if (name == null || name == string.Empty || name == "" || toName == null || toName == string.Empty || toName == "")
                throw new FileNotFoundException("Change File Name, Name null");

            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder + "/";

            strFTPFileFolder += name;

            Uri uri = new Uri(strFTPFileFolder);
            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.Rename;
            reqFtp.RenameTo = toName;

            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;
                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    if (resFtp.StatusCode != FtpStatusCode.CommandOK)
                    {
                        string msg = resFtp.StatusDescription;
                        resFtp.Close();
                        throw new IOException("Chang File Name, " + msg);
                    }
                    resFtp.Close();
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "ChangFileName, " + ex.Message);
                }
            }           
        }
        public void DeleteFile(string name, string folder = null)
        {
            if (name == null || name == string.Empty || name == "")
                throw new FileNotFoundException("Delete File, Name null");
            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder + "/";

            strFTPFileFolder += name;

            Uri uri = new Uri(strFTPFileFolder);
            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;

            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;
                    
                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    if (resFtp.StatusCode != FtpStatusCode.CommandOK)
                    {
                        string msg = resFtp.StatusDescription;
                        resFtp.Close();
                        throw new IOException("Delete File, " + msg);
                    }
                    resFtp.Close();
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "DeleteFile, " + ex.Message);
                }
            }
        }
        public void DeleteFolder(string folder)
        {
            if (folder == null || folder == string.Empty || folder == "")
                throw new DirectoryNotFoundException("Delete Folder, Name null");
            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder;
            Uri uri = new Uri(strFTPFileFolder);
            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.RemoveDirectory;

            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;
                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    if (resFtp.StatusCode != FtpStatusCode.CommandOK)
                    {
                        string msg = resFtp.StatusDescription;
                        resFtp.Close();
                        throw new IOException("Delete Folder, " + msg);
                    }
                    resFtp.Close();
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "DeleteFolder, " + ex.Message);
                }
            }
        }
        public void CreateFolder(string folder)
        {
            if (folder == null || folder == string.Empty || folder == "")
                throw new IOException("Create Folder, Name null");
            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder;
            Uri uri = new Uri(strFTPFileFolder);
            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.MakeDirectory;

            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;
                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    if (resFtp.StatusCode != FtpStatusCode.CommandOK)
                    {
                        string msg = resFtp.StatusDescription;
                        resFtp.Close();
                        throw new IOException("Create Folder, " + msg);
                    }
                    resFtp.Close();
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "GetFolderList, " + ex.Message);
                }
            }
        }

        public List<string> GetFolderList()
        {
            List<string> folders = new List<string>();
            Uri uri = new Uri(uriPath);
            FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
            reqFtp.Credentials = credential;
            reqFtp.Timeout = timeout;
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            int retryCount = 2;
            while (true)
            {
                try
                {
                    retryCount--;
                    FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                    StreamReader reader = new StreamReader(resFtp.GetResponseStream());
                    string strData = reader.ReadToEnd();
                    resFtp.Close();
                    string[] filesInDirectory = strData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string strName in filesInDirectory)
                    {
                        FileInfo info = new FileInfo();
                        if (info.From(strName) == true)
                        {
                            if (info.IsFolder)
                                folders.Add(info.Name);
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (retryCount <= 0)
                        throw ex;
                    SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "GetFolderList, " + ex.Message);
                }
            }
            return folders;
        }
        private string getExtension(string fileName)
        {
            string str = string.Empty;
            try
            {
                if (fileName == string.Empty || fileName == "")
                    return string.Empty;
                int index = fileName.LastIndexOf('.');
                if (index >= 0)
                    str = fileName.Substring(index + 1);
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
            return str;
        }
 
        public bool UriPathExist(string folder = null)
        {
            string strFTPFileFolder = uriPath;
            if (folder != string.Empty && folder != null && folder != "")
                strFTPFileFolder += folder + "/";

            Uri uri = new Uri(strFTPFileFolder);
            try
            {
                FtpWebRequest reqFtp = WebRequest.Create(uri) as FtpWebRequest;
                reqFtp.Credentials = credential;
                reqFtp.Timeout = timeout;
                reqFtp.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
                StreamReader reader = new StreamReader(resFtp.GetResponseStream());
                resFtp.Close();
                return true;

            }
            catch (Exception ex)
            {
                SystemLog.Output(SystemLog.MSG_TYPE.Err, "ServiceFTP", "UriPathExist, " + ex.Message);
                return false;
            }
        }
    }
        
}
