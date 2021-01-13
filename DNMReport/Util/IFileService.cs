using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace DNMReport.Util
{
    public enum DriveType
    {
        FTP,
        Disk
    }
    public interface IFileService
    {
        string UriPath
        {
            get;
            set;
        }
        int Timeout
        {
            get;
            set;
        }
        NetworkCredential Credntial
        {
            get;
            set;
        }
        DriveType ConnectionDrive
        { get; }

        MemoryStream ReadFile(string fileName, string folder = null);
        FileInfo ReadFileInfo(string fileName, string folder = null);

        void WriteFile(string fileName, MemoryStream stream, string folder = null);
        void WriteFileDateTime(string fileName, DateTime time, string folder = null);

        List<FileInfo> GetFileList(string folder = null, string extenstionFilter = null);
        void ChangFileName(string name, string toName, string folder = null);
        void DeleteFile(string name, string folder = null);
        void DeleteFolder(string folder);
        void CreateFolder(string folder);
        List<string> GetFolderList();
    }
}
