using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DNMReport.Util
{
    public class FileInfo
    {
        string fullname="";
        string name = "";
        string extension = "";

        public FileInfo()
        {
            IsFolder = false;
            Size = 0;
            //Date = DateTime.MinValue;
            ParserErrorMessage = "";
        }
        public FileInfo(string stream)
        {
            From(stream);
        }
        public bool From(string stream)
        {
            try
            {
                IsFolder = false;
                Size = 0;
                LastWriteTime = DateTime.MinValue;
                name = "";
                fullname = "";
                extension = "";
                ParserErrorMessage = "";

                if (stream == string.Empty || stream == "")
                {
                    ParserErrorMessage = "string null";
                    return false;
                }
                List<string> array = new List<string>();
                string tempStr = "";
                int index = 0;
                foreach(var str in stream)
                {
                    if (str == ' ')
                    {
                        if (tempStr.Length > 0)
                        {
                            array.Add(tempStr);
                            tempStr = "";
                            if(array.Count >= 8)
                            {
                                array.Add(stream.Substring(index).Trim());
                                break;
                            }
                        }
                    }
                    else
                        tempStr += str;
                    index++;
                }
                //string[] array = stream.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Count < 9)
                {
                    ParserErrorMessage = "string parsing error";
                    return false;
                }
                IsFolder = (array[0].Substring(0, 1) == "d" || array[0].Substring(0, 1) == "D");
                Size = Convert.ToInt32(array[4]);

                //string straa = "2000 " + array[5] + " " + array[6] + " " + array[7];
                DateTime nt = DateTime.Now;
                if (array[7] != null && array[7] != string.Empty)
                {
                    string[] timeArray = array[7].Split(':');
                    int day = Convert.ToInt32(array[6]);
                    int month = ChangeMonthToInt(array[5]);
                    if (timeArray.Length >= 2)
                    {
                        int hour = Convert.ToInt32(timeArray[0]);
                        int minute = Convert.ToInt32(timeArray[1]);
                        LastWriteTime = new DateTime(1, month, day, hour, minute, 0);
                    }
                    else
                    {
                        int year = Convert.ToInt32(timeArray[0]);
                        LastWriteTime = new DateTime(year, month, day, 0, 0, 0);
                    }
                }
                else
                    LastWriteTime = DateTime.MinValue;

                fullname = array[8];
                if (IsFolder)
                {
                    name = fullname;
                }
                else
                {
                    name = removeExtension(fullname);
                    extension = getExtension(fullname);
                }
                    
            }
            catch (Exception ex)
            {
                ParserErrorMessage = ex.Message;
                return false;
            }
            return true;
        }
        private string removeExtension(string fileName)
        {
            string str = string.Empty;
            try
            {
                if (fileName == string.Empty || fileName == "")
                    return string.Empty;
                int index = fileName.LastIndexOf('.');
                if (index < 0)
                    str = fileName;
                else
                    str = fileName.Substring(0, index);
            }
            catch (Exception ex)
            {
                ParserErrorMessage = ex.Message;
                return string.Empty;
            }
            return str;
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
                    str = fileName.Substring(index+1);
            }
            catch (Exception ex)
            {
                ParserErrorMessage = ex.Message;
                return string.Empty;
            }
            return str;
        }
        public bool IsFolder { get; set; }
        public int Size { get; set; }

        public DateTime LastWriteTime { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (extension != string.Empty && extension == "")
                    fullname = name;
                else
                    fullname = name + '.' + extension;
            }
        }
        public string FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
                name = removeExtension(fullname);
                extension = getExtension(fullname);
            }
        }
        public string Extension
        {
            get { return extension; }
            set
            {
                extension = value;
                if (name != string.Empty || name == "")
                    fullname = extension;
                else
                    fullname = name + '.' + extension;
            }
        }
        public string ParserErrorMessage { get; set; }

        static public int ChangeMonthToInt(string mon)
        {
            if (mon == null || mon == string.Empty)
                return 0;
            if (mon == "Jan")
                return 1;
            else if (mon == "Feb")
                return 2;
            else if (mon == "Mar")
                return 3;
            else if (mon == "Apr")
                return 4;
            else if (mon == "May")
                return 5;
            else if (mon == "Jun")
                return 6;
            else if (mon == "Jul")
                return 7;
            else if (mon == "Aug")
                return 8;
            else if (mon == "Sep")
                return 9;
            else if (mon == "Oct")
                return 10;
            else if (mon == "Nov")
                return 11;
            else if (mon == "Dec")
                return 11;
            return 0;
        }
    }
}
