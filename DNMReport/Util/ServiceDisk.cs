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
    public class ServiceDisk : IFileService
    {
        NetworkCredential credential = null;
        int timeout = 7000;
        string uriPath = string.Empty;
        public ServiceDisk()
        {
        }
        public ServiceDisk(string _rootPath)
        {
            UriPath = _rootPath;
        }
        public ServiceDisk(string user, string pw, string _rootPath)
        {
            credential = new NetworkCredential(user, pw);
            UriPath = _rootPath;
        }

        public string UriPath
        {
            get { return uriPath; }
            set
            {
                if (value == null || value == string.Empty || value == "")
                    return;
                uriPath = value;
                if ((uriPath.LastIndexOf('\\') >= uriPath.Length - 2) == false)
                    uriPath += "\\";
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
        { get { return DriveType.Disk; } }
        public MemoryStream ReadFile(string fileName, string folder = null)
        {
            if (fileName == null || fileName == string.Empty || fileName == "")
                throw new FileNotFoundException("Read File, File name null");
            string strFolder = (folder != null && folder != string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            strFolder += fileName;
            StreamReader reader = new StreamReader(strFolder);
            byte[] data = null;
            MemoryStream memStream = null;
            try
            {
                Stream stream = reader.BaseStream;
                stream.Position = 0;
                data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                memStream = new MemoryStream(data);
            }
            finally
            {
                reader.Close();
            }
            return memStream;
        }
        public FileInfo ReadFileInfo(string fileName, string folder = null)
        {
            if (fileName == null || fileName == string.Empty || fileName == "")
                throw new FileNotFoundException("Read File, File name null");
            string strFolder = (folder != null && folder != string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            strFolder += fileName;
            FileInfo fileInfo = null;
            try
            {
                System.IO.FileInfo info = new System.IO.FileInfo(strFolder);
                if (info.Exists == false)
                    return null;
                fileInfo = new FileInfo();
                fileInfo.FullName = info.Name;
                fileInfo.LastWriteTime = info.LastWriteTime;
                fileInfo.Size = (int)info.Length;
            }
            catch(Exception)
            {
                fileInfo = null;
            }

            return fileInfo;
        }
        public void WriteFile(string fileName, MemoryStream stream, string folder = null)
        {
            if (fileName == null || fileName == string.Empty || fileName == "")
                throw new IOException("Write File, File name null");
            if (stream == null)
                throw new IOException("Write File, File Stream null");
            string strFolder = (folder != null && folder != string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;

            DirectoryInfo info = new DirectoryInfo(strFolder);
            if (info.Exists == false)
            {
                info.Create();
                SystemLog.Output(SystemLog.MSG_TYPE.War, "ServiceDisk", "WriteFile, Make new folder, " + strFolder);
            }
                
            string strFullPath = strFolder + fileName;
            FileStream writer = new FileStream(strFullPath, FileMode.Create);
            try
            {
                stream.WriteTo(writer);
            }
            catch(Exception ex)
            {
                throw new FileNotFoundException("WriteFile, " + ex.Message);
            }
            finally
            {
                writer.Close();
                stream.Close();
            }
        }
        public void WriteFileDateTime(string fileName, DateTime time, string folder = null)
        {
            if (fileName == null || fileName == string.Empty || fileName == "")
                throw new FileNotFoundException("WriteFileDateTime, File name null");
            string strFolder = (folder != null && folder != string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            strFolder += fileName;
            try
            {
                System.IO.FileInfo info = new System.IO.FileInfo(strFolder);
                if (info.Exists == false)
                    return;
                info.LastWriteTime = time;
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException("WriteFileDateTime, " + ex.Message);
            }
        }
        public List<FileInfo> GetFileList(string folder = null, string extenstionFilter = null)
        {
            string strFolder = (folder != null && folder != string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            DirectoryInfo info = new DirectoryInfo(strFolder);
            if (info.Exists == false)
            {
                info.Create();
                SystemLog.Output(SystemLog.MSG_TYPE.War, "ServiceDisk", "GetFileList, Make new folder, " + strFolder);
                return new List<FileInfo>();
            }
            List<FileInfo> files = new List<FileInfo>();
            foreach (var file in info.EnumerateFiles())
            {
                FileInfo fi = new FileInfo();
                if (extenstionFilter != null && extenstionFilter != string.Empty)
                {
                    string str = file.Extension.Substring(1);
                    if (str.ToUpper() != extenstionFilter.ToUpper())
                        continue;
                }
                fi.IsFolder = false;
                fi.FullName = file.Name;
                fi.Size = (int)file.Length;
                fi.LastWriteTime = file.LastWriteTime;
                files.Add(fi);
            }
            return files;
        }
        public void ChangFileName(string name, string toName, string folder = null)
        {
            if (name == null || name == string.Empty || name == "" || toName == null || toName == string.Empty || toName == "")
                throw new FileNotFoundException("Change File Name, Name null");

            string strFolder = (folder != null && folder == string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            string sourceName = strFolder + name;
            string newName = strFolder + toName;
            if (File.Exists(sourceName) == false)
                throw new FileNotFoundException("Change File Name, Nof found file : " + name);
            File.Move(sourceName, newName);
        }
        public void DeleteFile(string name, string folder = null)
        {
            if (name == null || name == string.Empty || name == "")
                throw new FileNotFoundException("Delete File, Name null");

            string strFolder = (folder != null && folder == string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            strFolder += name;
            if (File.Exists(strFolder) == false)
                throw new DirectoryNotFoundException("Delete File, Not found file : " + name);
            File.Delete(strFolder);
        }
        public void DeleteFolder(string folder)
        {
            if (folder == null || folder == string.Empty || folder == "")
                throw new DirectoryNotFoundException("Delete Folder, Name null");

            string strFolder = uriPath + folder;
            DirectoryInfo info = new DirectoryInfo(strFolder);
            if (info.Exists == false)
                throw new DirectoryNotFoundException("Delete Folder, Not found folder : " + folder);
            info.Delete();
        }
        public void CreateFolder(string folder)
        {
            if (folder == null || folder == string.Empty || folder == "")
                throw new IOException("Create Folder, Name null");

            string strFolder = uriPath + folder;
            DirectoryInfo info = new DirectoryInfo(strFolder);
            if (info.Exists == false)
                throw new DirectoryNotFoundException("Create Folder, Exist folder : " + folder);
            info.Create();
        }

        public List<string> GetFolderList()
        {
            List<string> folders = new List<string>(Directory.EnumerateDirectories(uriPath));
            List<string> folderNames = new List<string>();
            foreach (string str in folders)
            {
                folderNames.Add(str.Substring(str.LastIndexOf("\\") + 1));
            }
            return folderNames;
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
            string strFolder = (folder != null && folder != string.Empty && folder != "") ? uriPath + folder + "\\" : uriPath;
            try
            {
                System.IO.DirectoryInfo info = new DirectoryInfo(strFolder);
                if (info.Exists == false)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
        
}
